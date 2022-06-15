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
using Esri.ArcGISMapsSDK.Renderer.GPUResources;
using Unity.Mathematics;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Renderer.SceneComponents
{
	internal interface ISceneComponent
	{
		public GameObject SceneComponentGameObject { get; }

		public OrientedBoundingBox OrientedBoundingBox { get; set; }
		public bool MaskTerrain { get; set; }

		public IGPUResourceMaterial Material { get; set; }

		public IGPUResourceMesh Mesh { get; set; }

		public double3 Location { set; }

		public string Name { get; set; }

		public bool IsVisible { get; set; }

		public bool IsMeshColliderEnabled { get; set; }

		public uint LayerId { get; set; }

		public void Destroy();
	}
}
