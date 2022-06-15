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
using UnityEditor;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Editor.Components
{
	static class Styles
	{
		public static readonly GUIContent EditorModeEnabled = EditorGUIUtility.TrTextContent("Enable Editor Mode", "When enabled, content from the ArcGIS Maps SDK will be shown in Edit mode.");
		public static readonly GUIContent OriginPosition = EditorGUIUtility.TrTextContent("Origin Position", "This real-world location will be used as Unity's 0,0,0 point. Using the ArcGIS Rebase component in Play mode or enabling 'Rebase With Scene View' in Edit mode updates this origin.");
		public static readonly GUIContent DataFetchWithSceneView = EditorGUIUtility.TrTextContent("Data Fetch Using Scene Camera", "When enabled, while navigating through the scene in Edit mode, the Scene camera is used to determine which data is fetched. When disabled, the ArcGIS Camera component will be used to fetch the data.");
		public static readonly GUIContent RebaseWithSceneView = EditorGUIUtility.TrTextContent("Rebase In Scene View", "When enabled, navigating around the scene in Edit mode will cause the Origin Position/HPRoot to be periodically updated. This is the same behavior the ArcGIS Rebase component provides in Play mode.");
		public static readonly GUIContent MeshCollidersEnabled = EditorGUIUtility.TrTextContent("Mesh Colliders Enabled", "When enabled, mesh colliders will be automatically generated for all the content from the ArcGIS Maps SDK. This will impact performance, but enable Raycasting and other workflows.");
	}

	[CustomEditor(typeof(ArcGISMapComponent))]
	public class ArcGISMapComponentEditor : UnityEditor.Editor
	{
		private bool showBasemapCategory = true;
		private bool showElevationCategory = true;
		private bool showExtentCategory = true;
		private bool showOriginCategory = true;
		private bool showAuthenticationCategory = true;

		SerializedProperty apiKeyProp;
		SerializedProperty basemapProp;
		SerializedProperty basemapTypeProp;
		SerializedProperty basemapAuthProp;
		SerializedProperty editorModeEnabledProp;
		SerializedProperty mapTypeProp;
		SerializedProperty dataFetchWithSceneViewProp;
		SerializedProperty rebaseWithSceneViewProp;
		SerializedProperty elevationProp;
		SerializedProperty elevationAuthProp;
		SerializedProperty enableExtentProp;
		SerializedProperty extentProp;
		SerializedProperty extentShapeProp;
		SerializedProperty extentShapeDimensionsProp;
		SerializedProperty layersProp;
		SerializedProperty meshCollidersEnabledProp;
		SerializedProperty originPositionProp;
		SerializedProperty configurationsProp;

		void OnEnable()
		{
			apiKeyProp = serializedObject.FindProperty("apiKey");
			basemapProp = serializedObject.FindProperty("basemap");
			basemapTypeProp = serializedObject.FindProperty("basemapType");
			basemapAuthProp = serializedObject.FindProperty("basemapAuthentication");
			editorModeEnabledProp = serializedObject.FindProperty("editorModeEnabled");
			mapTypeProp = serializedObject.FindProperty("mapType");
			dataFetchWithSceneViewProp = serializedObject.FindProperty("dataFetchWithSceneView");
			rebaseWithSceneViewProp = serializedObject.FindProperty("rebaseWithSceneView");
			elevationProp = serializedObject.FindProperty("elevation");
			elevationAuthProp = serializedObject.FindProperty("elevationAuthentication");
			enableExtentProp = serializedObject.FindProperty("enableExtent");
			extentProp = serializedObject.FindProperty("extent");
			extentShapeProp = extentProp.FindPropertyRelative("ExtentShape");
			extentShapeDimensionsProp = extentProp.FindPropertyRelative("ShapeDimensions");
			layersProp = serializedObject.FindProperty("layers");
			meshCollidersEnabledProp = serializedObject.FindProperty("meshCollidersEnabled");
			originPositionProp = serializedObject.FindProperty("originPosition");
			configurationsProp = serializedObject.FindProperty("configurations");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			var mapNeedsUpdate = false;
			var arcGISMapComponent = target as ArcGISMapComponent;

			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(editorModeEnabledProp, Styles.EditorModeEnabled);
			if (EditorGUI.EndChangeCheck())
			{
				arcGISMapComponent.EditorModeEnabled = editorModeEnabledProp.boolValue;
				EditorUtility.SetDirty(arcGISMapComponent);
			}

			if (arcGISMapComponent.EditorModeEnabled)
			{
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(dataFetchWithSceneViewProp, Styles.DataFetchWithSceneView);
				if (EditorGUI.EndChangeCheck())
				{
					arcGISMapComponent.DataFetchWithSceneView = dataFetchWithSceneViewProp.boolValue;
					EditorUtility.SetDirty(arcGISMapComponent);
				}

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(rebaseWithSceneViewProp, Styles.RebaseWithSceneView);
				if (EditorGUI.EndChangeCheck())
				{
					arcGISMapComponent.RebaseWithSceneView = rebaseWithSceneViewProp.boolValue;
					EditorUtility.SetDirty(arcGISMapComponent);
				}
			}

			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(meshCollidersEnabledProp, Styles.MeshCollidersEnabled);
			if (EditorGUI.EndChangeCheck())
			{
				arcGISMapComponent.MeshCollidersEnabled = meshCollidersEnabledProp.boolValue;
				EditorUtility.SetDirty(arcGISMapComponent);
			}

			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(mapTypeProp);
			mapNeedsUpdate |= EditorGUI.EndChangeCheck();

			showOriginCategory = EditorGUILayout.BeginFoldoutHeaderGroup(showOriginCategory, "Origin Position");
			if (showOriginCategory)
			{
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(originPositionProp, Styles.OriginPosition);
				mapNeedsUpdate |= EditorGUI.EndChangeCheck();
			}
			EditorGUILayout.EndFoldoutHeaderGroup();

			showBasemapCategory = EditorGUILayout.BeginFoldoutHeaderGroup(showBasemapCategory, "Basemap");
			if (showBasemapCategory)
			{
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(basemapProp);
				mapNeedsUpdate |= EditorGUI.EndChangeCheck();

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(basemapTypeProp);
				mapNeedsUpdate |= EditorGUI.EndChangeCheck();

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(basemapAuthProp, new GUIContent("Authentication"));
				mapNeedsUpdate |= EditorGUI.EndChangeCheck();
			}
			EditorGUILayout.EndFoldoutHeaderGroup();

			showElevationCategory = EditorGUILayout.BeginFoldoutHeaderGroup(showElevationCategory, "Elevation");
			if (showElevationCategory)
			{
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(elevationProp);
				mapNeedsUpdate |= EditorGUI.EndChangeCheck();

				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(elevationAuthProp, new GUIContent("Authentication"));
				mapNeedsUpdate |= EditorGUI.EndChangeCheck();
			}
			EditorGUILayout.EndFoldoutHeaderGroup();

			if (mapTypeProp.enumNames[mapTypeProp.enumValueIndex] == "Local")
			{
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(enableExtentProp);
				mapNeedsUpdate |= EditorGUI.EndChangeCheck();

				if (enableExtentProp.boolValue)
				{
					showExtentCategory = EditorGUILayout.BeginFoldoutHeaderGroup(showExtentCategory, "Extent");
					if (showExtentCategory)
					{
						GUIContent shapeLabel;

						if (extentShapeProp.enumNames[extentShapeProp.enumValueIndex] == "Circle")
						{
							shapeLabel = new GUIContent("Radius");
						}
						else if (extentShapeProp.enumNames[extentShapeProp.enumValueIndex] == "Square")
						{
							shapeLabel = new GUIContent("Length");
						}
						else
						{
							shapeLabel = new GUIContent("X");
						}

						EditorGUI.BeginChangeCheck();
						EditorGUILayout.PropertyField(extentProp.FindPropertyRelative("GeographicCenter"));
						EditorGUILayout.PropertyField(extentShapeProp);
						EditorGUILayout.PropertyField(extentShapeDimensionsProp.FindPropertyRelative("x"), shapeLabel);

						if (extentShapeProp.enumNames[extentShapeProp.enumValueIndex] == "Rectangle")
						{
							EditorGUILayout.PropertyField(extentShapeDimensionsProp.FindPropertyRelative("y"), new GUIContent("Y"));
						}

						mapNeedsUpdate |= EditorGUI.EndChangeCheck();
					}
					EditorGUILayout.EndFoldoutHeaderGroup();
				}
			}

			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(layersProp);
			mapNeedsUpdate |= EditorGUI.EndChangeCheck();

			showAuthenticationCategory = EditorGUILayout.BeginFoldoutHeaderGroup(showAuthenticationCategory, "Authentication");
			if (showAuthenticationCategory)
			{
				EditorGUI.BeginChangeCheck();
				EditorGUILayout.PropertyField(apiKeyProp, new GUIContent("API Key"));
				var endGUICheck = EditorGUI.EndChangeCheck();
				if (endGUICheck)
				{
					mapNeedsUpdate |= endGUICheck;
					EditorUtility.SetDirty(arcGISMapComponent);
				}
			}
			EditorGUILayout.EndFoldoutHeaderGroup();

			EditorGUI.BeginChangeCheck();
			EditorGUILayout.PropertyField(configurationsProp, new GUIContent("Authentication Configurations"));
			mapNeedsUpdate |= EditorGUI.EndChangeCheck();

			serializedObject.ApplyModifiedProperties();

			if (mapNeedsUpdate)
			{
				arcGISMapComponent.UpdateMap();
			}
		}
	}
}
