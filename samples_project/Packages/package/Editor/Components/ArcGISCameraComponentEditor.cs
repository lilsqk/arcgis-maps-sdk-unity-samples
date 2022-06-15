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
using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Geometry;
using UnityEditor;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Editor.Components
{
	[CustomEditor(typeof(ArcGISCameraComponent))]
	public class ArcGISCameraComponentEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var arcGISCameraComponent = target as ArcGISCameraComponent;
			var cameraComponent = arcGISCameraComponent.GetComponent<Camera>();
			bool cameraComponentPresent = cameraComponent != null;
			var updateClippingPlanes = arcGISCameraComponent.UpdateClippingPlanes;
			var useCameraViewportProperties = arcGISCameraComponent.UseCameraViewportProperties;
			var verticalFov = arcGISCameraComponent.verticalFov;
			var horizontalFov = arcGISCameraComponent.horizontalFov;
			var viewportSizeX = arcGISCameraComponent.viewportSizeX;
			var viewportSizeY = arcGISCameraComponent.viewportSizeY;

			if (Draw(cameraComponentPresent, ref updateClippingPlanes, ref useCameraViewportProperties, ref verticalFov, ref horizontalFov, ref viewportSizeX, ref viewportSizeY))
			{
				arcGISCameraComponent.UpdateClippingPlanes = updateClippingPlanes;
				arcGISCameraComponent.UseCameraViewportProperties = useCameraViewportProperties;
				arcGISCameraComponent.verticalFov = verticalFov;
				arcGISCameraComponent.horizontalFov = horizontalFov;
				arcGISCameraComponent.viewportSizeX = viewportSizeX;
				arcGISCameraComponent.viewportSizeY = viewportSizeY;

				EditorUtility.SetDirty(arcGISCameraComponent);
			}
		}

		private static bool Draw(bool cameraComponentPresent, ref bool updateClippingPlanes, ref bool useCameraViewportProperties, ref float verticalFov, ref float horizontalFov, ref uint viewportSizeX, ref uint viewportSizeY)
		{
			bool result = false;
			bool drawViewportProps = true;

			if (cameraComponentPresent)
			{
				var oldUpdateClippingPlanes = updateClippingPlanes;

				updateClippingPlanes = EditorGUILayout.Toggle("Update Clipping Planes", updateClippingPlanes, GUILayout.ExpandWidth(true));

				if (oldUpdateClippingPlanes != updateClippingPlanes)
				{
					result = true;
				}

				var oldUseCameraViewportProperties = useCameraViewportProperties;

				useCameraViewportProperties = EditorGUILayout.Toggle("Use Camera Viewport Properties", useCameraViewportProperties, GUILayout.ExpandWidth(true));

				if (oldUseCameraViewportProperties != useCameraViewportProperties)
				{
					result = true;
				}

				if (useCameraViewportProperties)
				{
					drawViewportProps = false;
				}
			}

			if (drawViewportProps)
			{
				// v fov
				var oldVerticalFov = verticalFov;

				verticalFov = EditorGUILayout.FloatField("Vertical FOV", verticalFov, GUILayout.ExpandWidth(true));

				if (oldVerticalFov != verticalFov)
				{
					result = true;
				}

				// h fov
				var oldhorizontalFov = horizontalFov;

				horizontalFov = EditorGUILayout.FloatField("Horizontal FOV", horizontalFov, GUILayout.ExpandWidth(true));

				if (oldhorizontalFov != horizontalFov)
				{
					result = true;
				}

				// viewport x
				var oldViewportSizeX = viewportSizeX;

				viewportSizeX = (uint)EditorGUILayout.IntField("Viewport Size X", (int)viewportSizeX, GUILayout.ExpandWidth(true));

				if (oldViewportSizeX != viewportSizeX)
				{
					result = true;
				}

				// viewport x
				var oldViewportSizeY = viewportSizeY;

				viewportSizeY = (uint)EditorGUILayout.IntField("Viewport Size Y", (int)viewportSizeY, GUILayout.ExpandWidth(true));

				if (oldViewportSizeY != viewportSizeY)
				{
					result = true;
				}
			}

			return result;
		}
	}
}
