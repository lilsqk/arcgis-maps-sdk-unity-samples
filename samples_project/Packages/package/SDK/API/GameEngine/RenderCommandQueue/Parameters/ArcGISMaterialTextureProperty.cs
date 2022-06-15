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
    /// Material parameter textures
    /// </summary>
    /// <since>1.0.0</since>
    public enum ArcGISMaterialTextureProperty
    {
        /// <summary>
        /// Base map
        /// </summary>
        /// <since>1.0.0</since>
        BaseMap = 0,
        
        /// <summary>
        /// Uv region lut
        /// </summary>
        /// <since>1.0.0</since>
        UvRegionLut = 1,
        
        /// <summary>
        /// Positions map 0
        /// </summary>
        /// <since>1.0.0</since>
        PositionsMap0 = 2,
        
        /// <summary>
        /// Positions map 1
        /// </summary>
        /// <since>1.0.0</since>
        PositionsMap1 = 3,
        
        /// <summary>
        /// Feature IDs.
        /// </summary>
        /// <remarks>
        /// Present on scene node meshes with feature data.
        /// The feature id for a given feature index (see <see cref="GameEngine.RenderCommandQueue.Parameters.ArcGISSetMeshCommandParameters.FeatureIndices">ArcGISSetMeshCommandParameters.FeatureIndices</see>) is stored at:
        /// x = feature_index % (tex_width / 2)
        /// y = floor(feature_index / (tex_width / 2))
        /// </remarks>
        /// <since>1.0.0</since>
        FeatureIds = 4
    };
}