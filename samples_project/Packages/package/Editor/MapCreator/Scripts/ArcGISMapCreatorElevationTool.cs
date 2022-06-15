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
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Esri.ArcGISMapsSDK.Editor.UI
{
	public static class ArcGISMapCreatorElevationTool 
	{
		private const string DefaultElevationSource = "https://elevation3d.arcgis.com/arcgis/rest/services/WorldElevation3D/Terrain3D/ImageServer";

		private static VisualElement rootElement;

		private static ArcGISMapComponent mapComponent;

		private static VisualTreeAsset cardTemplate;
		private static VisualElement cardHolder;

		private static TextField customSourceField;

		private static VisualElement elevationCard;
		private static VisualElement customElevationCard;

		private static Toggle elevationToggle;
		private static Toggle customElevationToggle;
		private static Button customSourceButton;
		private static PopupField<string> customSourceAuthPopup;

		public static string GetDefaultElevation()
		{
			return DefaultElevationSource;
		}

		public static VisualElement CreateElevationTool()
		{
			rootElement = new VisualElement();
			rootElement.name = "ArcGISMapCreatorElevationTool";

			mapComponent = GameObject.FindObjectOfType<ArcGISMapComponent>();

			var cardStyleSheet = MapCreatorUtilities.FindAssetPath("CardStyle");
			rootElement.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(cardStyleSheet));

			var elevationTemplatePath = MapCreatorUtilities.FindAssetPath("ElevationTemplate");
			var elevationTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(elevationTemplatePath);

			elevationTemplate.CloneTree(rootElement);

			cardHolder = rootElement.Query<VisualElement>(className: "card-holder");
			customSourceField = rootElement.Query<TextField>(name: "custom-elevation-source");

			customSourceButton = MapCreatorUtilities.InitializeFileSelectorButton(rootElement, customSourceField, "elevation-file-selector");

			var cardTemplatePath = MapCreatorUtilities.FindAssetPath("CardTemplate");
			cardTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(cardTemplatePath);

			elevationCard = CreateCard("default", "Default Elevation");
			customElevationCard = CreateCard("custom", "Custom Elevation");

			InitElevationCard();
			InitCustomElevationCard();
			SetInitialState();

			return rootElement;
		}

		private static VisualElement CreateCard(string cardName, string label)
		{
			var card = new VisualElement();
			card.name = cardName;
			cardTemplate.CloneTree(card);

			VisualElement image = card.Query<VisualElement>(className: "card-image");
			image.AddToClassList($"card-elevation-{cardName}");

			Label cardLabel = card.Query<Label>(className: "card-label");
			cardLabel.text = label;

			cardHolder.Add(card);

			return card;
		}

		private static void InitElevationCard()
		{
			elevationToggle = elevationCard.Query<Toggle>(className: "card-toggle");
			elevationToggle.RegisterValueChangedCallback(evnt =>
			{
				if (evnt.newValue == true)
				{
					SetCustomElevationState(false);

					if (mapComponent != null)
					{
						mapComponent.Elevation = DefaultElevationSource;
						MapCreatorUtilities.MarkDirty(mapComponent);
					}
				}
				else
				{
					SetEmptyElevationState();

					if (mapComponent != null)
					{
						mapComponent.Elevation = string.Empty;
						MapCreatorUtilities.MarkDirty(mapComponent);
					}
				}
			});
		}

		private static void InitCustomElevationCard()
		{
			customElevationToggle = customElevationCard.Query<Toggle>(className: "card-toggle");
			customElevationToggle.RegisterValueChangedCallback(evnt =>
			{
				if (evnt.newValue == true)
				{
					SetCustomElevationState(true);

					if (mapComponent != null)
					{
						mapComponent.Elevation = customSourceField.value;
						MapCreatorUtilities.MarkDirty(mapComponent);
					}
				}
				else
				{
					SetEmptyElevationState();

					if (mapComponent != null)
					{
						mapComponent.Elevation = string.Empty;
						MapCreatorUtilities.MarkDirty(mapComponent);
					}
				}
			});

			customSourceField.RegisterValueChangedCallback(evnt =>
			{
				if (mapComponent != null && customElevationToggle.value == true)
				{
					mapComponent.Elevation = evnt.newValue;
					MapCreatorUtilities.MarkDirty(mapComponent);
				}
			});

			VisualElement holder = rootElement.Query<VisualElement>(name: "custom-fields-holder");

			if (mapComponent != null)
			{
				customSourceAuthPopup = MapCreatorUtilities.InitializeAuthConfigMappingField(holder, mapComponent.ElevationAuthentication);
			}
			else
			{
				customSourceAuthPopup = MapCreatorUtilities.InitializeAuthConfigMappingField(holder, null);
			}
		}

		private static void SetCustomElevationState(bool isCustomElevation)
		{
			elevationToggle.SetValueWithoutNotify(!isCustomElevation);
			customElevationToggle.SetValueWithoutNotify(isCustomElevation);
			customSourceField.SetEnabled(isCustomElevation);
			customSourceButton.SetEnabled(isCustomElevation);
			customSourceAuthPopup.SetEnabled(isCustomElevation);
		}

		private static void SetEmptyElevationState()
		{
			elevationToggle.SetValueWithoutNotify(false);
			customElevationToggle.SetValueWithoutNotify(false);
			customSourceField.SetEnabled(false);
			customSourceButton.SetEnabled(false);
			customSourceAuthPopup.index = 0;
			customSourceAuthPopup.SetEnabled(false);
		}

		private static void SetInitialState()
		{
			if (mapComponent != null)
			{
				if (mapComponent.Elevation == DefaultElevationSource)
				{
					SetCustomElevationState(false);
				}
				else if (mapComponent.Elevation == string.Empty)
				{
					SetEmptyElevationState();
				}
				else
				{
					SetCustomElevationState(true);
					customSourceField.value = mapComponent.Elevation;
				}
			}
			else
			{
				SetEmptyElevationState();
			}
		}
	}
}
