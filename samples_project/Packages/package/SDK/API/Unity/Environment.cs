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
using Esri.GameEngine;
using System;

namespace Esri.Unity
{
	public static class Environment
	{
		public static void Initialize(string productName, string productVersion, string tempDirectory, string installDirectory)
		{
			var version = typeof(Environment).Assembly.GetName().Version.ToString();
			version = version.Substring(0, version.LastIndexOf('.'));

			ArcGISRuntimeEnvironment.SetRuntimeClient(Standard.ArcGISRuntimeClient.Unity, version);
			ArcGISRuntimeEnvironment.SetProductInfo(productName, productVersion);
			ArcGISRuntimeEnvironment.EnableBreakOnException(false);
			ArcGISRuntimeEnvironment.EnableLeakDetection(false);
			ArcGISRuntimeEnvironment.SetTempDirectory(tempDirectory);
			ArcGISRuntimeEnvironment.SetInstallDirectory(installDirectory);

			ArcGISRuntimeEnvironment.Error = delegate (Exception error)
			{
				UnityEngine.Debug.LogError(error.Message);
			};
		}
	}
}
