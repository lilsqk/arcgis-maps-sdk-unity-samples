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
namespace Esri.Standard
{
    /// <summary>
    /// The different types that a element can hold.
    /// </summary>
    /// <remarks>
    /// Each of the different supported element types. Can get the type by calling <see cref="">Element.objectType</see>.
    /// </remarks>
    /// <seealso cref="">Element</seealso>
    /// <seealso cref="">Element.objectType</seealso>
    /// <since>1.0.0</since>
    public enum ArcGISElementType
    {
        /// <summary>
        /// An <see cref="">ArcGISFeature</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ArcGISFeature = 0,
        
        /// <summary>
        /// An <see cref="">ArcGISFeatureServiceInfo</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ArcGISFeatureServiceInfo = 1,
        
        /// <summary>
        /// Deprecated. For internal use within C-API only.
        /// </summary>
        /// <since>1.0.0</since>
        ArcGISMapServiceInfo = 2,
        
        /// <summary>
        /// An ArcGIS sublayer object.
        /// </summary>
        /// <since>1.0.0</since>
        ArcGISSublayer = 3,
        
        /// <summary>
        /// An array.
        /// </summary>
        /// <since>1.0.0</since>
        Array = 4,
        
        /// <summary>
        /// An attachment value.
        /// </summary>
        /// <since>1.0.0</since>
        Attachment = 5,
        
        /// <summary>
        /// A network analyst attribute parameter value.
        /// </summary>
        /// <since>1.0.0</since>
        AttributeParameterValue = 6,
        
        /// <summary>
        /// An <see cref="">Basemap</see> object
        /// </summary>
        /// <since>1.0.0</since>
        Basemap = 7,
        
        /// <summary>
        /// A bookmark object.
        /// </summary>
        /// <since>1.0.0</since>
        Bookmark = 8,
        
        /// <summary>
        /// A boolean value.
        /// </summary>
        /// <since>1.0.0</since>
        Bool = 9,
        
        /// <summary>
        /// A buffer value.
        /// </summary>
        /// <since>1.0.0</since>
        Buffer = 10,
        
        /// <summary>
        /// A class break object.
        /// </summary>
        /// <since>1.0.0</since>
        ClassBreak = 11,
        
        /// <summary>
        /// An <see cref="">CodedValue</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        CodedValue = 12,
        
        /// <summary>
        /// A class closest facility parameters.
        /// </summary>
        /// <since>1.0.0</since>
        ClosestFacilityParameters = 13,
        
        /// <summary>
        /// A class closest facility result.
        /// </summary>
        /// <since>1.0.0</since>
        ClosestFacilityResult = 14,
        
        /// <summary>
        /// A class closest facility route.
        /// </summary>
        /// <since>1.0.0</since>
        ClosestFacilityRoute = 15,
        
        /// <summary>
        /// A class closest facility task.
        /// </summary>
        /// <since>1.0.0</since>
        ClosestFacilityTask = 16,
        
        /// <summary>
        /// A color object.
        /// </summary>
        /// <since>1.0.0</since>
        Color = 17,
        
        /// <summary>
        /// A network analyst cost attribute.
        /// </summary>
        /// <since>1.0.0</since>
        CostAttribute = 18,
        
        /// <summary>
        /// A date time value.
        /// </summary>
        /// <since>1.0.0</since>
        DateTime = 19,
        
        /// <summary>
        /// Element holds a dictionary.
        /// </summary>
        /// <since>1.0.0</since>
        Dictionary = 20,
        
        /// <summary>
        /// A network analyst direction event.
        /// </summary>
        /// <since>1.0.0</since>
        DirectionEvent = 21,
        
        /// <summary>
        /// A network analyst direction maneuver.
        /// </summary>
        /// <since>1.0.0</since>
        DirectionManeuver = 22,
        
        /// <summary>
        /// A network analyst direction message.
        /// </summary>
        /// <since>1.0.0</since>
        DirectionMessage = 23,
        
        /// <summary>
        /// An <see cref="">DistanceSymbolRange</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        DistanceSymbolRange = 24,
        
        /// <summary>
        /// A domain object.
        /// </summary>
        /// <since>1.0.0</since>
        Domain = 25,
        
        /// <summary>
        /// The result of an edit to an attachment.
        /// </summary>
        /// <since>1.0.0</since>
        EditResult = 26,
        
        /// <summary>
        /// An <see cref="">ElevationSource</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ElevationSource = 27,
        
