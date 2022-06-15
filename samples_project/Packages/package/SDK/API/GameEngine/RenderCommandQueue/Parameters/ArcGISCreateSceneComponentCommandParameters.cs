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
    /// A create scene component render command
    /// </summary>
    /// <since>1.0.0</since>
    [StructLayout(LayoutKind.Sequential)]
    internal struct ArcGISCreateSceneComponentCommandParameters
    {
        /// <summary>
        /// The id that will be use for the created render target.
        /// </summary>
        /// <since>1.0.0</since>
        public uint SceneComponentId;
        
        /// <summary>
        /// The type parameter of the scene component
        /// </summary>
        /// <since>1.0.0</since>
        public ArcGISSceneComponentType SceneComponentType;
        
        /// <summary>
        /// Internal 32-bit identifier of a layer. Only valid for Scene Nodes.
        /// </summary>
        /// <since>1.0.0</since>
        public uint LayerId;
    }
}