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
using Esri.ArcGISMapsSDK.Security;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.ArcGISMapsSDK.Components;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Esri.ArcGISMapsSDK.Editor.UI
{
	public static class MapCreatorUtilities
	{
		public static string FindAssetPath(string name)
		{
			var results = AssetDatabase.FindAssets(name);

			if (results.Length == 0 || results == null)
			{
				UnityEngine.Debug.LogError("Asset " + name + " not found");
			}
			else if (results.Length > 1)
			{
				UnityEngine.Debug.LogError("Found more than one asset named " + name + ".\nPlease give the asset a unique name");
			}

			return AssetDatabase.GUIDToAssetPath(results[0]);
		}

		public static DoubleField InitializeDoubleField(VisualElement element, string name, SerializedProperty serializedProperty, Action<double> valueChangedCallback = null, bool truncateDouble = false)
		{
			DoubleField doubleField = element.Query<DoubleField>($"{name}-text");

			if (doubleField == null)
			{
				Debug.LogError($"Double field {name}-text not found");
				return null;
			}

			if (serializedProperty != null)
			{
				doubleField.value = !truncateDouble ? serializedProperty.doubleValue : TruncateDoubleForUI(serializedProperty.doubleValue);
			}

			doubleField.RegisterValueChangedCallback(@event =>
			{
				if (@event.newValue != @event.previousValue)
				{
					if (serializedProperty != null)
					{
						serializedProperty.doubleValue = @event.newValue;
						serializedProperty.serializedObject.ApplyModifiedProperties();
					}

					if (valueChangedCallback != null)
					{
						valueChangedCallback(@event.newValue);
					}
				}
			});

			return doubleField;
		}
		public static IntegerField InitializeIntegerField(VisualElement element, string name, SerializedProperty serializedProperty = null, Action<int> valueChangedCallback = null)
		{
			IntegerField intField = element.Query<IntegerField>($"{name}-text");

			if (intField == null)
			{
				Debug.LogError($"Int field {name}-text not found");
				return null;
			}

			if (serializedProperty != null)
			{
				intField.value = serializedProperty.intValue;
			}

			intField.RegisterValueChangedCallback(@event =>
			{
				if (@event.newValue != @event.previousValue)
				{
					if (serializedProperty != null)
					{
						serializedProperty.intValue = @event.newValue;
						serializedProperty.serializedObject.ApplyModifiedProperties();
					}

					if (valueChangedCallback != null)
					{
						valueChangedCallback(@event.newValue);
					}
				}
			});

			return intField;
		}

		public static void UpdateArcGISPointLabels(DoubleField X, DoubleField Y, DoubleField Z, int wkid)
		{
			if (wkid == SpatialReferenceWkid.WGS84 || wkid == SpatialReferenceWkid.CGCS2000)
			{
				X.label = "Longitude";
				Y.label = "Latitude";

				if (Z != null)
				{
					Z.label = "Altitude";
				}
			}
			else
			{
				X.label = "X";
				Y.label = "Y";

				if (Z != null)
				{
					Z.label = "Z";
				}
			}
		}

		public static void MarkDirty(ArcGISMapComponent mapComponent)
		{
			if (mapComponent == null)
			{
				return;
			}

			EditorUtility.SetDirty(mapComponent);
			EditorSceneManager.MarkSceneDirty(mapComponent.gameObject.scene);
		}

		public static string SelectFile()
		{
			return EditorUtility.OpenFilePanel("", Application.dataPath, "");
		}

		public static Button InitializeFileSelectorButton(VisualElement root, TextField source, string buttonName)
		{
			Button button = root.Query<Button>(name: buttonName);
			button.clickable.activators.Clear();
			button.RegisterCallback<MouseDownEvent>(evnt =>
			{
				source.value = SelectFile();
			});

			return button;
		}

		public static PopupField<string> InitializeAuthConfigMappingField(VisualElement parent, OAuthAuthenticationConfigurationMapping configurationMapping)
		{
			List<string> configNames = new List<string>();

			configNames.Add("None");

			ArcGISMapComponent mapComponent = GameObject.FindObjectOfType<ArcGISMapComponent>();

			PopupField<string> field;

			if (mapComponent != null)
			{
				foreach (var data in mapComponent.Configurations)
				{
					configNames.Add(data.Name);
				}

				field = new PopupField<string>("Authentication", configNames, configurationMapping.ConfigurationIndex + 1);
				field.RegisterValueChangedCallback(evnt =>
				{
					configurationMapping.ConfigurationIndex = field.index - 1;

					if (mapComponent != null)
					{
						MapCreatorUtilities.MarkDirty(mapComponent);
					}
				});
			}
			else
			{
				field = new PopupField<string>("Authentication", configNames, 0);
			}

			parent.Add(field);
			return field;
		}

		public static double TruncateDoubleForUI(double toTruncate)
		{
			const double roundFactor = 1000000;
			return Math.Truncate(toTruncate * roundFactor) / roundFactor;
		}
	}
}