        /// <summary>
        /// An <see cref="">EstimateTileCacheSizeResult</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        EstimateTileCacheSizeResult = 28,
        
        /// <summary>
        /// An <see cref="">ExportTileCacheTask</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ExportTileCacheTask = 29,
        
        /// <summary>
        /// An <see cref="">ExportTileCacheParameters</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ExportTileCacheParameters = 30,
        
        /// <summary>
        /// An <see cref="">ExtensionLicense</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ExtensionLicense = 31,
        
        /// <summary>
        /// A closest facility.
        /// </summary>
        /// <since>1.0.0</since>
        Facility = 32,
        
        /// <summary>
        /// A feature object.
        /// </summary>
        /// <since>1.0.0</since>
        Feature = 33,
        
        /// <summary>
        /// A feature collection object.
        /// </summary>
        /// <since>1.0.0</since>
        FeatureCollection = 34,
        
        /// <summary>
        /// A feature collection table object.
        /// </summary>
        /// <since>1.0.0</since>
        FeatureCollectionTable = 35,
        
        /// <summary>
        /// The result of an edit to a feature.
        /// </summary>
        /// <since>1.0.0</since>
        FeatureEditResult = 36,
        
        /// <summary>
        /// A feature query result object.
        /// </summary>
        /// <since>1.0.0</since>
        FeatureQueryResult = 37,
        
        /// <summary>
        /// An <see cref="">FeatureTable</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        FeatureTable = 38,
        
        /// <summary>
        /// A feature template object.
        /// </summary>
        /// <since>1.0.0</since>
        FeatureTemplate = 39,
        
        /// <summary>
        /// A feature type object.
        /// </summary>
        /// <since>1.0.0</since>
        FeatureType = 40,
        
        /// <summary>
        /// A field value.
        /// </summary>
        /// <since>1.0.0</since>
        Field = 41,
        
        /// <summary>
        /// A 32 bit float value.
        /// </summary>
        /// <since>1.0.0</since>
        Float32 = 42,
        
        /// <summary>
        /// A 64 bit float value.
        /// </summary>
        /// <since>1.0.0</since>
        Float64 = 43,
        
        /// <summary>
        /// An <see cref="">GenerateGeodatabaseParameters</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        GenerateGeodatabaseParameters = 44,
        
        /// <summary>
        /// Options for a layer when generating a geodatabase using the sync task.
        /// </summary>
        /// <since>1.0.0</since>
        GenerateLayerOption = 45,
        
        /// <summary>
        /// A result of geocode operation.
        /// </summary>
        /// <since>1.0.0</since>
        GeocodeResult = 46,
        
        /// <summary>
        /// A geodatabase.
        /// </summary>
        /// <since>1.0.0</since>
        Geodatabase = 47,
        
        /// <summary>
        /// A geodatabase feature table.
        /// </summary>
        /// <since>1.0.0</since>
        GeodatabaseFeatureTable = 48,
        
        /// <summary>
        /// An <see cref="">GeodatabaseSyncTask</see> object
        /// </summary>
        /// <since>1.0.0</since>
        GeodatabaseSyncTask = 49,
        
        /// <summary>
        /// A geometry value.
        /// </summary>
        /// <since>1.0.0</since>
        Geometry = 50,
        
        /// <summary>
        /// An <see cref="">GeoprocessingFeatureSet</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        GeoprocessingFeatureSet = 51,
        
        /// <summary>
        /// An <see cref="">GeoprocessingParameter</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        GeoprocessingParameter = 52,
        
        /// <summary>
        /// A graphic object.
        /// </summary>
        /// <since>1.0.0</since>
        Graphic = 53,
        
        /// <summary>
        /// A graphics overlay object.
        /// </summary>
        /// <since>1.0.0</since>
        GraphicsOverlay = 54,
        
        /// <summary>
        /// A GUID value.
        /// </summary>
        /// <since>1.0.0</since>
        GUID = 55,
        
        /// <summary>
        /// An object containing the results of an identify on a graphics overlay.
        /// </summary>
        /// <since>1.0.0</since>
        IdentifyGraphicsOverlayResult = 56,
        
        /// <summary>
        /// An object containing the results of an identify on a layer.
        /// </summary>
        /// <since>1.0.0</since>
        IdentifyLayerResult = 57,
        
        /// <summary>
        /// An <see cref="">IdInfo</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        IdInfo = 58,
        
