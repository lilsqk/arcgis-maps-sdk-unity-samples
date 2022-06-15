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
using Esri.GameEngine.Map;
using Esri.Unity;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Esri.ArcGISMapsSDK.Editor.UI
{
	public static class ArcGISMapCreatorBasemapTool
	{
		private static string ImagerySource = "https://ibasemaps-api.arcgis.com/arcgis/rest/services/World_Imagery/MapServer";
		private static string OceansSource = "https://ibasemaps-api.arcgis.com/arcgis/rest/services/Ocean/World_Ocean_Base/MapServer";

		private const string Imagery = "imagery";
		private const string Oceans = "oceans";
		private const string Custom = "custom";

		private const string CardToggleString = "card-toggle-";
		private const string ApiKeyWarningName = "api-key-warning";
		private const string ApiKeyToolTipHolderName = "api-key-tooltip";

		private static VisualElement rootElement;

		private static ArcGISMapComponent mapComponent;
		private static VisualTreeAsset cardTemplate;
		private static VisualElement cardHolder;

		private static Toggle activeToggle;
		private static List<Toggle> cardToggles = new List<Toggle>();

		private static TextField customBasemapSource;
		private static EnumField customBasemapType;
		private static Button customSourceButton;
		private static PopupField<string> customSourceAuthPopup;

		public static string GetDefaultBasemap()
		{
			return ImagerySource;
		}

		public static VisualElement CreateBasemapTool()
		{
			rootElement = new VisualElement();
			rootElement.name = "ArcGISMapCreatorBasemapTool";

			cardToggles = new List<Toggle>();

			mapComponent = GameObject.FindObjectOfType<ArcGISMapComponent>();

			var cardStyleSheet = MapCreatorUtilities.FindAssetPath("CardStyle");
			rootElement.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(cardStyleSheet));

			var basemapTemplatePath = MapCreatorUtilities.FindAssetPath("BasemapTemplate");
			var basemapTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(basemapTemplatePath);

			basemapTemplate.CloneTree(rootElement);

			InitCustomFields();

			cardHolder = rootElement.Query<VisualElement>(className: "card-holder");

			var cardTemplatePath = MapCreatorUtilities.FindAssetPath("CardTemplate");
			cardTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(cardTemplatePath);

			CreateBasemapCards();
			InitSelectedCard();

			return rootElement;
		}

		private static void CreateBasemapCards()
		{
			CreateCard(Imagery, "Imagery");
			CreateCard(Oceans, "Oceans");
			CreateCard(Custom, "Custom Basemap");
		}

		private static void InitCustomFields()
		{
			customBasemapSource = rootElement.Query<TextField>(name: "custom-basemap-source");
			customBasemapSource.RegisterValueChangedCallback(evnt =>
			{
				if (mapComponent != null && activeToggle != null && activeToggle.name == CardToggleString + Custom && activeToggle.value == true)
				{
					mapComponent.Basemap = evnt.newValue;
				}
			});

			customSourceButton = MapCreatorUtilities.InitializeFileSelectorButton(rootElement, customBasemapSource, "basemap-file-selector");

			customBasemapType = rootElement.Query<EnumField>(name: "basemap-type-selector");
			customBasemapType.RegisterValueChangedCallback(evnt =>
			{
				if (mapComponent != null && activeToggle != null && activeToggle.name == CardToggleString + Custom && activeToggle.value == true)
				{
					mapComponent.BasemapType = (BasemapTypes)evnt.newValue;
				}
			});

			VisualElement holder = rootElement.Query<VisualElement>(name: "custom-fields-holder");

			if (mapComponent != null)
			{
				customSourceAuthPopup = MapCreatorUtilities.InitializeAuthConfigMappingField(holder, mapComponent.BasemapAuthentication);
			}
			else
			{
				customSourceAuthPopup = MapCreatorUtilities.InitializeAuthConfigMappingField(holder, null);
			}
		}

		private static void InitSelectedCard()
		{
			foreach (var cardToggle in cardToggles)
			{
				if (mapComponent == null)
				{
					break;
				}

				if (CheckMapComponentForDefaultBasemapSource(cardToggle.name) == true)
				{
					cardToggle.SetValueWithoutNotify(true);
					activeToggle = cardToggle;
					SetCustomFields(false);
					CreateAPIKeyWarning(cardToggle.parent);
					return;
				}
				else if (mapComponent.Basemap != string.Empty && cardToggle.name == CardToggleString + Custom)
				{
					cardToggle.SetValueWithoutNotify(true);
					activeToggle = cardToggle;
					SetCustomFields(true);

					customBasemapSource.value = mapComponent.Basemap;
					customBasemapType.value = mapComponent.BasemapType;
					customSourceAuthPopup.index = mapComponent.BasemapAuthentication.ConfigurationIndex + 1;

					return;
				}
			}

			SetCustomFields(false);
		}

		private static void SetCustomFields(bool isEnabled)
		{
			customBasemapSource.SetEnabled(isEnabled);
			customBasemapType.SetEnabled(isEnabled);
			customSourceButton.SetEnabled(isEnabled);
			customSourceAuthPopup.SetEnabled(isEnabled);
		}

		private static void CreateCard(string cardName, string label)
		{
			var card = new VisualElement();
			card.name = cardName;
			cardTemplate.CloneTree(card);

			VisualElement image = card.Query<VisualElement>(className: "card-image");
			image.AddToClassList($"card-basemap-{cardName}");

			Label cardLabel = card.Query<Label>(className: "card-label");
			cardLabel.text = label;

			Toggle cardToggle = card.Query<Toggle>(className: "card-toggle");
			cardToggle.name = $"card-toggle-{cardName}";
			cardToggles.Add(cardToggle);

			if (cardName == Custom)
			{
				cardToggle.RegisterValueChangedCallback(evnt => SetMap(evnt, false));
			}
			else
			{
				cardToggle.RegisterValueChangedCallback(evnt => SetMap(evnt, true));
			}

			cardHolder.Add(card);
		}

		private static void SetMap(ChangeEvent<bool> evnt, bool useAPIKeyWarning)
		{
			var toggle = (Toggle)evnt.currentTarget;

			if (activeToggle != null && activeToggle != toggle)
			{
				RemoveCurrentAPIKeyWarning();
				activeToggle.SetValueWithoutNotify(false);
			}

			if (evnt.newValue == true)
			{
				if (useAPIKeyWarning)
				{
					CreateAPIKeyWarning(toggle.parent);
				}

				SetBasemapSourceOnMapComponent(toggle.name);

				activeToggle = toggle;
			}
			else
			{
				RemoveCurrentAPIKeyWarning();
				SetBasemapSourceOnMapComponent(string.Empty);
			}
		}

		private static void SetBasemapSourceOnMapComponent(string name)
		{
			if (mapComponent == null)
			{
				return;
			}

			if (name == CardToggleString + Imagery)
			{
				mapComponent.Basemap = ImagerySource;
				mapComponent.BasemapType = BasemapTypes.ImageLayer;
				SetCustomFields(false);
			}
			else if (name == CardToggleString + Oceans)
			{
				mapComponent.Basemap = OceansSource;
				mapComponent.BasemapType = BasemapTypes.ImageLayer;
				SetCustomFields(false);
			}
			else if (name == CardToggleString + Custom)
			{
				mapComponent.Basemap = customBasemapSource.value;
				mapComponent.BasemapType = (BasemapTypes)customBasemapType.value;
				SetCustomFields(true);
			}
			else
			{
				mapComponent.Basemap = string.Empty;
				SetCustomFields(false);
			}

			MapCreatorUtilities.MarkDirty(mapComponent);
		}

		private static bool CheckMapComponentForDefaultBasemapSource(string name)
		{
			if (mapComponent == null)
			{
				return false;
			}

			if (mapComponent.Basemap == ImagerySource && name == CardToggleString + Imagery)
			{
				return true;
			}
			else if (mapComponent.Basemap == OceansSource && name == CardToggleString + Oceans)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		private static void CreateAPIKeyWarning(VisualElement cardInfoHolder)
		{
			RemoveCurrentAPIKeyWarning();

			if ((mapComponent == null && ArcGISMapCreatorAuthTool.APIKey == string.Empty)
				|| (mapComponent != null && mapComponent.APIKey == string.Empty))
			{
				VisualElement warningHolder = new VisualElement();
				warningHolder.name = ApiKeyWarningName;
				warningHolder.style.flexDirection = FlexDirection.Row;
				warningHolder.style.flexGrow = 1;

				VisualElement spacer = new VisualElement();
				spacer.style.flexGrow = 1;
				spacer.style.minWidth = 20;
				warningHolder.Add(spacer);

				Label label = new Label("API key required");
				label.style.fontSize = 10;
				label.RegisterCallback<MouseEnterEvent>(evnt => CreateWarningToolTip(label));
				warningHolder.Add(label);

				VisualElement icon = new VisualElement();
				icon.AddToClassList("warning-icon");
				warningHolder.Add(icon);

				cardInfoHolder.Add(warningHolder);
			}
		}

		private static void CreateWarningToolTip(VisualElement label)
		{
			RemoveCurrentTooltip();

			VisualElement tooltip = new VisualElement();
			tooltip.name = ApiKeyToolTipHolderName;
			tooltip.AddToClassList("card-warning-tooltip");
			tooltip.RegisterCallback<MouseLeaveEvent>(evnt =>
			{
				rootElement.Remove(tooltip);
			});

			tooltip.style.flexGrow = 1;

			Label textRow = new Label("You need to set an API key to load a preset basemap");
			Label textRowAPIKeyLink = new Label("Open the Authentication Tool");

			textRow.AddToClassList("api-key-tooltip-text");
			textRowAPIKeyLink.AddToClassList("api-key-tooltip-text");

			textRowAPIKeyLink.AddToClassList("hyperlink-text");

			textRowAPIKeyLink.RegisterCallback<MouseDownEvent>(evnt =>
			{
				ArcGISMapCreator.OpenAuthTool();
			});

			tooltip.Add(textRow);
			tooltip.Add(textRowAPIKeyLink);

			rootElement.Add(tooltip);

			tooltip.transform.position = new Vector3(-15, 0, 0);
			var y = label.worldBound.y - tooltip.worldBound.y;
			tooltip.transform.position = new Vector3(-15, y + 17, 0);
		}

		private static void RemoveCurrentAPIKeyWarning()
		{
			VisualElement currentWarningHolder = rootElement.Query<VisualElement>(name: ApiKeyWarningName);

			if (currentWarningHolder != null)
			{
				currentWarningHolder.parent.Remove(currentWarningHolder);
			}

			RemoveCurrentTooltip();
		}

		private static void RemoveCurrentTooltip()
		{
			VisualElement currentToolTip = rootElement.Query<VisualElement>(name: ApiKeyToolTipHolderName);

			if (currentToolTip != null)
			{
				rootElement.Remove(currentToolTip);
			}
		}
	}
}
