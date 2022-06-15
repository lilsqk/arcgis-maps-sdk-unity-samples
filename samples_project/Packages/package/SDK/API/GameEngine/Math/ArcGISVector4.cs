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

namespace Esri.GameEngine.Math
{
    /// <summary>
    /// A 4 dimensional vector made of floats
    /// </summary>
    /// <since>1.0.0</since>
    [StructLayout(LayoutKind.Sequential)]
    internal struct ArcGISVector4
    {
        /// <summary>
        /// The x parameter
        /// </summary>
        /// <since>1.0.0</since>
        public float X;
        
        /// <summary>
        /// The y parameter
        /// </summary>
        /// <since>1.0.0</since>
        public float Y;
        
        /// <summary>
        /// The z parameter
        /// </summary>
        /// <since>1.0.0</since>
        public float Z;
        
        /// <summary>
        /// The w parameter
        /// </summary>
        /// <since>1.0.0</since>
        public float W;
    }
}