        /// <summary>
        /// An image object.
        /// </summary>
        /// <since>1.0.0</since>
        Image = 59,
        
        /// <summary>
        /// A closest facility incident.
        /// </summary>
        /// <since>1.0.0</since>
        Incident = 60,
        
        /// <summary>
        /// A 16-bit integer value.
        /// </summary>
        /// <since>1.0.0</since>
        Int16 = 61,
        
        /// <summary>
        /// A 32-bit integer value.
        /// </summary>
        /// <since>1.0.0</since>
        Int32 = 62,
        
        /// <summary>
        /// A 64-bit integer value.
        /// </summary>
        /// <since>1.0.0</since>
        Int64 = 63,
        
        /// <summary>
        /// A 8-bit integer value.
        /// </summary>
        /// <since>1.0.0</since>
        Int8 = 64,
        
        /// <summary>
        /// An <see cref="">Job</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        Job = 65,
        
        /// <summary>
        /// A job message.
        /// </summary>
        /// <since>1.0.0</since>
        JobMessage = 66,
        
        /// <summary>
        /// A KML node object.
        /// </summary>
        /// <since>1.0.0</since>
        KMLNode = 67,
        
        /// <summary>
        /// A KML geometry object.
        /// </summary>
        /// <since>1.0.0</since>
        KMLGeometry = 68,
        
        /// <summary>
        /// A label class object.
        /// </summary>
        /// <since>1.0.0</since>
        LabelingInfo = 69,
        
        /// <summary>
        /// A layer object.
        /// </summary>
        /// <since>1.0.0</since>
        Layer = 70,
        
        /// <summary>
        /// A legend info object.
        /// </summary>
        /// <since>1.0.0</since>
        LegendInfo = 71,
        
        /// <summary>
        /// A tile info level of detail (LOD).
        /// </summary>
        /// <since>1.0.0</since>
        LevelOfDetail = 72,
        
        /// <summary>
        /// An <see cref="">LoadableImage</see> object. For internal use within C-API only.
        /// </summary>
        /// <since>1.0.0</since>
        LoadableImage = 73,
        
        /// <summary>
        /// An <see cref="">LocatorAttribute</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        LocatorAttribute = 74,
        
        /// <summary>
        /// An <see cref="">LocatorTask</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        LocatorTask = 75,
        
        /// <summary>
        /// An <see cref="">Map</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        Map = 76,
        
        /// <summary>
        /// An <see cref="">MobileBasemapLayer</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        MobileBasemapLayer = 77,
        
        /// <summary>
        /// An <see cref="">MobileMapPackage</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        MobileMapPackage = 78,
        
        /// <summary>
        /// An <see cref="">ModelSceneSymbol</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ModelSceneSymbol = 79,
        
        /// <summary>
        /// An order by enum value.
        /// </summary>
        /// <since>1.0.0</since>
        OrderBy = 80,
        
        /// <summary>
        /// An <see cref="">PictureMarkerSymbol</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        PictureMarkerSymbol = 81,
        
        /// <summary>
        /// A network analyst point barrier.
        /// </summary>
        /// <since>1.0.0</since>
        PointBarrier = 82,
        
        /// <summary>
        /// A network analyst polygon barrier.
        /// </summary>
        /// <since>1.0.0</since>
        PolygonBarrier = 83,
        
        /// <summary>
        /// A network analyst polyline barrier.
        /// </summary>
        /// <since>1.0.0</since>
        PolylineBarrier = 84,
        
        /// <summary>
        /// A <see cref="">Popup</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        Popup = 85,
        
        /// <summary>
        /// A popup field representing how a geo-element's attribute (field) should be displayed in a pop-up.
        /// </summary>
        /// <since>1.0.0</since>
        PopupField = 86,
        
        /// <summary>
        /// A popup media representing the media that is displayed in a pop-up for a geo-element.
        /// </summary>
        /// <since>1.0.0</since>
        PopupMedia = 87,
        
        /// <summary>
        /// A <see cref="">Portal</see> object. For internal use within C-API only.
        /// </summary>
        /// <since>1.0.0</since>
        Portal = 88,
        
        /// <summary>
        /// A <see cref="">PortalItem</see> object. For internal use within C-API only.
        /// </summary>
        /// <since>1.0.0</since>
        PortalItem = 89,
        
        /// <summary>
        /// A <see cref="">Raster</see> object. For internal use within C-API only.
        /// </summary>
        /// <since>1.0.0</since>
        Raster = 90,
        
