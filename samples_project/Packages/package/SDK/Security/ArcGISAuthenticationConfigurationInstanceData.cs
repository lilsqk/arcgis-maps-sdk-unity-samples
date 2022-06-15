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
// Unity
using System.Collections.Generic;

namespace Esri.ArcGISMapsSDK.Security
{
	enum ArcGISAuthenticationConfigurationType
	{
		OAuth = 0,
	}

	[System.Serializable]
	public class ArcGISAuthenticationConfigurationInstanceData
	{
		public string Name;
		public string ClientID;
		public string RedirectURI;

		private ArcGISAuthenticationConfigurationType Type;
	}

	[System.Serializable]
	public class OAuthAuthenticationConfigurationMapping
	{
		public const int NoConfigurationIndex = -1;
		public int ConfigurationIndex = NoConfigurationIndex;
	}

	public static class OAuthAuthenticationConfigurationMappingExtensions
	{
		public static List<ArcGISAuthenticationConfigurationInstanceData> Configurations { get; set; }
	}
}
