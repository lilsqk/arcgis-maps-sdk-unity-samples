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
using Esri.GameEngine.Map;
using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Esri.ArcGISMapsSDK.Editor.UI
{
	public static class ArcGISMapCreatorSettingsTool
	{
		private static ArcGISMapComponent mapComponent;

		private static Toggle editorModeToggle;
		private static Toggle meshColliderToggle;

		public static VisualElement CreateMapTool()
		{
			VisualElement rootElement = new VisualElement();
			rootElement.name = "ArcGISMapCreatorSettingsTool";

			var templatePath = MapCreatorUtilities.FindAssetPath("SettingsToolTemplate");
			var template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(templatePath);
			template.CloneTree(rootElement);

			var styleSheet = MapCreatorUtilities.FindAssetPath("SettingsToolStyle");
			rootElement.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(styleSheet));

			mapComponent = GameObject.FindObjectOfType<ArcGISMapComponent>();

			InitEditorModeToggle(rootElement);
			InitSceneViewDataFetchToggle(rootElement);
			InitSceneViewRebaseToggle(rootElement);
			InitMeshColliderToggle(rootElement);

			return rootElement;
		}

		private static void InitEditorModeToggle(VisualElement rootElement)
		{
			editorModeToggle = rootElement.Query<Toggle>(name: "toggle-enable-editor-mode");
			editorModeToggle.RegisterValueChangedCallback(evnt =>
			{
				if (mapComponent != null)
				{
					mapComponent.EditorModeEnabled = evnt.newValue;
				}
			});

			if (mapComponent != null)
			{
				editorModeToggle.value = mapComponent.EditorModeEnabled;	
			}
		}

		private static void InitSceneViewDataFetchToggle(VisualElement rootElement)
		{
			editorModeToggle = rootElement.Query<Toggle>(name: "toggle-enable-editor-mode-data-fetch");
			editorModeToggle.RegisterValueChangedCallback(evnt =>
			{
				if (mapComponent != null)
				{
					mapComponent.DataFetchWithSceneView = evnt.newValue;
				}
			});

			if (mapComponent != null)
			{
				editorModeToggle.value = mapComponent.DataFetchWithSceneView;	
			}
		}

		private static void InitSceneViewRebaseToggle(VisualElement rootElement)
		{
			editorModeToggle = rootElement.Query<Toggle>(name: "toggle-enable-editor-mode-rebase");
			editorModeToggle.RegisterValueChangedCallback(evnt =>
			{
				if (mapComponent != null)
				{
					mapComponent.RebaseWithSceneView = evnt.newValue;
				}
			});

			if (mapComponent != null)
			{
				editorModeToggle.value = mapComponent.RebaseWithSceneView;	
			}
		}

		private static void InitMeshColliderToggle(VisualElement rootElement)
		{
			meshColliderToggle = rootElement.Query<Toggle>(name: "toggle-enable-mesh-colliders");
			meshColliderToggle.RegisterValueChangedCallback(evnt =>
			{
				if (mapComponent != null)
				{
					mapComponent.MeshCollidersEnabled = evnt.newValue;
				}
			});

			if (mapComponent != null)
			{
				meshColliderToggle.value = mapComponent.MeshCollidersEnabled;	
			}
		}
	}
}
