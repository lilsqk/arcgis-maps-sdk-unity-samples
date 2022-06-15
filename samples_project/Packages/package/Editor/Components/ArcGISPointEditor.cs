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
using Esri.GameEngine.Geometry;
using UnityEditor;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Editor.Components
{
	[CustomPropertyDrawer(typeof(ArcGISPoint))]
	public class ArcGISPointEditor : PropertyDrawer 
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			var labelRect = new Rect(position.x, position.y, position.width, -2 + position.height / 5);
			EditorGUI.LabelField(labelRect, label);
			EditorGUI.indentLevel++;

			ArcGISPoint point = null;
			bool IsExtent = false;
			ArcGISMapComponent arcGISMap = null;
			try
			{
				// Extent works differently because it is a property embedded within a property so the reflection in GetValue won't find it on the targetObject
				// This could have implications for users who end up creating an ArcGISPoint within another property. To that I say tough luck.
				arcGISMap = property.serializedObject.targetObject as ArcGISMapComponent;
				point = fieldInfo.GetValue(arcGISMap.Extent) as ArcGISPoint;
				IsExtent = true;
			}
			catch
			{
			}

			if (point == null)
			{
				point = fieldInfo.GetValue(property.serializedObject.targetObject) as ArcGISPoint;
			}

			var xRect = new Rect(position.x, position.y + 1 * (position.height / 5), position.width, -2 + position.height / 5);
			var yRect = new Rect(position.x, position.y + 2 * (position.height / 5), position.width, -2 + position.height / 5);
			var zRect = new Rect(position.x, position.y + 3 * (position.height / 5) , position.width, -2 + position.height / 5);
			var srRect = new Rect(position.x, position.y + 4 * (position.height / 5) , position.width, -2 + position.height / 5);

			int wkid = 0;

			if (point == null)
			{
				point = new ArcGISPoint(0, 0, 0, new ArcGISSpatialReference(4326));
			}

			if (point.SpatialReference != null)
			{
				wkid = point.SpatialReference.WKID;
			}

			var xLabel = "X";
			var yLabel = "Y";
			var zLabel = "Z";
			var srLabel = "Spatial Reference WKID";

			if (wkid == SpatialReferenceWkid.WGS84 || wkid == SpatialReferenceWkid.CGCS2000)
			{
				xLabel = "Longitude";
				yLabel = "Latitude";
				zLabel = "Altitude";
			}

			var xProp = property.FindPropertyRelative("x");
			var yProp = property.FindPropertyRelative("y");
			var zProp = property.FindPropertyRelative("z");
			var srProp = property.FindPropertyRelative("SRWkid");

			EditorGUI.PropertyField(xRect, xProp, new GUIContent(xLabel));
			EditorGUI.PropertyField(yRect, yProp, new GUIContent(yLabel));
			EditorGUI.PropertyField(zRect, zProp, new GUIContent(zLabel));
			EditorGUI.PropertyField(srRect, srProp, new GUIContent(srLabel));

			var x = xProp.doubleValue;
			var y = yProp.doubleValue;
			var z = zProp.doubleValue;
			var srwkid = srProp.intValue;

			if (x != point.X || y != point.Y || z != point.Z || srwkid != wkid)
			{
				try
				{
					point = new ArcGISPoint(x, y, z, new ArcGISSpatialReference(srwkid));
					EditorUtility.SetDirty(property.serializedObject.targetObject);
				}
				catch
				{
				}
			}

			if (!IsExtent)
			{
				fieldInfo.SetValue(property.serializedObject.targetObject, point);
			}
			else
			{
				if (arcGISMap != null)
				{
					var extent = arcGISMap.Extent;
					extent.GeographicCenter = point;
					arcGISMap.Extent = extent;
				}
			}

			EditorGUI.indentLevel--;
			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return 100f;
		}
	}
}
