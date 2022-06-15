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
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.GameEngine.Geometry;
using UnityEditor;

namespace Esri.ArcGISMapsSDK.Editor.Components
{
	[CustomEditor(typeof(ArcGISLocationComponent))]
	public class ArcGISLocationComponentEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var arcGISLocationComponent = target as ArcGISLocationComponent;

			if (arcGISLocationComponent.enabled)
			{
				var position = arcGISLocationComponent.Position;
				var rotation = arcGISLocationComponent.Rotation;

				if (Draw(ref position, ref rotation))
				{
					arcGISLocationComponent.Position = position;
					arcGISLocationComponent.Rotation = rotation;

					EditorUtility.SetDirty(arcGISLocationComponent);
				}
			}
		}

		private static bool Draw(ref ArcGISPoint position, ref ArcGISRotation rotation)
		{
			bool result = false;

			var oldPosition = position;
			position = EditorUtilities.ArcGISPointField("Position", position, false);

			if (!oldPosition.IsValid || oldPosition.X != position.X || oldPosition.Y != position.Y || oldPosition.Z != position.Z || oldPosition.SpatialReference.WKID != position.SpatialReference.WKID)
			{
				result = true;
			}

			var oldRotation = rotation;
			rotation = EditorUtilities.ArcGISRotationField("Rotation", rotation);

			if (oldRotation != rotation)
			{
				result = true;
			}

			return result;
		}
	}
}
