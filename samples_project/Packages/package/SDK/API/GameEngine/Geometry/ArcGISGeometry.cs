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

namespace Esri.GameEngine.Geometry
{
    /// <summary>
    /// Base class for all classes that represent geometric shapes.
    /// </summary>
    /// <remarks>
    /// <see cref="GameEngine.Geometry.ArcGISGeometry">ArcGISGeometry</see> is the base class for two-dimensional (x,y) or three-dimensional
    /// (x,y,z) geometries. Objects that inherit from the <see cref="GameEngine.Geometry.ArcGISGeometry">ArcGISGeometry</see> class may also include a
    /// measure (m-value) for each vertex. The <see cref="GameEngine.Geometry.ArcGISGeometry">ArcGISGeometry</see> class provides functionality common
    /// to all types of geometry. <see cref="GameEngine.Geometry.ArcGISPoint">ArcGISPoint</see>, <see cref="GameEngine.Geometry.ArcGISMultipoint">ArcGISMultipoint</see>, <see cref="GameEngine.Geometry.ArcGISPolyline">ArcGISPolyline</see>, <see cref="GameEngine.Geometry.ArcGISPolygon">ArcGISPolygon</see>, and
    /// <see cref="GameEngine.Geometry.ArcGISEnvelope">ArcGISEnvelope</see> all inherit from <see cref="GameEngine.Geometry.ArcGISGeometry">ArcGISGeometry</see> and represent different types of shapes.
    /// 
    /// <see cref="GameEngine.Geometry.ArcGISGeometry">ArcGISGeometry</see> represents real-world objects by defining a shape at a specific geographic location.
    /// It is used throughout the API to represent the shapes of features and graphics, layer extents,
    /// viewpoints, and GPS locations. It is also used, for example, to define inputs and outputs for
    /// spatial analysis and geoprocessing operations and to measure distances and areas.
    /// 
    /// All types of geometry:
    /// * Have a <see cref="GameEngine.Geometry.ArcGISSpatialReference">ArcGISSpatialReference</see> indicating the coordinate system used by its coordinates
    /// * Can be empty, indicating that they have no specific location or shape
    /// * May have z-values and/or m-values to define elevation and measures respectively
    /// * Can be converted to and from JSON to be persisted or to be exchanged directly with
    /// REST services
    /// 
    /// Immutability
    /// Most geometries are created and not changed for their lifetime. Examples include features
    /// created to be stored in a geodatabase or read from a non-editable layer, and features returned
    /// from tasks such as a spatial query, geocode operation, network trace, or geoprocessing task.
    /// Immutable geometries (geometries that cannot be changed) offer some important benefits to
    /// your app. They are inherently thread-safe, help prevent inadvertent changes, and allow for
    /// certain performance optimizations.
    /// 
    /// Instead of changing the properties of existing geometries, you can create and update geometries
    /// using the various subclasses of <see cref="GameEngine.Geometry.ArcGISGeometryBuilder">ArcGISGeometryBuilder</see> (for example, <see cref="GameEngine.Geometry.ArcGISPolygonBuilder">ArcGISPolygonBuilder</see>), which can
    /// represent the state of a geometry under construction while allowing modifications, thus enabling
    /// editing workflows.
    /// 
    /// Additionally, <see cref="GameEngine.Geometry.ArcGISGeometryEngine">ArcGISGeometryEngine</see> offers a range of topological and spatial transformations that
    /// read the content of existing geometries and create new geometries, for example, project, buffer,
    /// union, and so on. Relational tests such as intersects and overlaps are also available on
    /// <see cref="GameEngine.Geometry.ArcGISGeometryEngine">ArcGISGeometryEngine</see>.
    /// 
    /// Coordinate units
    /// The coordinates that define a geometry are only meaningful in the context of the geometry's
    /// <see cref="GameEngine.Geometry.ArcGISSpatialReference">ArcGISSpatialReference</see>. The vertices and spatial reference together allow your app to translate a
    /// real-world object from its location on the Earth to its location on your map or scene.
    /// 
    /// In some cases, a geometry's spatial reference may not be set. For example, a <see cref="">Graphic</see> that
    /// does not have a spatial reference is drawn using the same spatial reference as the <see cref="">MapView</see>
    /// to which it was added. If the coordinates are in a different spatial reference, the graphics may
    /// not display in the correct location, or at all.
    /// 
    /// When using <see cref="GameEngine.Geometry.ArcGISGeometryBuilder">ArcGISGeometryBuilder</see> to create a <see cref="GameEngine.Geometry.ArcGISPolyline">ArcGISPolyline</see> or <see cref="GameEngine.Geometry.ArcGISPolygon">ArcGISPolygon</see> from a collection of
    /// <see cref="GameEngine.Geometry.ArcGISPoint">ArcGISPoint</see>, you don't need to set the spatial reference of every point before you add it to
    /// the builder, as it is assigned the spatial reference of the builder itself. In most other
    /// cases, such as when using a geometry in geometry operations or when editing a feature table,
    /// <see cref="GameEngine.Geometry.ArcGISGeometry.SpatialReference">ArcGISGeometry.SpatialReference</see> must be set.
    /// 
    /// Spatial reference and projection
    /// Changing the coordinates of a geometry to have the same shape and location represented using a
    /// different <see cref="GameEngine.Geometry.ArcGISSpatialReference">ArcGISSpatialReference</see> is known as "projection" or sometimes as "reprojection". Because
    /// geometries are immutable, they do not have any member methods that project, transform, or
    /// otherwise modify their content.
    /// </remarks>
    /// <seealso cref="GameEngine.Geometry.ArcGISGeometryEngine">ArcGISGeometryEngine</seealso>
    /// <seealso cref="GameEngine.Geometry.ArcGISGeometryBuilder">ArcGISGeometryBuilder</seealso>
    /// <since>1.0.0</since>
    [StructLayout(LayoutKind.Sequential)]
    public partial class ArcGISGeometry
    {
        #region Properties
        /// <summary>
        /// The number of dimensions for the geometry.
        /// </summary>
        /// <remarks>
        /// Returns <see cref="GameEngine.Geometry.ArcGISGeometryDimension.Unknown">ArcGISGeometryDimension.Unknown</see> if an error occurs.
        /// </remarks>
        /// <seealso cref="GameEngine.Geometry.ArcGISGeometryDimension">ArcGISGeometryDimension</seealso>
        /// <since>1.0.0</since>
        public ArcGISGeometryDimension Dimension
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Geometry_getDimension(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// <summary>
        /// The extent for the geometry.
        /// </summary>
        /// <remarks>
        /// The extent for the geometry which is a envelope and contains the same spatial reference
        /// as the input geometry.
        /// </remarks>
        /// <seealso cref="GameEngine.Geometry.ArcGISEnvelope">ArcGISEnvelope</seealso>
        /// <since>1.0.0</since>
        public ArcGISEnvelope Extent
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Geometry_getExtent(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                ArcGISEnvelope localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new ArcGISEnvelope(localResult);
                }
                
                return localLocalResult;
            }
        }
        
