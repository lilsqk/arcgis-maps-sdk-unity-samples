using System.Collections.Generic;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Renderer.SceneComponents
{
	internal interface ISceneComponentProvider
	{
		public IEnumerable<ISceneComponent> TerrainMaskingMeshes { get; }

		public IReadOnlyDictionary<uint, ISceneComponent> SceneComponents { get; }

		public ISceneComponent CreateSceneComponent(uint id, uint layerId);
		public void DestroySceneComponent(uint id);

		public ISceneComponent GetSceneComponentFrom(GameObject gameObject);
	}
}
