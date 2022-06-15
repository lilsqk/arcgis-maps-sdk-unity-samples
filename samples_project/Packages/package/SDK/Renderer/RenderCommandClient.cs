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
using Esri.ArcGISMapsSDK.Renderer.GPUComputing;
using Esri.ArcGISMapsSDK.Renderer.GPUResources;
using Esri.ArcGISMapsSDK.Renderer.SceneComponents;
using Esri.GameEngine.RenderCommandQueue;
using Esri.GameEngine.RenderCommandQueue.Parameters;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Mathematics;

namespace Esri.ArcGISMapsSDK.Renderer
{
	internal class RenderCommandClient : IRenderCommandClient
	{
		private static readonly int baseTextureUVIndex = 0;
		private static readonly int featureIndicesUVIndex = 1;
		private static readonly int uvRegionIdsUVIndex = 2;

		private readonly IGPUResourcesProvider gpuResourceProvider;
		private readonly ISceneComponentProvider sceneComponentProvider;
		private readonly INormalMapGenerator normalMapGenerator;
		private readonly IImageComposer imageComposer;

		private IGPUResourceTexture2D flatElevationTexture;

		public RenderCommandClient(IGPUResourcesProvider gpuResourceProvider, ISceneComponentProvider sceneComponentProvider, IImageComposer imageComposer, INormalMapGenerator normalMapGenerator)
		{
			this.gpuResourceProvider = gpuResourceProvider;
			this.sceneComponentProvider = sceneComponentProvider;
			this.normalMapGenerator = normalMapGenerator;
			this.imageComposer = imageComposer;
			flatElevationTexture = CreateFlatElevationTexture();
		}

		private static IGPUResourceTexture2D CreateFlatElevationTexture()
		{
			// Create flat elevation texture
			var nativeTexture = new UnityEngine.Texture2D(1, 1, UnityEngine.TextureFormat.RFloat, false, true);
			nativeTexture.wrapMode = UnityEngine.TextureWrapMode.Clamp;
			nativeTexture.filterMode = UnityEngine.FilterMode.Bilinear;
			var texture = new GPUResourceTexture2D(nativeTexture);
			texture.NativeTexture.SetPixelData(new float[] { 0.0f }, 0);
			return texture;
		}

