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
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Esri.GameEngine.Geometry
{
	[Serializable]
	public partial class ArcGISPoint : ISerializationCallbackReceiver
	{
		[FormerlySerializedAs("X")]
		[SerializeField]
		private double x;

		[FormerlySerializedAs("Y")]
		[SerializeField]
		private double y;

		[FormerlySerializedAs("Z")]
		[SerializeField]
		private double z;

		[SerializeField]
		private int SRWkid;

		public void OnBeforeSerialize()
		{
			try
			{
				x = X;
				y = Y;
				z = Z;
				SRWkid = SpatialReference.WKID;
			}
			catch
			{
				x = 0;
				y = 0;
				z = 0;
				SRWkid = 4326;
			}
		}

		public void OnAfterDeserialize()
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			ArcGISSpatialReference spatialReference;
			try
			{
				spatialReference = new ArcGISSpatialReference(SRWkid);
			}
			catch
			{
				spatialReference = new ArcGISSpatialReference(4326);
			}

			var localSpatialReference = spatialReference == null ? System.IntPtr.Zero : spatialReference.Handle;

			Handle = PInvoke.RT_Point_createWithXYZSpatialReference(x, y, z, localSpatialReference, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);
		}
	}
}

