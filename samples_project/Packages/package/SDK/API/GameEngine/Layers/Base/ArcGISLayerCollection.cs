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

namespace Esri.GameEngine.Layers.Base
{
    /// <summary>
    /// Defines a dynamic array that provides callbacks when ArcGISLayers are added or removed.
    /// </summary>
    /// <remarks>
    /// The vector holds <see cref="GameEngine.Layers.Base.ArcGISLayer">ArcGISLayer</see> objects.
    /// </remarks>
    /// <since>1.0.0</since>
    [StructLayout(LayoutKind.Sequential)]
    internal partial class ArcGISLayerCollection
    {
        #region Constructors
        /// <summary>
        /// Creates a vector. This allocates memory that must be deleted.
        /// </summary>
        /// <since>1.0.0</since>
        internal ArcGISLayerCollection()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_ArcGISLayerCollection_create(errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// <summary>
        /// Determines the number of values in the vector.
        /// </summary>
        /// <remarks>
        /// The number of values in the vector. If an error occurs a 0 will be returned.
        /// </remarks>
        /// <since>1.0.0</since>
        internal ulong Size
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArcGISLayerCollection_getSize(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult.ToUInt64();
            }
        }
        #endregion // Properties
        
        #region Methods
        /// <summary>
        /// Add a value to the vector.
        /// </summary>
        /// <param name="value">The value that is to be added.</param>
        /// <returns>
        /// The position of the value. Max value of size_t if an error occurs.
        /// </returns>
        /// <since>1.0.0</since>
        internal ulong Add(ArcGISLayer value)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localValue = value.Handle;
            
            var localResult = PInvoke.RT_ArcGISLayerCollection_add(Handle, localValue, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult.ToUInt64();
        }
        
        /// <summary>
        /// Appends a vector to a vector.
        /// </summary>
        /// <param name="vector2">The value that is to be added.</param>
        /// <returns>
        /// The new size of vector_1. Max value of size_t if an error occurs.
        /// </returns>
        /// <since>1.0.0</since>
        internal ulong AddArray(Unity.ArcGISCollection<ArcGISLayer> vector2)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localVector2 = vector2.Handle;
            
            var localResult = PInvoke.RT_ArcGISLayerCollection_addArray(Handle, localVector2, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult.ToUInt64();
        }
        
        /// <summary>
        /// Get a value at a specific position.
        /// </summary>
        /// <remarks>
        /// Retrieves the value at the specified position.
        /// </remarks>
        /// <param name="position">The position which you want to get the value.</param>
        /// <returns>
        /// The value, <see cref="">Element</see>, at the position requested.
        /// </returns>
        /// <since>1.0.0</since>
        internal ArcGISLayer At(ulong position)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localPosition = new UIntPtr(position);
            
            var localResult = PInvoke.RT_ArcGISLayerCollection_at(Handle, localPosition, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            ArcGISLayer localLocalResult = null;
            
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
                        localLocalResult = new ArcGISLayer(localResult);
                        break;
                }
            }
            
