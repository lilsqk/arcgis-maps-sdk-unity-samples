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
using Esri.ArcGISMapsSDK.Renderer;
using Esri.ArcGISMapsSDK.Renderer.SceneComponents;
using Unity.Mathematics;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Components
{
	[DisallowMultipleComponent]
	[ExecuteAlways]
	[AddComponentMenu("ArcGIS Maps SDK/ArcGIS Renderer")]
	public class ArcGISRendererComponent : MonoBehaviour
	{
		public bool disableTerrainOcclusion = false;

		private ArcGISMapComponent arcGISMapComponent;
		private ArcGISRenderer arcGISRenderer = null;

#if USE_HDRP_PACKAGE || USE_URP_PACKAGE
		public ArcGISTerrainOcclusionRenderer IntegratedMeshOcclusionRender { get; private set; } = null;
#endif

		private void OnEnable()
		{
			arcGISMapComponent = gameObject.GetComponentInParent<ArcGISMapComponent>();

#if UNITY_EDITOR
			UnityEditor.EditorApplication.update += OnForceRenderUpdate;
#endif

			if (arcGISMapComponent)
			{
				arcGISMapComponent.EditorModeEnabledChanged += new ArcGISMapComponent.EditorModeEnabledChangedEventHandler(OnEditorModeEnabledChanged);
				arcGISMapComponent.MeshCollidersEnabledChanged += new ArcGISMapComponent.MeshCollidersEnabledChangedEventHandler(OnMeshCollidersEnabledChanged);
			}

#if USE_HDRP_PACKAGE || USE_URP_PACKAGE
			if (!disableTerrainOcclusion && IntegratedMeshOcclusionRender == null)
			{
				IntegratedMeshOcclusionRender = new ArcGISTerrainOcclusionRenderer(GetArcGISRenderer().SceneComponentProvider, GetWorldMatrix);
			}
#endif
		}

		private double4x4 GetWorldMatrix()
		{
			return arcGISMapComponent ? arcGISMapComponent.WorldMatrix : double4x4.identity;
		}

		private void OnDisable()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.update -= OnForceRenderUpdate;
#endif

			if (arcGISMapComponent)
			{
				arcGISMapComponent.EditorModeEnabledChanged -= new ArcGISMapComponent.EditorModeEnabledChangedEventHandler(OnEditorModeEnabledChanged);
				arcGISMapComponent.MeshCollidersEnabledChanged -= new ArcGISMapComponent.MeshCollidersEnabledChangedEventHandler(OnMeshCollidersEnabledChanged);
			}

#if USE_HDRP_PACKAGE || USE_URP_PACKAGE
			if (IntegratedMeshOcclusionRender != null)
			{
				IntegratedMeshOcclusionRender.Release();
				IntegratedMeshOcclusionRender = null;
			}
#endif
		}

		private void Start()
		{
			if (arcGISMapComponent == null)
			{
				Debug.LogError("An ArcGISMapComponent could not be found. Please make sure this GameObject is a child of a GameObject with an ArcGISMapComponent attached");

				enabled = false;
				return;
			}
		}

		private void Update()
		{
			if (arcGISMapComponent != null && arcGISMapComponent.ShouldEditorComponentBeUpdated() && GetArcGISRenderer() != null)
			{
				GetArcGISRenderer().Update();
			}
		}

		private void OnDestroy()
		{
			if (arcGISRenderer != null)
			{
				arcGISRenderer.Release();
			}
		}

		private void OnTransformParentChanged()
		{
			OnEnable();
		}

		internal ISceneComponent GetSceneComponentByGameObject(GameObject gameObject)
		{
			return GetArcGISRenderer().GetSceneComponentByGameObject(gameObject);
		}

		private ArcGISRenderer GetArcGISRenderer()
		{
			if (arcGISRenderer == null)
			{
				if (arcGISMapComponent.View != null)
				{
					arcGISRenderer = new ArcGISRenderer(arcGISMapComponent.View, gameObject, arcGISMapComponent.MeshCollidersEnabled && Application.isPlaying);
				}
			}

			return arcGISRenderer;
		}

		private void ResetArcGISRenderer()
		{
			if (arcGISRenderer != null)
			{
				arcGISRenderer.Release();
			}

			arcGISRenderer = null;
		}

		private void OnEditorModeEnabledChanged()
		{
			if (arcGISMapComponent != null && !arcGISMapComponent.ShouldEditorComponentBeUpdated())
			{
				ResetArcGISRenderer();
			}
		}

		private void OnMeshCollidersEnabledChanged()
		{
			if (arcGISMapComponent != null && arcGISRenderer != null)
			{
				arcGISRenderer.AreMeshCollidersEnabled = arcGISMapComponent.MeshCollidersEnabled && Application.isPlaying;
			}
		}

#if UNITY_EDITOR
		private void OnForceRenderUpdate()
		{
			if (!Application.isPlaying && arcGISMapComponent.ShouldEditorComponentBeUpdated())
			{
				UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
				UnityEditor.SceneView.RepaintAll();
			}
		}
#endif
	}
}