        /// <summary>
        /// An attachment on a request object.
        /// </summary>
        /// <since>1.0.0</since>
        RequestAttachment = 91,
        
        /// <summary>
        /// A network analyst restriction attribute.
        /// </summary>
        /// <since>1.0.0</since>
        RestrictionAttribute = 92,
        
        /// <summary>
        /// A network analyst route.
        /// </summary>
        /// <since>1.0.0</since>
        Route = 93,
        
        /// <summary>
        /// A network analyst route parameters.
        /// </summary>
        /// <since>1.0.0</since>
        RouteParameters = 94,
        
        /// <summary>
        /// A network analyst route result.
        /// </summary>
        /// <since>1.0.0</since>
        RouteResult = 95,
        
        /// <summary>
        /// An <see cref="">RouteTask</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        RouteTask = 96,
        
        /// <summary>
        /// An <see cref="">Scene</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        Scene = 97,
        
        /// <summary>
        /// A service area facility.
        /// </summary>
        /// <since>1.0.0</since>
        ServiceAreaFacility = 98,
        
        /// <summary>
        /// A service area parameters.
        /// </summary>
        /// <since>1.0.0</since>
        ServiceAreaParameters = 99,
        
        /// <summary>
        /// A service area polygon.
        /// </summary>
        /// <since>1.0.0</since>
        ServiceAreaPolygon = 100,
        
        /// <summary>
        /// A service area polyline.
        /// </summary>
        /// <since>1.0.0</since>
        ServiceAreaPolyline = 101,
        
        /// <summary>
        /// A service area result.
        /// </summary>
        /// <since>1.0.0</since>
        ServiceAreaResult = 102,
        
        /// <summary>
        /// A service area task.
        /// </summary>
        /// <since>1.0.0</since>
        ServiceAreaTask = 103,
        
        /// <summary>
        /// A network analyst stop.
        /// </summary>
        /// <since>1.0.0</since>
        Stop = 104,
        
        /// <summary>
        /// A string value.
        /// </summary>
        /// <since>1.0.0</since>
        String = 105,
        
        /// <summary>
        /// A result of suggest operation.
        /// </summary>
        /// <since>1.0.0</since>
        SuggestResult = 106,
        
        /// <summary>
        /// An <see cref="">Surface</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        Surface = 107,
        
        /// <summary>
        /// A symbol object.
        /// </summary>
        /// <since>1.0.0</since>
        Symbol = 108,
        
        /// <summary>
        /// An <see cref="">SymbolStyle</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        SymbolStyle = 109,
        
        /// <summary>
        /// An <see cref="">SymbolStyleSearchParameters</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        SymbolStyleSearchParameters = 110,
        
        /// <summary>
        /// An <see cref="">SymbolStyleSearchResult</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        SymbolStyleSearchResult = 111,
        
        /// <summary>
        /// An <see cref="">SyncGeodatabaseParameters</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        SyncGeodatabaseParameters = 112,
        
        /// <summary>
        /// An <see cref="">SyncLayerOption</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        SyncLayerOption = 113,
        
        /// <summary>
        /// An <see cref="">SyncLayerResult</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        SyncLayerResult = 114,
        
        /// <summary>
        /// A <see cref="">TileCache</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        TileCache = 115,
        
        /// <summary>
        /// An <see cref="">TransportationNetworkDataset</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        TransportationNetworkDataset = 116,
        
        /// <summary>
        /// A travel mode.
        /// </summary>
        /// <since>1.0.0</since>
        TravelMode = 117,
        
        /// <summary>
        /// An unsigned 16-bit integer value.
        /// </summary>
        /// <since>1.0.0</since>
        UInt16 = 118,
        
        /// <summary>
        /// An unsigned 32-bit integer value.
        /// </summary>
        /// <since>1.0.0</since>
        UInt32 = 119,
        
        /// <summary>
        /// An unsigned 64-bit integer value.
        /// </summary>
        /// <since>1.0.0</since>
        UInt64 = 120,
        
        /// <summary>
        /// An unsigned 8-bit integer value.
        /// </summary>
        /// <since>1.0.0</since>
        UInt8 = 121,
        
        /// <summary>
        /// A unique value object.
        /// </summary>
        /// <since>1.0.0</since>
        UniqueValue = 122,
        
        /// <summary>
        /// An variant type.
        /// </summary>
        /// <since>1.0.0</since>
        Variant = 123,
        
