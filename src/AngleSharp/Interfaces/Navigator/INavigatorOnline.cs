namespace AngleSharp.Dom.Navigator
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Connectivity information regarding the navigator.
    /// </summary>
    [DomName("NavigatorOnLine")]
    [DomNoInterfaceObject]
    public interface INavigatorOnline
    {
        /// <summary>
        /// Gets if the connection is established.
        /// </summary>
        [DomName("onLine")]
        Boolean IsOnline { get; }
    }
}
