using Esri.GameEngine.RenderCommandQueue.Parameters;
using System.Collections.Generic;
using UnityEngine;

namespace Esri.ArcGISMapsSDK.Renderer.GPUResources
{
	internal interface IGPUResourcesProvider
	{
		public IReadOnlyDictionary<uint, IGPUResourceMaterial> Materials { get; }
		public IReadOnlyDictionary<uint, IGPUResourceMesh> Meshes { get; }
		public IReadOnlyDictionary<uint, IGPUResourceTexture2D> Textures { get; }
		public IReadOnlyDictionary<uint, IGPUResourceRenderTexture> RenderTextures { get; }

		public IGPUResourceMaterial CreateMaterial(uint id, ArcGISMaterialType materialType, Material customMaterial);
		public IGPUResourceMesh CreateMesh(uint id);
		public IGPUResourceRenderTexture CreateRenderTexture(uint id, uint width, uint height, Esri.GameEngine.RenderCommandQueue.Parameters.ArcGISTextureFormat format, bool useMipMaps);
		public IGPUResourceTexture2D CreateTexture(uint id, uint width, uint height, Esri.GameEngine.RenderCommandQueue.Parameters.ArcGISTextureFormat format, bool isSRGB);
		public void DestroyMaterial(uint id);
		public void DestroyMesh(uint id);
		public void DestroyTexture(uint id);
		public void DestroyRenderTexture(uint id);
		public void Release();
	}
}
