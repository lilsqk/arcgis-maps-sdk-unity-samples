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
    /// A set material render command
    /// </summary>
    /// <since>1.0.0</since>
    [StructLayout(LayoutKind.Sequential)]
    internal struct ArcGISSetMaterialCommandParameters
    {
        /// <summary>
        /// The scene component parameter of this render command
        /// </summary>
        /// <since>1.0.0</since>
        public uint SceneComponentId;
        
        /// <summary>
        /// The material parameter of this render command
        /// </summary>
        /// <since>1.0.0</since>
        public uint MaterialId;
        
        /// <summary>
        /// The oriented bounding box of the scene component
        /// </summary>
        /// <since>1.0.0</since>
        public GameEngine.Math.ArcGISOrientedBoundingBox OrientedBoundingBox;
        
        /// <summary>
        /// The boolean indicating whether this mesh should be used to mask terrain
        /// </summary>
        /// <since>1.0.0</since>
        [MarshalAs(UnmanagedType.I1)]
        public bool MaskTerrain;
    }
}