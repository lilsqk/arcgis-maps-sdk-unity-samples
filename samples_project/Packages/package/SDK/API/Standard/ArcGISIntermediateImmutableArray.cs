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
    /// Defines a immutable dynamic array.
    /// </summary>
    /// <remarks>
    /// The array holds <see cref="">Element</see> objects. Use <see cref="Standard.ArcGISIntermediateImmutableArray<T>.ValueType">ArcGISIntermediateImmutableArray<T>.ValueType</see> to determine what type of <see cref="">Element</see>
    /// objects are being stored in the array. A <see cref="">Element</see> that has been retrieved from the array can be converted
    /// to its underlying type by calling (for example) int32_t int_value = RT_Element_getValueAsInt32(element_handle, error_handler);
    /// </remarks>
    /// <since>1.0.0</since>
    [StructLayout(LayoutKind.Sequential)]
    internal partial class ArcGISIntermediateImmutableArray<T>
    {
        #region Properties
        /// <summary>
        /// The type of the array.
        /// </summary>
        /// <remarks>
        /// The type of the array object.
        /// </remarks>
        /// <seealso cref="Standard.ArcGISArrayType">ArcGISArrayType</seealso>
        /// <since>1.0.0</since>
        internal ArcGISArrayType ObjectType
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Array_getObjectType(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// <summary>
        /// Determines the number of values in the array.
        /// </summary>
        /// <remarks>
        /// The number of values in the array. If an error occurs a 0 will be returned.
        /// </remarks>
        /// <since>1.0.0</since>
        internal ulong Size
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Array_getSize(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult.ToUInt64();
            }
        }
        
        /// <summary>
        /// The type of the values this array holds.
        /// </summary>
        /// <since>1.0.0</since>
        internal ArcGISElementType ValueType
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Array_getValueType(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Methods
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
        internal T At(ulong position)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localPosition = new UIntPtr(position);
            
            var localResult = PInvoke.RT_Array_at(Handle, localPosition, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            Standard.ArcGISElement localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Standard.ArcGISElement(localResult);
            }
            
            return Unity.Convert.FromArcGISElement<T>(localLocalResult);
        }
        
        /// <summary>
        /// Does the array contain the given value.
        /// </summary>
        /// <remarks>
        /// Does the array contain a specific value.
        /// </remarks>
        /// <param name="value">The value you want to find.</param>
        /// <returns>
        /// True if the value is in the array otherwise false.
        /// </returns>
        /// <since>1.0.0</since>
        internal bool Contains(T value)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localValue = Unity.Convert.ToArcGISElement(value);
            
            var localResult = PInvoke.RT_Array_contains(Handle, localValue.Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// <summary>
        /// Creates a <see cref="Standard.ArcGISIntermediateImmutableArrayBuilder<T>">ArcGISIntermediateImmutableArrayBuilder<T></see>.
        /// </summary>
        /// <param name="valueType">The type of the values the returned <see cref="Standard.ArcGISIntermediateImmutableArrayBuilder<T>">ArcGISIntermediateImmutableArrayBuilder<T></see> holds.</param>
        /// <returns>
        /// A <see cref="Standard.ArcGISIntermediateImmutableArrayBuilder<T>">ArcGISIntermediateImmutableArrayBuilder<T></see>
        /// </returns>
        /// <seealso cref="Standard.ArcGISIntermediateImmutableArrayBuilder<T>">ArcGISIntermediateImmutableArrayBuilder<T></seealso>
        /// <since>1.0.0</since>
        internal static ArcGISIntermediateImmutableArrayBuilder<T> CreateBuilder(ArcGISElementType valueType)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Array_createBuilder(valueType, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            ArcGISIntermediateImmutableArrayBuilder<T> localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new ArcGISIntermediateImmutableArrayBuilder<T>(localResult);
            }
            
            return localLocalResult;
        }
        
        /// <summary>
        /// Returns true if the two arrays are equal, false otherwise.
        /// </summary>
        /// <param name="array2">The second array.</param>
        /// <returns>
        /// Returns true if the two arrays are equal, false otherwise.
        /// </returns>
        /// <since>1.0.0</since>
        internal bool Equals(Unity.ArcGISImmutableArray<T> array2)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localArray2 = array2.Handle;
            
            var localResult = PInvoke.RT_Array_equals(Handle, localArray2, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// <summary>
        /// Get the first value in the array.
        /// </summary>
        /// <returns>
        /// The value, <see cref="">Element</see>, at the position requested.
        /// </returns>
        /// <since>1.0.0</since>
        internal T First()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Array_first(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            Standard.ArcGISElement localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Standard.ArcGISElement(localResult);
            }
            
            return Unity.Convert.FromArcGISElement<T>(localLocalResult);
        }
        
        /// <summary>
        /// Retrieves the position of the given value in the array.
        /// </summary>
        /// <param name="value">The value you want to find.</param>
        /// <returns>
        /// The position of the value in the array, or the max value of size_t otherwise.
        /// </returns>
        /// <since>1.0.0</since>
        internal ulong IndexOf(T value)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localValue = Unity.Convert.ToArcGISElement(value);
            
            var localResult = PInvoke.RT_Array_indexOf(Handle, localValue.Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult.ToUInt64();
        }
        
        /// <summary>
        /// Determines if there are any values in the array.
        /// </summary>
        /// <remarks>
        /// Check if the array object has any values in it.
        /// </remarks>
        /// <returns>
        /// true if the  array object contains no values otherwise false. Will return true if an error occurs.
        /// </returns>
        /// <since>1.0.0</since>
        internal bool IsEmpty()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Array_isEmpty(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// <summary>
        /// Get the last value in the array.
        /// </summary>
        /// <returns>
        /// The value, <see cref="">Element</see>, at the position requested.
        /// </returns>
        /// <since>1.0.0</since>
        internal T Last()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Array_last(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            Standard.ArcGISElement localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Standard.ArcGISElement(localResult);
            }
            
            return Unity.Convert.FromArcGISElement<T>(localLocalResult);
        }
        #endregion // Methods
        
        #region Internal Members
        internal ArcGISIntermediateImmutableArray(IntPtr handle) => Handle = handle;
        
        ~ArcGISIntermediateImmutableArray()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                PInvoke.RT_Array_destroy(Handle, errorHandler);
                
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
        internal static extern ArcGISArrayType RT_Array_getObjectType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern UIntPtr RT_Array_getSize(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern ArcGISElementType RT_Array_getValueType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_Array_at(IntPtr handle, UIntPtr position, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Array_contains(IntPtr handle, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_Array_createBuilder(ArcGISElementType valueType, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Array_equals(IntPtr handle, IntPtr array2, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_Array_first(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern UIntPtr RT_Array_indexOf(IntPtr handle, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Array_isEmpty(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_Array_last(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_Array_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}