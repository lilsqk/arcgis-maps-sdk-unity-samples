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
    /// The different types of array.
    /// </summary>
    /// <remarks>
    /// Each of the different supported array types. Can get the type by calling <see cref="Standard.ArcGISIntermediateImmutableArray<T>.ObjectType">ArcGISIntermediateImmutableArray<T>.ObjectType</see>.
    /// </remarks>
    /// <seealso cref="Standard.ArcGISIntermediateImmutableArray<T>.ObjectType">ArcGISIntermediateImmutableArray<T>.ObjectType</seealso>
    /// <since>1.0.0</since>
    public enum ArcGISArrayType
    {
        /// <summary>
        /// The default array type.
        /// </summary>
        /// <seealso cref="Standard.ArcGISIntermediateImmutableArray<T>">ArcGISIntermediateImmutableArray<T></seealso>
        /// <since>1.0.0</since>
        Array = 0,
        
        /// <summary>
        /// The observable array type.
        /// </summary>
        /// <seealso cref="">ObservableArray</seealso>
        /// <since>1.0.0</since>
        ArrayObservable = 1
    };
}