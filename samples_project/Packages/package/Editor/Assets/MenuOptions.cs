using Esri.ArcGISMapsSDK.Components;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Esri.ArcGISMapsSDK.Editor.Assets
{
	static class MenuOptions
	{
		[MenuItem("GameObject/ArcGISMaps SDK/Camera", false, 10)]
		static void AddCamera(MenuCommand menuCommand)
		{
			var cameraGameObject = new GameObject("ArcGISCamera");

			cameraGameObject.AddComponent<Camera>();

			if (SceneView.lastActiveSceneView != null)
			{
				cameraGameObject.transform.position = SceneView.lastActiveSceneView.camera.transform.position;
				cameraGameObject.transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
			}

			if (!Camera.main)
			{
				cameraGameObject.tag = "MainCamera";
			}

			AttachToMap(cameraGameObject, menuCommand);

			cameraGameObject.AddComponent<ArcGISCameraComponent>();

			Undo.RegisterCreatedObjectUndo(cameraGameObject, "Create " + cameraGameObject.name);

			Selection.activeGameObject = cameraGameObject;
		}

		[MenuItem("GameObject/ArcGISMaps SDK/Map", false, 10)]
		static void AddMap(MenuCommand menuCommand)
		{
			CreateMapGameObject();
		}

		static void AttachToMap(GameObject element, MenuCommand menuCommand)
		{
			GameObject parent = menuCommand.context as GameObject;

			if (parent == null)
			{
				parent = GetOrCreateMapGameObject();
			}

			SceneManager.MoveGameObjectToScene(element, parent.scene);

			if (element.transform.parent == null)
			{
				Undo.SetTransformParent(element.transform, parent.transform, true, "Parent " + element.name);
			}

			GameObjectUtility.EnsureUniqueNameForSibling(element);
		}

		static GameObject CreateMapGameObject()
		{
			var gameObject = new GameObject("ArcGISMap");

			gameObject.AddComponent<ArcGISMapComponent>();

			GameObjectUtility.EnsureUniqueNameForSibling(gameObject);

			Undo.RegisterCreatedObjectUndo(gameObject, "Create " + gameObject.name);

			return gameObject;
		}

		static GameObject GetOrCreateMapGameObject()
		{
			GameObject selectedGo = Selection.activeGameObject;

			var arcGISMapComponent = selectedGo != null ? selectedGo.GetComponentInParent<ArcGISMapComponent>() : null;

			if (IsValidMap(arcGISMapComponent))
			{
				return arcGISMapComponent.gameObject;
			}

			ArcGISMapComponent[] arcGISMapComponentArray = StageUtility.GetCurrentStageHandle().FindComponentsOfType<ArcGISMapComponent>();

			for (int i = 0; i < arcGISMapComponentArray.Length; i++)
			{
				if (IsValidMap(arcGISMapComponentArray[i]))
				{
					return arcGISMapComponentArray[i].gameObject;
				}
			}

			return CreateMapGameObject();
		}

		static bool IsValidMap(ArcGISMapComponent arcGISMapComponent)
		{
			if (arcGISMapComponent == null || !arcGISMapComponent.gameObject.activeInHierarchy)
			{
				return false;
			}

			return true;
		}
	}
}
