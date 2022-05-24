// Copyright 2022 Esri.
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License.
// You may obtain a copy of the License at: http://www.apache.org/licenses/LICENSE-2.0
//
using Esri.ArcGISMapsSDK.Components;
using Esri.ArcGISMapsSDK.Security;
using UnityEngine;

[ExecuteAlways]
public class OAuthChallengeHandlersInitializer : MonoBehaviour
{
	private OAuthAuthenticationChallengeHandler oauthAuthenticationChallengeHandler;

	private void Initialize()
	{
#if (UNITY_ANDROID || UNITY_IOS || UNITY_WSA) && !UNITY_EDITOR
		oauthAuthenticationChallengeHandler = new MobileOAuthAuthenticationChallengeHandler();
#else
		oauthAuthenticationChallengeHandler = new DesktopOAuthAuthenticationChallengeHandler();
#endif

		Esri.ArcGISMapsSDK.Security.AuthenticationChallengeManager.OAuthChallengeHandler = oauthAuthenticationChallengeHandler;
	}

	private void Awake()
	{
		var mapComponent = GetComponent<ArcGISMapComponent>();
		if (mapComponent != null)
		{
			mapComponent.BeginOAuthWorkflow += new ArcGISMapComponent.AuthenticateEventHandler(Initialize);
		}

		if (Application.isPlaying)
		{
			Initialize();
		}
	}

	private void OnDestroy()
	{
		if (oauthAuthenticationChallengeHandler != null)
		{
			oauthAuthenticationChallengeHandler.Dispose();
		}
	}
}
