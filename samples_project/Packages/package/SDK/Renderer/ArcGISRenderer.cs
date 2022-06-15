// COPYRIGHT 1995-2021 ESRI
// TRADE SECRETS: ESRI PROPRIETARY AND CONFIDENTIAL
// Unpublished material - all rights reserved under the
// Copyright Laws of the United States and applicable international
// laws, treaties, and conventions.
//
// For additional information, contact:
// Attn: Contracts and Legal Department
// Environmental Systems Research Institute, Inc.
// 380 New York Street
// Redlands, California 92373
// USA
//
// email: legal@esri.com
using Esri.ArcGISMapsSDK.Renderer.GPUComputing;
using Esri.ArcGISMapsSDK.Renderer.GPUResources;
using Esri.ArcGISMapsSDK.Renderer.SceneComponents;
using Esri.GameEngine.RenderCommandQueue;
using Esri.GameEngine.View;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Renderer
{
	public class ArcGISRenderer
	{
		private readonly RenderCommandClient renderCommandClient;
		private readonly ArcGISView view;
		private readonly INormalMapGenerator normalMapGenerator;
		private readonly IImageComposer imageComposer;
		private readonly GPUResourcesProvider gpuResourceProvider = new GPUResourcesProvider();
		private readonly SceneComponentProvider sceneComponentProvider;

		private readonly RenderCommandThrottle renderCommandThrottle = new RenderCommandThrottle();
		private static readonly bool throttlingManagerEnabled = true;

		private DecodedRenderCommandQueue currentRenderCommandQueue;

		internal ISceneComponentProvider SceneComponentProvider => sceneComponentProvider;

		public bool AreMeshCollidersEnabled
		{
			get
			{
				return sceneComponentProvider.AreMeshCollidersEnabled;
			}
			set
			{
				sceneComponentProvider.AreMeshCollidersEnabled = value;
			}
		}

		public ArcGISRenderer(ArcGISView view, GameObject gameObject, bool areMeshCollidersEnabled)
		{
			while (gameObject.transform.childCount != 0)
			{
#if UNITY_EDITOR
				if (Application.isEditor)
				{
					GameObject.DestroyImmediate(gameObject.transform.GetChild(0).gameObject);
				}
				else
#endif
				{
					GameObject.Destroy(gameObject.transform.GetChild(0).gameObject);
				}
			}

			if (SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3 ||
					SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Metal)
			{
				normalMapGenerator = new NormalMapGeneratorPS(view);
				imageComposer = new ImageComposerPS();
			}
			else
			{
				normalMapGenerator = new NormalMapGeneratorCS(view);
				imageComposer = new ImageComposerCS();
			}

			this.view = view;
			sceneComponentProvider = new SceneComponentProvider(500, gameObject, areMeshCollidersEnabled);
			renderCommandClient = new RenderCommandClient(gpuResourceProvider, sceneComponentProvider, imageComposer, normalMapGenerator);

			currentRenderCommandQueue = GetCurrentDecodedRenderCommandQueue();
		}

		internal DecodedRenderCommandQueue GetCurrentDecodedRenderCommandQueue()
		{
			return new DecodedRenderCommandQueue(view.RenderCommandServer.GetRenderCommands());
		}

		public void Update()
		{
			if (view.SpatialReference == null)
			{
				// The normal map generator compute shader needs a spatial reference
				return;
			}

			if (throttlingManagerEnabled)
			{
				renderCommandThrottle.Clear();
				RenderCommand renderCommand = currentRenderCommandQueue.GetNextCommand();

				do
				{
					if (renderCommand != null)
					{
						renderCommandClient.ExecuteRenderCommand(renderCommand);
						if (renderCommandThrottle.DoThrottle(renderCommand))
						{
							// Break and defer processing of the remaining commands to next Update
							break;
						}

						renderCommand = currentRenderCommandQueue.GetNextCommand();
					}
					else
					{
						currentRenderCommandQueue = GetCurrentDecodedRenderCommandQueue();
						renderCommand = currentRenderCommandQueue.GetNextCommand();
					}
				}
				while (renderCommand != null);
			}
			else
			{
				currentRenderCommandQueue = GetCurrentDecodedRenderCommandQueue();

				for (var renderCommand = currentRenderCommandQueue.GetNextCommand(); renderCommand != null; renderCommand = currentRenderCommandQueue.GetNextCommand())
				{
					renderCommandClient.ExecuteRenderCommand(renderCommand);
				}
			}
		}

		public void Release()
		{
			gpuResourceProvider.Release();
			sceneComponentProvider.Release();
		}

		internal ISceneComponent GetSceneComponentByGameObject(GameObject gameObject)
		{
			return sceneComponentProvider.GetSceneComponentFrom(gameObject);
		}
	}
}