        /// <summary>
        /// Element holds a vector.
        /// </summary>
        /// <since>1.0.0</since>
        Vector = 124,
        
        /// <summary>
        /// An <see cref="">VectorTileSourceInfo</see> object. For internal use within C-API only.
        /// </summary>
        /// <since>1.0.0</since>
        VectorTileSourceInfo = 125,
        
        /// <summary>
        /// An <see cref="">GeoprocessingParameterInfo</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        GeoprocessingParameterInfo = 126,
        
        /// <summary>
        /// An <see cref="">GeoprocessingTask</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        GeoprocessingTask = 127,
        
        /// <summary>
        /// An <see cref="">GeoprocessingParameters</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        GeoprocessingParameters = 128,
        
        /// <summary>
        /// An <see cref="">WMTSLayerInfo</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        WMTSLayerInfo = 129,
        
        /// <summary>
        /// An <see cref="">WMTSServiceInfo</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        WMTSServiceInfo = 130,
        
        /// <summary>
        /// An <see cref="">WMTSTileMatrix</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        WMTSTileMatrix = 131,
        
        /// <summary>
        /// An <see cref="">WMTSTileMatrixSet</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        WMTSTileMatrixSet = 132,
        
        /// <summary>
        /// An <see cref="">TileImageFormat</see> enum value.
        /// </summary>
        /// <since>1.0.0</since>
        TileImageFormat = 133,
        
        /// <summary>
        /// An <see cref="">OfflineMapTask</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        OfflineMapTask = 134,
        
        /// <summary>
        /// An <see cref="">ExportVectorTilesTask</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ExportVectorTilesTask = 135,
        
        /// <summary>
        /// An <see cref="">ExportVectorTilesParameters</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ExportVectorTilesParameters = 136,
        
        /// <summary>
        /// An <see cref="">ArcGISFeatureTable</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ArcGISFeatureTable = 137,
        
        /// <summary>
        /// An <see cref="">RelationshipInfo</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        RelationshipInfo = 138,
        
        /// <summary>
        /// An <see cref="">RelatedFeatureQueryResult</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        RelatedFeatureQueryResult = 139,
        
        /// <summary>
        /// An <see cref="">WMTSService</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        WMTSService = 140,
        
        /// <summary>
        /// A <see cref="Standard.ArcGISError">ArcGISError</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        Error = 141,
        
        /// <summary>
        /// A <see cref="">ServiceFeatureTable</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ServiceFeatureTable = 142,
        
        /// <summary>
        /// A <see cref="">GenerateOfflineMapParameters</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        GenerateOfflineMapParameters = 143,
        
        /// <summary>
        /// An <see cref="">PictureFillSymbol</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        PictureFillSymbol = 145,
        
        /// <summary>
        /// A <see cref="">OfflineCapability</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        OfflineCapability = 146,
        
        /// <summary>
        /// A <see cref="">OfflineCapability</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        OfflineMapCapabilities = 147,
        
        /// <summary>
        /// An <see cref="">RenderingRuleInfo</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        RenderingRuleInfo = 148,
        
        /// <summary>
        /// A <see cref="">LabelDefinition</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        LabelDefinition = 149,
        
        /// <summary>
        /// A <see cref="">RelationshipConstraintViolationType</see> value.
        /// </summary>
        /// <since>1.0.0</since>
        RelationshipConstraintViolation = 150,
        
        /// <summary>
        /// A <see cref="">OfflineMapSyncTask</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        OfflineMapSyncTask = 151,
        
        /// <summary>
        /// An <see cref="">OfflineMapSyncLayerResult</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        OfflineMapSyncLayerResult = 152,
        
        /// <summary>
        /// A <see cref="">PopupRelatedFeaturesSortOrder</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        PopupRelatedFeaturesSortOrder = 153,
        
        /// <summary>
        /// An <see cref="">StatisticDefinition</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        StatisticDefinition = 154,
        
        /// <summary>
        /// An <see cref="">StatisticsQueryResult</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        StatisticsQueryResult = 155,
        
        /// <summary>
        /// An <see cref="">StatisticRecord</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        StatisticRecord = 156,
        
        /// <summary>
        /// A <see cref="">KMLDataset</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        KMLDataset = 157,
        
        /// <summary>
        /// A <see cref="">PreplannedMapArea</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        PreplannedMapArea = 158,
        
        /// <summary>
        /// An <see cref="">WMSService</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        WMSService = 159,
        
