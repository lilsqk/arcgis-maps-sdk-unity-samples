// COPYRIGHT 1995-2022 ESRI
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
using Esri.ArcGISMapsSDK.Components;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.ArcGISMapsSDK.Utils.Math;
using Esri.HPFramework;
using Esri.GameEngine.Geometry;
using System;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Esri.ArcGISMapsSDK.Editor.UI
{
	public static class ArcGISMapCreatorCameraTool
	{
		private static ArcGISCameraComponent selectedCamera;
		private static ArcGISLocationComponent selectedLocationComponent;

		private static VisualElement rootElement;

		private static DoubleField LocationFieldX;
		private static DoubleField LocationFieldY;
		private static DoubleField LocationFieldZ;
		private static IntegerField LocationFieldSR;

		private static DoubleField RotationFieldHeading;
		private static DoubleField RotationFieldPitch;
		private static DoubleField RotationFieldRoll;

		private static SceneView lastSceneView;

		public static VisualElement CreateCameraTool()
		{
			rootElement = new VisualElement();
			rootElement.name = "ArcGISMapCreatorCameraTool";

			var templatePath = MapCreatorUtilities.FindAssetPath("CameraToolTemplate");
			var template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(templatePath);
			template.CloneTree(rootElement);

			FindCamera();

			InitializeCreateCameraButton();

			PopulateCameraFields();

			InitAlignCameraToViewButton();

			return rootElement;
		}

		private static void FindCamera()
		{
			selectedCamera = GameObject.FindObjectOfType<ArcGISCameraComponent>();

			if (selectedCamera != null)
			{
				if (selectedCamera.transform.parent == null || selectedCamera.transform.parent.GetComponent<ArcGISMapComponent>() == null)
				{
					Debug.LogWarning("Parent the ArcGIS Camera game object to a game object with an ArcGIS Map component to use the Camera UI tool");
				}

				selectedLocationComponent = selectedCamera.GetComponent<ArcGISLocationComponent>();

				if (selectedLocationComponent == null)
				{
					Debug.LogWarning("Attach an ArcGIS Location component to the ArcGIS Camera game object to use the full capability of the Camera UI tool");
				}
			}
		}

		private static void InitializeCreateCameraButton()
		{
			Button createCamButton = rootElement.Query<Button>(name: "button-create-camera");

			createCamButton.clickable.activators.Clear();
			createCamButton.RegisterCallback<MouseDownEvent>(evnt => CreateCamera(evnt));

			if (selectedCamera != null && selectedLocationComponent != null)
			{
				createCamButton.SetEnabled(false);
			}
		}

		private static void CreateCamera(MouseDownEvent evnt)
		{
			GameObject cameraGameObject;

			if (Camera.main != null)
			{
				cameraGameObject = Camera.main.gameObject;
			}
			else
			{
				cameraGameObject = new GameObject();
				cameraGameObject.AddComponent<Camera>();
				cameraGameObject.tag = "MainCamera";
			}

			cameraGameObject.name = "ArcGISCamera";

			if (SceneView.lastActiveSceneView != null)
			{
				cameraGameObject.transform.position = SceneView.lastActiveSceneView.camera.transform.position;
				cameraGameObject.transform.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
			}

			var mapComponent = GameObject.FindObjectOfType<ArcGISMapComponent>();

			if (mapComponent != null)
			{
				cameraGameObject.transform.parent = mapComponent.transform;
			}

			selectedCamera = cameraGameObject.AddComponent<ArcGISCameraComponent>();

			selectedLocationComponent = cameraGameObject.AddComponent<ArcGISLocationComponent>();
			selectedLocationComponent.Position = new ArcGISPoint(LocationFieldX.value, LocationFieldY.value, LocationFieldZ.value, new ArcGISSpatialReference(LocationFieldSR.value));
			selectedLocationComponent.Rotation = new ArcGISRotation(RotationFieldHeading.value, RotationFieldPitch.value, RotationFieldRoll.value);

			PopulateCameraFields();

			var button = (Button)evnt.currentTarget;
			button.SetEnabled(false);

			Undo.RegisterCreatedObjectUndo(cameraGameObject, "Create " + cameraGameObject.name);

			Selection.activeGameObject = cameraGameObject;
		}

		private static void PopulateCameraFields()
		{
			Action<int> intFieldValueChangedCallback = (int value) =>
			{
				try
				{

					var spatialRef = new ArcGISSpatialReference(value);

					if (selectedLocationComponent != null)
					{
						selectedLocationComponent.Position = new ArcGISPoint(selectedLocationComponent.Position.X, selectedLocationComponent.Position.Y, selectedLocationComponent.Position.Z, spatialRef);
					}

					MapCreatorUtilities.UpdateArcGISPointLabels(LocationFieldX, LocationFieldY, LocationFieldZ, LocationFieldSR.value);
				}
				catch
				{
				}
			};

			Action<double> XOriginChanged = (double value) =>
			{
				if (selectedLocationComponent != null)
				{
					selectedLocationComponent.Position = new ArcGISPoint(value, selectedLocationComponent.Position.Y, selectedLocationComponent.Position.Z, selectedLocationComponent.Position.SpatialReference);
				}
			};

			Action<double> YOriginChanged = (double value) =>
			{
				if (selectedLocationComponent != null)
				{
					selectedLocationComponent.Position = new ArcGISPoint(selectedLocationComponent.Position.X, value, selectedLocationComponent.Position.Z, selectedLocationComponent.Position.SpatialReference); ;
				}
			};

			Action<double> ZOriginChanged = (double value) =>
			{
				if (selectedLocationComponent != null)
				{
					selectedLocationComponent.Position = new ArcGISPoint(selectedLocationComponent.Position.X, selectedLocationComponent.Position.Y, value, selectedLocationComponent.Position.SpatialReference);
				}
			};

			Action<double> HeadingChanged = (double value) =>
			{
				if (selectedLocationComponent != null)
				{
					selectedLocationComponent.Rotation = new ArcGISRotation(value, selectedLocationComponent.Rotation.Pitch, selectedLocationComponent.Rotation.Roll);
				}
			};

			Action<double> PitchChanged = (double value) =>
			{
				if (selectedLocationComponent != null)
				{
					selectedLocationComponent.Rotation = new ArcGISRotation(selectedLocationComponent.Rotation.Heading, value, selectedLocationComponent.Rotation.Roll);
				}
			};

			Action<double> RollChanged = (double value) =>
			{
				if (selectedLocationComponent != null)
				{
					selectedLocationComponent.Rotation = new ArcGISRotation(selectedLocationComponent.Rotation.Heading, selectedLocationComponent.Rotation.Pitch, value);
				}
			};

			LocationFieldX = MapCreatorUtilities.InitializeDoubleField(rootElement, "cam-position-x", null, XOriginChanged);
			LocationFieldY = MapCreatorUtilities.InitializeDoubleField(rootElement, "cam-position-y", null, YOriginChanged);
			LocationFieldZ = MapCreatorUtilities.InitializeDoubleField(rootElement, "cam-position-z", null, ZOriginChanged);
			LocationFieldSR = MapCreatorUtilities.InitializeIntegerField(rootElement, "cam-position-wkid", null, intFieldValueChangedCallback);

			RotationFieldHeading = MapCreatorUtilities.InitializeDoubleField(rootElement, "cam-rotation-heading", null, HeadingChanged);
			RotationFieldPitch = MapCreatorUtilities.InitializeDoubleField(rootElement, "cam-rotation-pitch", null, PitchChanged);
			RotationFieldRoll = MapCreatorUtilities.InitializeDoubleField(rootElement, "cam-rotation-roll", null, RollChanged);

			if (selectedLocationComponent == null)
			{
				LocationFieldSR.SetValueWithoutNotify(SpatialReferenceWkid.WGS84);
			}
			else
			{
				LocationFieldX.SetValueWithoutNotify(MapCreatorUtilities.TruncateDoubleForUI(selectedLocationComponent.Position.X));
				LocationFieldY.SetValueWithoutNotify(MapCreatorUtilities.TruncateDoubleForUI(selectedLocationComponent.Position.Y));
				LocationFieldZ.SetValueWithoutNotify(MapCreatorUtilities.TruncateDoubleForUI(selectedLocationComponent.Position.Z));
				LocationFieldSR.SetValueWithoutNotify(selectedLocationComponent.Position.SpatialReference.WKID);

				RotationFieldHeading.SetValueWithoutNotify(MapCreatorUtilities.TruncateDoubleForUI(selectedLocationComponent.Rotation.Heading));
				RotationFieldPitch.SetValueWithoutNotify(MapCreatorUtilities.TruncateDoubleForUI(selectedLocationComponent.Rotation.Pitch));
				RotationFieldRoll.SetValueWithoutNotify(MapCreatorUtilities.TruncateDoubleForUI(selectedLocationComponent.Rotation.Roll));
			}

			MapCreatorUtilities.UpdateArcGISPointLabels(LocationFieldX, LocationFieldY, LocationFieldZ, LocationFieldSR.value);
		}

		private static void InitAlignCameraToViewButton()
		{
			Button AlignCameraToViewButton = rootElement.Query<Button>(name: "button-transfer-to-camera");
			AlignCameraToViewButton.clickable.activators.Clear();
			AlignCameraToViewButton.RegisterCallback<MouseDownEvent>(evnt =>
			{
				if (Application.isPlaying)
				{
					return;
				}

				if (lastSceneView == null || lastSceneView != SceneView.lastActiveSceneView)
				{
					lastSceneView = SceneView.lastActiveSceneView;
				}

				if (selectedCamera != null)
				{
					var cameraTransform = selectedCamera.GetComponent<HPTransform>();
					var mapComponent = cameraTransform.GetComponentInParent<ArcGISMapComponent>();

					Selection.activeGameObject = cameraTransform.gameObject;
					lastSceneView.AlignWithView();

					var worldPosition = math.inverse(mapComponent.WorldMatrix).HomogeneousTransformPoint(SceneView.lastActiveSceneView.camera.transform.position.ToDouble3());
					var geoPosition = mapComponent.View.WorldToGeographic(worldPosition);

					geoPosition = GeoUtils.ProjectToSpatialReference(geoPosition, new ArcGISSpatialReference(LocationFieldSR.value));

					if (!Double.IsNaN(geoPosition.X) && !Double.IsNaN(geoPosition.Y) && !Double.IsNaN(geoPosition.Z))
					{
						LocationFieldX.SetValueWithoutNotify(MapCreatorUtilities.TruncateDoubleForUI(geoPosition.X));
						LocationFieldY.SetValueWithoutNotify(MapCreatorUtilities.TruncateDoubleForUI(geoPosition.Y));
						LocationFieldZ.SetValueWithoutNotify(MapCreatorUtilities.TruncateDoubleForUI(geoPosition.Z));

						var worldRotation = mapComponent.UniverseRotation * SceneView.lastActiveSceneView.camera.transform.rotation;
						var geoRotation = GeoUtils.FromCartesianRotation(worldPosition, quaternionExtensions.ToQuaterniond(worldRotation), new ArcGISSpatialReference(LocationFieldSR.value), mapComponent.MapType);

						RotationFieldHeading.SetValueWithoutNotify(MapCreatorUtilities.TruncateDoubleForUI(geoRotation.Heading));
						RotationFieldPitch.SetValueWithoutNotify(MapCreatorUtilities.TruncateDoubleForUI(geoRotation.Pitch));
						RotationFieldRoll.SetValueWithoutNotify(MapCreatorUtilities.TruncateDoubleForUI(geoRotation.Roll));

						if (selectedLocationComponent != null)
						{
							selectedLocationComponent.SyncPositionWithHPTransform();
							selectedLocationComponent.Position = geoPosition;
							selectedLocationComponent.Rotation = geoRotation;
						}
					}
				}
			});
		}
	}
}
