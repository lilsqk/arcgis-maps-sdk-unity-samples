// COPYRIGHT 1995-2021 ESRI
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
using Esri.ArcGISMapsSDK.Utils.Math;
using System;

namespace Esri.ArcGISMapsSDK.Utils.GeoCoord
{
	// This is the equivalent for LonLatAlt and is used to avoid instancing API entities
	[Serializable]
	public struct ArcGISRotation
	{
		public double Heading;
		public double Pitch;
		public double Roll;

		public ArcGISRotation(double heading, double pitch, double roll)
		{
			Heading = heading;
			Pitch = pitch;
			Roll = roll;
		}

		public override int GetHashCode()
		{
			return Heading.GetHashCode() ^ (Pitch.GetHashCode() << 2) ^ (Roll.GetHashCode() >> 2);
		}

		public override bool Equals(object o)
		{
			const double epsilon = 1e-11;
			if (o != null && o.GetType().Equals(this.GetType()))
			{
				var rotator = (ArcGISRotation)o;
				return System.Math.Abs(rotator.Heading - Heading) < epsilon &&
						System.Math.Abs(rotator.Pitch - Pitch) < epsilon &&
						System.Math.Abs(rotator.Roll - Roll) < epsilon;
			}
			return false;
		}

		public static bool operator ==(ArcGISRotation lhs, ArcGISRotation rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(ArcGISRotation lhs, ArcGISRotation rhs)
		{
			return !lhs.Equals(rhs);
		}

		public static ArcGISRotation Normalize(ArcGISRotation rotator)
		{
			ArcGISRotation outputRotator;

			outputRotator.Heading = MathUtils.NormalizeAngleDegrees(rotator.Heading);
			outputRotator.Pitch = MathUtils.NormalizeAngleDegrees(rotator.Pitch);
			outputRotator.Roll = MathUtils.NormalizeAngleDegrees(rotator.Roll);

			return outputRotator;
		}
	}
}