        /// <summary>
        /// An <see cref="">WMSServiceInfo</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        WMSServiceInfo = 160,
        
        /// <summary>
        /// An <see cref="">WMSLayerInfo</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        WMSLayerInfo = 161,
        
        /// <summary>
        /// An <see cref="">MapServiceImageFormat</see> enum value.
        /// </summary>
        /// <since>1.0.0</since>
        MapServiceImageFormat = 162,
        
        /// <summary>
        /// An <see cref="GameEngine.Geometry.ArcGISSpatialReference">ArcGISSpatialReference</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        SpatialReference = 163,
        
        /// <summary>
        /// A <see cref="">GeoPackage</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        Geopackage = 164,
        
        /// <summary>
        /// A <see cref="">GeoPackageFeatureTable</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        GeopackageFeatureTable = 165,
        
        /// <summary>
        /// A <see cref="">GeoPackageRaster</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        GeopackageRaster = 166,
        
        /// <summary>
        /// An <see cref="">WMSSublayer</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        WMSSublayer = 167,
        
        /// <summary>
        /// A <see cref="">VectorTileCache</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        VectorTileCache = 168,
        
        /// <summary>
        /// A <see cref="">Analysis</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        Analysis = 169,
        
        /// <summary>
        /// A <see cref="">AnalysisOverlay</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        AnalysisOverlay = 170,
        
        /// <summary>
        /// An <see cref="">ItemResourceCache</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ItemResourceCache = 171,
        
        /// <summary>
        /// An <see cref="">WMSFeature</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        WMSFeature = 172,
        
        /// <summary>
        /// A <see cref="">NMEASatelliteInfo</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        NMEASatelliteInfo = 173,
        
        /// <summary>
        /// A symbol layer object.
        /// </summary>
        /// <since>1.0.0</since>
        SymbolLayer = 174,
        
        /// <summary>
        /// A vector marker symbol element object.
        /// </summary>
        /// <since>1.0.0</since>
        VectorMarkerSymbolElement = 175,
        
        /// <summary>
        /// A geometric effect object.
        /// </summary>
        /// <since>1.0.0</since>
        GeometricEffect = 176,
        
        /// <summary>
        /// A picture marker symbol layer object.
        /// </summary>
        /// <since>1.0.0</since>
        PictureMarkerSymbolLayer = 177,
        
        /// <summary>
        /// A picture fill symbol layer object.
        /// </summary>
        /// <since>1.0.0</since>
        PictureFillSymbolLayer = 178,
        
        /// <summary>
        /// A feature subtype object.
        /// </summary>
        /// <since>1.0.0</since>
        FeatureSubtype = 179,
        
        /// <summary>
        /// A <see cref="">LabelStackSeparator</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        LabelStackSeparator = 180,
        
        /// <summary>
        /// An <see cref="">WFSService</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        WFSService = 181,
        
        /// <summary>
        /// An <see cref="">WFSLayerInfo</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        WFSLayerInfo = 182,
        
        /// <summary>
        /// A <see cref="">OfflineMapParametersKey</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        OfflineMapParametersKey = 183,
        
        /// <summary>
        /// A <see cref="">GenerateOfflineMapParameterOverrides</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        GenerateOfflineMapParameterOverrides = 184,
        
        /// <summary>
        /// An <see cref="">QueryParameters</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        QueryParameters = 186,
        
        /// <summary>
        /// An <see cref="">StatisticsQueryParameters</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        StatisticsQueryParameters = 187,
        
        /// <summary>
        /// A <see cref="">RouteTracker</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        RouteTracker = 188,
        
        /// <summary>
        /// A <see cref="">WFSFeatureTable</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        WFSFeatureTable = 189,
        
        /// <summary>
        /// A <see cref="">DownloadPreplannedOfflineMapParameters</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        DownloadPreplannedOfflineMapParameters = 190,
        
        /// <summary>
        /// A <see cref="">AnnotationSublayer</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        AnnotationSublayer = 192,
        
        /// <summary>
        /// A <see cref="">OfflineMapSyncParameters</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        OfflineMapSyncParameters = 193,
        
        /// <summary>
        /// A <see cref="">OfflineMapUpdatesInfo</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        OfflineMapUpdatesInfo = 194,
        
        /// <summary>
        /// A <see cref="">DictionarySymbolStyleConfiguration</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        DictionarySymbolStyleConfiguration = 195,
        
        /// <summary>
        /// A <see cref="">Location</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        Location = 197,
        
