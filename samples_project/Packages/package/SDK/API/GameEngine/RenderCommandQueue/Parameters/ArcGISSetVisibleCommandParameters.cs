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
    /// A set visible render command
    /// </summary>
    /// <since>1.0.0</since>
    [StructLayout(LayoutKind.Sequential)]
    internal struct ArcGISSetVisibleCommandParameters
    {
        /// <summary>
        /// The scene component parameter of this render command
        /// </summary>
        /// <since>1.0.0</since>
        public uint SceneComponentId;
        
        /// <summary>
        /// The visible parameter of this render command
        /// </summary>
        /// <since>1.0.0</since>
        [MarshalAs(UnmanagedType.I1)]
        public bool IsVisible;
    }
}