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
    /// Defines a dynamic array that provides callbacks when elements are added or removed.
    /// </summary>
    /// <remarks>
    /// The vector holds <see cref="">Element</see> objects. Use <see cref="Standard.ArcGISIntermediateMutableArray<T>.ValueType">ArcGISIntermediateMutableArray<T>.ValueType</see> to determine what type of <see cref="">Element</see>
    /// objects are being stored in the vector. A <see cref="">Element</see> that has been retrieved from the vector can be converted
    /// to its underlying type by calling (for example) int32_t int_value = RT_Element_getValueAsInt32(element_handle, error_handler);
    /// 
    /// There are a couple of performance optimizations to consider with the generic collection type:
    /// 1.  The <see cref="">Element</see> object can be reused for multiple calls while adding/inserting/updating values within
    /// the collection.  So you can instantiate one <see cref="">Element</see> and iteratively change its value and call
    /// add/update/insert on the collection to modify the values of the collection.
    /// 2.  If the overhead of creating these <see cref="">Element</see> does become too much for a specific value type, then
    /// consider subclassing the collection type and provide "overloaded" methods that would accept that value type
    /// directly.  This would eliminate the overhead of instantiating these <see cref="">Element</see> objects.
    /// </remarks>
    /// <since>1.0.0</since>
    [StructLayout(LayoutKind.Sequential)]
    internal partial class ArcGISIntermediateMutableArray<T>
    {
        #region Constructors
        /// <summary>
        /// Creates a vector. This allocates memory that must be deleted.
        /// </summary>
        /// <param name="valueType">The type of the values this vector holds.</param>
        /// <since>1.0.0</since>
        internal ArcGISIntermediateMutableArray(ArcGISElementType valueType)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_Vector_create(valueType, errorHandler);
            
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
                
                var localResult = PInvoke.RT_Vector_getSize(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult.ToUInt64();
            }
        }
        
        /// <summary>
        /// The type of the values this vector holds.
        /// </summary>
        /// <since>1.0.0</since>
        internal ArcGISElementType ValueType
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Vector_getValueType(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Methods
        /// <summary>
        /// Appends a vector to a vector.
        /// </summary>
        /// <param name="vector2">A vector object that contains the values to append.</param>
        /// <returns>
        /// The new size of vector_1. Max value of size_t if an error occurs.
        /// </returns>
        /// <since>1.0.0</since>
        internal ulong Add(Unity.ArcGISMutableArray<T> vector2)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localVector2 = vector2.Handle;
            
            var localResult = PInvoke.RT_Vector_addArray(Handle, localVector2, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult.ToUInt64();
        }
        
        /// <summary>
        /// Add a value to the vector.
        /// </summary>
        /// <param name="value">The value that is to be added.</param>
        /// <returns>
        /// The position of the value. Max value of size_t if an error occurs.
        /// </returns>
        /// <since>1.0.0</since>
        internal ulong Add(T value)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localValue = Unity.Convert.ToArcGISElement(value);
            
            var localResult = PInvoke.RT_Vector_add(Handle, localValue.Handle, errorHandler);
            
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
        internal T At(ulong position)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localPosition = new UIntPtr(position);
            
            var localResult = PInvoke.RT_Vector_at(Handle, localPosition, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            Standard.ArcGISElement localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Standard.ArcGISElement(localResult);
            }
            
            return Unity.Convert.FromArcGISElement<T>(localLocalResult);
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
        internal bool Contains(T value)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localValue = Unity.Convert.ToArcGISElement(value);
            
            var localResult = PInvoke.RT_Vector_contains(Handle, localValue.Handle, errorHandler);
            
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
        internal bool Equals(Unity.ArcGISMutableArray<T> vector2)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localVector2 = vector2.Handle;
            
            var localResult = PInvoke.RT_Vector_equals(Handle, localVector2, errorHandler);
            
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
        internal T First()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Vector_first(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            Standard.ArcGISElement localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Standard.ArcGISElement(localResult);
            }
            
            return Unity.Convert.FromArcGISElement<T>(localLocalResult);
        }
        
        /// <summary>
        /// Retrieves the position of the given value in the vector.
        /// </summary>
        /// <param name="value">The value you want to find.</param>
        /// <returns>
        /// The position of the value in the vector, Max value of size_t otherwise.
        /// </returns>
        /// <since>1.0.0</since>
        internal ulong IndexOf(T value)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localValue = Unity.Convert.ToArcGISElement(value);
            
            var localResult = PInvoke.RT_Vector_indexOf(Handle, localValue.Handle, errorHandler);
            
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
        internal void Insert(ulong position, T value)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localPosition = new UIntPtr(position);
            var localValue = Unity.Convert.ToArcGISElement(value);
            
            PInvoke.RT_Vector_insert(Handle, localPosition, localValue.Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        
        /// <summary>
        /// Determines if there are any values in the vector.
        /// </summary>
        /// <remarks>
        /// Check if the vector object has any values in it.
        /// </remarks>
        /// <returns>
        /// true if the vector object contains no values otherwise false. Will return true if an error occurs.
        /// </returns>
        /// <since>1.0.0</since>
        internal bool IsEmpty()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Vector_isEmpty(Handle, errorHandler);
            
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
        internal T Last()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Vector_last(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            Standard.ArcGISElement localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Standard.ArcGISElement(localResult);
            }
            
            return Unity.Convert.FromArcGISElement<T>(localLocalResult);
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
            
            PInvoke.RT_Vector_move(Handle, localOldPosition, localNewPosition, errorHandler);
            
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
            
            var localResult = PInvoke.RT_Vector_npos(errorHandler);
            
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
            
            PInvoke.RT_Vector_remove(Handle, localPosition, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        
        /// <summary>
        /// Remove all values from the vector.
        /// </summary>
        /// <since>1.0.0</since>
        internal void RemoveAll()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            PInvoke.RT_Vector_removeAll(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        #endregion // Methods
        
        #region Events
        /// <summary>
        /// Callback invoked when an element has been added to the vector.
        /// </summary>
        /// <since>1.0.0</since>
        internal ArcGISMutableArrayElementAddedEvent<T> ElementAdded
        {
            get
            {
                return _elementAddedHandler.Delegate;
            }
            set
            {
                if (_elementAddedHandler.Delegate == value)
                {
                    return;
                }
                
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                if (value != null)
                {
                    _elementAddedHandler.Delegate = value;
                    
                    PInvoke.RT_Vector_setElementAddedCallback(Handle, ArcGISMutableArrayElementAddedEventHandler<T>.HandlerFunction, _elementAddedHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_Vector_setElementAddedCallback(Handle, null, _elementAddedHandler.UserData, errorHandler);
                    
                    _elementAddedHandler.Dispose();
                }
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
            }
        }
        
        /// <summary>
        /// Callback invoked when an element has been removed from the vector.
        /// </summary>
        /// <since>1.0.0</since>
        internal ArcGISMutableArrayElementRemovedEvent<T> ElementRemoved
        {
            get
            {
                return _elementRemovedHandler.Delegate;
            }
            set
            {
                if (_elementRemovedHandler.Delegate == value)
                {
                    return;
                }
                
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                if (value != null)
                {
                    _elementRemovedHandler.Delegate = value;
                    
                    PInvoke.RT_Vector_setElementRemovedCallback(Handle, ArcGISMutableArrayElementRemovedEventHandler<T>.HandlerFunction, _elementRemovedHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_Vector_setElementRemovedCallback(Handle, null, _elementRemovedHandler.UserData, errorHandler);
                    
                    _elementRemovedHandler.Dispose();
                }
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
            }
        }
        #endregion // Events
        
        #region Internal Members
        internal ArcGISIntermediateMutableArray(IntPtr handle) => Handle = handle;
        
        ~ArcGISIntermediateMutableArray()
        {
            if (Handle != IntPtr.Zero)
            {
                if (_elementAddedHandler.Delegate != null)
                {
                    PInvoke.RT_Vector_setElementAddedCallback(Handle, null, _elementAddedHandler.UserData, IntPtr.Zero);
                    
                    _elementAddedHandler.Dispose();
                }
                
                if (_elementRemovedHandler.Delegate != null)
                {
                    PInvoke.RT_Vector_setElementRemovedCallback(Handle, null, _elementRemovedHandler.UserData, IntPtr.Zero);
                    
                    _elementRemovedHandler.Dispose();
                }
                
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                PInvoke.RT_Vector_destroy(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
            }
        }
        
        internal IntPtr Handle { get; set; }
        
        internal ArcGISMutableArrayElementAddedEventHandler<T> _elementAddedHandler = new ArcGISMutableArrayElementAddedEventHandler<T>();
        
        internal ArcGISMutableArrayElementRemovedEventHandler<T> _elementRemovedHandler = new ArcGISMutableArrayElementRemovedEventHandler<T>();
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_Vector_create(ArcGISElementType valueType, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern UIntPtr RT_Vector_getSize(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern ArcGISElementType RT_Vector_getValueType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern UIntPtr RT_Vector_addArray(IntPtr handle, IntPtr vector2, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern UIntPtr RT_Vector_add(IntPtr handle, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_Vector_at(IntPtr handle, UIntPtr position, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Vector_contains(IntPtr handle, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Vector_equals(IntPtr handle, IntPtr vector2, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_Vector_first(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern UIntPtr RT_Vector_indexOf(IntPtr handle, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_Vector_insert(IntPtr handle, UIntPtr position, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Vector_isEmpty(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_Vector_last(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_Vector_move(IntPtr handle, UIntPtr oldPosition, UIntPtr newPosition, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern UIntPtr RT_Vector_npos(IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_Vector_remove(IntPtr handle, UIntPtr position, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_Vector_removeAll(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_Vector_setElementAddedCallback(IntPtr handle, ArcGISMutableArrayElementAddedEventInternal elementAdded, IntPtr userData, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_Vector_setElementRemovedCallback(IntPtr handle, ArcGISMutableArrayElementRemovedEventInternal elementRemoved, IntPtr userData, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_Vector_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}