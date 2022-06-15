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
using Esri.ArcGISMapsSDK.Utils.Math;
using Esri.HPFramework;
using Unity.Mathematics;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System;
using Esri.GameEngine.View;

namespace Esri.ArcGISMapsSDK.Components
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[RequireComponent(typeof(HPTransform))]
	[RequireComponent(typeof(ArcGISCameraComponent))]
	[AddComponentMenu("ArcGIS Maps SDK/ArcGIS Editor Camera")]
	public class ArcGISEditorCameraComponent : MonoBehaviour
	{
		private ArcGISMapComponent arcGISMapComponent;
		private bool initialized = false;

		private double3 lastEditorCameraPosition;
		private double3 lastRootPosition;
		private HPTransform hpTransform;

		bool worldRepositionEnabled = false;

		public bool WorldRepositionEnabled
		{
			get => worldRepositionEnabled;

			set
			{
				worldRepositionEnabled = value;
			}
		}

		public bool EditorViewEnabled
		{
			get => GetComponent<ArcGISCameraComponent>().enabled;

			set
			{
				GetComponent<ArcGISCameraComponent>().enabled = value;
			}
		}

		private void Awake()
		{
			GetComponent<Camera>().enabled = false;
			GetComponent<ArcGISCameraComponent>().enabled = false;
			hpTransform = GetComponent<HPTransform>();
			hpTransform.enabled = false;
		}

		private void OnEnable()
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				arcGISMapComponent = GetComponentInParent<ArcGISMapComponent>();

				GetComponent<ArcGISCameraComponent>().enabled = arcGISMapComponent.DataFetchWithSceneView;
				worldRepositionEnabled = arcGISMapComponent.RebaseWithSceneView;
				hpTransform.enabled = true;
				initialized = false;

				arcGISMapComponent.MapTypeChanged += new ArcGISMapComponent.MapTypeChangedEventHandler(() => { initialized = false; });
			}
#endif
		}

		private void OnDisable()
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				GetComponent<ArcGISCameraComponent>().enabled = false;
				hpTransform.enabled = false;

				arcGISMapComponent.MapTypeChanged -= new ArcGISMapComponent.MapTypeChangedEventHandler(() => { initialized = false; });
			}

			initialized = false;
#endif
		}

		private void OnTransformParentChanged()
		{
			OnEnable();
		}

		private void Update()
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				if (SceneView.lastActiveSceneView)
				{
					var rootDeltaPosition = initialized ? arcGISMapComponent.UniversePosition - lastRootPosition : double3.zero;
					hpTransform.UniversePosition = math.inverse(arcGISMapComponent.WorldMatrix).HomogeneousTransformPoint(SceneView.lastActiveSceneView.camera.transform.position.ToDouble3()) - rootDeltaPosition;
					hpTransform.UniverseRotation = arcGISMapComponent.UniverseRotation * SceneView.lastActiveSceneView.camera.transform.rotation;

					var camera = GetComponent<Camera>();
					camera.fieldOfView = SceneView.lastActiveSceneView.cameraSettings.fieldOfView;
					camera.aspect = SceneView.lastActiveSceneView.camera.aspect;

					if (arcGISMapComponent.View.SpatialReference != null)
					{
						var altitude = Mathf.Max(0, (float)arcGISMapComponent.View.AltitudeAtCartesianPosition(hpTransform.UniversePosition));

						// SceneView Camera NearPlane is updated a frame after update cameraSettings, because of that our GetCameraNearPlane doesn't work.
						// A very simple solution is implemented to resolve near-far distance problem
						float near = altitude > 20000.0f ? 10000.0f : 0.5f;
						var mapType = arcGISMapComponent.View.Map?.MapType ?? GameEngine.Map.ArcGISMapType.Global;
						var sr = arcGISMapComponent.View.SpatialReference;

						SceneView.lastActiveSceneView.cameraSettings.nearClip = near;
						SceneView.lastActiveSceneView.cameraSettings.farClip = (float)Math.Max(near, Utils.FrustumHelpers.CalculateFarPlaneDistance(altitude, mapType, sr));
					}

					if (!initialized)
					{
						lastEditorCameraPosition = hpTransform.UniversePosition;
						SceneView.lastActiveSceneView.cameraSettings.dynamicClip = false;

						initialized = true;
					}
					else
					{
						var delta = lastEditorCameraPosition - hpTransform.UniversePosition;
						lastEditorCameraPosition = hpTransform.UniversePosition;

						if (worldRepositionEnabled && delta.Equals(double3.zero) && !arcGISMapComponent.UniversePosition.Equals(hpTransform.UniversePosition))
						{
							arcGISMapComponent.UniversePosition = hpTransform.UniversePosition;
							arcGISMapComponent.SyncPositionWithHPRoot();
							SceneView.lastActiveSceneView.pivot -= SceneView.lastActiveSceneView.camera.transform.position;
						}
					}

					lastRootPosition = arcGISMapComponent.UniversePosition;
				}
			}
#endif
		}
	}
}
