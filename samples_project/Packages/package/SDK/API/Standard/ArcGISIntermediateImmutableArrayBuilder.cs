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

namespace Esri.Standard
{
    /// <summary>
    /// Use to create and populate <see cref="Standard.ArcGISIntermediateImmutableArray<T>">ArcGISIntermediateImmutableArray<T></see> collections.
    /// </summary>
    /// <remarks>
    /// The <see cref="Standard.ArcGISIntermediateImmutableArrayBuilder<T>">ArcGISIntermediateImmutableArrayBuilder<T></see> provides a mechanism for creating and populating <see cref="Standard.ArcGISIntermediateImmutableArray<T>">ArcGISIntermediateImmutableArray<T></see> objects.
    /// Because <see cref="Standard.ArcGISIntermediateImmutableArray<T>">ArcGISIntermediateImmutableArray<T></see> objects cannot be created or populate directly (they are immutable
    /// objects) the <see cref="Standard.ArcGISIntermediateImmutableArrayBuilder<T>">ArcGISIntermediateImmutableArrayBuilder<T></see> provides an efficient means to overcome this.
    /// 
    /// Use <see cref="Standard.ArcGISIntermediateImmutableArray<T>.CreateBuilder">ArcGISIntermediateImmutableArray<T>.CreateBuilder</see> to create a <see cref="Standard.ArcGISIntermediateImmutableArrayBuilder<T>">ArcGISIntermediateImmutableArrayBuilder<T></see>.
    /// 
    /// There are a couple of performance optimizations to consider when adding values to an <see cref="Standard.ArcGISIntermediateImmutableArrayBuilder<T>">ArcGISIntermediateImmutableArrayBuilder<T></see>:
    /// 1. A single instance of a <see cref="">Element</see> object can be reused by setting its value prior to each call
    /// to <see cref="Standard.ArcGISIntermediateImmutableArrayBuilder<T>.Add">ArcGISIntermediateImmutableArrayBuilder<T>.Add</see>.
    /// 2. If the overhead of creating these <see cref="">Element</see> is too much for a specific value type, consider
    /// subclassing the <see cref="Standard.ArcGISIntermediateImmutableArrayBuilder<T>">ArcGISIntermediateImmutableArrayBuilder<T></see> and provide "overloaded" methods that would accept that value type
    /// directly. This would eliminate the overhead of instantiating these <see cref="">Element</see> objects.
    /// </remarks>
    /// <seealso cref="Standard.ArcGISIntermediateImmutableArray<T>">ArcGISIntermediateImmutableArray<T></seealso>
    /// <seealso cref="Standard.ArcGISIntermediateMutableArray<T>">ArcGISIntermediateMutableArray<T></seealso>
    /// <since>1.0.0</since>
    [StructLayout(LayoutKind.Sequential)]
    internal partial class ArcGISIntermediateImmutableArrayBuilder<T>
    {
        #region Properties
        /// <summary>
        /// The type of the values this <see cref="Standard.ArcGISIntermediateImmutableArrayBuilder<T>">ArcGISIntermediateImmutableArrayBuilder<T></see> holds.
        /// </summary>
        /// <since>1.0.0</since>
        internal ArcGISElementType ValueType
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_ArrayBuilder_getValueType(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Methods
        /// <summary>
        /// Add a value to the <see cref="Standard.ArcGISIntermediateImmutableArrayBuilder<T>">ArcGISIntermediateImmutableArrayBuilder<T></see>.
        /// </summary>
        /// <param name="value">The value that is to be added.</param>
        /// <since>1.0.0</since>
        internal void Add(T value)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localValue = Unity.Convert.ToArcGISElement(value);
            
            PInvoke.RT_ArrayBuilder_add(Handle, localValue.Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        
        /// <summary>
        /// Creates a <see cref="Standard.ArcGISIntermediateImmutableArray<T>">ArcGISIntermediateImmutableArray<T></see> containing the values added to this <see cref="Standard.ArcGISIntermediateImmutableArrayBuilder<T>">ArcGISIntermediateImmutableArrayBuilder<T></see>.
        /// </summary>
        /// <remarks>
        /// The order of the values in the returned <see cref="Standard.ArcGISIntermediateImmutableArray<T>">ArcGISIntermediateImmutableArray<T></see> matches the order in which the
        /// values were added to this <see cref="Standard.ArcGISIntermediateImmutableArrayBuilder<T>">ArcGISIntermediateImmutableArrayBuilder<T></see>.
        /// 
        /// This call empties this <see cref="Standard.ArcGISIntermediateImmutableArrayBuilder<T>">ArcGISIntermediateImmutableArrayBuilder<T></see> of values, but leaves it in a valid
        /// (re-usable) state.
        /// </remarks>
        /// <returns>
        /// A <see cref="Standard.ArcGISIntermediateImmutableArray<T>">ArcGISIntermediateImmutableArray<T></see> containing the values added to this <see cref="Standard.ArcGISIntermediateImmutableArrayBuilder<T>">ArcGISIntermediateImmutableArrayBuilder<T></see>.
        /// </returns>
        /// <since>1.0.0</since>
        internal Unity.ArcGISImmutableArray<T> MoveToArray()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_ArrayBuilder_moveToArray(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            Unity.ArcGISImmutableArray<T> localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Unity.ArcGISImmutableArray<T>(localResult);
            }
            
            return localLocalResult;
        }
        #endregion // Methods
        
        #region Internal Members
        internal ArcGISIntermediateImmutableArrayBuilder(IntPtr handle) => Handle = handle;
        
        ~ArcGISIntermediateImmutableArrayBuilder()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                PInvoke.RT_ArrayBuilder_destroy(Handle, errorHandler);
                
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
        internal static extern ArcGISElementType RT_ArrayBuilder_getValueType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArrayBuilder_add(IntPtr handle, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_ArrayBuilder_moveToArray(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_ArrayBuilder_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}