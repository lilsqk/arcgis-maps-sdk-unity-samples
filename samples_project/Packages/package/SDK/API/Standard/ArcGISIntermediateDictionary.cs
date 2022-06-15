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
    /// Defines a dictionary object.
    /// </summary>
    /// <remarks>
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
    internal partial class ArcGISIntermediateDictionary<Key, Value>
    {
        #region Constructors
        /// <summary>
        /// Creates a dictionary. This allocates memory that must be deleted.
        /// </summary>
        /// <param name="keyType">The type of the dictionary's key.</param>
        /// <param name="valueType">The type of the dictionary's value.</param>
        /// <since>1.0.0</since>
        internal ArcGISIntermediateDictionary(ArcGISElementType keyType, ArcGISElementType valueType)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            Handle = PInvoke.RT_Dictionary_create(keyType, valueType, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// <summary>
        /// The type of the keys this dictionary holds.
        /// </summary>
        /// <since>1.0.0</since>
        internal ArcGISElementType KeyType
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Dictionary_getKeyType(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// <summary>
        /// Determines the number of values in the dictionary.
        /// </summary>
        /// <remarks>
        /// The number of values in the dictionary. If an error occurs a 0 will be returned.
        /// </remarks>
        /// <since>1.0.0</since>
        internal ulong Size
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Dictionary_getSize(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult.ToUInt64();
            }
        }
        
        /// <summary>
        /// The type of the values this dictionary holds.
        /// </summary>
        /// <since>1.0.0</since>
        internal ArcGISElementType ValueType
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Dictionary_getValueType(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Methods
        /// <summary>
        /// Get a value for a specific key.
        /// </summary>
        /// <remarks>
        /// Retrieves the value for the specified key.
        /// </remarks>
        /// <param name="key">The key which you want to get the value.</param>
        /// <returns>
        /// The value for the key requested. A null if an error occurs or the key doesn't exist.
        /// </returns>
        /// <since>1.0.0</since>
        internal Value At(Key key)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localKey = Unity.Convert.ToArcGISElement(key);
            
            var localResult = PInvoke.RT_Dictionary_at(Handle, localKey.Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            Standard.ArcGISElement localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Standard.ArcGISElement(localResult);
            }
            
            return Unity.Convert.FromArcGISElement<Value>(localLocalResult);
        }
        
        /// <summary>
        /// Does the dictionary contain a key.
        /// </summary>
        /// <remarks>
        /// Does the dictionary contain a specific key.
        /// </remarks>
        /// <param name="key">The key you want to find.</param>
        /// <returns>
        /// True if the key is in the dictionary otherwise false.
        /// </returns>
        /// <since>1.0.0</since>
        internal bool Contains(Key key)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localKey = Unity.Convert.ToArcGISElement(key);
            
            var localResult = PInvoke.RT_Dictionary_contains(Handle, localKey.Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// <summary>
        /// Returns true if the two dictionaries are equal, false otherwise.
        /// </summary>
        /// <param name="dictionary2">The second dictionary.</param>
        /// <returns>
        /// Returns true if the two dictionaries are equal, false otherwise.
        /// </returns>
        /// <since>1.0.0</since>
        internal bool Equals(Unity.ArcGISDictionary<Key, Value> dictionary2)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localDictionary2 = dictionary2.Handle;
            
            var localResult = PInvoke.RT_Dictionary_equals(Handle, localDictionary2, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// <summary>
        /// Returns the type that the value of a given key should be, if the dictionary instance supports it.  Otherwise, <see cref="Standard.ArcGISElementType.Variant">ArcGISElementType.Variant</see> will be returned.
        /// </summary>
        /// <param name="key">The key you want to now the value type for.</param>
        /// <returns>
        /// An <see cref="Standard.ArcGISElementType">ArcGISElementType</see> value, or <see cref="Standard.ArcGISElementType.Variant">ArcGISElementType.Variant</see> if an error occurs or the dictionary does not support the type lookup.
        /// </returns>
        /// <since>1.0.0</since>
        internal ArcGISElementType GetTypeForKey(Key key)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localKey = Unity.Convert.ToArcGISElement(key);
            
            var localResult = PInvoke.RT_Dictionary_getTypeForKey(Handle, localKey.Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// <summary>
        /// Insert a value for a given key in the dictionary.
        /// </summary>
        /// <remarks>
        /// Insert a value at a specified key position to the dictionary.
        /// </remarks>
        /// <param name="key">The key position which you want to insert the value.</param>
        /// <param name="value">The value that is to be added.</param>
        /// <since>1.0.0</since>
        internal void Insert(Key key, Value value)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localKey = Unity.Convert.ToArcGISElement(key);
            var localValue = Unity.Convert.ToArcGISElement(value);
            
            PInvoke.RT_Dictionary_insert(Handle, localKey.Handle, localValue.Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        
        /// <summary>
        /// Determines if there are any values in the dictionary.
        /// </summary>
        /// <remarks>
        /// Check if the dictionary object has any values in it.
        /// </remarks>
        /// <returns>
        /// true if the dictionary object contains no values otherwise false. Will return true if an error occurs.
        /// </returns>
        /// <since>1.0.0</since>
        internal bool IsEmpty()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Dictionary_isEmpty(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// <summary>
        /// Remove a value at a specific key position in the dictionary.
        /// </summary>
        /// <remarks>
        /// Remove a value at a specific position in the dictionary.
        /// </remarks>
        /// <param name="key">The key position which you want to remove the value.</param>
        /// <since>1.0.0</since>
        internal void Remove(Key key)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localKey = Unity.Convert.ToArcGISElement(key);
            
            PInvoke.RT_Dictionary_remove(Handle, localKey.Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        
        /// <summary>
        /// Remove all values from the dictionary.
        /// </summary>
        /// <since>1.0.0</since>
        internal void RemoveAll()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            PInvoke.RT_Dictionary_removeAll(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        
        /// <summary>
        /// Replace a value at a specific key position in the dictionary.
        /// </summary>
        /// <param name="key">The key position which you want to replace the value.</param>
        /// <param name="newValue">The new value.</param>
        /// <since>1.0.0</since>
        internal void Replace(Key key, Value newValue)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localKey = Unity.Convert.ToArcGISElement(key);
            var localNewValue = Unity.Convert.ToArcGISElement(newValue);
            
            PInvoke.RT_Dictionary_replace(Handle, localKey.Handle, localNewValue.Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        #endregion // Methods
        
        #region Internal Members
        internal ArcGISIntermediateDictionary(IntPtr handle) => Handle = handle;
        
        ~ArcGISIntermediateDictionary()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                PInvoke.RT_Dictionary_destroy(Handle, errorHandler);
                
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
        internal static extern IntPtr RT_Dictionary_create(ArcGISElementType keyType, ArcGISElementType valueType, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern ArcGISElementType RT_Dictionary_getKeyType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern UIntPtr RT_Dictionary_getSize(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern ArcGISElementType RT_Dictionary_getValueType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_Dictionary_at(IntPtr handle, IntPtr key, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Dictionary_contains(IntPtr handle, IntPtr key, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Dictionary_equals(IntPtr handle, IntPtr dictionary2, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern ArcGISElementType RT_Dictionary_getTypeForKey(IntPtr handle, IntPtr key, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_Dictionary_insert(IntPtr handle, IntPtr key, IntPtr value, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Dictionary_isEmpty(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_Dictionary_remove(IntPtr handle, IntPtr key, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_Dictionary_removeAll(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_Dictionary_replace(IntPtr handle, IntPtr key, IntPtr newValue, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_Dictionary_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}