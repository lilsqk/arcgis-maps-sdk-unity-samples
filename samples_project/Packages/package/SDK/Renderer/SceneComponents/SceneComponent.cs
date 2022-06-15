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
using Esri.HPFramework;
using Unity.Mathematics;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Renderer.SceneComponents
{
	internal class SceneComponent : ISceneComponent
	{
		public GameObject SceneComponentGameObject { get; }

		public IGPUResourceMaterial Material
		{
			get
			{
				var material = SceneComponentGameObject.GetComponent<MeshRenderer>().material;

				if (material != null)
				{
					return new GPUResourceMaterial(material);
				}

				return null;
			}

			set
			{
				SceneComponentGameObject.GetComponent<MeshRenderer>().material = value.NativeMaterial;
			}
		}

		public IGPUResourceMesh Mesh
		{
			get
			{
				var mesh = SceneComponentGameObject.GetComponent<MeshFilter>().sharedMesh;

				if (mesh != null)
				{
					return new GPUResourceMesh(mesh);
				}

				return null;
			}

			set
			{
				var meshFilter = SceneComponentGameObject.GetComponent<MeshFilter>();
				var collisionComponent = SceneComponentGameObject.GetComponent<MeshCollider>();

				var mesh = value != null ? value.NativeMesh : null;

				meshFilter.sharedMesh = mesh;
				collisionComponent.sharedMesh = collisionComponent.enabled ? mesh : null;
			}
		}

		public double3 Location
		{
			set
			{
				var hpTransform = SceneComponentGameObject.GetComponent<HPTransform>();

				hpTransform.UniversePosition = value;
				hpTransform.UniverseRotation = Quaternion.identity;
			}
		}

		public string Name
		{
			get
			{
				return SceneComponentGameObject.name;
			}

			set
			{
				SceneComponentGameObject.name = value;
			}
		}

		public bool IsVisible
		{
			get
			{
				return SceneComponentGameObject.activeInHierarchy;
			}

			set
			{
				SceneComponentGameObject.SetActive(value);
			}
		}

		public bool IsMeshColliderEnabled
		{
			get
			{
				return SceneComponentGameObject.GetComponent<MeshCollider>();
			}

			set
			{
				var component = SceneComponentGameObject.GetComponent<MeshCollider>();
				component.enabled = value;

				if (value)
				{
					component.sharedMesh = SceneComponentGameObject.GetComponent<MeshFilter>().sharedMesh;
				}
			}
		}

		public uint LayerId { get; set; } = 0;

		public OrientedBoundingBox OrientedBoundingBox { get; set; }
		public bool MaskTerrain { get; set; }

		public SceneComponent(GameObject gameObject)
		{
			SceneComponentGameObject = gameObject;
		}

		public void Destroy()
		{
			if (Application.isEditor)
			{
				GameObject.DestroyImmediate(SceneComponentGameObject);
			}
			else
			{
				GameObject.Destroy(SceneComponentGameObject);
			}
		}
	}
}