        /// <summary>
        /// A <see cref="">ImageOverlay</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ImageFrame = 198,
        
        /// <summary>
        /// A <see cref="">ImageOverlay</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ImageOverlay = 199,
        
        /// <summary>
        /// A <see cref="">GeodatabaseDeltaInfo</see>.
        /// </summary>
        /// <since>1.0.0</since>
        GeodatabaseDeltaInfo = 200,
        
        /// <summary>
        /// An <see cref="">OGCFeatureService</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        OGCFeatureService = 300,
        
        /// <summary>
        /// An <see cref="">OGCFeatureCollectionInfo</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        OGCFeatureCollectionInfo = 301,
        
        /// <summary>
        /// A <see cref="">OGCFeatureCollectionTable</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        OGCFeatureCollectionTable = 302,
        
        /// <summary>
        /// A <see cref="GameEngine.Geometry.ArcGISDatumTransformation">ArcGISDatumTransformation</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        DatumTransformation = 500,
        
        /// <summary>
        /// A <see cref="GameEngine.Geometry.ArcGISGeographicTransformationStep">ArcGISGeographicTransformationStep</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        GeographicTransformationStep = 501,
        
        /// <summary>
        /// A <see cref="GameEngine.Geometry.ArcGISHorizontalVerticalTransformationStep">ArcGISHorizontalVerticalTransformationStep</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        HorizontalVerticalTransformationStep = 502,
        
        /// <summary>
        /// A <see cref="">ENCCell</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ENCCell = 1000,
        
        /// <summary>
        /// A <see cref="">ENCDataset</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ENCDataset = 1001,
        
        /// <summary>
        /// A <see cref="">ENCExchangeSet</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ENCExchangeSet = 1002,
        
        /// <summary>
        /// A <see cref="">ENCFeature</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ENCFeature = 1003,
        
        /// <summary>
        /// A <see cref="">MobileScenePackage</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        MobileScenePackage = 1004,
        
        /// <summary>
        /// A popup expression defining an arcade expression on a popup.
        /// </summary>
        /// <since>1.0.0</since>
        PopupExpression = 1005,
        
        /// <summary>
        /// A <see cref="">UtilityNetwork</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityNetwork = 1006,
        
        /// <summary>
        /// A <see cref="">UtilityAssetType</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityAssetType = 1007,
        
        /// <summary>
        /// A <see cref="">UtilityAssetGroup</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityAssetGroup = 1008,
        
        /// <summary>
        /// A <see cref="">UtilityCategory</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityCategory = 1009,
        
        /// <summary>
        /// A <see cref="">UtilityTerminal</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityTerminal = 1010,
        
        /// <summary>
        /// A <see cref="">UtilityNetworkAttribute</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityNetworkAttribute = 1011,
        
        /// <summary>
        /// A <see cref="">UtilityNetworkSource</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityNetworkSource = 1012,
        
        /// <summary>
        /// A <see cref="">UtilityElement</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityElement = 1013,
        
        /// <summary>
        /// A <see cref="">UtilityTraceResultType</see> value.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityTraceResultType = 1014,
        
        /// <summary>
        /// A <see cref="">TrackingStatus</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        TrackingStatus = 1015,
        
        /// <summary>
        /// A <see cref="">UtilityTraceResult</see> value.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityTraceResult = 1016,
        
        /// <summary>
        /// A <see cref="">UtilityDomainNetwork</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityDomainNetwork = 1017,
        
        /// <summary>
        /// A <see cref="">UtilityTerminalConfiguration</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityTerminalConfiguration = 1018,
        
        /// <summary>
        /// A <see cref="">UtilityTier</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityTier = 1019,
        
        /// <summary>
        /// A <see cref="">UtilityTierGroup</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityTierGroup = 1020,
        
        /// <summary>
        /// A <see cref="">UtilityPropagator</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityPropagator = 1021,
        
        /// <summary>
        /// A <see cref="">SubtypeSublayer</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        SubtypeSublayer = 1022,
        
        /// <summary>
        /// A <see cref="">UtilityAssociation</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityAssociation = 1023,
        
        /// <summary>
        /// A <see cref="">UtilityTraceFunctionBarrier</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityTraceFunctionBarrier = 1024,
        
        /// <summary>
        /// A <see cref="">LicenseInfo</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        LicenseInfo = 1025,
        
        /// <summary>
        /// A <see cref="">RasterCell</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        RasterCell = 1026,
        
