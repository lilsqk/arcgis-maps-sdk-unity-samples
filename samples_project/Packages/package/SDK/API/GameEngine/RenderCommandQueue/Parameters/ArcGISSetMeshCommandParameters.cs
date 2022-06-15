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
    /// A set mesh render command
    /// </summary>
    /// <since>1.0.0</since>
    [StructLayout(LayoutKind.Sequential)]
    internal struct ArcGISSetMeshCommandParameters
    {
        /// <summary>
        /// The scene component parameter of this render command
        /// </summary>
        /// <since>1.0.0</since>
        public uint SceneComponentId;
        
        /// <summary>
        /// The triangles parameter of this render command
        /// </summary>
        /// <since>1.0.0</since>
        public GameEngine.RenderCommandQueue.ArcGISDataBufferView Triangles;
        
        /// <summary>
        /// The positions parameter of this render command
        /// </summary>
        /// <since>1.0.0</since>
        public GameEngine.RenderCommandQueue.ArcGISDataBufferView Positions;
        
        /// <summary>
        /// The normals parameter of this render command
        /// </summary>
        /// <since>1.0.0</since>
        public GameEngine.RenderCommandQueue.ArcGISDataBufferView Normals;
        
        /// <summary>
        /// The tangents parameter of this render command
        /// </summary>
        /// <since>1.0.0</since>
        public GameEngine.RenderCommandQueue.ArcGISDataBufferView Tangents;
        
        /// <summary>
        /// The uvs parameter of this render command
        /// </summary>
        /// <since>1.0.0</since>
        public GameEngine.RenderCommandQueue.ArcGISDataBufferView Uvs;
        
        /// <summary>
        /// The colors parameter of this render command
        /// </summary>
        /// <since>1.0.0</since>
        public GameEngine.RenderCommandQueue.ArcGISDataBufferView Colors;
        
        /// <summary>
        /// The ID of the uv region parameter of this render command
        /// </summary>
        /// <since>1.0.0</since>
        public GameEngine.RenderCommandQueue.ArcGISDataBufferView UvRegionIds;
        
        /// <summary>
        /// The mesh's feature indices
        /// </summary>
        /// <remarks>
        /// A zero-based id that is unique for each feature contained in the scene node. 
        /// Used to look up feature ID in the <see cref="GameEngine.RenderCommandQueue.Parameters.ArcGISMaterialTextureProperty.FeatureIds">ArcGISMaterialTextureProperty.FeatureIds</see> texture.
        /// </remarks>
        /// <since>1.0.0</since>
        public GameEngine.RenderCommandQueue.ArcGISDataBufferView FeatureIndices;
    }
}