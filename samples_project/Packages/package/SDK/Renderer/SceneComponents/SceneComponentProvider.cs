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
using Esri.HPFramework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace Esri.ArcGISMapsSDK.Renderer.SceneComponents
{
	internal class SceneComponentProvider : ISceneComponentProvider
	{
		private readonly Dictionary<GameObject, ISceneComponent> gameObjectToSceneComponentMap = new Dictionary<GameObject, ISceneComponent>();
		private readonly Dictionary<uint, ISceneComponent> activeSceneComponents = new Dictionary<uint, ISceneComponent>();
		private readonly List<ISceneComponent> freeSceneComponents = new List<ISceneComponent>();

		private bool areMeshCollidersEnabled = false;

		private readonly GameObject unused = null;

		private readonly GameObject parent;

		public IReadOnlyDictionary<uint, ISceneComponent> SceneComponents => activeSceneComponents;

		public bool AreMeshCollidersEnabled
		{
			get
			{
				return areMeshCollidersEnabled;
			}

			set
			{
				if (areMeshCollidersEnabled != value)
				{
					areMeshCollidersEnabled = value;

					foreach (var sceneComponent in activeSceneComponents)
					{
						sceneComponent.Value.IsMeshColliderEnabled = value;
					}
				}
			}
		}

		public IEnumerable<ISceneComponent> TerrainMaskingMeshes => SceneComponents.Values.Where(sc => sc.IsVisible && sc.MaskTerrain);

		public SceneComponentProvider(int initSize, GameObject parent, bool areMeshCollidersEnabled)
		{
			this.parent = parent;
			this.areMeshCollidersEnabled = areMeshCollidersEnabled;

			unused = new GameObject();
			unused.name = "UnusedPoolGOs";
			unused.hideFlags = HideFlags.DontSaveInEditor;
			unused.transform.SetParent(parent.transform, false);

			for (int i = 0; i < initSize; i++)
			{
				var sceneComponent = new SceneComponent(CreateGameObject(i));
				sceneComponent.SceneComponentGameObject.transform.SetParent(unused.transform, false);
				freeSceneComponents.Add(sceneComponent);
			}
		}

		public ISceneComponent CreateSceneComponent(uint id, uint layerId)
		{
			if (freeSceneComponents.Count > 0)
			{
				var sceneComponent = freeSceneComponents[0];
				sceneComponent.SceneComponentGameObject.transform.SetParent(parent.transform, false);
				sceneComponent.IsVisible = false;
				sceneComponent.IsMeshColliderEnabled = areMeshCollidersEnabled;
				sceneComponent.Name = "ArcGISGameObject_" + id;
				sceneComponent.LayerId = layerId;

				freeSceneComponents.RemoveAt(0);
				activeSceneComponents.Add(id, sceneComponent);
				gameObjectToSceneComponentMap.Add(sceneComponent.SceneComponentGameObject, sceneComponent);

				return sceneComponent;
			}
			else
			{
				var sceneComponent = new SceneComponent(CreateGameObject(activeSceneComponents.Count + freeSceneComponents.Count));
				sceneComponent.SceneComponentGameObject.transform.SetParent(parent.transform, false);
				sceneComponent.IsMeshColliderEnabled = areMeshCollidersEnabled;
				sceneComponent.Name = "ArcGISGameObject_" + id;
				sceneComponent.LayerId = layerId;

				activeSceneComponents.Add(id, sceneComponent);
				gameObjectToSceneComponentMap.Add(sceneComponent.SceneComponentGameObject, sceneComponent);

				return sceneComponent;
			}
		}

		public void DestroySceneComponent(uint id)
		{
			var sceneComponent = activeSceneComponents[id];

			sceneComponent.SceneComponentGameObject.transform.SetParent(unused.transform, false);
			sceneComponent.IsVisible = false;
			sceneComponent.Mesh = null;

			gameObjectToSceneComponentMap.Remove(sceneComponent.SceneComponentGameObject);
			activeSceneComponents.Remove(id);
			freeSceneComponents.Add(sceneComponent);
		}

		public void Release()
		{
			foreach (var sceneComponent in activeSceneComponents)
			{
				sceneComponent.Value.Destroy();
			}

			foreach (var sceneComponent in freeSceneComponents)
			{
				sceneComponent.Destroy();
			}

			activeSceneComponents.Clear();
			freeSceneComponents.Clear();

			if (unused)
			{
				if (Application.isEditor)
				{
					GameObject.DestroyImmediate(unused);
				}
				else
				{
					GameObject.Destroy(unused);
				}
			}
		}

		private static GameObject CreateGameObject(int id)
		{
			var gameObject = new GameObject();
			gameObject.name = "ArcGISGameObject" + id;
			gameObject.hideFlags = HideFlags.DontSaveInEditor;
			gameObject.SetActive(false);
			var renderer = gameObject.AddComponent<MeshRenderer>();
			renderer.shadowCastingMode = ShadowCastingMode.TwoSided;
			renderer.enabled = true;
			gameObject.AddComponent<MeshFilter>();
			gameObject.AddComponent<HPTransform>();
			gameObject.AddComponent<MeshCollider>();

			return gameObject;
		}

		public ISceneComponent GetSceneComponentFrom(GameObject gameObject)
		{
			gameObjectToSceneComponentMap.TryGetValue(gameObject, out var sceneComponent);
			return sceneComponent;
		}
	}
}
