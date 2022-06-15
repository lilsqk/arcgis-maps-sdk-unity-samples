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
using System;

namespace Esri.GameEngine.RenderCommandQueue
{
    /// <summary>
    /// Stream render commands to the game engine
    /// </summary>
    /// <since>1.0.0</since>
    [StructLayout(LayoutKind.Sequential)]
    internal partial class ArcGISRenderCommandServer
    {
        #region Methods
        /// <summary>
        /// Get an object with the render command cache since the last time the method was called
        /// </summary>
        /// <remarks>
        /// The DataBuffer contains the render commands serialized as a consecutive array of [type_of_command, render_command_parameters].
        /// Since the length of each render command is different, the de-serialization has to be done dynamically, reading the commands one by one
        /// and dynamically advancing the pointer to the DataBuffer.
        /// </remarks>
        /// <returns>
        /// An object with the render command cache since the last time the method was called
        /// </returns>
        /// <since>1.0.0</since>
        internal Unity.ArcGISDataBuffer<byte> GetRenderCommands()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_RenderCommandServer_getRenderCommands(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            Unity.ArcGISDataBuffer<byte> localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Unity.ArcGISDataBuffer<byte>(localResult);
            }
            
            return localLocalResult;
        }
        #endregion // Methods
        
        #region Internal Members
        internal ArcGISRenderCommandServer(IntPtr handle) => Handle = handle;
        
        ~ArcGISRenderCommandServer()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                PInvoke.RT_RenderCommandServer_destroy(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
            }
        }
        
        internal IntPtr Handle { get; set; }
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_RenderCommandServer_getRenderCommands(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_RenderCommandServer_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}