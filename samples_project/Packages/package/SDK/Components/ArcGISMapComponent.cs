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
using Esri.ArcGISMapsSDK.Memory;
using Esri.ArcGISMapsSDK.Security;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.ArcGISMapsSDK.Utils.Math;
using Esri.GameEngine;
using Esri.GameEngine.Extent;
using Esri.GameEngine.Geometry;
using Esri.GameEngine.Layers;
using Esri.GameEngine.Layers.Base;
using Esri.GameEngine.Map;
using Esri.GameEngine.Security;
using Esri.GameEngine.View;
using Esri.HPFramework;
using Esri.Unity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace Esri.ArcGISMapsSDK.Components
{
	public enum MapExtentShapes
	{
		Square = 0,
		Rectangle = 1,
		Circle = 2,
	};

	public enum BasemapTypes
	{
		Basemap = 0,
		ImageLayer = 1,
	}

	// This needs to be kept in parity with ArcGISLayerType
	public enum LayerTypes
	{
		[InspectorName("ArcGIS Image Layer")]
		ArcGISImageLayer = 0,
		[InspectorName("ArcGIS 3DObject Scene Layer")]
		ArcGIS3DObjectSceneLayer = 1,
		[InspectorName("ArcGIS Integrated Mesh Layer")]
		ArcGISIntegratedMeshLayer = 2,
		[InspectorName(null)]
		ArcGISUnsupportedLayer = 4,
		[InspectorName("")]
		ArcGISUnknownLayer = 5,
	}

	[System.Serializable]
	public struct ArcGISLayerInstanceData
	{
		public string Name;
		public LayerTypes Type;
		public string Source;
		public float Opacity;
		public bool IsVisible;
		public OAuthAuthenticationConfigurationMapping Authentication;

		public override bool Equals(object o)
		{
			var mapLayer = (ArcGISLayerInstanceData)o;
			bool authMatch = mapLayer.Authentication == Authentication;
			if (mapLayer.Authentication != null && Authentication != null)
			{
				authMatch = mapLayer.Authentication.ConfigurationIndex == Authentication.ConfigurationIndex;
			}
			return mapLayer.Name == Name
				&& mapLayer.Type == Type
				&& mapLayer.Source == Source
				&& mapLayer.IsVisible == IsVisible
				&& mapLayer.Opacity == Opacity
				&& authMatch;
		}

		public override int GetHashCode()
		{
			return Type.GetHashCode() ^ (Source.GetHashCode() << 2) ^ (Opacity.GetHashCode() >> 2);
		}

		public static bool operator !=(ArcGISLayerInstanceData lhs, ArcGISLayerInstanceData rhs)
		{
			return !lhs.Equals(rhs);
		}

		public static bool operator ==(ArcGISLayerInstanceData lhs, ArcGISLayerInstanceData rhs)
		{
			return lhs.Equals(rhs);
		}
	}

	[System.Serializable]
	public struct ArcGISExtentInstanceData
	{
		public ArcGISPoint GeographicCenter;
		public MapExtentShapes ExtentShape;
		public double2 ShapeDimensions;

		public override bool Equals(object o)
		{
			var mapExtent = (ArcGISExtentInstanceData)o;
			return GeographicCenter == mapExtent.GeographicCenter && ExtentShape == mapExtent.ExtentShape && ShapeDimensions.Equals(mapExtent.ShapeDimensions);
		}

		public override int GetHashCode()
		{
			return GeographicCenter.GetHashCode() ^ (ExtentShape.GetHashCode() << 2) ^ (ShapeDimensions.GetHashCode() >> 2);
		}

		public static bool operator ==(ArcGISExtentInstanceData lhs, ArcGISExtentInstanceData rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(ArcGISExtentInstanceData lhs, ArcGISExtentInstanceData rhs)
		{
			return !lhs.Equals(rhs);
		}
	}

	[DisallowMultipleComponent]
	[ExecuteAlways]
	[RequireComponent(typeof(HPRoot))]
	[AddComponentMenu("ArcGIS Maps SDK/ArcGIS Map")]
	public class ArcGISMapComponent : MonoBehaviour, IMemorySystem
	{
		[SerializeField]
		private string apiKey = "";
		public string APIKey
		{
			get => apiKey;
			set
			{
				apiKey = value != null ? value : "";
			}
		}

		[SerializeField]
		private string basemap = "";
		public string Basemap
		{
			get => basemap;
			set
			{
				basemap = value != null ? value : "";
			}
		}

		[SerializeField]
		private BasemapTypes basemapType = BasemapTypes.Basemap;
		public BasemapTypes BasemapType
		{
			get => basemapType;
			set => basemapType = value;
		}

		[SerializeField]
		private OAuthAuthenticationConfigurationMapping basemapAuthentication;
		public OAuthAuthenticationConfigurationMapping BasemapAuthentication => basemapAuthentication;

		[SerializeField]
		private string elevation = "";
		public string Elevation
		{
			get => elevation;
			set
			{
				elevation = value != null ? value : "";
			}
		}

		[SerializeField]
		private bool enableExtent = false;
		public bool EnableExtent
		{
			get => enableExtent;
			set
			{
				enableExtent = value;
				UpdateMap();
			}
		}

		[SerializeField]
		private OAuthAuthenticationConfigurationMapping elevationAuthentication;
		public OAuthAuthenticationConfigurationMapping ElevationAuthentication => elevationAuthentication;

		[SerializeField]
		private ArcGISExtentInstanceData extent = new ArcGISExtentInstanceData() { GeographicCenter = new ArcGISPoint(0, 0, 0, ArcGISSpatialReference.WGS84()) };
		public ArcGISExtentInstanceData Extent
		{
			get => extent;
			set
			{
				extent = value != null ? value : new ArcGISExtentInstanceData();
			}
		}

		[SerializeField]
		private List<ArcGISLayerInstanceData> layers = new List<ArcGISLayerInstanceData>();
		public List<ArcGISLayerInstanceData> Layers
		{
			get
			{
				return layers;
			}
			set
			{
				layers = value != null ? value : new List<ArcGISLayerInstanceData>();
			}
		}

		private IMemorySystemHandler memorySystemHandler;
		public IMemorySystemHandler MemorySystemHandler
		{
			get
			{
				if (memorySystemHandler == null)
				{
#if UNITY_ANDROID
					memorySystemHandler = new AndroidDefaultMemorySystemHandler();
#else
					memorySystemHandler = new DefaultMemorySystemHandler();
#endif
				}

				return memorySystemHandler;
			}
			set
			{
				if (memorySystemHandler != value)
				{
					memorySystemHandler = value;

					if (memorySystemHandler != null && view != null)
					{
						InitializeMemorySystem();
					}
				}
			}
		}

		[SerializeField]
		private ArcGISPoint originPosition = new ArcGISPoint(0, 0, 0, ArcGISSpatialReference.WGS84());
		public ArcGISPoint OriginPosition
		{
			get => originPosition;
			set
			{
				if (originPosition != value)
				{
					originPosition = value;
					OnOriginPositionChanged();
				}
			}
		}

		[SerializeField]
		private ArcGISMapType mapType = ArcGISMapType.Local;
		public ArcGISMapType MapType
		{
			get => mapType;
			set
			{
				if (mapType != value)
				{
					mapType = value;
					OnMapTypeChanged();
				}
			}
		}

#if UNITY_EDITOR
		[SerializeField]
		private bool editorModeEnabled = true;
		public bool EditorModeEnabled
		{
			get => editorModeEnabled;
			set
			{
				if (editorModeEnabled != value && !Application.isPlaying)
				{
					editorModeEnabled = value;

					if (!editorModeEnabled)
					{
						map = view.Map;
						view.Map = null;
						view = null;
					}
					else if (map != null)
					{
						View.Map = map;
						map = null;
					}

					if (EditorModeEnabledChanged != null)
					{
						EditorModeEnabledChanged();
					}
				}
			}
		}

		[SerializeField]
		private bool dataFetchWithSceneView = true;
		public bool DataFetchWithSceneView
		{
			get => dataFetchWithSceneView;

			set
			{
				if (dataFetchWithSceneView != value && !Application.isPlaying)
				{
					dataFetchWithSceneView = value;

					if (!value)
					{
						editorCameraComponent.EditorViewEnabled = value;
						EnableMainCameraView(!value);
					}
					else
					{
						EnableMainCameraView(!value);
						editorCameraComponent.EditorViewEnabled = value;
					}
				}
			}
		}

		[SerializeField]
		private bool rebaseWithSceneView = false;
		public bool RebaseWithSceneView
		{
			get => rebaseWithSceneView;

			set
			{
				if (rebaseWithSceneView != value && !Application.isPlaying)
				{
					rebaseWithSceneView = value;
					editorCameraComponent.WorldRepositionEnabled = value;
				}
			}
		}
#endif

		[SerializeField]
		private bool meshCollidersEnabled = false;
		public bool MeshCollidersEnabled
		{
			get => meshCollidersEnabled;
			set
			{
				if (meshCollidersEnabled != value)
				{
					meshCollidersEnabled = value;

					if (MeshCollidersEnabledChanged != null)
					{
						MeshCollidersEnabledChanged();
					}
				}
			}
		}

		private ArcGISView view = null;
		public ArcGISView View
		{
			get
			{
				if (view == null)
				{
					view = new ArcGISView(ArcGISGameEngineType.Unity, Esri.GameEngine.MapView.ArcGISGlobeModel.Ellipsoid);

					view.SpatialReferenceChanged += () => internalHasChanged = true;

					InitializeMemorySystem();
				}

				return view;
			}
		}

		public double3 UniversePosition
		{
			get => hpRoot.RootUniversePosition;
			set
			{
				var tangentToWorld = View.GetENUReference(value).ToQuaterniond();

				universePosition = value;
				universeRotation = tangentToWorld.ToQuaternion();

				hpRoot.SetRootTR(universePosition, universeRotation);
				RootChanged.Invoke();
			}
		}

		public Quaternion UniverseRotation
		{
			get => hpRoot.RootUniverseRotation;
		}

		public double4x4 WorldMatrix
		{
			get
			{
				return hpRoot.WorldMatrix;
			}
		}

		[SerializeField]
		private List<ArcGISAuthenticationConfigurationInstanceData> configurations = new List<ArcGISAuthenticationConfigurationInstanceData>();
		public List<ArcGISAuthenticationConfigurationInstanceData> Configurations => configurations;

		private ArcGISMapType lastMapType = ArcGISMapType.Global;
		private ArcGISPoint lastOriginPosition;
		private bool lastEnableExtent;
		private string lastAPIKey = "";
		private string lastBasemap = "";
		private BasemapTypes lastBasemapType = BasemapTypes.Basemap;
		private string lastElevation = "";
		private ArcGISExtentInstanceData lastExtent = new ArcGISExtentInstanceData();
		private List<ArcGISLayerInstanceData> lastLayers = new List<ArcGISLayerInstanceData>();

		private GameObject rendererGO = null;

		public delegate void MapTypeChangedEventHandler();
		public delegate void EditorModeEnabledChangedEventHandler();

		public delegate void MeshCollidersEnabledChangedEventHandler();

		public event MapTypeChangedEventHandler MapTypeChanged;
		public event EditorModeEnabledChangedEventHandler EditorModeEnabledChanged;

		public event MeshCollidersEnabledChangedEventHandler MeshCollidersEnabledChanged;

		public UnityEvent RootChanged = new UnityEvent();

		private HPRoot hpRoot;
		private bool internalHasChanged = false;
		private double3 universePosition;
		private Quaternion universeRotation;

		private ArcGISMap map = null;

#if UNITY_EDITOR
		private ArcGISEditorCameraComponent editorCameraComponent = null;
#endif

		private void Awake()
		{
			if (originPosition != null && originPosition.IsValid)
			{
				// Ensure HPRoot is sync'd from geoPosition, rather than geoPosition being sync'd from HPRoot
				internalHasChanged = true;
			}

			if (rendererGO == null || !gameObject.GetComponentInChildren<ArcGISRendererComponent>())
			{
				rendererGO = new GameObject();
				rendererGO.name = "ArcGISRenderer";
				rendererGO.hideFlags = HideFlags.HideAndDontSave;
				rendererGO.transform.parent = transform;
				rendererGO.AddComponent<ArcGISRendererComponent>();
			}

			if (configurations != null)
			{
				OAuthAuthenticationConfigurationMappingExtensions.Configurations = configurations;
			}
		}

		private void InitializeMemorySystem()
		{
			MemorySystemHandler.Initialize(this);

			SetMemoryQuotas(MemorySystemHandler.GetMemoryQuotas());
		}

		private void OnEnable()
		{
			hpRoot = GetComponent<HPRoot>();

			if (rendererGO)
			{
				rendererGO.SetActive(true);
			}

			SyncPositionWithHPRoot();

#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				EnableMainCameraView(!dataFetchWithSceneView);

				// Avoid repeated element when ArcGISMapComponent is copied.
				var lastEditorCameraComponent = GetComponentInChildren<ArcGISEditorCameraComponent>();

				if (lastEditorCameraComponent)
				{
					DestroyImmediate(lastEditorCameraComponent.gameObject);
				}

				var editorCamera = new GameObject();
				editorCamera.name = "EditorCamera";
				editorCamera.hideFlags = HideFlags.HideAndDontSave;
				editorCamera.transform.SetParent(transform);
				editorCamera.SetActive(false);
				editorCameraComponent = editorCamera.AddComponent<ArcGISEditorCameraComponent>();
				editorCameraComponent.WorldRepositionEnabled = rebaseWithSceneView;
				editorCameraComponent.EditorViewEnabled = dataFetchWithSceneView;
				editorCamera.SetActive(true);
			}
			else
			{
#endif
				EnableMainCameraView(true);
#if UNITY_EDITOR
			}
#endif

			InitializeArcGISMap();
			
			Authenticate();
		}

		private void OnDisable()
		{
			if (rendererGO)
			{
				rendererGO.SetActive(false);
			}

#if UNITY_EDITOR
			if (!Application.isPlaying && editorCameraComponent)
			{
				EnableMainCameraView(true);
				DestroyImmediate(editorCameraComponent.gameObject);
			}
#endif
		}

		private void Start()
		{
			if (Application.isPlaying)
			{
				Authenticate();
			}
		}

		private void InitializeArcGISMap()
		{
			lastOriginPosition = null;
			lastBasemap = "";
			lastElevation = "";
			lastAPIKey = "";
			lastExtent = new ArcGISExtentInstanceData();
			lastLayers.Clear();

			var arcGISMap = new ArcGISMap(MapType);
			View.Map = arcGISMap;
		}

		private void Update()
		{
			SyncPositionWithHPRoot();

			if (ShouldEditorComponentBeUpdated())
			{
				UpdateMap();
			}
		}

		internal void UpdateMap()
		{
			if (lastAPIKey != APIKey)
			{
				InitializeArcGISMap();
				lastAPIKey = APIKey;
			}

			if (MapType != lastMapType)
			{
				OnMapTypeChanged();
				lastMapType = MapType;
			}

			if (originPosition != lastOriginPosition)
			{
				OnOriginPositionChanged();
				lastOriginPosition = originPosition;
			}

			if (Basemap != lastBasemap)
			{
				UpdateBasemap();
				lastBasemap = Basemap;
			}

			if (BasemapType != lastBasemapType)
			{
				UpdateBasemap();
				lastBasemapType = BasemapType;
			}

			if (Elevation != lastElevation)
			{
				View.Map.Elevation = new ArcGISMapElevation(new Esri.GameEngine.Elevation.ArcGISImageElevationSource(Elevation, "Elevation", APIKey));
				lastElevation = Elevation;
			}

			if (enableExtent != lastEnableExtent || extent != lastExtent)
			{
				ApplyExtentUpdate();
				lastEnableExtent = enableExtent;
			}

			UpdateLayers(apiKey, lastLayers, layers);

			lastLayers = Layers.ToList();
		}

		private void UpdateBasemap()
		{
			if (!string.IsNullOrEmpty(Basemap))
			{
				if (BasemapType == BasemapTypes.ImageLayer)
				{
					if (APIKey == "" && (Basemap == "https://ibasemaps-api.arcgis.com/arcgis/rest/services/World_Imagery/MapServer" ||
						Basemap == "https://ibasemaps-api.arcgis.com/arcgis/rest/services/Ocean/World_Ocean_Base/MapServer"))
					{
						Debug.LogError("An API Key must be set on the ArcGISMapComponent for content to load");
					}

					View.Map.Basemap = new ArcGISBasemap(Basemap, ArcGISLayerType.ArcGISImageLayer, APIKey);
				}
				else
				{
					View.Map.Basemap = new ArcGISBasemap(Basemap, APIKey);
				}
			}
			else
			{
				View.Map.Basemap = new ArcGISBasemap();
			}
		}

		private void UpdateLayers(string APIKey, List<ArcGISLayerInstanceData> oldLayers, List<ArcGISLayerInstanceData> newLayers)
		{
			var currentLayers = oldLayers.ToList();

			// Remove old ones
			foreach (var layer in oldLayers)
			{
				if (!newLayers.Contains(layer))
				{
					var index = currentLayers.IndexOf(layer);

					View.Map.Layers.Remove((ulong)index);

					currentLayers.RemoveAt(index);
				}
			}

			// Add new ones
			for (int i = 0; i < newLayers.Count; i++)
			{
				var layer = newLayers[i];

				if (!oldLayers.Contains(layer))
				{
					var newLayer = CreateArcGISLayerFromDefinition(layer, APIKey);

					if (newLayer != null)
					{
						View.Map.Layers.Insert((ulong)i, newLayer);
					}

					currentLayers.Insert(i, layer);
				}
			}

			// Swap elements to get the same order
			for (int i = 0; i < Mathf.Min(newLayers.Count, oldLayers.Count); i++)
			{
				if (newLayers[i] != currentLayers[i])
				{
					int index = currentLayers.IndexOf(newLayers[i]);
					View.Map.Layers.Move((ulong)index, (ulong)i);
				}
			}
		}

		public ArcGISLayer CreateArcGISLayerFromDefinition(ArcGISLayerInstanceData layerDefinition, string APIKey)
		{
			ArcGISLayer layer = null;

			var opacity = Mathf.Max(Mathf.Min(layerDefinition.Opacity, 1.0f), 0.0f);

			if (layerDefinition.Type == LayerTypes.ArcGIS3DObjectSceneLayer)
			{
				layer = new ArcGIS3DObjectSceneLayer(layerDefinition.Source, layerDefinition.Name, opacity, layerDefinition.IsVisible, APIKey);
			}
			else if (layerDefinition.Type == LayerTypes.ArcGISImageLayer)
			{
				layer = new ArcGISImageLayer(layerDefinition.Source, layerDefinition.Name, opacity, layerDefinition.IsVisible, APIKey);
			}
			else if (layerDefinition.Type == LayerTypes.ArcGISIntegratedMeshLayer)
			{
				layer = new ArcGISIntegratedMeshLayer(layerDefinition.Source, layerDefinition.Name, opacity, layerDefinition.IsVisible, APIKey);
			}

			return layer;
		}

		private void Authenticate()
		{
			ArcGISAuthenticationManager.AuthenticationConfigurations.Clear();

			foreach (var config in configurations)
			{
				ArcGISAuthenticationConfiguration authenticationConfiguration;

				authenticationConfiguration = new ArcGISOAuthAuthenticationConfiguration(config.ClientID.Trim(), "", config.RedirectURI.Trim());

				if (basemapAuthentication.ConfigurationIndex == configurations.IndexOf(config))
				{
					ArcGISAuthenticationManager.AuthenticationConfigurations.Add(basemap, authenticationConfiguration);
				}
				else if (elevationAuthentication.ConfigurationIndex == configurations.IndexOf(config))
				{
					ArcGISAuthenticationManager.AuthenticationConfigurations.Add(elevation, authenticationConfiguration);
				}
				else
				{
					foreach (var layer in Layers)
					{
						if (layer.Authentication.ConfigurationIndex == configurations.IndexOf(config))
						{
							ArcGISAuthenticationManager.AuthenticationConfigurations.Add(layer.Source, authenticationConfiguration);
							break;
						}
					}
				}
			}
		}

		private void ApplyExtentUpdate()
		{
			if (MapType == ArcGISMapType.Local)
			{
				if (enableExtent && IsExtentDefinitionValid(extent))
				{
					View.Map.ClippingArea = CreateArcGISExtentFromDefinition(extent);
					lastExtent = extent;
				}
				else
				{
					View.Map.ClippingArea = null;
					lastExtent = extent;
				}
			}
		}
		public void EnableMainCameraView(bool enable)
		{
			var arcGISCamerasInThisView = GetComponentsInChildren<ArcGISCameraComponent>(true);

			ArcGISCameraComponent mainMapCamera = null;

			foreach (var camera in arcGISCamerasInThisView)
			{
				if (!camera.GetComponent<ArcGISEditorCameraComponent>())
				{
					mainMapCamera = camera;
					break;
				}
			}

			if (mainMapCamera)
			{
				mainMapCamera.enabled = enable;

				if (mainMapCamera.gameObject.GetComponent<ArcGISRebaseComponent>())
				{
					mainMapCamera.gameObject.GetComponent<ArcGISRebaseComponent>().enabled = enable;
				}
			}
		}

		private ArcGISExtent CreateArcGISExtentFromDefinition(ArcGISExtentInstanceData Extent)
		{
			var center = new ArcGISPoint(Extent.GeographicCenter.X, Extent.GeographicCenter.Y, Extent.GeographicCenter.Z, Extent.GeographicCenter.SpatialReference);

			ArcGISExtent extent;

			switch (Extent.ExtentShape)
			{
				case MapExtentShapes.Rectangle:
					extent = new ArcGISExtentRectangle(center, Extent.ShapeDimensions.x, Extent.ShapeDimensions.y);
					break;
				case MapExtentShapes.Square:
					extent = new ArcGISExtentRectangle(center, Extent.ShapeDimensions.x, Extent.ShapeDimensions.x);
					break;
				case MapExtentShapes.Circle:
					extent = new ArcGISExtentCircle(center, Extent.ShapeDimensions.x);
					break;
				default:
					extent = new ArcGISExtentRectangle(center, Extent.ShapeDimensions.x, Extent.ShapeDimensions.y);
					break;
			}

			return extent;
		}

		bool IsExtentDefinitionValid(ArcGISExtentInstanceData Extent)
		{
			if (Extent.ShapeDimensions.x < 0)
			{
				Extent.ShapeDimensions.x = 0;
			}

			if (Extent.ShapeDimensions.y < 0)
			{
				Extent.ShapeDimensions.y = 0;
			}

			return Extent.ShapeDimensions.x > 0 && (Extent.ExtentShape != MapExtentShapes.Rectangle || Extent.ShapeDimensions.y > 0);
		}

		public void NotifyLowMemoryWarning()
		{
			if (view != null)
			{
				view.HandleLowMemoryWarning();
			}
		}

		protected internal void OnOriginPositionChanged()
		{
			internalHasChanged = true;
		}

		private void OnMapTypeChanged()
		{
			InitializeArcGISMap();

			if (MapTypeChanged != null)
			{
				MapTypeChanged();
			}
		}

		private void PullChangesFromHPRoot()
		{
			universePosition = hpRoot.RootUniversePosition;
			universeRotation = hpRoot.RootUniverseRotation;

			this.originPosition = View.WorldToGeographic(universePosition);   // May result in NaN position
		}

		private void PushChangesToHPRoot()
		{
			var cartesianPosition = View.GeographicToWorld(originPosition);

			if (!cartesianPosition.IsValid())
			{
				// If the geographic position is not a valid cartesian position, ignore it
				PullChangesFromHPRoot(); // Reset position from current, assumed value, cartesian position

				return;
			}

			UniversePosition = cartesianPosition;
		}

		public void SetMemoryQuotas(MemoryQuotas memoryQuotas)
		{
			if (view != null)
			{
				view.SetMemoryQuotas(memoryQuotas.SystemMemory.GetValueOrDefault(-1L), memoryQuotas.VideoMemory.GetValueOrDefault(-1L));
			}
		}

		public bool ShouldEditorComponentBeUpdated()
		{
#if UNITY_EDITOR
			return Application.isPlaying || (editorModeEnabled && Application.isEditor);
#else
			return true;
#endif
		}

		internal void SyncPositionWithHPRoot()
		{
			if (View.SpatialReference == null)
			{
				// Defer until we have a spatial reference
				return;
			}

			if (internalHasChanged && originPosition.IsValid)
			{
				PushChangesToHPRoot();
			}
			else if (!originPosition.IsValid || !universePosition.Equals(hpRoot.RootUniversePosition) || universeRotation != hpRoot.RootUniverseRotation)
			{
				PullChangesFromHPRoot();
			}

			internalHasChanged = false;
		}

		public void CheckNumArcGISCameraComponentsEnabled()
		{
			var cameraComponents = GetComponentsInChildren<ArcGISCameraComponent>(false);

			int numEnabled = 0;

			foreach (var component in cameraComponents)
			{
				numEnabled += component.enabled ? 1 : 0;
			}

			if (numEnabled > 1)
			{
				Debug.LogWarning("Multiple ArcGISCameraComponents enabled at the same time!");
			}
		}

		// Position a gameObject to look at an extent
		// if there is no Camera component to get the fov from just default it to 90 degrees
		public bool ZoomToExtent(GameObject gameObject, Esri.GameEngine.Extent.ArcGISExtent extent)
		{
			var spatialReference = View.SpatialReference;

			if (spatialReference == null)
			{
				Debug.LogWarning("View must have a spatial reference to run zoom to layer");
				return false;
			}

			if (extent == null)
			{
				Debug.LogWarning("Extent cannot be null");
				return false;
			}

			var cameraPosition = extent.Center;
			double largeSide;
			if (Esri.GameEngine.Extent.ArcGISExtentType.ArcGISExtentRectangle == extent.ObjectType)
			{
				var rectangleExtent = extent as Esri.GameEngine.Extent.ArcGISExtentRectangle;
				largeSide = System.Math.Max(rectangleExtent.Width, rectangleExtent.Height);
			}
			else if (Esri.GameEngine.Extent.ArcGISExtentType.ArcGISExtentCircle == extent.ObjectType)
			{
				var rectangleExtent = extent as Esri.GameEngine.Extent.ArcGISExtentCircle;
				largeSide = rectangleExtent.Radius * 2;
			}
			else
			{
				Debug.LogWarning(extent.ObjectType.ToString() + "extent type is not supported");
				return false;
			}

			// Accounts for an internal error where the dimmension was being divided instead of multiplied
			if (largeSide < 0.01)
			{
				double earthCircumference = 40e6;
				double meterPerEquaterDegree = earthCircumference / 360;
				largeSide *= meterPerEquaterDegree * meterPerEquaterDegree;
			}

			// In global mode we can't see the entire layer if it is on a global scale,
			// so we just need to see the diameter of the planet
			if (MapType == Esri.GameEngine.Map.ArcGISMapType.Global)
			{
				var globeRadius = spatialReference.SpheroidData.MajorSemiAxis;
				largeSide = System.Math.Min(largeSide, 2 * globeRadius);
			}

			var cameraComponent = gameObject.GetComponent<Camera>();

			double radFOVAngle = Mathf.PI / 2; // 90 degrees
			if (cameraComponent != null)
			{
				radFOVAngle = cameraComponent.fieldOfView * MathUtils.DegreesToRadians;
			}

			var radHFOV = System.Math.Atan(System.Math.Tan(radFOVAngle / 2));
			var zOffset = 0.5 * largeSide / System.Math.Tan(radHFOV);

			var newPosition = new ArcGISPoint(cameraPosition.X,
											  cameraPosition.Y,
											  cameraPosition.Z + zOffset,
											  cameraPosition.SpatialReference);
			var newRotation = new ArcGISRotation(0, 0, 0);

			ArcGISLocationComponent.SetPositionAndRotation(gameObject, newPosition, newRotation);

			return true;
		}

		// Position a gameObject to look at a layer
		// if there is no Camera component to get the fov from just default it to 90 degrees
		public async Task<bool> ZoomToLayer(GameObject gameObject, Esri.GameEngine.Layers.Base.ArcGISLayer layer)
		{
			if (layer == null)
			{
				Debug.LogWarning("Invalid layer passed to zoom to layer");
				return false;
			}

			if (layer.LoadStatus != GameEngine.ArcGISLoadStatus.Loaded)
			{
				if (layer.LoadStatus == GameEngine.ArcGISLoadStatus.NotLoaded)
				{
					layer.Load();
				}
				else if (layer.LoadStatus != GameEngine.ArcGISLoadStatus.FailedToLoad)
				{
					layer.RetryLoad();
				}

				await Task.Run(() =>
				{
					while (layer.LoadStatus == GameEngine.ArcGISLoadStatus.Loading)
					{
					}
				});

				if (layer.LoadStatus == GameEngine.ArcGISLoadStatus.FailedToLoad)
				{
					Debug.LogWarning("Layer passed to zoom to layer must be loaded");
					return false;
				}
			}

			var layerExtent = layer.Extent;

			if (layerExtent == null)
			{
				Debug.LogWarning("The layer passed to zoom to layer does not have a valid extent");
				return false;
			}

			return ZoomToExtent(gameObject, layerExtent);
		}

		public Physics.ArcGISRaycastHit GetArcGISRaycastHit(RaycastHit raycastHit)
		{
			Physics.ArcGISRaycastHit output;
			output.featureId = -1;
			output.layer = null;

			var arcGISRendererComponent = rendererGO.GetComponent<ArcGISRendererComponent>();

			if (raycastHit.collider != null && arcGISRendererComponent != null)
			{
				var sceneComponent = arcGISRendererComponent.GetSceneComponentByGameObject(raycastHit.collider.gameObject);
				output.featureId = Physics.RaycastHelpers.GetFeatureIndexByTriangleIndex(raycastHit.collider.gameObject, raycastHit.triangleIndex);
				output.layer = View.Map?.FindLayerById(sceneComponent.LayerId);
			}

			return output;
		}

		public ArcGISPoint EngineToGeographic(Vector3 position)
		{
			var worldPosition = math.inverse(WorldMatrix).HomogeneousTransformPoint(position.ToDouble3());
			return View.WorldToGeographic(worldPosition);
		}
	}
}
