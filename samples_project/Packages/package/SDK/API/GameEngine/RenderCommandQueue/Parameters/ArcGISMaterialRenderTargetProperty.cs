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
    /// Material parameter render targets
    /// </summary>
    /// <since>1.0.0</since>
    public enum ArcGISMaterialRenderTargetProperty
    {
        /// <summary>
        /// Imagery map 0
        /// </summary>
        /// <since>1.0.0</since>
        ImageryMap0 = 0,
        
        /// <summary>
        /// Imagery map 1
        /// </summary>
        /// <since>1.0.0</since>
        ImageryMap1 = 1,
        
        /// <summary>
        /// Normal map 0
        /// </summary>
        /// <since>1.0.0</since>
        NormalMap0 = 2,
        
        /// <summary>
        /// Normal map 1
        /// </summary>
        /// <since>1.0.0</since>
        NormalMap1 = 3
    };
}