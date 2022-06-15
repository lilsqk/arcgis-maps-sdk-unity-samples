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
    /// The map contains layers and additional properties and can be displayed in a ArcGISRenderComponent.
    /// </summary>
    /// <remarks>
    /// The map represent the document with all data that will be renderer by ArcGISRenderComponent.
    /// </remarks>
    /// <since>1.0.0</since>
    [StructLayout(LayoutKind.Sequential)]
    public partial class ArcGISMap :
        GameEngine.ArcGISLoadable
    {
        #region Constructors
        /// <summary>
        /// Create a new Map document.
        /// </summary>
        /// <remarks>
        /// Creates map view for displaying a map. We only be able to have one map by scene, it will be automatically rendered on the ArcGISRenderComponent.
        /// </remarks>
        /// <param name="basemap">Specifies the basemap</param>
        /// <param name="mapType">Specifies the map type.</param>
        /// <since>1.0.0</since>
        public ArcGISMap(ArcGISBasemap basemap, ArcGISMapType mapType)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localBasemap = basemap.Handle;
            
            Handle = PInvoke.RT_ArcGISMap_createWithBasemapAndMapType(localBasemap, mapType, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        
        /// <summary>
        /// Create a new Map document.
        /// </summary>
        /// <remarks>
        /// Creates map view for displaying a map. We only be able to have one map by scene, it will be automatically rendered on the ArcGISRenderComponent.
        /// </remarks>
        /// <param name="mapType">Specifies the map type.</param>
        /// <since>1.0.0</since>
        public ArcGISMap(ArcGISMapType mapType)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGISMap_createWithMapType(mapType, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        
        /// <summary>
        /// Creates a map with spatial reference and map type.
        /// </summary>
        /// <param name="spatialReference">A spatial reference object.</param>
        /// <param name="mapType">Specifies the map type.</param>
        /// <since>1.0.0</since>
        public ArcGISMap(GameEngine.Geometry.ArcGISSpatialReference spatialReference, ArcGISMapType mapType)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localSpatialReference = spatialReference == null ? System.IntPtr.Zero : spatialReference.Handle;
            
            Handle = PInvoke.RT_ArcGISMap_createWithSpatialReferenceAndMapType(localSpatialReference, mapType, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// <summary>
        /// Definition for basemap.
        /// </summary>
        /// <since>1.0.0</since>
        public ArcGISBasemap Basemap
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMap_getBasemap(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                ArcGISBasemap localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new ArcGISBasemap(localResult);
                }
                
                return localLocalResult;
            }
            set
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localValue = value == null ? System.IntPtr.Zero : value.Handle;
                
                PInvoke.RT_ArcGISMap_setBasemap(Handle, localValue, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
            }
        }
        
        /// <summary>
        /// Definition of map's clipping area. This property will be applied in Local mode only.
        /// </summary>
        /// <remarks>
        /// Setting a non-null clipping area in Global mode will result in an error.
        /// </remarks>
        /// <since>1.0.0</since>
        public GameEngine.Extent.ArcGISExtent ClippingArea
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMap_getClippingArea(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                GameEngine.Extent.ArcGISExtent localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    var objectType = GameEngine.Extent.PInvoke.RT_ArcGISExtent_getObjectType(localResult, IntPtr.Zero);
                    
                    switch (objectType)
                    {
                        case GameEngine.Extent.ArcGISExtentType.ArcGISExtentCircle:
                            localLocalResult = new GameEngine.Extent.ArcGISExtentCircle(localResult);
                            break;
                        case GameEngine.Extent.ArcGISExtentType.ArcGISExtentRectangle:
                            localLocalResult = new GameEngine.Extent.ArcGISExtentRectangle(localResult);
                            break;
                        default:
                            localLocalResult = new GameEngine.Extent.ArcGISExtent(localResult);
                            break;
                    }
                }
                
                return localLocalResult;
            }
            set
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localValue = value == null ? System.IntPtr.Zero : value.Handle;
                
                PInvoke.RT_ArcGISMap_setClippingArea(Handle, localValue, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
            }
        }
        
        /// <summary>
        /// Definition of map elevation.
        /// </summary>
        /// <since>1.0.0</since>
        public ArcGISMapElevation Elevation
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMap_getElevation(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                ArcGISMapElevation localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new ArcGISMapElevation(localResult);
                }
                
                return localLocalResult;
            }
            set
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localValue = value.Handle;
                
                PInvoke.RT_ArcGISMap_setElevation(Handle, localValue, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
            }
        }
        
        /// <summary>
        /// List of <see cref="GameEngine.Layers.Base.ArcGISLayer">ArcGISLayer</see> included on the map
        /// </summary>
        /// <since>1.0.0</since>
        public Unity.ArcGISCollection<GameEngine.Layers.Base.ArcGISLayer> Layers
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMap_getLayers(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                Unity.ArcGISCollection<GameEngine.Layers.Base.ArcGISLayer> localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new Unity.ArcGISCollection<GameEngine.Layers.Base.ArcGISLayer>(localResult);
                }
                
                return localLocalResult;
            }
            set
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localValue = value.Handle;
                
                PInvoke.RT_ArcGISMap_setLayers(Handle, localValue, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
            }
        }
        
        /// <summary>
        /// Definition for the map, if it's local or global.
        /// </summary>
        /// <since>1.0.0</since>
        public ArcGISMapType MapType
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMap_getMapType(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// <summary>
        /// The spatial reference for the map.
        /// </summary>
        /// <seealso cref="GameEngine.Geometry.ArcGISSpatialReference">ArcGISSpatialReference</seealso>
        /// <since>1.0.0</since>
        public GameEngine.Geometry.ArcGISSpatialReference SpatialReference
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMap_getSpatialReference(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                GameEngine.Geometry.ArcGISSpatialReference localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new GameEngine.Geometry.ArcGISSpatialReference(localResult);
                }
                
                return localLocalResult;
            }
        }
        #endregion // Properties
        
        #region Methods
        /// <summary>
        /// Searches for the layer with the given id.
        /// </summary>
        /// <param name="layerId">The id of the layer to find</param>
        /// <returns>
        /// An <see cref="GameEngine.Layers.Base.ArcGISLayer">ArcGISLayer</see> or null if not found.
        /// </returns>
        /// <since>1.0.0</since>
        internal GameEngine.Layers.Base.ArcGISLayer FindLayerById(uint layerId)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISMap_findLayerById(Handle, layerId, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            GameEngine.Layers.Base.ArcGISLayer localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                var objectType = GameEngine.Layers.Base.PInvoke.RT_ArcGISLayer_getObjectType(localResult, IntPtr.Zero);
                
                switch (objectType)
                {
                    case GameEngine.Layers.Base.ArcGISLayerType.ArcGIS3DObjectSceneLayer:
                        localLocalResult = new GameEngine.Layers.ArcGIS3DObjectSceneLayer(localResult);
                        break;
                    case GameEngine.Layers.Base.ArcGISLayerType.ArcGISImageLayer:
                        localLocalResult = new GameEngine.Layers.ArcGISImageLayer(localResult);
                        break;
                    case GameEngine.Layers.Base.ArcGISLayerType.ArcGISIntegratedMeshLayer:
                        localLocalResult = new GameEngine.Layers.ArcGISIntegratedMeshLayer(localResult);
                        break;
                    case GameEngine.Layers.Base.ArcGISLayerType.ArcGISUnknownLayer:
                        localLocalResult = new GameEngine.Layers.ArcGISUnknownLayer(localResult);
                        break;
                    case GameEngine.Layers.Base.ArcGISLayerType.ArcGISUnsupportedLayer:
                        localLocalResult = new GameEngine.Layers.ArcGISUnsupportedLayer(localResult);
                        break;
                    default:
                        localLocalResult = new GameEngine.Layers.Base.ArcGISLayer(localResult);
                        break;
                }
            }
            
            return localLocalResult;
        }
        #endregion // Methods
        
        #region GameEngine.ArcGISLoadable
        public Exception LoadError
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMap_getLoadError(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return Unity.Convert.FromArcGISError(new Standard.ArcGISError(localResult));
            }
        }
        
        public GameEngine.ArcGISLoadStatus LoadStatus
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISMap_getLoadStatus(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        public void CancelLoad()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISMap_cancelLoad(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        
        public void Load()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISMap_load(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        
        public void RetryLoad()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISMap_retryLoad(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        
        public GameEngine.ArcGISLoadableDoneLoadingEvent DoneLoading
        {
            get
            {
                return _doneLoadingHandler.Delegate;
            }
            set
            {
                if (_doneLoadingHandler.Delegate == value)
                {
                    return;
                }
                
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                if (value != null)
                {
                    _doneLoadingHandler.Delegate = value;
                    
                    PInvoke.RT_ArcGISMap_setDoneLoadingCallback(Handle, GameEngine.ArcGISLoadableDoneLoadingEventHandler.HandlerFunction, _doneLoadingHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_ArcGISMap_setDoneLoadingCallback(Handle, null, _doneLoadingHandler.UserData, errorHandler);
                    
                    _doneLoadingHandler.Dispose();
                }
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
            }
        }
        
        public GameEngine.ArcGISLoadableLoadStatusChangedEvent LoadStatusChanged
        {
            get
            {
                return _loadStatusChangedHandler.Delegate;
            }
            set
            {
                if (_loadStatusChangedHandler.Delegate == value)
                {
                    return;
                }
                
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                if (value != null)
                {
                    _loadStatusChangedHandler.Delegate = value;
                    
                    PInvoke.RT_ArcGISMap_setLoadStatusChangedCallback(Handle, GameEngine.ArcGISLoadableLoadStatusChangedEventHandler.HandlerFunction, _loadStatusChangedHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_ArcGISMap_setLoadStatusChangedCallback(Handle, null, _loadStatusChangedHandler.UserData, errorHandler);
                    
                    _loadStatusChangedHandler.Dispose();
                }
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
            }
        }
        #endregion // GameEngine.ArcGISLoadable
        
        #region Internal Members
        internal ArcGISMap(IntPtr handle) => Handle = handle;
        
        ~ArcGISMap()
        {
            if (Handle != IntPtr.Zero)
            {
                if (_doneLoadingHandler.Delegate != null)
                {
                    PInvoke.RT_ArcGISMap_setDoneLoadingCallback(Handle, null, _doneLoadingHandler.UserData, IntPtr.Zero);
                    
                    _doneLoadingHandler.Dispose();
                }
                
                if (_loadStatusChangedHandler.Delegate != null)
                {
                    PInvoke.RT_ArcGISMap_setLoadStatusChangedCallback(Handle, null, _loadStatusChangedHandler.UserData, IntPtr.Zero);
                    
                    _loadStatusChangedHandler.Dispose();
                }
                
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISMap_destroy(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
            }
        }
        
        internal IntPtr Handle { get; set; }
        
        internal GameEngine.ArcGISLoadableDoneLoadingEventHandler _doneLoadingHandler = new GameEngine.ArcGISLoadableDoneLoadingEventHandler();
        
        internal GameEngine.ArcGISLoadableLoadStatusChangedEventHandler _loadStatusChangedHandler = new GameEngine.ArcGISLoadableLoadStatusChangedEventHandler();
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_createWithBasemapAndMapType(IntPtr basemap, ArcGISMapType mapType, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_createWithMapType(ArcGISMapType mapType, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_createWithSpatialReferenceAndMapType(IntPtr spatialReference, ArcGISMapType mapType, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_getBasemap(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISMap_setBasemap(IntPtr handle, IntPtr basemap, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_getClippingArea(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISMap_setClippingArea(IntPtr handle, IntPtr clippingArea, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_getElevation(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISMap_setElevation(IntPtr handle, IntPtr elevation, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_getLayers(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISMap_setLayers(IntPtr handle, IntPtr layers, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern ArcGISMapType RT_ArcGISMap_getMapType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_getSpatialReference(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_findLayerById(IntPtr handle, uint layerId, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISMap_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
        
        #region GameEngine.ArcGISLoadable P-Invoke Declarations
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISMap_getLoadError(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern GameEngine.ArcGISLoadStatus RT_ArcGISMap_getLoadStatus(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISMap_cancelLoad(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISMap_load(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISMap_retryLoad(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISMap_setDoneLoadingCallback(IntPtr handle, GameEngine.ArcGISLoadableDoneLoadingEventInternal doneLoading, IntPtr userData, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISMap_setLoadStatusChangedCallback(IntPtr handle, GameEngine.ArcGISLoadableLoadStatusChangedEventInternal loadStatusChanged, IntPtr userData, IntPtr errorHandler);
        #endregion // GameEngine.ArcGISLoadable P-Invoke Declarations
    }
}