		public void ExecuteRenderCommand(RenderCommand renderCommand)
		{
			switch (renderCommand.Type)
			{
				// Creation Commands
				case ArcGISRenderCommandType.CreateMaterial:
					{
						var parameters = (ArcGISCreateMaterialCommandParameters)renderCommand.CommandParameters;

						var material = Unity.Convert.FromArcGISMaterialReference(parameters.Material);
						gpuResourceProvider.CreateMaterial(parameters.MaterialId, parameters.MaterialType, material);
					}
					break;

				case ArcGISRenderCommandType.CreateRenderTarget:
					{
						var parameters = (ArcGISCreateRenderTargetCommandParameters)renderCommand.CommandParameters;
						gpuResourceProvider.CreateRenderTexture(parameters.RenderTargetId, parameters.Width, parameters.Height, parameters.TextureFormat, parameters.HasMipMaps);
					}
					break;

				case ArcGISRenderCommandType.CreateSceneComponent:
					{
						var parameters = (ArcGISCreateSceneComponentCommandParameters)renderCommand.CommandParameters;
						sceneComponentProvider.CreateSceneComponent(parameters.SceneComponentId, parameters.LayerId);
					}
					break;

				case ArcGISRenderCommandType.CreateTexture:
					{
						var parameters = (ArcGISCreateTextureCommandParameters)renderCommand.CommandParameters;
						gpuResourceProvider.CreateTexture(parameters.TextureId, parameters.Width, parameters.Height, parameters.TextureFormat, parameters.IsSRGB);
					}
					break;

				// Destruction Commands
				case ArcGISRenderCommandType.DestroyMaterial:
					{
						var parameters = (ArcGISDestroyMaterialCommandParameters)renderCommand.CommandParameters;

						gpuResourceProvider.DestroyMaterial(parameters.MaterialId);
					}
					break;

				case ArcGISRenderCommandType.DestroyRenderTarget:
					{
						var parameters = (ArcGISDestroyRenderTargetCommandParameters)renderCommand.CommandParameters;

						gpuResourceProvider.DestroyRenderTexture(parameters.RenderTargetId);
					}
					break;

				case ArcGISRenderCommandType.DestroySceneComponent:
					{
						var parameters = (ArcGISDestroySceneComponentCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(sceneComponentProvider.SceneComponents.ContainsKey(parameters.SceneComponentId));

						sceneComponentProvider.DestroySceneComponent(parameters.SceneComponentId);
						gpuResourceProvider.DestroyMesh(parameters.SceneComponentId);
					}
					break;

				case ArcGISRenderCommandType.DestroyTexture:
					{
						var parameters = (ArcGISDestroyTextureCommandParameters)renderCommand.CommandParameters;

						gpuResourceProvider.DestroyTexture(parameters.TextureId);
					}
					break;

				// SceneComponent operations
				case ArcGISRenderCommandType.SetVisible:
					{
						var parameters = (ArcGISSetVisibleCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(sceneComponentProvider.SceneComponents.ContainsKey(parameters.SceneComponentId));

						sceneComponentProvider.SceneComponents[parameters.SceneComponentId].IsVisible = parameters.IsVisible;
					}
					break;

				case ArcGISRenderCommandType.SetMaterial:
					{
						var parameters = (ArcGISSetMaterialCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(sceneComponentProvider.SceneComponents.ContainsKey(parameters.SceneComponentId) && gpuResourceProvider.Materials.ContainsKey(parameters.MaterialId));

						var sceneComponent = sceneComponentProvider.SceneComponents[parameters.SceneComponentId];

						sceneComponent.Material = gpuResourceProvider.Materials[parameters.MaterialId];

						sceneComponent.MaskTerrain = parameters.MaskTerrain;

						sceneComponent.OrientedBoundingBox =
							new OrientedBoundingBox(
								math.double3(parameters.OrientedBoundingBox.CenterX, parameters.OrientedBoundingBox.CenterY, parameters.OrientedBoundingBox.CenterZ),
								math.double3(parameters.OrientedBoundingBox.ExtentX, parameters.OrientedBoundingBox.ExtentY, parameters.OrientedBoundingBox.ExtentZ),
								math.double4(parameters.OrientedBoundingBox.OrientationX, parameters.OrientedBoundingBox.OrientationY, parameters.OrientedBoundingBox.OrientationZ, parameters.OrientedBoundingBox.OrientationW));
					}
					break;

				case ArcGISRenderCommandType.SetMesh:
					{
						var parameters = (ArcGISSetMeshCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(sceneComponentProvider.SceneComponents.ContainsKey(parameters.SceneComponentId));

						var sceneComponent = sceneComponentProvider.SceneComponents[parameters.SceneComponentId];

						var mesh = sceneComponent.Mesh;

						if (mesh == null)
						{
							mesh = sceneComponent.Mesh = gpuResourceProvider.CreateMesh(parameters.SceneComponentId);
							mesh.MarkDynamic();
						}

						mesh.Clear();

						mesh.SetVertices(parameters.Positions.ToNativeArray<UnityEngine.Vector3>());
						mesh.SetNormals(parameters.Normals.ToNativeArray<UnityEngine.Vector3>());
						mesh.SetTangents(parameters.Tangents.ToNativeArray<UnityEngine.Vector4>());
						mesh.SetColors(parameters.Colors.ToNativeArray<UnityEngine.Color32>());
						mesh.SetUVs(baseTextureUVIndex, parameters.Uvs.ToNativeArray<UnityEngine.Vector2>());
						mesh.SetUVs(featureIndicesUVIndex, parameters.FeatureIndices.ToNativeArray<float>());
						mesh.SetUVs(uvRegionIdsUVIndex, parameters.UvRegionIds.ToNativeArray<float>());
						mesh.SetTriangles(parameters.Triangles.ToNativeArray<int>());

						mesh.RecalculateBounds();
					}
					break;

				case ArcGISRenderCommandType.SetMeshBuffer:
					{
						var parameters = (ArcGISSetMeshBufferCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(sceneComponentProvider.SceneComponents.ContainsKey(parameters.SceneComponentId));

						var sceneComponent = sceneComponentProvider.SceneComponents[parameters.SceneComponentId];
						var mesh = sceneComponent.Mesh;

						switch (parameters.MeshBufferChangeType)
						{
							case ArcGISMeshBufferChangeType.Positions:
								mesh.SetVertices(parameters.Buffer.ToNativeArray<UnityEngine.Vector3>());
								break;

							case ArcGISMeshBufferChangeType.Normals:
								mesh.SetNormals(parameters.Buffer.ToNativeArray<UnityEngine.Vector3>());
								break;

							case ArcGISMeshBufferChangeType.Uv0:
								mesh.SetUVs(baseTextureUVIndex, parameters.Buffer.ToNativeArray<UnityEngine.Vector2>());
								break;

							case ArcGISMeshBufferChangeType.Colors:
								mesh.SetColors(parameters.Buffer.ToNativeArray<UnityEngine.Color32>());
								break;

							case ArcGISMeshBufferChangeType.Tangents:
								mesh.SetTangents(parameters.Buffer.ToNativeArray<UnityEngine.Vector4>());
								break;

						}
					}
					break;

				case ArcGISRenderCommandType.SetSceneComponentPivot:
					{
						var parameters = (ArcGISSetSceneComponentPivotCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(sceneComponentProvider.SceneComponents.ContainsKey(parameters.SceneComponentId));

						sceneComponentProvider.SceneComponents[parameters.SceneComponentId].Location = math.double3(parameters.X, parameters.Y, parameters.Z);
					}
					break;

				// Texture and RenderTexture Operations
				case ArcGISRenderCommandType.SetTexturePixelData:
					{
						var parameters = (ArcGISSetTexturePixelDataCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(gpuResourceProvider.Textures.ContainsKey(parameters.TextureId));

						gpuResourceProvider.Textures[parameters.TextureId].SetPixelData(parameters.Pixels.Data, parameters.Pixels.Size);
					}
					break;

				case ArcGISRenderCommandType.GenerateMipMaps:
					{
						var parameters = (ArcGISGenerateMipMapsCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(gpuResourceProvider.RenderTextures.ContainsKey(parameters.RenderTargetId));

						gpuResourceProvider.RenderTextures[parameters.RenderTargetId].GenerateMipMaps();
					}
					break;

				// Material Operations
				case ArcGISRenderCommandType.SetMaterialRenderTargetProperty:
					{
						var parameters = (ArcGISSetMaterialRenderTargetPropertyCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(gpuResourceProvider.Materials.ContainsKey(parameters.MaterialId) && gpuResourceProvider.RenderTextures.ContainsKey(parameters.Value));

						var shaderParam = getRenderTextureMaterialShaderParameterName(parameters.MaterialRenderTargetProperty);
						gpuResourceProvider.Materials[parameters.MaterialId].SetTexture(shaderParam, gpuResourceProvider.RenderTextures[parameters.Value]);
					}
					break;

				case ArcGISRenderCommandType.SetMaterialScalarProperty:
					{
						var parameters = (ArcGISSetMaterialScalarPropertyCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(gpuResourceProvider.Materials.ContainsKey(parameters.MaterialId));

						var shaderParam = getScalarMaterialShaderParameterName(parameters.MaterialScalarProperty);
						gpuResourceProvider.Materials[parameters.MaterialId].SetFloat(shaderParam, parameters.Value);
					}
					break;

				case ArcGISRenderCommandType.SetMaterialTextureProperty:
					{
						var parameters = (ArcGISSetMaterialTexturePropertyCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(gpuResourceProvider.Materials.ContainsKey(parameters.MaterialId) && gpuResourceProvider.Textures.ContainsKey(parameters.Value));

						var shaderParam = getTextureMaterialShaderParameterName(parameters.MaterialTextureProperty);
						gpuResourceProvider.Materials[parameters.MaterialId].SetTexture(shaderParam, gpuResourceProvider.Textures[parameters.Value]);
					}
					break;

				case ArcGISRenderCommandType.SetMaterialNamedTextureProperty:
					{
						var parameters = (ArcGISSetMaterialNamedTexturePropertyCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(gpuResourceProvider.Materials.ContainsKey(parameters.MaterialId) && gpuResourceProvider.Textures.ContainsKey(parameters.Value));

						var textureName = Marshal.PtrToStringAnsi(parameters.TextureName.Data);

						gpuResourceProvider.Materials[parameters.MaterialId].SetTexture(textureName, gpuResourceProvider.Textures[parameters.Value]);
					}
					break;

				case ArcGISRenderCommandType.SetMaterialVectorProperty:
					{
						var parameters = (ArcGISSetMaterialVectorPropertyCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(gpuResourceProvider.Materials.ContainsKey(parameters.MaterialId));

						UnityEngine.Vector4 value;
						value.x = parameters.Value.X;
						value.y = parameters.Value.Y;
						value.z = parameters.Value.Z;
						value.w = parameters.Value.W;

						var shaderParam = getVectorMaterialShaderParameterName(parameters.MaterialVectorProperty);
						gpuResourceProvider.Materials[parameters.MaterialId].SetVector(shaderParam, value);
					}
					break;

				// Compose and normal operations
				case ArcGISRenderCommandType.Compose:
					{
						// TODO: Implement when blending is added
					}
					break;

				case ArcGISRenderCommandType.MultipleCompose:
					{
						var parameters = (ArcGISMultipleComposeCommandParameters)renderCommand.CommandParameters;

						Debug.Assert(gpuResourceProvider.RenderTextures.ContainsKey(parameters.TargetId));

						var composables = parameters.ComposedTextures.ToArray<ArcGISComposedTextureElement>();
						ComposableImage[] blenderInputArray = new ComposableImage[composables.Length];
						int pos = 0;

						foreach (var composable in composables)
						{
							Debug.Assert(gpuResourceProvider.Textures.ContainsKey(composable.TextureId));

							ComposableImage blenderInput;

							blenderInput.opacity = composable.Opacity;
							blenderInput.image = gpuResourceProvider.Textures[composable.TextureId];
							blenderInput.extent = new UnityEngine.Vector4(composable.Region.X, composable.Region.Y, composable.Region.Z, composable.Region.W);

							blenderInputArray[pos++] = blenderInput;
						}

						imageComposer.Compose(blenderInputArray, gpuResourceProvider.RenderTextures[parameters.TargetId]);
					}
					break;

				case ArcGISRenderCommandType.Copy:
					{
						// TODO: Implement when blending is added
					}
					break;

				case ArcGISRenderCommandType.GenerateNormalTexture:
					{
						var parameters = (ArcGISGenerateNormalTextureCommandParameters)renderCommand.CommandParameters;

						Debug.Assert((parameters.ElevationId == 0 || gpuResourceProvider.Textures.ContainsKey(parameters.ElevationId)) && gpuResourceProvider.RenderTextures.ContainsKey(parameters.TargetId));

						var elevationTexture = parameters.ElevationId == 0 ? flatElevationTexture : gpuResourceProvider.Textures[parameters.ElevationId];

						var outputTexture = gpuResourceProvider.RenderTextures[parameters.TargetId];
						if ((elevationTexture.Height != 1 && elevationTexture.Width != 1) && (elevationTexture.Height != outputTexture.Height + 1 || elevationTexture.Width != outputTexture.Height + 1))
						{
							UnityEngine.Debug.Log("Normal map generator expects input elevation texture to be either 1 texel in size, or 1 texel wider and taller than the output normal map.");
						}

						normalMapGenerator.Compute(elevationTexture, new UnityEngine.Vector4(parameters.TileExtent.X, parameters.TileExtent.Y, parameters.TileExtent.Z, parameters.TileExtent.W),
												new UnityEngine.Vector4(parameters.TextureExtent.X, parameters.TextureExtent.Y, parameters.TextureExtent.Z, parameters.TextureExtent.W), outputTexture);
					}
					break;

				case ArcGISRenderCommandType.CommandGroupBegin:
				case ArcGISRenderCommandType.CommandGroupEnd:
					break;

				default:

					Debug.Fail("Unknown RenderCommand Type!");

					break;
			}
		}

		private static string getRenderTextureMaterialShaderParameterName(ArcGISMaterialRenderTargetProperty parameter)
		{
			switch (parameter)
			{
				case ArcGISMaterialRenderTargetProperty.ImageryMap0:
					return "_MainTex";
				case ArcGISMaterialRenderTargetProperty.NormalMap0:
					return "_BumpMap";
				default:
					Debug.Fail("Not implemented ArcGISMaterialRenderTargetProperty!");
					return "";
			}
		}

		private static string getTextureMaterialShaderParameterName(ArcGISMaterialTextureProperty parameter)
		{
			switch (parameter)
			{
				case ArcGISMaterialTextureProperty.BaseMap:
					return "_MainTex";
				case ArcGISMaterialTextureProperty.UvRegionLut:
					return "_UVRegionLUT";
				case ArcGISMaterialTextureProperty.FeatureIds:
					return "_FeatureIds";
				case ArcGISMaterialTextureProperty.PositionsMap0:
					return "_VertexOffset";
				default:
					Debug.Fail("Not implemented ArcGISMaterialTextureProperty!");
					return "";
			}
		}

		private static string getScalarMaterialShaderParameterName(ArcGISMaterialScalarProperty parameter)
		{
			switch (parameter)
			{
				case ArcGISMaterialScalarProperty.ClippingMode:
					return "_ClippingMode";
				case ArcGISMaterialScalarProperty.UseUvRegionLut:
					return "_UseUvRegionLut";
				case ArcGISMaterialScalarProperty.Opacity:
					return "_Opacity";
				default:
					Debug.Fail($"Not implemented ArcGISMaterialScalarProperty: {parameter}!");
					return "";
			}
		}

		private static string getVectorMaterialShaderParameterName(ArcGISMaterialVectorProperty parameter)
		{
			switch (parameter)
			{
				case ArcGISMaterialVectorProperty.MapAreaMax:
					return "_MapAreaMax";
				case ArcGISMaterialVectorProperty.MapAreaMin:
					return "_MapAreaMin";
				default:
					Debug.Fail("Not implemented ArcGISMaterialVectorProperty!");
					return "";
			}
		}
	}
}
