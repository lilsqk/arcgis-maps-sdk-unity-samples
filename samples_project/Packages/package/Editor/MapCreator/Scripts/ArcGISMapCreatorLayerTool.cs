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
using Esri.ArcGISMapsSDK.Security;
using Esri.GameEngine.Layers.Base;
using Esri.HPFramework;
using Esri.Unity;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Esri.ArcGISMapsSDK.Editor.UI
{
	public static class ArcGISMapCreatorLayerTool
	{
		private class LayerContainer
		{
			public LayerContainer() { }

			public LayerContainer(VisualElement row, ArcGISLayerInstanceData layer)
			{
				this.row = row;
				this.layer = layer;
			}

			public VisualElement row;
			public ArcGISLayerInstanceData layer;
		}

		private static ArcGISMapComponent mapComponent;

		private static VisualTreeAsset rowTemplate;
		private static VisualTreeAsset optionsTemplate;

		private static VisualElement rootElement;
		private static VisualElement layerHolder;
		private static VisualElement optionsMenuHolder;

		private static EnumField addDataLayerTypeSelector;
		private static TextField addDataSourceField;
		private static TextField addDataNameField;

		private static LayerContainer currentContainer;
		private static TextField currentNameField;
		private static Label currentNameLabel;

		private static OAuthAuthenticationConfigurationMapping addDataAuthentication = new OAuthAuthenticationConfigurationMapping();
		private static PopupField<string> addDataConfigPopup;

		private static List<LayerContainer> layers = new List<LayerContainer>();

		public static VisualElement CreateLayerTool()
		{
			rootElement = new VisualElement();
			rootElement.name = "ArcGISMapCreatorLayerTool";

			mapComponent = GameObject.FindObjectOfType<ArcGISMapComponent>();

			var layerStyleSheet = MapCreatorUtilities.FindAssetPath("LayerStyle");
			rootElement.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(layerStyleSheet));

			var toolTemplatePath = MapCreatorUtilities.FindAssetPath("LayerToolTemplate");
			var toolTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(toolTemplatePath);
			toolTemplate.CloneTree(rootElement);

			var rowTemplatePath = MapCreatorUtilities.FindAssetPath("LayerRowTemplate");
			rowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(rowTemplatePath);

			var optionsTemplatePath = MapCreatorUtilities.FindAssetPath("LayerOptionsTemplate");
			optionsTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(optionsTemplatePath);

			layerHolder = rootElement.Query<VisualElement>(name: "layers-holder");

			InitAddDataFields();
			CreateOptionsMenu();
			CreateMapComponentLayers();

			return rootElement;
		}

		private static void InitAddDataFields()
		{
			addDataLayerTypeSelector = rootElement.Query<EnumField>(name: "add-data-type-selector");
			addDataSourceField = rootElement.Query<TextField>(name: "add-data-layer-source");

			MapCreatorUtilities.InitializeFileSelectorButton(rootElement, addDataSourceField, "add-data-file-selector");

			addDataNameField = rootElement.Query<TextField>(name: "add-data-name-text");

			VisualElement configHolder = rootElement.Query<VisualElement>(name: "add-data-config-selector");
			addDataConfigPopup = MapCreatorUtilities.InitializeAuthConfigMappingField(configHolder, addDataAuthentication);

			Button clearButton = rootElement.Query<Button>(name: "button-add-data-clear");
			clearButton.clickable.activators.Clear();
			clearButton.RegisterCallback<MouseDownEvent>(evnt => ClearAddDataFields());

			Button addButton = rootElement.Query<Button>(name: "button-add-data-add");
			addButton.clickable.activators.Clear();
			addButton.RegisterCallback<MouseDownEvent>(evnt =>
			{
				if (addDataNameField.value == string.Empty || addDataSourceField.value == string.Empty)
				{
					Debug.LogWarning("Please provide a name and a source to create a new layer");
					return;
				}

				if (mapComponent == null)
				{
					Debug.LogWarning("Please add a map component to the scene to create a new layer");
					return;
				}

				var layer = new ArcGISLayerInstanceData();
				layer.Name = addDataNameField.value;
				layer.Source = addDataSourceField.value;
				layer.Type = (LayerTypes)addDataLayerTypeSelector.value;
				layer.Opacity = 1.0f;
				layer.IsVisible = true;
				layer.Authentication = new OAuthAuthenticationConfigurationMapping();
				layer.Authentication.ConfigurationIndex = addDataAuthentication.ConfigurationIndex;

				mapComponent.Layers.Add(layer);

				CreateLayerRow(layer);
				ClearAddDataFields();
			});
		}

		private static void ClearAddDataFields()
		{
			addDataLayerTypeSelector.value = LayerTypes.ArcGISImageLayer;
			addDataSourceField.SetValueWithoutNotify(string.Empty);
			addDataNameField.SetValueWithoutNotify(string.Empty);
			addDataAuthentication.ConfigurationIndex = OAuthAuthenticationConfigurationMapping.NoConfigurationIndex;
			addDataConfigPopup.index = 0;
		}

		private static void CreateMapComponentLayers()
		{
			if (mapComponent == null)
			{
				return;
			}

			foreach (var layer in mapComponent.Layers)
			{
				CreateLayerRow(layer);
			}
		}

		private static void ResetLayers()
		{
			layerHolder.Clear();
			CreateMapComponentLayers();
			EditorUtility.SetDirty(mapComponent);
		}

		private static void CreateLayerRow(ArcGISLayerInstanceData layer)
		{
			var row = new VisualElement();
			row.name = "Layer Row";
			layerHolder.Add(row);
			rowTemplate.CloneTree(row);

			var container = new LayerContainer(row, layer);

			SetEnableToggle(container);
			SetLayerNameHolder(container);
			SetOpacitySlider(container);
			SetTypeSelector(container);
			SetSourceField(container);
			SetAuthConfigMappingField(container);
		}

		private static void SetAuthConfigMappingField(LayerContainer container)
		{
			Foldout foldout = container.row.Query<Foldout>(className: "foldout-layer-row");
			foldout.value = false;
			MapCreatorUtilities.InitializeAuthConfigMappingField(foldout, container.layer.Authentication);
		}

		private static void SetLayerNameHolder(LayerContainer container)
		{
			TextField nameField = container.row.Query<TextField>(className: "layer-name-text");
			Label nameLabel = container.row.Query<Label>(className: "layer-name-label");

			SetLayerOptionsButton(container, nameField, nameLabel);

			nameLabel.RegisterCallback<MouseDownEvent>(evnt =>
			{
				evnt.StopImmediatePropagation();
				EnableLayerNameTextField(nameLabel, nameField, container.layer.Name);
			});

			nameField.RegisterCallback<FocusInEvent>(evnt =>
			{
				nameField.SelectAll();
			});

			nameField.RegisterCallback<FocusOutEvent>(evnt =>
			{
				var index = mapComponent.Layers.IndexOf(container.layer);
				if (container.layer.Name != nameField.value)
				{
					EditorUtility.SetDirty(mapComponent);
				}
				container.layer.Name = nameField.value;
				mapComponent.Layers[index] = container.layer;
				SetLayerNameLabel(nameLabel, nameField, AdjustStringSize(container.layer.Name));
			});

			nameField.RegisterCallback<KeyDownEvent>(evnt =>
			{
				if (evnt.keyCode != KeyCode.Return)
				{
					return;
				}

				evnt.StopImmediatePropagation();
				var index = mapComponent.Layers.IndexOf(container.layer);
				if (container.layer.Name != nameField.value)
				{
					EditorUtility.SetDirty(mapComponent);
				}
				container.layer.Name = nameField.value;
				mapComponent.Layers[index] = container.layer;
				SetLayerNameLabel(nameLabel, nameField, AdjustStringSize(container.layer.Name));
			});

			nameField.visible = false;
			nameField.style.display = DisplayStyle.None;
			nameLabel.text = AdjustStringSize(container.layer.Name);

			VisualElement layerNameHolder = container.row.Query<VisualElement>(className: "layer-name-holder");
			VisualElement foldoutToggle = container.row.Query<VisualElement>(className: "unity-foldout__input");
			foldoutToggle.Add(layerNameHolder);
		}

		private static void EnableLayerNameTextField(Label layerNameLabel, TextField layerNameField, string textValue)
		{
			layerNameLabel.style.display = DisplayStyle.None;

			layerNameField.visible = true;
			layerNameField.style.display = DisplayStyle.Flex;
			layerNameField.SetValueWithoutNotify(textValue);
		}

		private static void SetLayerNameLabel(Label layerNameLabel, TextField layerNameField, string textValue)
		{
			layerNameLabel.style.display = DisplayStyle.Flex;
			layerNameLabel.text = textValue;

			layerNameField.visible = false;
			layerNameField.style.display = DisplayStyle.None;
		}

		private static void SetLayerOptionsButton(LayerContainer container, TextField nameField, Label nameLabel)
		{
			Button optionsButton = container.row.Query<Button>(name: "layer-options-button");
			optionsButton.clickable.activators.Clear();
			optionsButton.RegisterCallback<MouseDownEvent>(evnt =>
			{
				evnt.StopImmediatePropagation();
				currentContainer = container;
				currentNameField = nameField;
				currentNameLabel = nameLabel;
				OpenOptionsMenu(optionsButton);
			});
		}

		private static void CreateOptionsMenu()
		{
			optionsMenuHolder = new VisualElement();
			optionsMenuHolder.AddToClassList("layer-options-dropdown");
			optionsTemplate.CloneTree(optionsMenuHolder);

			rootElement.Add(optionsMenuHolder);

			Button moveUpButton = optionsMenuHolder.Query<Button>(name: "layer-options-move-up");
			moveUpButton.clickable.activators.Clear();
			moveUpButton.RegisterCallback<MouseDownEvent>(evnt =>
			{
				MoveLayerUp();
				CloseOptionsMenu();
			});

			Button moveDownButton = optionsMenuHolder.Query<Button>(name: "layer-options-move-down");
			moveDownButton.clickable.activators.Clear();
			moveDownButton.RegisterCallback<MouseDownEvent>(evnt =>
			{
				MoveLayerDown();
				CloseOptionsMenu();
			});

			Button renameButton = optionsMenuHolder.Query<Button>(name: "layer-options-rename");
			renameButton.clickable.activators.Clear();
			renameButton.RegisterCallback<MouseDownEvent>(evnt =>
			{
				EnableLayerNameTextField(currentNameLabel, currentNameField, currentContainer.layer.Name);
				CloseOptionsMenu();
			});

			Button removeButton = optionsMenuHolder.Query<Button>(name: "layer-options-remove");
			removeButton.clickable.activators.Clear();
			removeButton.RegisterCallback<MouseDownEvent>(evnt =>
			{
				RemoveLayer();
				CloseOptionsMenu();
			});

			Button zoomButton = optionsMenuHolder.Query<Button>(name: "layer-options-zoom");
			zoomButton.clickable.activators.Clear();
			zoomButton.RegisterCallback<MouseDownEvent>(async evnt =>
			{
				CloseOptionsMenu();
				await ZoomToLayer();
			});

			Button copyButton = optionsMenuHolder.Query<Button>(name: "layer-options-copy");
			copyButton.clickable.activators.Clear();
			copyButton.RegisterCallback<MouseDownEvent>(evnt =>
			{
				EditorGUIUtility.systemCopyBuffer = currentContainer.layer.Source;
				Debug.Log("Layer source copied to clipboard: " + currentContainer.layer.Source);
				CloseOptionsMenu();
			});

			CloseOptionsMenu();
		}

		private static void OpenOptionsMenu(Button optionsButton)
		{
			optionsMenuHolder.visible = !optionsMenuHolder.visible;

			if (optionsMenuHolder.visible)
			{
				optionsMenuHolder.style.display = DisplayStyle.Flex;
				optionsMenuHolder.transform.position = new Vector3(-15, 0, 0);
				var y = optionsButton.worldBound.y - optionsMenuHolder.worldBound.y;
				optionsMenuHolder.transform.position = new Vector3(-15, y + 17, 0);
			}
			else
			{
				CloseOptionsMenu();
			}
		}

		private static void CloseOptionsMenu()
		{
			optionsMenuHolder.visible = false;
			optionsMenuHolder.style.display = DisplayStyle.None;
		}

		private static void MoveLayerUp()
		{
			var layerIndex = mapComponent.Layers.IndexOf(currentContainer.layer);

			if (layerIndex == 0)
			{
				return;
			}

			mapComponent.Layers.RemoveAt(layerIndex);
			mapComponent.Layers.Insert(layerIndex - 1, currentContainer.layer);
			ResetLayers();
		}

		private static void MoveLayerDown()
		{
			var layerIndex = mapComponent.Layers.IndexOf(currentContainer.layer);

			if (layerIndex >= layerHolder.childCount - 1)
			{
				return;
			}

			mapComponent.Layers.RemoveAt(layerIndex);
			mapComponent.Layers.Insert(layerIndex + 1, currentContainer.layer);
			ResetLayers();
		}

		private static void RemoveLayer()
		{
			layerHolder.Remove(currentContainer.row);
			mapComponent.Layers.Remove(currentContainer.layer);

			currentContainer = null;
			currentNameField = null;
			currentNameLabel = null;
		}

		private static async Task<bool> ZoomToLayer()
		{
			var Layers = mapComponent.View.Map.Layers;

			if (Layers.GetSize() == 0)
			{
				return false;
			}

			ArcGISLayer layer = null;

			for (ulong i = 0; i < Layers.GetSize(); i++)
			{
				var testLayer = Layers.At(i);

				if (testLayer.Source == currentContainer.layer.Source && testLayer.ObjectType == (ArcGISLayerType)currentContainer.layer.Type)
				{
					layer = testLayer;
					break;
				}
			}

			if (layer == null)
			{
				return false;
			}

#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				var EditorCameraComponent = mapComponent.GetComponentInChildren<ArcGISEditorCameraComponent>();

				if (EditorCameraComponent != null && EditorCameraComponent.enabled)
				{
					bool success = await mapComponent.ZoomToLayer(EditorCameraComponent.gameObject, layer);

					if (!success)
					{
						return false;
					}

					var hP = EditorCameraComponent.GetComponent<HPTransform>();
					SceneView.lastActiveSceneView.pivot = new Vector3(0, 0, 0);
					SceneView.lastActiveSceneView.LookAt(new Vector3(0, 0, 0), Quaternion.Euler(90, 0, 0));
					mapComponent.UniversePosition = hP.UniversePosition;
					mapComponent.SyncPositionWithHPRoot();
					SceneView.lastActiveSceneView.Repaint();

					return true;
				}
			}
#endif

			return false;
		}

		private static void SetEnableToggle(LayerContainer container)
		{
			Toggle enableToggle = container.row.Query<Toggle>(name: "layer-enable-toggle");
			enableToggle.RegisterValueChangedCallback(evnt =>
			{
				if (enableToggle.value == true)
				{
					var index = mapComponent.Layers.IndexOf(container.layer);
					container.layer.IsVisible = true;
					mapComponent.Layers[index] = container.layer;
				}
				else
				{
					var index = mapComponent.Layers.IndexOf(container.layer);
					container.layer.IsVisible = false;
					mapComponent.Layers[index] = container.layer;
				}
				EditorUtility.SetDirty(mapComponent);
			});

			var enabled = container.layer.IsVisible;
			enableToggle.SetValueWithoutNotify(enabled);
		}

		private static void SetOpacitySlider(LayerContainer container)
		{
			Slider slider = container.row.Query<Slider>(className: "layer-opacity-slider");
			FloatField floatField = container.row.Query<FloatField>(className: "layer-opacity-field");

			slider.RegisterValueChangedCallback(evnt =>
			{
				floatField.SetValueWithoutNotify(evnt.newValue);

				var index = mapComponent.Layers.IndexOf(container.layer);
				container.layer.Opacity = evnt.newValue;
				mapComponent.Layers[index] = container.layer;
				EditorUtility.SetDirty(mapComponent);
			});

			floatField.RegisterValueChangedCallback(evnt =>
			{
				float newValue = evnt.newValue;

				if (newValue > 1)
				{
					newValue = 1;
				}
				else if (newValue < 0)
				{
					newValue = 0;
				}

				floatField.SetValueWithoutNotify(newValue);
				slider.SetValueWithoutNotify(newValue);

				var index = mapComponent.Layers.IndexOf(container.layer);
				container.layer.Opacity = newValue;
				mapComponent.Layers[index] = container.layer;
				EditorUtility.SetDirty(mapComponent);
			});

			slider.value = container.layer.Opacity;
			floatField.value = container.layer.Opacity;
		}

		private static void SetTypeSelector(LayerContainer container)
		{
			EnumField typeSelector = container.row.Query<EnumField>(className: "layer-type-selector");
			typeSelector.RegisterValueChangedCallback(evnt =>
			{
				var index = mapComponent.Layers.IndexOf(container.layer);
				container.layer.Type = (LayerTypes)evnt.newValue;
				mapComponent.Layers[index] = container.layer;
				EditorUtility.SetDirty(mapComponent);
			});

			typeSelector.SetValueWithoutNotify(container.layer.Type);
		}

		private static void SetSourceField(LayerContainer container)
		{
			TextField sourceField = container.row.Query<TextField>(name: "layer-source");
			sourceField.RegisterValueChangedCallback(evnt =>
			{
				var index = mapComponent.Layers.IndexOf(container.layer);
				container.layer.Source = evnt.newValue;
				mapComponent.Layers[index] = container.layer;
				EditorUtility.SetDirty(mapComponent);
			});

			MapCreatorUtilities.InitializeFileSelectorButton(container.row, sourceField, "layer-file-selector");
			sourceField.SetValueWithoutNotify(container.layer.Source);
		}

		private static string AdjustStringSize(string original)
		{
			if (original.Length > 20)
			{
				return original.Substring(0, 20) + " ...";
			}
			return original;
		}
	}
}
