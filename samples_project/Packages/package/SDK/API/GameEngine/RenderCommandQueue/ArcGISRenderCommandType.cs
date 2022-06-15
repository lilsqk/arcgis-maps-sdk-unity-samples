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
namespace Esri.GameEngine.RenderCommandQueue
{
    /// <summary>
    /// Render command types
    /// </summary>
    /// <since>1.0.0</since>
    public enum ArcGISRenderCommandType
    {
        /// <summary>
        /// Create material
        /// </summary>
        /// <since>1.0.0</since>
        CreateMaterial = 0,
        
        /// <summary>
        /// Create render target
        /// </summary>
        /// <since>1.0.0</since>
        CreateRenderTarget = 1,
        
        /// <summary>
        /// Create texture
        /// </summary>
        /// <since>1.0.0</since>
        CreateTexture = 2,
        
        /// <summary>
        /// Create scene component
        /// </summary>
        /// <since>1.0.0</since>
        CreateSceneComponent = 3,
        
        /// <summary>
        /// Destroy material
        /// </summary>
        /// <since>1.0.0</since>
        DestroyMaterial = 4,
        
        /// <summary>
        /// Destroy render target
        /// </summary>
        /// <since>1.0.0</since>
        DestroyRenderTarget = 5,
        
        /// <summary>
        /// Destroy texture
        /// </summary>
        /// <since>1.0.0</since>
        DestroyTexture = 6,
        
        /// <summary>
        /// Destroy scene component
        /// </summary>
        /// <since>1.0.0</since>
        DestroySceneComponent = 7,
        
        /// <summary>
        /// Multiple compose
        /// </summary>
        /// <since>1.0.0</since>
        MultipleCompose = 8,
        
        /// <summary>
        /// Compose
        /// </summary>
        /// <since>1.0.0</since>
        Compose = 9,
        
        /// <summary>
        /// Copy
        /// </summary>
        /// <since>1.0.0</since>
        Copy = 10,
        
        /// <summary>
        /// Generate normal texture
        /// </summary>
        /// <since>1.0.0</since>
        GenerateNormalTexture = 11,
        
        /// <summary>
        /// Set the pixel data of a texture
        /// </summary>
        /// <since>1.0.0</since>
        SetTexturePixelData = 12,
        
        /// <summary>
        /// Set the value of a material's scalar property
        /// </summary>
        /// <since>1.0.0</since>
        SetMaterialScalarProperty = 13,
        
        /// <summary>
        /// Set the value of a material's vector property
        /// </summary>
        /// <since>1.0.0</since>
        SetMaterialVectorProperty = 14,
        
        /// <summary>
        /// Set the value of a material's render target property
        /// </summary>
        /// <since>1.0.0</since>
        SetMaterialRenderTargetProperty = 15,
        
        /// <summary>
        /// Set the value of a material's texture property
        /// </summary>
        /// <since>1.0.0</since>
        SetMaterialTextureProperty = 16,
        
        /// <summary>
        /// Generate MipMaps
        /// </summary>
        /// <since>1.0.0</since>
        GenerateMipMaps = 17,
        
        /// <summary>
        /// Set visible
        /// </summary>
        /// <since>1.0.0</since>
        SetVisible = 18,
        
        /// <summary>
        /// Set material
        /// </summary>
        /// <since>1.0.0</since>
        SetMaterial = 19,
        
        /// <summary>
        /// Set mesh
        /// </summary>
        /// <since>1.0.0</since>
        SetMesh = 20,
        
        /// <summary>
        /// Set mesh buffer
        /// </summary>
        /// <since>1.0.0</since>
        SetMeshBuffer = 21,
        
        /// <summary>
        /// Set the pivot of a scene component
        /// </summary>
        /// <since>1.0.0</since>
        SetSceneComponentPivot = 22,
        
        /// <summary>
        /// Set a named texture on a material
        /// </summary>
        /// <since>1.0.0</since>
        SetMaterialNamedTextureProperty = 23,
        
        /// <summary>
        /// Mark the start of a group of commands that should be executed atomically
        /// </summary>
        /// <since>1.0.0</since>
        CommandGroupBegin = 24,
        
        /// <summary>
        /// Mark the end of a group of commands that should be executed atomically
        /// </summary>
        /// <since>1.0.0</since>
        CommandGroupEnd = 25
    };
}