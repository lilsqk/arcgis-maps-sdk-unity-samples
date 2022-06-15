// COPYRIGHT 1995-2020 ESRI
// TRADE SECRETS: ESRI PROPRIETARY AND CONFIDENTIAL
// Unpublished material - all rights reserved under the
// Copyright Laws of the United States and applicable international
// laws, treaties, and conventions.
//
// For additional information, contact:
// Environmental Systems Research Institute, Inc.
// Attn: Contracts and Legal Services Department
// 380 New York Street
// Redlands, California, 92373
// USA
//
// email: contracts@esri.com
using System;
using System.Runtime.InteropServices;

namespace Esri.Standard
{
	[StructLayout(LayoutKind.Sequential)]
	internal partial class ArcGISElement
	{
		#region Constructors
		/// Creates an empty, unknown element type.
		///
		/// - Since: 100.0.0
		internal ArcGISElement()
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			Handle = PInvoke.RT_Element_create(errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);
		}
		#endregion // Constructors

		#region Properties
		/// The type that the element is holding.
		///
		/// - Since: 100.0.0
		internal ArcGISElementType ObjectType
		{
			get
			{
				var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

				var localResult = PInvoke.RT_Element_getObjectType(Handle, errorHandler);

				Unity.ArcGISErrorManager.CheckError(errorHandler);

				return localResult;
			}
		}
		#endregion // Properties

