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

namespace Esri.Unity
{
	internal static partial class Convert
	{
		internal static object FromArcGISElement(Standard.ArcGISElement element)
		{
			var type = element.ObjectType;

			object result;

			switch (type)
			{
				case Standard.ArcGISElementType.Attribute:
					result = element.GetValueAsAttribute();
					break;
				case Standard.ArcGISElementType.Bool:
					result = element.GetValueAsBool();
					break;
				case Standard.ArcGISElementType.Float32:
					result = element.GetValueAsFloat32();
					break;
				case Standard.ArcGISElementType.Float64:
					result = element.GetValueAsFloat64();
					break;
				case Standard.ArcGISElementType.Int16:
					result = element.GetValueAsInt16();
					break;
				case Standard.ArcGISElementType.Int32:
					result = element.GetValueAsInt32();
					break;
				case Standard.ArcGISElementType.Int64:
					result = element.GetValueAsInt64();
					break;
				case Standard.ArcGISElementType.Int8:
					result = element.GetValueAsInt8();
					break;
				case Standard.ArcGISElementType.String:
					result = element.GetValueAsString();
					break;
				case Standard.ArcGISElementType.UInt16:
					result = element.GetValueAsUInt16();
					break;
				case Standard.ArcGISElementType.UInt32:
					result = element.GetValueAsUInt32();
					break;
				case Standard.ArcGISElementType.UInt64:
					result = element.GetValueAsUInt64();
					break;
				case Standard.ArcGISElementType.UInt8:
					result = element.GetValueAsUInt8();
					break;
				case Standard.ArcGISElementType.VisualizationAttribute:
					result = element.GetValueAsVisualizationAttribute();
					break;
				case Standard.ArcGISElementType.VisualizationAttributeDescription:
					result = element.GetValueAsVisualizationAttributeDescription();
					break;
				default:
					throw new InvalidCastException();
			}

			return result;
		}

		internal static T FromArcGISElement<T>(Standard.ArcGISElement element)
		{
			return (T)System.Convert.ChangeType(FromArcGISElement(element), typeof(T));
		}

		internal static Standard.ArcGISElement ToArcGISElement<T>(T value)
		{
			Standard.ArcGISElement result;

			switch (value)
			{
				case GameEngine.Security.ArcGISAuthenticationConfiguration converted:
					result = Standard.ArcGISElement.FromArcGISAuthenticationConfiguration(converted);
					break;
				case bool converted:
					result = Standard.ArcGISElement.FromBool(converted);
					break;
				case float converted:
					result = Standard.ArcGISElement.FromFloat32(converted);
					break;
				case double converted:
					result = Standard.ArcGISElement.FromFloat64(converted);
					break;
				case short converted:
					result = Standard.ArcGISElement.FromInt16(converted);
					break;
				case int converted:
					result = Standard.ArcGISElement.FromInt32(converted);
					break;
				case long converted:
					result = Standard.ArcGISElement.FromInt64(converted);
					break;
				case sbyte converted:
					result = Standard.ArcGISElement.FromInt8(converted);
					break;
				case string converted:
					result = Standard.ArcGISElement.FromArcGISString(converted);
					break;
				case ushort converted:
					result = Standard.ArcGISElement.FromUInt16(converted);
					break;
				case uint converted:
					result = Standard.ArcGISElement.FromUInt32(converted);
					break;
				case ulong converted:
					result = Standard.ArcGISElement.FromUInt64(converted);
					break;
				case byte converted:
					result = Standard.ArcGISElement.FromUInt8(converted);
					break;
				case GameEngine.Attributes.ArcGISVisualizationAttributeDescription converted:
					result = Standard.ArcGISElement.FromArcGISVisualizationAttributeDescription(converted);
					break;
				default:
					throw new InvalidCastException();
			}

			return result;
		}

		internal static Standard.ArcGISElementType ToArcGISElementType<T>()
		{
			if (typeof(T) == typeof(GameEngine.Security.ArcGISAuthenticationConfiguration))
			{
				return Standard.ArcGISElementType.ArcGISAuthenticationConfiguration;
			}
			else if (typeof(T) == typeof(bool))
			{
				return Standard.ArcGISElementType.Bool;
			}
			else if (typeof(T) == typeof(float))
			{
				return Standard.ArcGISElementType.Float32;
			}
			else if (typeof(T) == typeof(double))
			{
				return Standard.ArcGISElementType.Float64;
			}
			else if (typeof(T) == typeof(short))
			{
				return Standard.ArcGISElementType.Int16;
			}
			else if (typeof(T) == typeof(int))
			{
				return Standard.ArcGISElementType.Int32;
			}
			else if (typeof(T) == typeof(long))
			{
				return Standard.ArcGISElementType.Int64;
			}
			else if (typeof(T) == typeof(sbyte))
			{
				return Standard.ArcGISElementType.Int8;
			}
			else if (typeof(T) == typeof(string))
			{
				return Standard.ArcGISElementType.String;
			}
			else if (typeof(T) == typeof(ushort))
			{
				return Standard.ArcGISElementType.UInt16;
			}
			else if (typeof(T) == typeof(uint))
			{
				return Standard.ArcGISElementType.UInt32;
			}
			else if (typeof(T) == typeof(ulong))
			{
				return Standard.ArcGISElementType.UInt64;
			}
			else if (typeof(T) == typeof(byte))
			{
				return Standard.ArcGISElementType.UInt8;
			}
			else if (typeof(T) == typeof(GameEngine.Attributes.ArcGISVisualizationAttributeDescription))
			{
				return Standard.ArcGISElementType.VisualizationAttributeDescription;
			}
			else
			{
				throw new InvalidCastException();
			}
		}
	}
}