            return localLocalResult;
        }
        
        /// <summary>
        /// Does the vector contain the given value.
        /// </summary>
        /// <remarks>
        /// Does the vector contain a specific value.
        /// </remarks>
        /// <param name="value">The value you want to find.</param>
        /// <returns>
        /// True if the value is in the vector otherwise false.
        /// </returns>
        /// <since>1.0.0</since>
        internal bool Contains(ArcGISLayer value)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localValue = value.Handle;
            
            var localResult = PInvoke.RT_ArcGISLayerCollection_contains(Handle, localValue, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// <summary>
        /// Returns true if the two vectors are equal, false otherwise.
        /// </summary>
        /// <param name="vector2">The second vector.</param>
        /// <returns>
        /// Returns true if the two vectors are equal, false otherwise.
        /// </returns>
        /// <since>1.0.0</since>
        internal bool Equals(Unity.ArcGISCollection<ArcGISLayer> vector2)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localVector2 = vector2.Handle;
            
            var localResult = PInvoke.RT_ArcGISLayerCollection_equals(Handle, localVector2, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// <summary>
        /// Get the first value in the vector.
        /// </summary>
        /// <returns>
        /// The value, <see cref="">Element</see>, at the position requested.
        /// </returns>
        /// <since>1.0.0</since>
        internal ArcGISLayer First()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISLayerCollection_first(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            ArcGISLayer localLocalResult = null;
            
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
                        localLocalResult = new ArcGISLayer(localResult);
                        break;
                }
            }
            
            return localLocalResult;
        }
        
        /// <summary>
        /// Retrieves the position of the given value in the vector.
        /// </summary>
        /// <param name="value">The value you want to find.</param>
        /// <returns>
        /// The position of the value in the vector, Max value of size_t otherwise.
        /// </returns>
        /// <since>1.0.0</since>
        internal ulong IndexOf(ArcGISLayer value)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localValue = value.Handle;
            
            var localResult = PInvoke.RT_ArcGISLayerCollection_indexOf(Handle, localValue, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult.ToUInt64();
        }
        
        /// <summary>
        /// Insert a value at the specified position in the vector.
        /// </summary>
        /// <remarks>
        /// Insert a value at a specified position to the vector.
        /// </remarks>
        /// <param name="position">The position which you want to insert the value.</param>
        /// <param name="value">The value that is to be added.</param>
        /// <since>1.0.0</since>
        internal void Insert(ulong position, ArcGISLayer value)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localPosition = new UIntPtr(position);
            var localValue = value.Handle;
            
            PInvoke.RT_ArcGISLayerCollection_insert(Handle, localPosition, localValue, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        
        /// <summary>
        /// Determines if there are any values in the vector.
        /// </summary>
        /// <remarks>
        /// Check if the vector object has any values in it.
        /// </remarks>
        /// <returns>
        /// true if the  vector object contains no values otherwise false. Will return true if an error occurs.
        /// </returns>
        /// <since>1.0.0</since>
        internal bool IsEmpty()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISLayerCollection_isEmpty(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// <summary>
        /// Get the last value in the vector.
        /// </summary>
        /// <returns>
        /// The value, <see cref="">Element</see>, at the position requested.
        /// </returns>
        /// <since>1.0.0</since>
        internal ArcGISLayer Last()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISLayerCollection_last(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            ArcGISLayer localLocalResult = null;
            
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
                        localLocalResult = new ArcGISLayer(localResult);
                        break;
                }
            }
            
            return localLocalResult;
        }
        
        /// <summary>
        /// Move a value from the current position to a new position in the string vector.
        /// </summary>
        /// <remarks>
        /// Move a value from the current position to a new position in the vector.
        /// </remarks>
        /// <param name="oldPosition">The current position of the value.</param>
        /// <param name="newPosition">The position which you want to move the value to.</param>
        /// <since>1.0.0</since>
        internal void Move(ulong oldPosition, ulong newPosition)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localOldPosition = new UIntPtr(oldPosition);
            var localNewPosition = new UIntPtr(newPosition);
            
            PInvoke.RT_ArcGISLayerCollection_move(Handle, localOldPosition, localNewPosition, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        
        /// <summary>
        /// Returns a value indicating a bad position within the vector.
        /// </summary>
        /// <returns>
        /// A size_t.
        /// </returns>
        /// <since>1.0.0</since>
        internal static ulong Npos()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArcGISLayerCollection_npos(errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult.ToUInt64();
        }
        
        /// <summary>
        /// Remove a value at a specific position in the vector.
        /// </summary>
        /// <param name="position">The position which you want to remove the value.</param>
        /// <since>1.0.0</since>
        internal void Remove(ulong position)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localPosition = new UIntPtr(position);
            
            PInvoke.RT_ArcGISLayerCollection_remove(Handle, localPosition, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        
        /// <summary>
        /// Remove all values from the vector.
        /// </summary>
        /// <since>1.0.0</since>
        internal void RemoveAll()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            PInvoke.RT_ArcGISLayerCollection_removeAll(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        #endregion // Methods
        
        #region Internal Members
        internal ArcGISLayerCollection(IntPtr handle) => Handle = handle;
        
        ~ArcGISLayerCollection()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                PInvoke.RT_ArcGISLayerCollection_destroy(Handle, errorHandler);
                
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
        internal static extern IntPtr RT_ArcGISLayerCollection_create(IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern UIntPtr RT_ArcGISLayerCollection_getSize(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern UIntPtr RT_ArcGISLayerCollection_add(IntPtr handle, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern UIntPtr RT_ArcGISLayerCollection_addArray(IntPtr handle, IntPtr vector2, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISLayerCollection_at(IntPtr handle, UIntPtr position, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_ArcGISLayerCollection_contains(IntPtr handle, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_ArcGISLayerCollection_equals(IntPtr handle, IntPtr vector2, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISLayerCollection_first(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern UIntPtr RT_ArcGISLayerCollection_indexOf(IntPtr handle, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISLayerCollection_insert(IntPtr handle, UIntPtr position, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_ArcGISLayerCollection_isEmpty(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArcGISLayerCollection_last(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISLayerCollection_move(IntPtr handle, UIntPtr oldPosition, UIntPtr newPosition, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern UIntPtr RT_ArcGISLayerCollection_npos(IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISLayerCollection_remove(IntPtr handle, UIntPtr position, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISLayerCollection_removeAll(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArcGISLayerCollection_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}