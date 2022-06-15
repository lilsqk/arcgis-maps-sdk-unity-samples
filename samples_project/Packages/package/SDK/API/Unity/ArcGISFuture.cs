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
	public class ArcGISFuture
	{
		private Standard.ArcGISIntermediateFuture<object> intermediateFuture;

		internal ArcGISFuture(IntPtr handle)
		{
			intermediateFuture = new Standard.ArcGISIntermediateFuture<object>(handle);
		}

		public object Get()
		{
			return intermediateFuture.Get();
		}

		public Standard.ArcGISFutureCompletedEvent TaskCompleted
		{
			get
			{
				return intermediateFuture.TaskCompleted;
			}
			set
			{
				intermediateFuture.TaskCompleted = value;
			}
		}
	}

	public class ArcGISFuture<T>
	{
		private Standard.ArcGISIntermediateFuture<T> intermediateFuture;

		internal ArcGISFuture(IntPtr handle)
		{
			intermediateFuture = new Standard.ArcGISIntermediateFuture<T>(handle);
		}

		public T Get()
		{
			return intermediateFuture.Get();
		}

		public Standard.ArcGISFutureCompletedEvent TaskCompleted
		{
			get
			{
				return intermediateFuture.TaskCompleted;
			}
			set
			{
				intermediateFuture.TaskCompleted = value;
			}
		}
	}
}
