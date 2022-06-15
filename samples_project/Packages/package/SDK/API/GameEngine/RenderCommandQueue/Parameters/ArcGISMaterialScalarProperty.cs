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
    /// Material parameter scalars
    /// </summary>
    /// <since>1.0.0</since>
    public enum ArcGISMaterialScalarProperty
    {
        /// <summary>
        /// Clipping mode
        /// </summary>
        /// <since>1.0.0</since>
        ClippingMode = 0,
        
        /// <summary>
        /// Use uv region lut
        /// </summary>
        /// <since>1.0.0</since>
        UseUvRegionLut = 1,
        
        /// <summary>
        /// Blend factor
        /// </summary>
        /// <since>1.0.0</since>
        BlendFactor = 2,
        
        /// <summary>
        /// Positions blend factor
        /// </summary>
        /// <since>1.0.0</since>
        PositionsBlendFactor = 3,
        
        /// <summary>
        /// Opacity factor
        /// </summary>
        /// <since>1.0.0</since>
        Opacity = 4
    };
}