		#region Methods
		/// Removes the value from the element and sets the type to ArcGISElementType.none.
		///
		/// - Since: 100.0.0
		internal void Clear()
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			PInvoke.RT_Element_clear(Handle, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);
		}

		/// Tests if two element are equal.
		///
		/// - Parameter element2: The second element.
		/// - Returns: true if the two elements are equal, false otherwise.
		/// - Since: 100.0.0
		internal bool Equals(ArcGISElement element2)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localElement2 = element2.Handle;

			var localResult = PInvoke.RT_Element_equals(Handle, localElement2, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			return localResult;
		}

		/// Creates an element object from an authentication configuration.
		///
		/// - Parameter value: The ArcGISAuthenticationConfiguration.
		/// - Returns: A ArcGISElement.
		/// - Since: 100.11.0
		internal static ArcGISElement FromArcGISAuthenticationConfiguration(Esri.GameEngine.Security.ArcGISAuthenticationConfiguration value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localValue = value.Handle;

			var localResult = PInvoke.RT_Element_fromArcGISAuthenticationConfiguration(localValue, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			ArcGISElement localLocalResult = null;

			if (localResult != IntPtr.Zero)
			{
				localLocalResult = new ArcGISElement(localResult);
			}

			return localLocalResult;
		}

		/// Creates an element from an boolean value.
		///
		/// - Parameter value: The value.
		/// - Returns: A ArcGISElement.
		/// - Since: 100.0.0
		internal static ArcGISElement FromBool(bool value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_fromBool(value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			ArcGISElement localLocalResult = null;

			if (localResult != IntPtr.Zero)
			{
				localLocalResult = new ArcGISElement(localResult);
			}

			return localLocalResult;
		}

		/// Creates an element from a float32 value.
		///
		/// - Parameter value: The value.
		/// - Returns: A ArcGISElement.
		/// - Since: 100.0.0
		internal static ArcGISElement FromFloat32(float value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_fromFloat32(value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			ArcGISElement localLocalResult = null;

			if (localResult != IntPtr.Zero)
			{
				localLocalResult = new ArcGISElement(localResult);
			}

			return localLocalResult;
		}

		/// Creates an element from a float64 value.
		///
		/// - Parameter value: The value.
		/// - Returns: A ArcGISElement.
		/// - Since: 100.0.0
		internal static ArcGISElement FromFloat64(double value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_fromFloat64(value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			ArcGISElement localLocalResult = null;

			if (localResult != IntPtr.Zero)
			{
				localLocalResult = new ArcGISElement(localResult);
			}

			return localLocalResult;
		}

		/// Creates an element from an int16_t value.
		///
		/// - Parameter value: The value.
		/// - Returns: A ArcGISElement.
		/// - Since: 100.0.0
		internal static ArcGISElement FromInt16(short value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_fromInt16(value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			ArcGISElement localLocalResult = null;

			if (localResult != IntPtr.Zero)
			{
				localLocalResult = new ArcGISElement(localResult);
			}

			return localLocalResult;
		}

		/// Creates an element from an int32_t value.
		///
		/// - Parameter value: The value.
		/// - Returns: A ArcGISElement.
		/// - Since: 100.0.0
		internal static ArcGISElement FromInt32(int value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_fromInt32(value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			ArcGISElement localLocalResult = null;

			if (localResult != IntPtr.Zero)
			{
				localLocalResult = new ArcGISElement(localResult);
			}

			return localLocalResult;
		}

		/// Creates an element from an int64_t value.
		///
		/// - Parameter value: The value.
		/// - Returns: A ArcGISElement
		/// - Since: 100.0.0
		internal static ArcGISElement FromInt64(long value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_fromInt64(value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			ArcGISElement localLocalResult = null;

			if (localResult != IntPtr.Zero)
			{
				localLocalResult = new ArcGISElement(localResult);
			}

			return localLocalResult;
		}

		/// Creates an element from an int8_t value.
		///
		/// - Parameter value: The value.
		/// - Returns: A ArcGISElement.
		/// - Since: 100.0.0
		internal static ArcGISElement FromInt8(sbyte value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_fromInt8(value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			ArcGISElement localLocalResult = null;

			if (localResult != IntPtr.Zero)
			{
				localLocalResult = new ArcGISElement(localResult);
			}

			return localLocalResult;
		}

		/// Creates an element from a string value.
		///
		/// - Parameter value: The value.
		/// - Returns: A ArcGISElement.
		/// - Since: 100.0.0
		internal static ArcGISElement FromArcGISString(string value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_fromString(value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			ArcGISElement localLocalResult = null;

			if (localResult != IntPtr.Zero)
			{
				localLocalResult = new ArcGISElement(localResult);
			}

			return localLocalResult;
		}

		/// Creates an element from a uint16_t value.
		///
		/// - Parameter value: The value.
		/// - Returns: A ArcGISElement.
		/// - Since: 100.0.0
		internal static ArcGISElement FromUInt16(ushort value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_fromUInt16(value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			ArcGISElement localLocalResult = null;

			if (localResult != IntPtr.Zero)
			{
				localLocalResult = new ArcGISElement(localResult);
			}

			return localLocalResult;
		}

		/// Creates an element from a uint32_t value.
		///
		/// - Parameter value: The value.
		/// - Returns: A ArcGISElement.
		/// - Since: 100.0.0
		internal static ArcGISElement FromUInt32(uint value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_fromUInt32(value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			ArcGISElement localLocalResult = null;

			if (localResult != IntPtr.Zero)
			{
				localLocalResult = new ArcGISElement(localResult);
			}

			return localLocalResult;
		}

		/// Creates an element from a uint64_t value.
		///
		/// - Parameter value: The value.
		/// - Returns: A ArcGISElement.
		/// - Since: 100.0.0
		internal static ArcGISElement FromUInt64(ulong value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_fromUInt64(value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			ArcGISElement localLocalResult = null;

			if (localResult != IntPtr.Zero)
			{
				localLocalResult = new ArcGISElement(localResult);
			}

			return localLocalResult;
		}

		/// Creates an element from a uint8_t value.
		///
		/// - Parameter value: The value.
		/// - Returns: A ArcGISElement
		/// - Since: 100.0.0
		internal static ArcGISElement FromUInt8(byte value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_fromUInt8(value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			ArcGISElement localLocalResult = null;

			if (localResult != IntPtr.Zero)
			{
				localLocalResult = new ArcGISElement(localResult);
			}

			return localLocalResult;
		}

		/// Creates an element object from a visualization attribute description.
		///
		/// - Parameter value: The value.
		/// - Returns: A ArcGISElement
		/// - Since: 100.12.0
		internal static ArcGISElement FromArcGISVisualizationAttributeDescription(Esri.GameEngine.Attributes.ArcGISVisualizationAttributeDescription value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localValue = value.Handle;

			var localResult = PInvoke.RT_Element_fromVisualizationAttributeDescription(localValue, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			ArcGISElement localLocalResult = null;

			if (localResult != IntPtr.Zero)
			{
				localLocalResult = new ArcGISElement(localResult);
			}

			return localLocalResult;
		}

		/// Gets the value of an element as an attribute.
		///
		/// - Returns: A Attribute.
		/// - Since: 100.12.0
		internal GameEngine.Attributes.ArcGISAttribute GetValueAsAttribute()
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_getValueAsAttribute(Handle, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			GameEngine.Attributes.ArcGISAttribute localLocalResult = null;

			if (localResult != IntPtr.Zero)
			{
				localLocalResult = new GameEngine.Attributes.ArcGISAttribute(localResult);
			}

			return localLocalResult;
		}

		/// Gets the value of the element.
		///
		/// - Returns: An boolean. Returns false if an error occurs.
		/// - Since: 100.0.0
		internal bool GetValueAsBool()
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_getValueAsBool(Handle, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			return localResult;
		}

		/// Gets the value of the element.
		///
		/// - Returns: An float32.
		/// - Since: 100.0.0
		internal float GetValueAsFloat32()
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_getValueAsFloat32(Handle, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			return localResult;
		}

		/// Gets the value of the element.
		///
		/// - Returns: An float64.
		/// - Since: 100.0.0
		internal double GetValueAsFloat64()
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_getValueAsFloat64(Handle, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			return localResult;
		}

		/// Gets the value of the element.
		///
		/// - Returns: An int16_t.
		/// - Since: 100.0.0
		internal short GetValueAsInt16()
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_getValueAsInt16(Handle, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			return localResult;
		}

		/// Gets the value of the element.
		///
		/// - Returns: An int32_t.
		/// - Since: 100.0.0
		internal int GetValueAsInt32()
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_getValueAsInt32(Handle, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			return localResult;
		}

		/// Gets the value of the element.
		///
		/// - Returns: An int64_t.
		/// - Since: 100.0.0
		internal long GetValueAsInt64()
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_getValueAsInt64(Handle, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			return localResult;
		}

		/// Gets the value of the element.
		///
		/// - Returns: An int8_t.
		/// - Since: 100.0.0
		internal sbyte GetValueAsInt8()
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_getValueAsInt8(Handle, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			return localResult;
		}

		/// Gets the value of the element.
		///
		/// - Returns: An String.
		/// - Since: 100.0.0
		internal string GetValueAsString()
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_getValueAsString(Handle, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			return Unity.Convert.FromArcGISString(localResult);
		}

		/// Gets the value of the element.
		///
		/// - Returns: An uint16_t.
		/// - Since: 100.0.0
		internal ushort GetValueAsUInt16()
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_getValueAsUInt16(Handle, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			return localResult;
		}

		/// Gets the value of the element.
		///
		/// - Returns: An uint32_t.
		/// - Since: 100.0.0
		internal uint GetValueAsUInt32()
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_getValueAsUInt32(Handle, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			return localResult;
		}

		/// Gets the value of the element.
		///
		/// - Returns: An uint64_t.
		/// - Since: 100.0.0
		internal ulong GetValueAsUInt64()
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_getValueAsUInt64(Handle, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			return localResult;
		}

		/// Gets the value of the element.
		///
		/// - Returns: An uint8_t.
		/// - Since: 100.0.0
		internal byte GetValueAsUInt8()
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_getValueAsUInt8(Handle, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			return localResult;
		}

		/// Gets the value of an element as a visualization attribute.
		///
		/// - Returns: A VisualizationAttribute.
		/// - Since: 100.12.0
		internal GameEngine.Attributes.ArcGISVisualizationAttribute GetValueAsVisualizationAttribute()
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_getValueAsVisualizationAttribute(Handle, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			GameEngine.Attributes.ArcGISVisualizationAttribute localLocalResult = null;

			if (localResult != IntPtr.Zero)
			{
				localLocalResult = new GameEngine.Attributes.ArcGISVisualizationAttribute(localResult);
			}

			return localLocalResult;
		}

		/// Gets the value of an element as a visualization attribute description.
		///
		/// - Returns: A VisualizationAttributeDescription.
		/// - Since: 100.12.0
		internal GameEngine.Attributes.ArcGISVisualizationAttributeDescription GetValueAsVisualizationAttributeDescription()
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_getValueAsVisualizationAttributeDescription(Handle, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			GameEngine.Attributes.ArcGISVisualizationAttributeDescription localLocalResult = null;

			if (localResult != IntPtr.Zero)
			{
				localLocalResult = new GameEngine.Attributes.ArcGISVisualizationAttributeDescription(localResult);
			}

			return localLocalResult;
		}

		/// Returns true if the element doesn't have a value.
		///
		/// - Returns: true if the element doesn't have a value, false otherwise.
		/// - Since: 100.0.0
		internal bool IsEmpty()
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			var localResult = PInvoke.RT_Element_isEmpty(Handle, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);

			return localResult;
		}

		/// Sets the value of the element.
		///
		/// - Parameter value: The value.
		/// - Since: 100.0.0
		internal void SetValueFromBool(bool value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			PInvoke.RT_Element_setValueFromBool(Handle, value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);
		}

		/// Sets the value of the element.
		///
		/// - Parameter value: The value.
		/// - Since: 100.0.0
		internal void SetValueFromFloat32(float value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			PInvoke.RT_Element_setValueFromFloat32(Handle, value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);
		}

		/// Sets the value of the element.
		///
		/// - Parameter value: The value.
		/// - Since: 100.0.0
		internal void SetValueFromFloat64(double value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			PInvoke.RT_Element_setValueFromFloat64(Handle, value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);
		}

		/// Sets the value of the element.
		///
		/// - Parameter value: The value.
		/// - Since: 100.0.0
		internal void SetValueFromInt16(short value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			PInvoke.RT_Element_setValueFromInt16(Handle, value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);
		}

		/// Sets the value of the element.
		///
		/// - Parameter value: The value.
		/// - Since: 100.0.0
		internal void SetValueFromInt32(int value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			PInvoke.RT_Element_setValueFromInt32(Handle, value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);
		}

		/// Sets the value of the element.
		///
		/// - Parameter value: The value.
		/// - Since: 100.0.0
		internal void SetValueFromInt64(long value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			PInvoke.RT_Element_setValueFromInt64(Handle, value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);
		}

		/// Sets the value of the element.
		///
		/// - Parameter value: The value.
		/// - Since: 100.0.0
		internal void SetValueFromInt8(sbyte value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			PInvoke.RT_Element_setValueFromInt8(Handle, value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);
		}

		/// Sets the value of the element.
		///
		/// - Parameter value: The value.
		/// - Since: 100.0.0
		internal void SetValueFromArcGISString(string value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			PInvoke.RT_Element_setValueFromString(Handle, value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);
		}

		/// Sets the value of the element.
		///
		/// - Parameter value: The value.
		/// - Since: 100.0.0
		internal void SetValueFromUInt16(ushort value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			PInvoke.RT_Element_setValueFromUInt16(Handle, value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);
		}

		/// Sets the value of the element.
		///
		/// - Parameter value: The value.
		/// - Since: 100.0.0
		internal void SetValueFromUInt32(uint value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			PInvoke.RT_Element_setValueFromUInt32(Handle, value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);
		}

		/// Sets the value of the element.
		///
		/// - Parameter value: The value.
		/// - Since: 100.0.0
		internal void SetValueFromUInt64(ulong value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			PInvoke.RT_Element_setValueFromUInt64(Handle, value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);
		}

		/// Sets the value of the element.
		///
		/// - Parameter value: The value.
		/// - Since: 100.0.0
		internal void SetValueFromUInt8(byte value)
		{
			var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

			PInvoke.RT_Element_setValueFromUInt8(Handle, value, errorHandler);

			Unity.ArcGISErrorManager.CheckError(errorHandler);
		}
		#endregion // Methods

		#region Internal Members
		internal ArcGISElement(IntPtr handle) => Handle = handle;

		~ArcGISElement()
		{
			if (Handle != IntPtr.Zero)
			{
				var errorHandler = Unity.ArcGISErrorManager.CreateHandler();

				PInvoke.RT_Element_destroy(Handle, errorHandler);

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
		internal static extern IntPtr RT_Element_create(IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern ArcGISElementType RT_Element_getObjectType(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern void RT_Element_clear(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		[return: MarshalAs(UnmanagedType.I1)]
		internal static extern bool RT_Element_equals(IntPtr handle, IntPtr element2, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern IntPtr RT_Element_fromArcGISAuthenticationConfiguration(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern IntPtr RT_Element_fromBool([MarshalAs(UnmanagedType.I1)] bool value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern IntPtr RT_Element_fromFloat32(float value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern IntPtr RT_Element_fromFloat64(double value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern IntPtr RT_Element_fromInt16(short value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern IntPtr RT_Element_fromInt32(int value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern IntPtr RT_Element_fromInt64(long value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern IntPtr RT_Element_fromInt8(sbyte value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern IntPtr RT_Element_fromString([MarshalAs(UnmanagedType.LPStr)] string value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern IntPtr RT_Element_fromUInt16(ushort value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern IntPtr RT_Element_fromUInt32(uint value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern IntPtr RT_Element_fromUInt64(ulong value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern IntPtr RT_Element_fromUInt8(byte value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern IntPtr RT_Element_fromVisualizationAttributeDescription(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern IntPtr RT_Element_getValueAsAttribute(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		[return: MarshalAs(UnmanagedType.I1)]
		internal static extern bool RT_Element_getValueAsBool(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern float RT_Element_getValueAsFloat32(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern double RT_Element_getValueAsFloat64(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern short RT_Element_getValueAsInt16(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern int RT_Element_getValueAsInt32(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern long RT_Element_getValueAsInt64(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern sbyte RT_Element_getValueAsInt8(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern IntPtr RT_Element_getValueAsString(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern ushort RT_Element_getValueAsUInt16(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern uint RT_Element_getValueAsUInt32(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern ulong RT_Element_getValueAsUInt64(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern byte RT_Element_getValueAsUInt8(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern IntPtr RT_Element_getValueAsVisualizationAttribute(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern IntPtr RT_Element_getValueAsVisualizationAttributeDescription(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		[return: MarshalAs(UnmanagedType.I1)]
		internal static extern bool RT_Element_isEmpty(IntPtr handle, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern void RT_Element_setValueFromBool(IntPtr handle, [MarshalAs(UnmanagedType.I1)] bool value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern void RT_Element_setValueFromFloat32(IntPtr handle, float value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern void RT_Element_setValueFromFloat64(IntPtr handle, double value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern void RT_Element_setValueFromInt16(IntPtr handle, short value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern void RT_Element_setValueFromInt32(IntPtr handle, int value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern void RT_Element_setValueFromInt64(IntPtr handle, long value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern void RT_Element_setValueFromInt8(IntPtr handle, sbyte value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern void RT_Element_setValueFromString(IntPtr handle, [MarshalAs(UnmanagedType.LPStr)] string value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern void RT_Element_setValueFromUInt16(IntPtr handle, ushort value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern void RT_Element_setValueFromUInt32(IntPtr handle, uint value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern void RT_Element_setValueFromUInt64(IntPtr handle, ulong value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern void RT_Element_setValueFromUInt8(IntPtr handle, byte value, IntPtr errorHandler);

		[DllImport(Unity.Interop.Dll)]
		internal static extern void RT_Element_destroy(IntPtr handle, IntPtr errorHandle);
		#endregion // P-Invoke Declarations
	}
}