        /// <summary>
        /// A <see cref="">UtilityTerminalPath</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityTerminalPath = 1027,
        
        /// <summary>
        /// A <see cref="">UtilityTerminalConfigurationPath</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityTerminalConfigurationPath = 1028,
        
        /// <summary>
        /// The element is currently not holding any value.
        /// </summary>
        /// <since>1.0.0</since>
        None = -1,
        
        /// <summary>
        /// An object that represents the result of an attempt to evaluate a popup expression.
        /// </summary>
        /// <since>1.0.0</since>
        PopupExpressionEvaluation = 1029,
        
        /// <summary>
        /// A <see cref="">UtilityTraceFunction</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityTraceFunction = 1030,
        
        /// <summary>
        /// A <see cref="">UtilityFunctionTraceResult</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityFunctionTraceResult = 1031,
        
        /// <summary>
        /// A <see cref="">UtilityTraceFunctionOutput</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityTraceFunctionOutput = 1032,
        
        /// <summary>
        /// A <see cref="">ServiceVersionInfo</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ServiceVersionInfo = 1033,
        
        /// <summary>
        /// A <see cref="">UtilityGeometryTraceResult</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityGeometryTraceResult = 1034,
        
        /// <summary>
        /// A <see cref="">FeatureTableEditResult</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        FeatureTableEditResult = 1035,
        
        /// <summary>
        /// A <see cref="GameEngine.Security.ArcGISAuthenticationConfiguration">ArcGISAuthenticationConfiguration</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ArcGISAuthenticationConfiguration = 1036,
        
        /// <summary>
        /// A <see cref="">UtilityNamedTraceConfiguration</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        UtilityNamedTraceConfiguration = 1037,
        
        /// <summary>
        /// A <see cref="">WifiReading</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        WifiReading = 1038,
        
        /// <summary>
        /// A <see cref="">LocalFeatureEdit</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        LocalFeatureEdit = 1039,
        
        /// <summary>
        /// A <see cref="">FloorFacility</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        FloorFacility = 1041,
        
        /// <summary>
        /// A <see cref="">FloorLevel</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        FloorLevel = 1042,
        
        /// <summary>
        /// A <see cref="">FloorSite</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        FloorSite = 1043,
        
        /// <summary>
        /// A <see cref="GameEngine.Attributes.ArcGISVisualizationAttributeDescription">ArcGISVisualizationAttributeDescription</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        VisualizationAttributeDescription = 1044,
        
        /// <summary>
        /// A <see cref="GameEngine.Attributes.ArcGISVisualizationAttribute">ArcGISVisualizationAttribute</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        VisualizationAttribute = 1045,
        
        /// <summary>
        /// A <see cref="GameEngine.Attributes.ArcGISAttribute">ArcGISAttribute</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        Attribute = 1046,
        
        /// <summary>
        /// A <see cref="">LocalFeatureEditsResult</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        LocalFeatureEditsResult = 1047,
        
        /// <summary>
        /// A <see cref="">Geotrigger</see>.
        /// </summary>
        /// <since>1.0.0</since>
        Geotrigger = 1048,
        
        /// <summary>
        /// A <see cref="">DisplayFilter</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        DisplayFilter = 1049,
        
        /// <summary>
        /// A <see cref="">ScaleRangeDisplayFilter</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ScaleRangeDisplayFilter = 1050,
        
        /// <summary>
        /// A <see cref="">Contingency</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        Contingency = 1052,
        
        /// <summary>
        /// A <see cref="">ContingencyConstraintViolation</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ContingencyConstraintViolation = 1053,
        
        /// <summary>
        /// A <see cref="">ContingentValue</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ContingentValue = 1054,
        
        /// <summary>
        /// A <see cref="">FieldGroup</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        FieldGroup = 1055,
        
        /// <summary>
        /// A <see cref="">ArcadeEvaluationResult</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ArcadeEvaluationResult = 1056,
        
        /// <summary>
        /// A <see cref="">ServiceGeodatabase</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        ServiceGeodatabase = 1058,
        
        /// <summary>
        /// A <see cref="">PopupElement</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        PopupElement = 1060,
        
        /// <summary>
        /// A <see cref="">FieldDescription</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        FieldDescription = 1061,
        
        /// <summary>
        /// A <see cref="">CodedValueDescription</see> object.
        /// </summary>
        /// <since>1.0.0</since>
        CodedValueDescription = 1062
    };
}