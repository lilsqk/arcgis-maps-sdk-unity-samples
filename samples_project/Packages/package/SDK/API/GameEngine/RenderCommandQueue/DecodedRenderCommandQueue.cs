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
using Esri.GameEngine.RenderCommandQueue.Parameters;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Esri.GameEngine.RenderCommandQueue
{
	internal class DecodedRenderCommandQueue
	{
		private readonly Unity.ArcGISDataBuffer<byte> rawRenderCommands;
		private ulong currentOffset = 0;

		public DecodedRenderCommandQueue(Unity.ArcGISDataBuffer<byte> rawRenderCommands)
		{
			this.rawRenderCommands = rawRenderCommands;
		}

		public RenderCommand GetNextCommand()
		{
			if (currentOffset < rawRenderCommands.SizeBytes)
			{
				var commandType = GetCommandType(rawRenderCommands);

				switch (commandType)
				{
					case ArcGISRenderCommandType.CreateMaterial:
						{
							var parameters = GetCommandParams<ArcGISCreateMaterialCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.CreateMaterial, parameters);
						}
					case ArcGISRenderCommandType.CreateRenderTarget:
						{
							var parameters = GetCommandParams<ArcGISCreateRenderTargetCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.CreateRenderTarget, parameters);
						}
					case ArcGISRenderCommandType.CreateTexture:
						{
							var parameters = GetCommandParams<ArcGISCreateTextureCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.CreateTexture, parameters);
						}
					case ArcGISRenderCommandType.CreateSceneComponent:
						{
							var parameters = GetCommandParams<ArcGISCreateSceneComponentCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.CreateSceneComponent, parameters);
						}
					case ArcGISRenderCommandType.DestroyMaterial:
						{
							var parameters = GetCommandParams<ArcGISDestroyMaterialCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.DestroyMaterial, parameters);
						}
					case ArcGISRenderCommandType.DestroyRenderTarget:
						{
							var parameters = GetCommandParams<ArcGISDestroyRenderTargetCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.DestroyRenderTarget, parameters);
						}
					case ArcGISRenderCommandType.DestroyTexture:
						{
							var parameters = GetCommandParams<ArcGISDestroyTextureCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.DestroyTexture, parameters);
						}
					case ArcGISRenderCommandType.DestroySceneComponent:
						{
							var parameters = GetCommandParams<ArcGISDestroySceneComponentCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.DestroySceneComponent, parameters);
						}
					case ArcGISRenderCommandType.MultipleCompose:
						{
							var parameters = GetCommandParams<ArcGISMultipleComposeCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.MultipleCompose, parameters);
						}
					case ArcGISRenderCommandType.Compose:
						{
							var parameters = GetCommandParams<ArcGISComposeCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.Compose, parameters);
						}
					case ArcGISRenderCommandType.Copy:
						{
							var parameters = GetCommandParams<ArcGISCopyCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.Copy, parameters);
						}
					case ArcGISRenderCommandType.GenerateNormalTexture:
						{
							var parameters = GetCommandParams<ArcGISGenerateNormalTextureCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.GenerateNormalTexture, parameters);
						}
					case ArcGISRenderCommandType.SetTexturePixelData:
						{
							var parameters = GetCommandParams<ArcGISSetTexturePixelDataCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.SetTexturePixelData, parameters);
						}
					case ArcGISRenderCommandType.SetMaterialScalarProperty:
						{
							var parameters = GetCommandParams<ArcGISSetMaterialScalarPropertyCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.SetMaterialScalarProperty, parameters);
						}
					case ArcGISRenderCommandType.SetMaterialVectorProperty:
						{
							var parameters = GetCommandParams<ArcGISSetMaterialVectorPropertyCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.SetMaterialVectorProperty, parameters);
						}
					case ArcGISRenderCommandType.SetMaterialRenderTargetProperty:
						{
							var parameters = GetCommandParams<ArcGISSetMaterialRenderTargetPropertyCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.SetMaterialRenderTargetProperty, parameters);
						}
					case ArcGISRenderCommandType.SetMaterialTextureProperty:
						{
							var parameters = GetCommandParams<ArcGISSetMaterialTexturePropertyCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.SetMaterialTextureProperty, parameters);
						}
					case ArcGISRenderCommandType.SetMaterialNamedTextureProperty:
						{
							var parameters = GetCommandParams<ArcGISSetMaterialNamedTexturePropertyCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.SetMaterialNamedTextureProperty, parameters);
						}
					case ArcGISRenderCommandType.GenerateMipMaps:
						{
							var parameters = GetCommandParams<ArcGISGenerateMipMapsCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.GenerateMipMaps, parameters);
						}
					case ArcGISRenderCommandType.SetVisible:
						{
							var parameters = GetCommandParams<ArcGISSetVisibleCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.SetVisible, parameters);
						}
					case ArcGISRenderCommandType.SetMaterial:
						{
							var parameters = GetCommandParams<ArcGISSetMaterialCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.SetMaterial, parameters);
						}
					case ArcGISRenderCommandType.SetMesh:
						{
							var parameters = GetCommandParams<ArcGISSetMeshCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.SetMesh, parameters);
						}
					case ArcGISRenderCommandType.SetMeshBuffer:
						{
							var parameters = GetCommandParams<ArcGISSetMeshBufferCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.SetMeshBuffer, parameters);
						}
					case ArcGISRenderCommandType.SetSceneComponentPivot:
						{
							var parameters = GetCommandParams<ArcGISSetSceneComponentPivotCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.SetSceneComponentPivot, parameters);
						}
					case ArcGISRenderCommandType.CommandGroupBegin:
						{
							var parameters = GetCommandParams<ArcGISNullCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.CommandGroupBegin, parameters);
						}
					case ArcGISRenderCommandType.CommandGroupEnd:
						{
							var parameters = GetCommandParams<ArcGISNullCommandParameters>(rawRenderCommands);
							return new RenderCommand(ArcGISRenderCommandType.CommandGroupEnd, parameters);
						}
					default:
						Debug.Fail("Cannot decode unknown renderCommand type {commandType}");
						break;
				}
			}

			return null;
		}

		private ArcGISRenderCommandType GetCommandType(Unity.ArcGISDataBuffer<byte> dataBuffer)
		{
			ArcGISRenderCommandType commandType;
			System.IntPtr unmanagedElement = new System.IntPtr(dataBuffer.Data.ToInt64() + (long)currentOffset);

			unsafe
			{
				commandType = *((ArcGISRenderCommandType*)unmanagedElement.ToPointer());
			}

			var typeSize = (ulong)sizeof(ArcGISRenderCommandType);
			currentOffset += typeSize;

			return commandType;
		}

		private T GetCommandParams<T>(Unity.ArcGISDataBuffer<byte> dataBuffer)
		{
			var typeSize = (ulong)Marshal.SizeOf(typeof(T));

			System.IntPtr unmanagedElement = new System.IntPtr(dataBuffer.Data.ToInt64() + (long)currentOffset);
			var commandParameters = Marshal.PtrToStructure<T>(unmanagedElement);

			currentOffset += typeSize;

			return commandParameters;
		}
	}
}
