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
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Esri.ArcGISMapsSDK.Editor.UI
{
	public class ArcGISMapCreator : EditorWindow
	{
		private VisualElement toolbarElement;
		private VisualElement currentActiveTool;
		private List<Button> toolbarButtons = new List<Button>();

		[MenuItem("ArcGIS Maps SDK/Map Creator", false, 1)]
		private static void CreateMapCreatorEditorWindow()
		{
			var window = (ArcGISMapCreator)EditorWindow.GetWindow(typeof(ArcGISMapCreator));
			window.Show();
		}

		private void OnEnable()
		{
			titleContent.text = "ArcGISMapsSDK";
			titleContent.image = Resources.Load<Texture>("ArcGISMapsSDKEditor/MapCreatorIcons/arcgis-maps-sdks-16");
			minSize = new Vector2(75, 100);
			EditorSceneManager.activeSceneChangedInEditMode += OnSceneChange;
		}

		public static void OpenAuthTool()
		{
			var window = (ArcGISMapCreator)EditorWindow.GetWindow(typeof(ArcGISMapCreator));
			window.Show();
			Button openAuthToolButton = window.toolbarElement.Query<Button>("button-open-auth-tool");
			window.SetActiveTool(openAuthToolButton, ArcGISMapCreatorAuthTool.CreateAuthTool());
		}

		private void OnSceneChange(Scene prev, Scene next)
		{
			if (currentActiveTool != null)
			{
				Button activeButton = toolbarElement.Query<Button>(className: "button-selected");
				switch (currentActiveTool.name)
				{
					case "ArcGISMapCreatorAuthTool":
						SetActiveTool(activeButton, ArcGISMapCreatorAuthTool.CreateAuthTool());
						break;
					case "ArcGISMapCreatorBasemapTool":
						SetActiveTool(activeButton, ArcGISMapCreatorBasemapTool.CreateBasemapTool());
						break;
					case "ArcGISMapCreatorCameraTool":
						SetActiveTool(activeButton, ArcGISMapCreatorCameraTool.CreateCameraTool());
						break;
					case "ArcGISMapCreatorElevationTool":
						SetActiveTool(activeButton, ArcGISMapCreatorElevationTool.CreateElevationTool());
						break;
					case "ArcGISMapCreatorHelpTool":
						SetActiveTool(activeButton, ArcGISMapCreatorHelpTool.CreateHelpTool());
						break;
					case "ArcGISMapCreatorLayerTool":
						SetActiveTool(activeButton, ArcGISMapCreatorLayerTool.CreateLayerTool());
						break;
					case "ArcGISMapCreatorMapTool":
						SetActiveTool(activeButton, ArcGISMapCreatorMapTool.CreateMapTool());
						break;
					case "ArcGISMapCreatorSettingsTool":
						SetActiveTool(activeButton, ArcGISMapCreatorSettingsTool.CreateMapTool());
						break;
				}
			}
		}

		private void CreateGUI()
		{
			var styleSheet = MapCreatorUtilities.FindAssetPath("MapCreatorStyle");
			rootVisualElement.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(styleSheet));

			CreateToolbar();
		}

		private void CreateToolbar()
		{
			toolbarElement = new VisualElement();

			var templatePath = MapCreatorUtilities.FindAssetPath("ToolbarTemplate");
			var template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(templatePath);
			template.CloneTree(toolbarElement);

			var styleSheet = MapCreatorUtilities.FindAssetPath("ToolbarStyle");
			toolbarElement.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(styleSheet));

			Button openMapToolButton = toolbarElement.Query<Button>("button-open-map-tool");
			toolbarButtons.Add(openMapToolButton);
			openMapToolButton.RegisterCallback<MouseUpEvent>(evnt =>
			{
				SetActiveTool((Button)evnt.currentTarget, ArcGISMapCreatorMapTool.CreateMapTool());
			});

			Button openCameraToolButton = toolbarElement.Query<Button>("button-open-camera-tool");
			toolbarButtons.Add(openCameraToolButton);
			openCameraToolButton.RegisterCallback<MouseUpEvent>(evnt =>
			{
				SetActiveTool((Button)evnt.currentTarget, ArcGISMapCreatorCameraTool.CreateCameraTool());
			});

			Button openBasemapToolButton = toolbarElement.Query<Button>("button-open-basemap-tool");
			toolbarButtons.Add(openBasemapToolButton);
			openBasemapToolButton.RegisterCallback<MouseUpEvent>(evnt =>
			{
				SetActiveTool((Button)evnt.currentTarget, ArcGISMapCreatorBasemapTool.CreateBasemapTool());
			});

			Button openElevationToolButton = toolbarElement.Query<Button>("button-open-elevation-tool");
			toolbarButtons.Add(openElevationToolButton);
			openElevationToolButton.RegisterCallback<MouseUpEvent>(evnt =>
			{
				SetActiveTool((Button)evnt.currentTarget, ArcGISMapCreatorElevationTool.CreateElevationTool());
			});

			Button openLayersToolButton = toolbarElement.Query<Button>("button-open-layers-tool");
			toolbarButtons.Add(openLayersToolButton);
			openLayersToolButton.RegisterCallback<MouseUpEvent>(evnt =>
			{
				SetActiveTool((Button)evnt.currentTarget, ArcGISMapCreatorLayerTool.CreateLayerTool());
			});

			Button openAuthToolButton = toolbarElement.Query<Button>("button-open-auth-tool");
			toolbarButtons.Add(openAuthToolButton);
			openAuthToolButton.RegisterCallback<MouseUpEvent>(evnt =>
			{
				SetActiveTool((Button)evnt.currentTarget, ArcGISMapCreatorAuthTool.CreateAuthTool());
			});

			Button openSettingsToolButton = toolbarElement.Query<Button>("button-open-settings-tool");
			toolbarButtons.Add(openSettingsToolButton);
			openSettingsToolButton.RegisterCallback<MouseUpEvent>(evnt =>
			{
				SetActiveTool((Button)evnt.currentTarget, ArcGISMapCreatorSettingsTool.CreateMapTool());
			});

			Button openHelpToolButton = toolbarElement.Query<Button>("button-open-help-tool");
			toolbarButtons.Add(openHelpToolButton);
			openHelpToolButton.RegisterCallback<MouseUpEvent>(evnt =>
			{
				SetActiveTool((Button)evnt.currentTarget, ArcGISMapCreatorHelpTool.CreateHelpTool());
			});

			rootVisualElement.Add(toolbarElement);

			SetActiveTool(openMapToolButton, ArcGISMapCreatorMapTool.CreateMapTool());
		}

		private void SetActiveTool(Button target, VisualElement tool)
		{
			if (currentActiveTool != null && toolbarElement.Contains(currentActiveTool))
			{
				toolbarElement.Remove(currentActiveTool);
			}

			foreach (var button in toolbarButtons)
			{
				button.RemoveFromClassList("button-selected");
			}

			currentActiveTool = tool;
			toolbarElement.Add(currentActiveTool);
			target.AddToClassList("button-selected");
		}
	}
}
