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
    /// Public class that will contain a basemap.
    /// </summary>
    /// <since>1.0.0</since>
    [StructLayout(LayoutKind.Sequential)]
    public partial class ArcGISBasemap :
        GameEngine.ArcGISLoadable
    {
        #region Constructors
        /// <summary>
        /// Creates an empty ArcGISBasemap. Basemap is created in a loaded state.
        /// </summary>
        /// <since>1.0.0</since>
        public ArcGISBasemap()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGISBasemap_create(errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        
        /// <summary>
        /// Creates a ArcGISBasemap from a URI and ArcGISLayerType
        /// </summary>
        /// <param name="source">ArcGISLayer source</param>
        /// <param name="type">Layer type definition.</param>
        /// <param name="APIKey">API Key used to load data.</param>
        /// <since>1.0.0</since>
        public ArcGISBasemap(string source, GameEngine.Layers.Base.ArcGISLayerType type, string APIKey)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGISBasemap_createWithLayerSourceAndType(source, type, APIKey, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        
        /// <summary>
        /// Creates a ArcGISBasemap from a basemap URI
        /// </summary>
        /// <param name="source">ArcGISBasemap source</param>
        /// <param name="APIKey">API Key used to load data.</param>
        /// <since>1.0.0</since>
        public ArcGISBasemap(string source, string APIKey)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGISBasemap_createWithBasemapSource(source, APIKey, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// <summary>
        /// API Key will be sended on loading process to match the new credit system.
        /// </summary>
        /// <since>1.0.0</since>
        public string APIKey
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISBasemap_getAPIKey(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return Unity.Convert.FromArcGISString(localResult);
            }
        }
        
        /// <summary>
        /// The base layers.
        /// </summary>
        /// <remarks>
        /// The collection is specific to a basemap so you can't reuse an ArcGISLayerCollection coming from another <see cref="GameEngine.Map.ArcGISMap">ArcGISMap</see> or <see cref="GameEngine.Map.ArcGISBasemap">ArcGISBasemap</see>.
        /// You have to create a new layer collection by using <see cref="GameEngine.Layers.Base.ArcGISLayerCollection">ArcGISLayerCollection</see>.
        /// </remarks>
        /// <seealso cref="GameEngine.Layers.Base.ArcGISLayer">ArcGISLayer</seealso>
        /// <seealso cref="GameEngine.Layers.Base.ArcGISLayerCollection">ArcGISLayerCollection</seealso>
        /// <warning>
        /// <see cref="Standard.ArcGISErrorType.MappingUnsupportedLayerType">ArcGISErrorType.MappingUnsupportedLayerType</see> thrown if <see cref="">GroupLayer</see> added to the base layer collection after retrieval.
        /// </warning>
        /// <since>1.0.0</since>
        public Unity.ArcGISCollection<GameEngine.Layers.Base.ArcGISLayer> BaseLayers
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISBasemap_getBaseLayers(Handle, errorHandler);
                
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
                
                PInvoke.RT_ArcGISBasemap_setBaseLayers(Handle, localValue, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
            }
        }
        
        /// <summary>
        /// This property will help the user to identify the layer on his application.
        /// </summary>
        /// <since>1.0.0</since>
        public string Name
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISBasemap_getName(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return Unity.Convert.FromArcGISString(localResult);
            }
            set
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISBasemap_setName(Handle, value, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
            }
        }
        
        /// <summary>
        /// The reference layers.
        /// </summary>
        /// <remarks>
        /// The collection is specific to a basemap so you can't reuse an ArcGISLayerCollection coming from another <see cref="GameEngine.Map.ArcGISMap">ArcGISMap</see> or <see cref="GameEngine.Map.ArcGISBasemap">ArcGISBasemap</see>.
        /// You have to create a new layer collection by using <see cref="GameEngine.Layers.Base.ArcGISLayerCollection">ArcGISLayerCollection</see>.
        /// </remarks>
        /// <seealso cref="GameEngine.Layers.Base.ArcGISLayer">ArcGISLayer</seealso>
        /// <seealso cref="GameEngine.Layers.Base.ArcGISLayerCollection">ArcGISLayerCollection</seealso>
        /// <warning>
        /// <see cref="Standard.ArcGISErrorType.MappingUnsupportedLayerType">ArcGISErrorType.MappingUnsupportedLayerType</see> thrown if <see cref="">GroupLayer</see> added to the reference layer collection after retrieval.
        /// </warning>
        /// <since>1.0.0</since>
        public Unity.ArcGISCollection<GameEngine.Layers.Base.ArcGISLayer> ReferenceLayers
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISBasemap_getReferenceLayers(Handle, errorHandler);
                
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
                
                PInvoke.RT_ArcGISBasemap_setReferenceLayers(Handle, localValue, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
            }
        }
        
        /// <summary>
        /// Source property will be a read only, it will be setup on the constructor
        /// </summary>
        /// <since>1.0.0</since>
        public string Source
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISBasemap_getSource(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return Unity.Convert.FromArcGISString(localResult);
            }
        }
        #endregion // Properties
        
        #region Methods
        /// <summary>
        /// Create a ArcGISBasemap Imagery Type
        /// </summary>
        /// <param name="APIKey">API Key used to load data.</param>
        /// <returns>
        /// A <see cref="GameEngine.Map.ArcGISBasemap">ArcGISBasemap</see>
        /// </returns>
        /// <since>1.0.0</since>
        public static ArcGISBasemap CreateImagery(string APIKey)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISBasemap_createImagery(APIKey, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            ArcGISBasemap localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new ArcGISBasemap(localResult);
            }
            
            return localLocalResult;
        }
        
        /// <summary>
        /// Create a ArcGISBasemap Oceans Type
        /// </summary>
        /// <param name="APIKey">API Key used to load data.</param>
        /// <returns>
        /// A <see cref="GameEngine.Map.ArcGISBasemap">ArcGISBasemap</see>
        /// </returns>
        /// <since>1.0.0</since>
        public static ArcGISBasemap CreateOceans(string APIKey)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISBasemap_createOceans(APIKey, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            ArcGISBasemap localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new ArcGISBasemap(localResult);
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
                
                var localResult = PInvoke.RT_ArcGISBasemap_getLoadError(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return Unity.Convert.FromArcGISError(new Standard.ArcGISError(localResult));
            }
        }
        
        public GameEngine.ArcGISLoadStatus LoadStatus
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISBasemap_getLoadStatus(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        public void CancelLoad()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISBasemap_cancelLoad(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        
        public void Load()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISBasemap_load(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        
        public void RetryLoad()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISBasemap_retryLoad(Handle, errorHandler);
            
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
                    
                    PInvoke.RT_ArcGISBasemap_setDoneLoadingCallback(Handle, GameEngine.ArcGISLoadableDoneLoadingEventHandler.HandlerFunction, _doneLoadingHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_ArcGISBasemap_setDoneLoadingCallback(Handle, null, _doneLoadingHandler.UserData, errorHandler);
                    
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
                    
                    PInvoke.RT_ArcGISBasemap_setLoadStatusChangedCallback(Handle, GameEngine.ArcGISLoadableLoadStatusChangedEventHandler.HandlerFunction, _loadStatusChangedHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_ArcGISBasemap_setLoadStatusChangedCallback(Handle, null, _loadStatusChangedHandler.UserData, errorHandler);
                    
                    _loadStatusChangedHandler.Dispose();
                }
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
            }
        }
        #endregion // GameEngine.ArcGISLoadable
        
        #region Internal Members
        internal ArcGISBasemap(IntPtr handle) => Handle = handle;
        
        ~ArcGISBasemap()
        {
            if (Handle != IntPtr.Zero)
            {
                if (_doneLoadingHandler.Delegate != null)
                {
                    PInvoke.RT_ArcGISBasemap_setDoneLoadingCallback(Handle, null, _doneLoadingHandler.UserData, IntPtr.Zero);
                    
                    _doneLoadingHandler.Dispose();
                }
                
                if (_loadStatusChangedHandler.Delegate != null)
                {
                    PInvoke.RT_ArcGISBasemap_setLoadStatusChangedCallback(Handle, null, _loadStatusChangedHandler.UserData, IntPtr.Zero);
                    
                    _loadStatusChangedHandler.Dispose();
                }
                
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISBasemap_destroy(Handle, errorHandler);
                
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
        internal static extern IntPtr RT_ArcGISBasemap_create(IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_createWithLayerSourceAndType([MarshalAs(UnmanagedType.LPStr)]string source, GameEngine.Layers.Base.ArcGISLayerType type, [MarshalAs(UnmanagedType.LPStr)]string APIKey, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_createWithBasemapSource([MarshalAs(UnmanagedType.LPStr)]string source, [MarshalAs(UnmanagedType.LPStr)]string APIKey, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_getAPIKey(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_getBaseLayers(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISBasemap_setBaseLayers(IntPtr handle, IntPtr baseLayers, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_getName(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISBasemap_setName(IntPtr handle, [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_getReferenceLayers(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISBasemap_setReferenceLayers(IntPtr handle, IntPtr referenceLayers, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_getSource(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_createImagery([MarshalAs(UnmanagedType.LPStr)]string APIKey, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_createOceans([MarshalAs(UnmanagedType.LPStr)]string APIKey, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISBasemap_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
        
        #region GameEngine.ArcGISLoadable P-Invoke Declarations
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISBasemap_getLoadError(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern GameEngine.ArcGISLoadStatus RT_ArcGISBasemap_getLoadStatus(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISBasemap_cancelLoad(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISBasemap_load(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISBasemap_retryLoad(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISBasemap_setDoneLoadingCallback(IntPtr handle, GameEngine.ArcGISLoadableDoneLoadingEventInternal doneLoading, IntPtr userData, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISBasemap_setLoadStatusChangedCallback(IntPtr handle, GameEngine.ArcGISLoadableLoadStatusChangedEventInternal loadStatusChanged, IntPtr userData, IntPtr errorHandler);
        #endregion // GameEngine.ArcGISLoadable P-Invoke Declarations
    }
}