        /// <summary>
        /// True if this geometry contains curve segments; false otherwise.
        /// </summary>
        /// <remarks>
        /// The ArcGIS Platform supports polygon and polyline geometries that contain curve segments (where 
        /// <see cref="GameEngine.Geometry.ArcGISSegment.IsCurve">ArcGISSegment.IsCurve</see> is true, sometimes known as true curves or nonlinear segments). Curves may be present
        /// in certain types of data - for example Mobile Map Packages (MMPK) or geometry JSON. When connecting to
        /// ArcGIS feature services that <see cref="support curves">ArcGISFeatureServiceInfo.supportsTrueCurve</see>, ArcGIS
        /// Runtime retrieves densified versions of curve feature geometries by default.
        /// 
        /// If a polygon or polyline geometry contains curve segments, this property returns true. Prior to v100.12,
        /// it was not possible to access curve segments, and only <see cref="GameEngine.Geometry.ArcGISLineSegment">ArcGISLineSegment</see> instances would be returned
        /// when iterating through the segments in a <see cref="GameEngine.Geometry.ArcGISPolygon">ArcGISPolygon</see> or <see cref="GameEngine.Geometry.ArcGISPolyline">ArcGISPolyline</see> object, irrespective of this property.
        /// 
        /// From v100.12, you can use curve segments when using a <see cref="GameEngine.Geometry.ArcGISMultipartBuilder">ArcGISMultipartBuilder</see> to create or edit polygon and
        /// polyline geometries, and also get curve segments when iterating through the segments of existing
        /// <see cref="GameEngine.Geometry.ArcGISMultipart">ArcGISMultipart</see> geometries when this property returns true. You can also choose to return true curves from
        /// feature services by using <see cref="">ArcGISRuntimeEnvironment.serviceCurveGeometryMode</see>.
        /// </remarks>
        /// <seealso cref="GameEngine.Geometry.ArcGISGeometryBuilder.HasCurves">ArcGISGeometryBuilder.HasCurves</seealso>
        /// <seealso cref="GameEngine.Geometry.ArcGISImmutablePart.HasCurves">ArcGISImmutablePart.HasCurves</seealso>
        /// <seealso cref="GameEngine.Geometry.ArcGISSegment.IsCurve">ArcGISSegment.IsCurve</seealso>
        /// <seealso cref="GameEngine.Geometry.ArcGISCubicBezierSegment">ArcGISCubicBezierSegment</seealso>
        /// <seealso cref="GameEngine.Geometry.ArcGISEllipticArcSegment">ArcGISEllipticArcSegment</seealso>
        /// <since>1.0.0</since>
        public bool HasCurves
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Geometry_getHasCurves(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// <summary>
        /// A value indicating if the geometry has M.
        /// </summary>
        /// <remarks>
        /// If an error occurs false is returned.
        /// M is a vertex value that is stored with the geometry.
        /// </remarks>
        /// <since>1.0.0</since>
        public bool HasM
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Geometry_getHasM(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// <summary>
        /// A value indicating if the geometry has Z.
        /// </summary>
        /// <remarks>
        /// If an error occurs false is returned.
        /// Z typically represent elevations or heights.
        /// </remarks>
        /// <since>1.0.0</since>
        public bool HasZ
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Geometry_getHasZ(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// <summary>
        /// Check if a geometry is empty or not.
        /// </summary>
        /// <remarks>
        /// Only check the geometry to see if it is empty. Does not check the spatial reference. Returns true if an error occurs.
        /// </remarks>
        /// <since>1.0.0</since>
        public bool IsEmpty
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Geometry_getIsEmpty(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// <summary>
        /// The type of geometry.
        /// </summary>
        /// <remarks>
        /// The geometry type for a specific geometry. Returns <see cref="GameEngine.Geometry.ArcGISGeometryType.Unknown">ArcGISGeometryType.Unknown</see> if an error occurs.
        /// </remarks>
        /// <seealso cref="GameEngine.Geometry.ArcGISGeometryType">ArcGISGeometryType</seealso>
        /// <since>1.0.0</since>
        internal ArcGISGeometryType ObjectType
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Geometry_getObjectType(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// <summary>
        /// The spatial reference for the geometry.
        /// </summary>
        /// <remarks>
        /// If the geometry does not have a spatial reference null is returned.
        /// </remarks>
        /// <seealso cref="GameEngine.Geometry.ArcGISSpatialReference">ArcGISSpatialReference</seealso>
        /// <since>1.0.0</since>
        public ArcGISSpatialReference SpatialReference
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Geometry_getSpatialReference(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                ArcGISSpatialReference localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new ArcGISSpatialReference(localResult);
                }
                
                return localLocalResult;
            }
        }
        #endregion // Properties
        
        #region Methods
        /// <summary>
        /// Check if two geometries are equal.
        /// </summary>
        /// <remarks>
        /// Returns false if an error occurs.
        /// </remarks>
        /// <param name="right">The second geometry.</param>
        /// <returns>
        /// True if the geometries (including their spatial references) are equal, otherwise returns false.
        /// </returns>
        /// <since>1.0.0</since>
        public bool Equals(ArcGISGeometry right)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localRight = right.Handle;
            
            var localResult = PInvoke.RT_Geometry_equals(Handle, localRight, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// <summary>
        /// Check if two geometries are equal to within some tolerance.
        /// </summary>
        /// <remarks>
        /// This function performs a lightweight comparison of two geometries,
        /// such as might be useful when writing test code.
        /// It uses the tolerance to compare each of x, y, and any other values
        /// the geometries possess (such as z or m) independently in the manner:
        /// abs(value1 - value2) <= tolerance.
        /// Returns true if the difference of each is within the tolerance and
        /// all other properties of the geometries are exactly equal (spatial
        /// reference, vertex count, etc.).
        /// A single tolerance is used even if the units for the horizontal
        /// coordinates and other values differ, e.g horizontal coordinates in
        /// degrees and vertical coordinates in meters. This function does not
        /// respect modular arithmetic of spatial references which wrap around,
        /// so longitudes of -180 and +180 degrees are considered to differ by
        /// 360 degrees.
        /// Returns false if an error occurs.
        /// For topological equality, use relational operators
        /// instead of this function. See <see cref="GameEngine.Geometry.ArcGISGeometryEngine.Equals">ArcGISGeometryEngine.Equals</see>.
        /// </remarks>
        /// <param name="right">The second geometry.</param>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns>
        /// True if the geometries are equal, within the tolerance, otherwise false.
        /// </returns>
        /// <since>1.0.0</since>
        public bool Equals(ArcGISGeometry right, double tolerance)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localRight = right.Handle;
            
            var localResult = PInvoke.RT_Geometry_equalsWithTolerance(Handle, localRight, tolerance, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// <summary>
        /// Creates a geometry from an ArcGIS JSON geometry representation.
        /// </summary>
        /// <remarks>
        /// <see cref="GameEngine.Geometry.ArcGISGeometry">ArcGISGeometry</see> can be serialized and de-serialized to and from JSON. The
        /// <see cref="ArcGIS REST API documentation">https://developers.arcgis.com/documentation/common-data-types/geometry-objects.htm</see>
        /// describes the JSON representation of geometry objects.
        /// You can use this encoding and decoding mechanism to exchange geometries with REST Web services
        /// or to store them in text files.
        /// </remarks>
        /// <param name="inputJSON">JSON representation of geometry.</param>
        /// <param name="spatialReference">The geometry's spatial reference.</param>
        /// <returns>
        /// Geometry converted from a JSON String.
        /// </returns>
        /// <seealso cref="GameEngine.Geometry.ArcGISGeometry">ArcGISGeometry</seealso>
        /// <since>1.0.0</since>
        public static ArcGISGeometry FromJSON(string inputJSON, ArcGISSpatialReference spatialReference)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localSpatialReference = spatialReference == null ? System.IntPtr.Zero : spatialReference.Handle;
            
            var localResult = PInvoke.RT_Geometry_fromJSONWithSpatialReference(inputJSON, localSpatialReference, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            ArcGISGeometry localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                var objectType = GameEngine.Geometry.PInvoke.RT_Geometry_getObjectType(localResult, IntPtr.Zero);
                
                switch (objectType)
                {
                    case GameEngine.Geometry.ArcGISGeometryType.Envelope:
                        localLocalResult = new ArcGISEnvelope(localResult);
                        break;
                    case GameEngine.Geometry.ArcGISGeometryType.Multipoint:
                        localLocalResult = new ArcGISMultipoint(localResult);
                        break;
                    case GameEngine.Geometry.ArcGISGeometryType.Point:
                        localLocalResult = new ArcGISPoint(localResult);
                        break;
                    case GameEngine.Geometry.ArcGISGeometryType.Polygon:
                        localLocalResult = new ArcGISPolygon(localResult);
                        break;
                    case GameEngine.Geometry.ArcGISGeometryType.Polyline:
                        localLocalResult = new ArcGISPolyline(localResult);
                        break;
                    default:
                        localLocalResult = new ArcGISGeometry(localResult);
                        break;
                }
            }
            
            return localLocalResult;
        }
        #endregion // Methods
        
        #region Internal Members
        internal ArcGISGeometry(IntPtr handle) => Handle = handle;
        
        ~ArcGISGeometry()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                PInvoke.RT_Geometry_destroy(Handle, errorHandler);
                
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
        internal static extern ArcGISGeometryDimension RT_Geometry_getDimension(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_Geometry_getExtent(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Geometry_getHasCurves(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Geometry_getHasM(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Geometry_getHasZ(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Geometry_getIsEmpty(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern ArcGISGeometryType RT_Geometry_getObjectType(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_Geometry_getSpatialReference(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Geometry_equals(IntPtr handle, IntPtr right, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Geometry_equalsWithTolerance(IntPtr handle, IntPtr right, double tolerance, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_Geometry_fromJSONWithSpatialReference([MarshalAs(UnmanagedType.LPStr)]string inputJSON, IntPtr spatialReference, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_Geometry_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}