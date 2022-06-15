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
namespace Esri.GameEngine.Geometry
{
    /// <summary>
    /// The different types of segments.
    /// </summary>
    /// <remarks>
    /// Each of the different supported segment types. The segment type can be accessed by calling <see cref="GameEngine.Geometry.ArcGISSegment.ObjectType">ArcGISSegment.ObjectType</see>.
    /// </remarks>
    /// <seealso cref="GameEngine.Geometry.ArcGISSegment">ArcGISSegment</seealso>
    /// <seealso cref="GameEngine.Geometry.ArcGISSegment.ObjectType">ArcGISSegment.ObjectType</seealso>
    /// <since>1.0.0</since>
    public enum ArcGISSegmentType
    {
        /// <summary>
        /// An unknown segment.
        /// </summary>
        /// <since>1.0.0</since>
        Unknown = -1,
        
        /// <summary>
        /// Line segment.
        /// </summary>
        /// <since>1.0.0</since>
        LineSegment = 1,
        
        /// <summary>
        /// Cubic bezier curve segment.
        /// </summary>
        /// <since>1.0.0</since>
        CubicBezierSegment = 2,
        
        /// <summary>
        /// Elliptic arc curve segment.
        /// </summary>
        /// <since>1.0.0</since>
        EllipticArcSegment = 3
    };
}