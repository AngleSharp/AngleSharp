namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Providers timers to the Window object.
    /// </summary>
    [DomName("WindowTimers")]
    [DomNoInterfaceObject]
    public interface IWindowTimers 
    {
        /// <summary>
        /// Executes the provided handler after the timeout.
        /// </summary>
        /// <param name="handler">
        /// The handler that is called after the timeout.
        /// </param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        /// <returns>
        /// The handler to reference clear the timeout.
        /// </returns>
        [DomName("setTimeout")]
        Int32 SetTimeout(Action<IWindow> handler, Int32 timeout = 0);
        
        /// <summary>
        /// Clears a timeout.
        /// </summary>
        /// <param name="handle">
        /// The handle that specifies, which timeout to clear.
        /// </param>
        [DomName("clearTimeout")]
        void ClearTimeout(Int32 handle = 0);

        /// <summary>
        /// Keeps executing the provided handler with the interval.
        /// </summary>
        /// <param name="handler">The handler that is called each time.</param>
        /// <param name="timeout">The interval in milliseconds.</param>
        /// <returns>The handler to reference clear the interval.</returns>
        [DomName("setInterval")]
        Int32 SetInterval(Action<IWindow> handler, Int32 timeout = 0);

        /// <summary>
        /// Clears an interval.
        /// </summary>
        /// <param name="handle">
        /// The handle that specifies, which interval to clear.
        /// </param>
        [DomName("clearInterval")]
        void ClearInterval(Int32 handle = 0);
    }
}
