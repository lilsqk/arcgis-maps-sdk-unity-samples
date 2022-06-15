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

namespace Esri.GameEngine.MapView
{
    /// <summary>
    /// A camera on a view.
    /// </summary>
    /// <remarks>
    /// An immutable object used to set a location of the camera on a view.
    /// </remarks>
    /// <since>1.0.0</since>
    [StructLayout(LayoutKind.Sequential)]
    public partial class ArcGISCamera
    {
        #region Constructors
        /// <summary>
        /// Create a camera object.
        /// </summary>
        /// <param name="locationPoint">A point geometry containing the location and altitude at which to place the camera.</param>
        /// <param name="heading">The heading of the camera.</param>
        /// <param name="pitch">The pitch of the camera. The value must be from 0 to 180 and represents the angle applied to the camera when rotating around its Y axis in the East, North, Up (ENU) ground reference frame. 0 is looking straight down towards the center of the earth, 180 looking straight up towards the sky. Negative pitches are not allowed and the values do not wrap around. If the behavior of a negative pitch is required, then the corresponding transformation with positive pitch can be set instead. For example if heading:0 pitch:-20 roll:0 is required then heading:180 pitch:20 roll:180 can be used instead.</param>
        /// <param name="roll">The roll of the camera.</param>
        /// <since>1.0.0</since>
        public ArcGISCamera(GameEngine.Geometry.ArcGISPoint locationPoint, double heading, double pitch, double roll)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localLocationPoint = locationPoint.Handle;
            
            Handle = PInvoke.RT_Camera_createWithLocationHeadingPitchRoll(localLocationPoint, heading, pitch, roll, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
        }
        #endregion // Constructors
        
        #region Properties
        /// <summary>
        /// The heading of the camera.
        /// </summary>
        /// <seealso cref="GameEngine.MapView.ArcGISCamera">ArcGISCamera</seealso>
        /// <since>1.0.0</since>
        public double Heading
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Camera_getHeading(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// <summary>
        /// The location of the camera.
        /// </summary>
        /// <seealso cref="GameEngine.MapView.ArcGISCamera">ArcGISCamera</seealso>
        /// <seealso cref="GameEngine.Geometry.ArcGISPoint">ArcGISPoint</seealso>
        /// <since>1.0.0</since>
        public GameEngine.Geometry.ArcGISPoint Location
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Camera_getLocation(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                GameEngine.Geometry.ArcGISPoint localLocalResult = null;
                
                if (localResult != IntPtr.Zero)
                {
                    localLocalResult = new GameEngine.Geometry.ArcGISPoint(localResult);
                }
                
                return localLocalResult;
            }
        }
        
        /// <summary>
        /// The pitch of the camera.
        /// </summary>
        /// <remarks>
        /// The pitch value must be from 0 to 180 and represents the angle applied to the
        /// camera when rotating around its Y axis in the East, North, Up (ENU) ground reference frame. 0 is looking straight
        /// down towards the center of the earth, 180 looking straight up towards the sky. Negative
        /// pitches are not allowed and the values do not wrap around. If the behavior of a negative pitch is required, then the
        /// corresponding transformation with positive pitch can be set instead. For example if heading:0 pitch:-20 roll:0 is
        /// required then heading:180 pitch:20 roll:180 can be used instead.
        /// </remarks>
        /// <seealso cref="GameEngine.MapView.ArcGISCamera">ArcGISCamera</seealso>
        /// <since>1.0.0</since>
        public double Pitch
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Camera_getPitch(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        
        /// <summary>
        /// The roll of the camera.
        /// </summary>
        /// <seealso cref="GameEngine.MapView.ArcGISCamera">ArcGISCamera</seealso>
        /// <since>1.0.0</since>
        public double Roll
        {
            get
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                var localResult = PInvoke.RT_Camera_getRoll(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
                
                return localResult;
            }
        }
        #endregion // Properties
        
        #region Methods
        /// <summary>
        /// Creates a copy of the camera with the altitude adjusted.
        /// </summary>
        /// <param name="deltaAltitude">The altitude delta to apply to the output camera.</param>
        /// <returns>
        /// A copy of the camera with an elevation delta adjusted by the parameter delta_altitude.
        /// </returns>
        /// <seealso cref="GameEngine.MapView.ArcGISCamera">ArcGISCamera</seealso>
        /// <since>1.0.0</since>
        public ArcGISCamera Elevate(double deltaAltitude)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Camera_elevate(Handle, deltaAltitude, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            ArcGISCamera localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new ArcGISCamera(localResult);
            }
            
            return localLocalResult;
        }
        
