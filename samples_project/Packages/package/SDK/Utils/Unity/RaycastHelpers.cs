using Esri.GameEngine.Layers.Base;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Physics
{
	public struct ArcGISRaycastHit
	{
		public int featureId;
		public ArcGISLayer layer;

		public ArcGISRaycastHit(int featureId, ArcGISLayer layer)
		{
			this.featureId = featureId;
			this.layer = layer;
		}
	};

	public static class RaycastHelpers
    {
		static internal int GetFeatureIndexByTriangleIndex(GameObject gameObject, int primitiveIndex)
		{
			if (gameObject.GetComponent<MeshFilter>())
			{
				var mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;

				if (primitiveIndex < mesh.triangles.Length)
				{
					var vertexIndex = mesh.triangles[3 * primitiveIndex];

					if (vertexIndex < mesh.uv2.Length)
					{
						return (int)mesh.uv2[vertexIndex].x;
					}
				}
			}

			return -1;
		}
    }
}
