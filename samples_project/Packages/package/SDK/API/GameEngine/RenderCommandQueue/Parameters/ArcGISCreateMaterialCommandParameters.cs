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
using System;

namespace Esri.GameEngine.RenderCommandQueue.Parameters
{
    /// <summary>
    /// A create material render command
    /// </summary>
    /// <since>1.0.0</since>
    [StructLayout(LayoutKind.Sequential)]
    internal struct ArcGISCreateMaterialCommandParameters
    {
        /// <summary>
        /// The id that will be use for the created material.
        /// </summary>
        /// <since>1.0.0</since>
        public uint MaterialId;
        
        /// <summary>
        /// The type parameter of the material
        /// </summary>
        /// <since>1.0.0</since>
        public ArcGISMaterialType MaterialType;
        
        /// <summary>
        /// The game engine material
        /// </summary>
        /// <since>1.0.0</since>
        public IntPtr Material;
    }
}