        /// <summary>
        /// Check if the cameras are equal.
        /// </summary>
        /// <param name="otherCamera">The other camera object.</param>
        /// <returns>
        /// true if the cameras are equals otherwise false. False if an error occurs.
        /// </returns>
        /// <seealso cref="GameEngine.MapView.ArcGISCamera">ArcGISCamera</seealso>
        /// <since>1.0.0</since>
        internal bool Equals(ArcGISCamera otherCamera)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localOtherCamera = otherCamera.Handle;
            
            var localResult = PInvoke.RT_Camera_equals(Handle, localOtherCamera, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// <summary>
        /// Creates a copy of the camera with the location changed.
        /// </summary>
        /// <param name="location">The location to move the output camera to.</param>
        /// <returns>
        /// A copy of the camera with the location changed.
        /// </returns>
        /// <seealso cref="GameEngine.MapView.ArcGISCamera">ArcGISCamera</seealso>
        /// <since>1.0.0</since>
        public ArcGISCamera MoveTo(GameEngine.Geometry.ArcGISPoint location)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localLocation = location.Handle;
            
            var localResult = PInvoke.RT_Camera_moveTo(Handle, localLocation, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            ArcGISCamera localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new ArcGISCamera(localResult);
            }
            
            return localLocalResult;
        }
        
        /// <summary>
        /// Creates a copy of the camera with a change in pitch, heading and roll to the given angles in degrees
        /// </summary>
        /// <param name="heading">The angle in degrees to which the output camera heading will be rotated</param>
        /// <param name="pitch">The angle in degrees to which the output camera pitch will be rotated. The value must be from 0 to 180 and represents the angle applied to the camera when rotating around its Y axis in the East, North, Up (ENU) ground reference frame. 0 is looking straight down towards the center of the earth, 180 looking straight up towards the sky. Negative pitches are not allowed and the values do not wrap around. If the behavior of a negative pitch is required, then the corresponding transformation with positive pitch can be set instead. For example if heading:0 pitch:-20 roll:0 is required then heading:180 pitch:20 roll:180 can be used instead.</param>
        /// <param name="roll">The angle in degrees to which the output camera roll will be rotated</param>
        /// <returns>
        /// A copy of the camera with the position moved
        /// </returns>
        /// <seealso cref="GameEngine.MapView.ArcGISCamera">ArcGISCamera</seealso>
        /// <since>1.0.0</since>
        public ArcGISCamera RotateTo(double heading, double pitch, double roll)
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Camera_rotateTo(Handle, heading, pitch, roll, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            ArcGISCamera localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new ArcGISCamera(localResult);
            }
            
            return localLocalResult;
        }
        #endregion // Methods
        
        #region Internal Members
        internal ArcGISCamera(IntPtr handle) => Handle = handle;
        
        ~ArcGISCamera()
        {
            if (Handle != IntPtr.Zero)
            {
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                PInvoke.RT_Camera_destroy(Handle, errorHandler);
                
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
        internal static extern IntPtr RT_Camera_createWithLocationHeadingPitchRoll(IntPtr locationPoint, double heading, double pitch, double roll, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern double RT_Camera_getHeading(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_Camera_getLocation(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern double RT_Camera_getPitch(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern double RT_Camera_getRoll(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_Camera_elevate(IntPtr handle, double deltaAltitude, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Camera_equals(IntPtr handle, IntPtr otherCamera, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_Camera_moveTo(IntPtr handle, IntPtr location, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_Camera_rotateTo(IntPtr handle, double heading, double pitch, double roll, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_Camera_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}