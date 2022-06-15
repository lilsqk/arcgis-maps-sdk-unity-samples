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
namespace Esri.GameEngine.RenderCommandQueue.Parameters
{
    /// <summary>
    /// Mesh buffer change type
    /// </summary>
    /// <since>1.0.0</since>
    public enum ArcGISMeshBufferChangeType
    {
        /// <summary>
        /// Vertex positions
        /// </summary>
        /// <since>1.0.0</since>
        Positions = 0,
        
        /// <summary>
        /// Vertex normals
        /// </summary>
        /// <since>1.0.0</since>
        Normals = 1,
        
        /// <summary>
        /// Vertex tangents
        /// </summary>
        /// <since>1.0.0</since>
        Tangents = 2,
        
        /// <summary>
        /// Vertex colors
        /// </summary>
        /// <since>1.0.0</since>
        Colors = 3,
        
        /// <summary>
        /// Vertex uvs, channel 0
        /// </summary>
        /// <since>1.0.0</since>
        Uv0 = 4,
        
        /// <summary>
        /// Vertex uvs, channel 1
        /// </summary>
        /// <since>1.0.0</since>
        Uv1 = 5,
        
        /// <summary>
        /// Vertex uvs, channel 2
        /// </summary>
        /// <since>1.0.0</since>
        Uv2 = 6,
        
        /// <summary>
        /// Vertex uvs, channel 3
        /// </summary>
        /// <since>1.0.0</since>
        Uv3 = 7
    };
}