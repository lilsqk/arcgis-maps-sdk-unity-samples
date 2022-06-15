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
using System.Runtime.InteropServices;

namespace Esri.GameEngine.RenderCommandQueue.Parameters
{
    /// <summary>
    /// Parameters type for the <see cref="GameEngine.RenderCommandQueue.ArcGISRenderCommandType.SetMaterialNamedTextureProperty">ArcGISRenderCommandType.SetMaterialNamedTextureProperty</see> command
    /// </summary>
    /// <since>1.0.0</since>
    [StructLayout(LayoutKind.Sequential)]
    internal struct ArcGISSetMaterialNamedTexturePropertyCommandParameters
    {
        /// <summary>
        /// The material parameter of this render command
        /// </summary>
        /// <since>1.0.0</since>
        public uint MaterialId;
        
        /// <summary>
        /// The utf8 name of the texture
        /// </summary>
        /// <since>1.0.0</since>
        public GameEngine.RenderCommandQueue.ArcGISDataBufferView TextureName;
        
        /// <summary>
        /// The textureId of the texture to be assigned to the material
        /// </summary>
        /// <since>1.0.0</since>
        public uint Value;
    }
}