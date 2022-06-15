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

namespace Esri.Standard
{
    /// <summary>
    /// A <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> represents work that can be completed asynchronously and concurrently with other work.
    /// </summary>
    /// <remarks>
    /// A <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> can be either executing or complete as indicated by <see cref="Standard.ArcGISIntermediateFuture<T>.IsDone">ArcGISIntermediateFuture<T>.IsDone</see>. 
    /// Notification of completion is available through the callback <see cref="Standard.ArcGISFutureCompletedEvent">ArcGISFutureCompletedEvent</see>.
    /// 
    /// When complete, the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> will have either completed successfully or failed with an error
    /// during its execution. A successfully completed <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> returns null from <see cref="Standard.ArcGISIntermediateFuture<T>.GetError">ArcGISIntermediateFuture<T>.GetError</see>
    /// and the result can be obtained from <see cref="Standard.ArcGISIntermediateFuture<T>.Get">ArcGISIntermediateFuture<T>.Get</see>. Whereas a failed <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> returns an <see cref="Standard.ArcGISError">ArcGISError</see>,
    /// including if the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> was canceled.
    /// 
    /// Successfully completed <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> instances may return a result to the caller. The result is accessed by
    /// calling <see cref="Standard.ArcGISIntermediateFuture<T>.Get">ArcGISIntermediateFuture<T>.Get</see>. If there is no result an empty <see cref="">Element</see> is returned.
    /// 
    /// If the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> is executing and <see cref="Standard.ArcGISIntermediateFuture<T>.Get">ArcGISIntermediateFuture<T>.Get</see> or <see cref="Standard.ArcGISIntermediateFuture<T>.Wait">ArcGISIntermediateFuture<T>.Wait</see> is called the 
    /// thread will be blocked until the completion of the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see>. Once the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> is complete
    /// subsequent calls will no longer block. If the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> fails, but not canceled, both
    /// <see cref="Standard.ArcGISIntermediateFuture<T>.Get">ArcGISIntermediateFuture<T>.Get</see> or <see cref="Standard.ArcGISIntermediateFuture<T>.Wait">ArcGISIntermediateFuture<T>.Wait</see> will result in an error. A cancelled <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> will return a null
    /// from <see cref="Standard.ArcGISIntermediateFuture<T>.Get">ArcGISIntermediateFuture<T>.Get</see> and <see cref="Standard.ArcGISFutureStatus.Canceled">ArcGISFutureStatus.Canceled</see> from <see cref="Standard.ArcGISIntermediateFuture<T>.Wait">ArcGISIntermediateFuture<T>.Wait</see>.
    ///         
    /// To avoid blocking calling threads with either <see cref="Standard.ArcGISIntermediateFuture<T>.Get">ArcGISIntermediateFuture<T>.Get</see> or <see cref="Standard.ArcGISIntermediateFuture<T>.Wait">ArcGISIntermediateFuture<T>.Wait</see>, it is recommended to
    /// use the <see cref="Standard.ArcGISFutureCompletedEvent">ArcGISFutureCompletedEvent</see> to receive a completion notification before checking <see cref="Standard.ArcGISIntermediateFuture<T>.GetError">ArcGISIntermediateFuture<T>.GetError</see>
    /// for errors and then calling <see cref="Standard.ArcGISIntermediateFuture<T>.Get">ArcGISIntermediateFuture<T>.Get</see> to access the return value.
    /// 
    /// An executing <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> can be canceled by calling <see cref="Standard.ArcGISIntermediateFuture<T>.Cancel">ArcGISIntermediateFuture<T>.Cancel</see>. Cancellation results in the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see>
    /// returning an <see cref="Standard.ArcGISError">ArcGISError</see> from <see cref="Standard.ArcGISIntermediateFuture<T>.GetError">ArcGISIntermediateFuture<T>.GetError</see>.
    /// </remarks>
    /// <since>1.0.0</since>
    [StructLayout(LayoutKind.Sequential)]
    internal partial class ArcGISIntermediateFuture<T>
    {
        #region Methods
        /// <summary>
        /// Cancels the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see>.
        /// </summary>
        /// <remarks>
        /// Sets the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> to a failed and canceled state and causes the following:
        /// * The property <see cref="Standard.ArcGISIntermediateFuture<T>.IsCanceled">ArcGISIntermediateFuture<T>.IsCanceled</see> returns true
        /// * <see cref="Standard.ArcGISIntermediateFuture<T>.GetError">ArcGISIntermediateFuture<T>.GetError</see> returns an error indicating cancellation
        /// * <see cref="Standard.ArcGISIntermediateFuture<T>.Get">ArcGISIntermediateFuture<T>.Get</see> returns null
        /// * <see cref="Standard.ArcGISIntermediateFuture<T>.Wait">ArcGISIntermediateFuture<T>.Wait</see> returns <see cref="Standard.ArcGISFutureStatus.Canceled">ArcGISFutureStatus.Canceled</see>
        /// 
        /// The underlying asynchronous code cooperatively checks for cancellation status
        /// and may continue to execute for a short while after the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> is set to canceled.
        /// </remarks>
        /// <returns>
        /// true if the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> was canceled, false if the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> is already canceled.
        /// Returns false if an error occurs.
        /// </returns>
        /// <since>1.0.0</since>
        internal bool Cancel()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Task_cancel(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// <summary>
        /// Returns the result of the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see>.
        /// </summary>
        /// <remarks>
        /// If the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> is successful then <see cref="Standard.ArcGISIntermediateFuture<T>.Get">ArcGISIntermediateFuture<T>.Get</see> will return the result. For a
        /// <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> which is successful but has no result then an empty <see cref="">Element</see> is returned.
        /// 
        /// If the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> has failed during execution, the call to <see cref="Standard.ArcGISIntermediateFuture<T>.Get">ArcGISIntermediateFuture<T>.Get</see> will result
        /// in an error.
        /// 
        /// Canceled <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> are an exception and return a null with no error.
        /// 
        /// If the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> is not complete, a call to <see cref="Standard.ArcGISIntermediateFuture<T>.Get">ArcGISIntermediateFuture<T>.Get</see> will block the calling
        /// thread until the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> completes execution.
        /// </remarks>
        /// <returns>
        /// The result of the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> or null if the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> was canceled.
        /// </returns>
        /// <since>1.0.0</since>
        internal T Get()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Task_get(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            Standard.ArcGISElement localLocalResult = null;
            
            if (localResult != IntPtr.Zero)
            {
                localLocalResult = new Standard.ArcGISElement(localResult);
            }
            
            return Unity.Convert.FromArcGISElement<T>(localLocalResult);
        }
        
        /// <summary>
        /// If the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> is executing, or has completed successfully, a null is returned. If the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> has failed returns the  error.
        /// </summary>
        /// <remarks>
        /// If the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> is executing, or completed successfully null is returned. For a
        /// completed but failed <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> the failure is returned in an <see cref="Standard.ArcGISError">ArcGISError</see>.
        /// 
        /// If the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> was canceled this is also a failure and returns an error.
        /// </remarks>
        /// <returns>
        /// Returns the <see cref="Standard.ArcGISError">ArcGISError</see> instance or null.
        /// </returns>
        /// <since>1.0.0</since>
        internal Exception GetError()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Task_getError(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return Unity.Convert.FromArcGISError(new Standard.ArcGISError(localResult));
        }
        
        /// <summary>
        /// Indicates if the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> was canceled.
        /// </summary>
        /// <returns>
        /// true if the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> was canceled or false otherwise.
        /// </returns>
        /// <since>1.0.0</since>
        internal bool IsCanceled()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Task_isCanceled(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// <summary>
        /// Indicates if the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> has completed execution.
        /// </summary>
        /// <returns>
        /// true if the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> has completed, false otherwise.
        /// </returns>
        /// <since>1.0.0</since>
        internal bool IsDone()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Task_isDone(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        
        /// <summary>
        /// Waits for the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> to complete.
        /// </summary>
        /// <remarks>
        /// If the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> is successful or canceled then <see cref="Standard.ArcGISIntermediateFuture<T>.Wait">ArcGISIntermediateFuture<T>.Wait</see> will return the
        /// <see cref="Standard.ArcGISFutureStatus">ArcGISFutureStatus</see>.
        /// 
        /// If the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> has failed during execution, the call to <see cref="Standard.ArcGISIntermediateFuture<T>.Wait">ArcGISIntermediateFuture<T>.Wait</see> will
        /// result in an error.
        /// 
        /// If the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> is not complete, a call to <see cref="Standard.ArcGISIntermediateFuture<T>.Wait">ArcGISIntermediateFuture<T>.Wait</see> will block the calling
        /// thread until the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> completes execution.
        /// </remarks>
        /// <returns>
        /// The <see cref="Standard.ArcGISFutureStatus">ArcGISFutureStatus</see>. Returns <see cref="Standard.ArcGISFutureStatus.Unknown">ArcGISFutureStatus.Unknown</see> if an error occurs.
        /// </returns>
        /// <since>1.0.0</since>
        internal ArcGISFutureStatus Wait()
        {
            var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
            
            var localResult = PInvoke.RT_Task_wait(Handle, errorHandler);
            
            Unity.ArcGISErrorManager.CheckError(errorHandler);
            
            return localResult;
        }
        #endregion // Methods
        
        #region Events
        /// <summary>
        /// Sets the function that will be called when the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> is completed.
        /// </summary>
        /// <remarks>
        /// When the <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> completes then <see cref="Standard.ArcGISIntermediateFuture<T>.IsDone">ArcGISIntermediateFuture<T>.IsDone</see> is true and this callback
        /// will be called.
        /// 
        /// Setting this callback after <see cref="Standard.ArcGISIntermediateFuture<T>">ArcGISIntermediateFuture<T></see> has completed will immediately call the
        /// callback.
        /// 
        /// Setting the callback to null after it has already been set will stop the function
        /// from being called.
        /// </remarks>
        /// <since>1.0.0</since>
        internal ArcGISFutureCompletedEvent TaskCompleted
        {
            get
            {
                return _taskCompletedHandler.Delegate;
            }
            set
            {
                if (_taskCompletedHandler.Delegate == value)
                {
                    return;
                }
                
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                if (value != null)
                {
                    _taskCompletedHandler.Delegate = value;
                    
                    PInvoke.RT_Task_setTaskCompletedCallback(Handle, ArcGISFutureCompletedEventHandler.HandlerFunction, _taskCompletedHandler.UserData, errorHandler);
                }
                else
                {
                    PInvoke.RT_Task_setTaskCompletedCallback(Handle, null, _taskCompletedHandler.UserData, errorHandler);
                    
                    _taskCompletedHandler.Dispose();
                }
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
            }
        }
        #endregion // Events
        
        #region Internal Members
        internal ArcGISIntermediateFuture(IntPtr handle) => Handle = handle;
        
        ~ArcGISIntermediateFuture()
        {
            if (Handle != IntPtr.Zero)
            {
                if (_taskCompletedHandler.Delegate != null)
                {
                    PInvoke.RT_Task_setTaskCompletedCallback(Handle, null, _taskCompletedHandler.UserData, IntPtr.Zero);
                    
                    _taskCompletedHandler.Dispose();
                }
                
                var errorHandler = Unity.ArcGISErrorManager.CreateHandler();
                
                PInvoke.RT_Task_destroy(Handle, errorHandler);
                
                Unity.ArcGISErrorManager.CheckError(errorHandler);
            }
        }
        
        internal IntPtr Handle { get; set; }
        
        internal ArcGISFutureCompletedEventHandler _taskCompletedHandler = new ArcGISFutureCompletedEventHandler();
        #endregion // Internal Members
    }
    
    internal static partial class PInvoke
    {
        #region P-Invoke Declarations
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Task_cancel(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_Task_get(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern IntPtr RT_Task_getError(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Task_isCanceled(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool RT_Task_isDone(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern ArcGISFutureStatus RT_Task_wait(IntPtr handle, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_Task_setTaskCompletedCallback(IntPtr handle, ArcGISFutureCompletedEventInternal taskCompleted, IntPtr userData, IntPtr errorHandler);
        
        [DllImport(Unity.Interop.Dll)]
        internal static extern void RT_Task_destroy(IntPtr handle, IntPtr errorHandle);
        #endregion // P-Invoke Declarations
    }
}