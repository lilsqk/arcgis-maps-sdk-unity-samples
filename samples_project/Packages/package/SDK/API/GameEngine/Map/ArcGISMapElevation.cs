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

namespace Esri.GameEngine.Map
{
    /// <summary>
    /// The map contains elevation sources and additional properties and can be displayed in a ArcGISRenderComponent.
    /// </summary>
    /// <remarks>
    /// The map represent the document with all data that will be renderer by ArcGISRenderComponent.
    /// </remarks>
    /// <since>1.0.0</since>
    [StructLayout(LayoutKind.Sequential)]
    public partial class ArcGISMapElevation
    {
        #region Constructors
        /// <summary>
        /// Create a elevation for the map with no elevation sources
        /// </summary>
        /// <remarks>
        /// Create elevation with no elevation sources
        /// </remarks>
        /// <since>1.0.0</since>
        public ArcGISMapElevation()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGISMapElevation_create(errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        
        /// <summary>
        /// Create a elevation for the map with one elevation source
        /// </summary>
        /// <remarks>
        /// Create elevation with a single elevation source
        /// </remarks>
        /// <param name="elevationSource">Elevation source</param>
        /// <since>1.0.0</since>
        public ArcGISMapElevation(GameEngine.Elevation.Base.ArcGISElevationSource elevationSource)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localElevationSource = elevationSource.Handle;
            
            Handle = PInvoke.RT_ArcGISMapElevation_createWithElevationSource(localElevationSource, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// <summary>
        /// List of elevation sources included on the elevation.
        /// </summary>
        /// <remarks>
        /// At present, the ArcGISElevationSourceCollection may contain a maximum of one elevation source.
        /// </remarks>
        /// <since>1.0.0</since>
        public Unity.ArcGISCollection<GameEngine.Elevation.Base.ArcGISElevationSource> ElevationSources
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMapElevation_getElevationSources(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                Unity.ArcGISCollection<GameEngine.Elevation.Base.ArcGISElevationSource> localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new Unity.ArcGISCollection<GameEngine.Elevation.Base.ArcGISElevationSource>(localResult);
                }
                
                return localLocalResult;
            }
            set
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localValue = value.Handle;
                
                PInvoke.RT_ArcGISMapElevation_setElevationSources(Handle, localValue, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
            }
        }
        #endregion // Properties
        
        #region Internal Members
        internal ArcGISMapElevation(IntPtr handle) => Handle = handle;
        
        ~ArcGISMapElevation()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISMapElevation_destroy(Handle, errorHandler);
                
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
        internal static extern IntPtr RT_ArcGISMapElevation_create(IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMapElevation_createWithElevationSource(IntPtr elevationSource, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMapElevation_getElevationSources(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISMapElevation_setElevationSources(IntPtr handle, IntPtr elevationSources, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISMapElevation_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}