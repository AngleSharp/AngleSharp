namespace AngleSharp.Dom.Navigator
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// A set of utilities to modify the navigator's behavior.
    /// </summary>
    [DomName("NavigatorContentUtils")]
    [DomNoInterfaceObject]
    public interface INavigatorContentUtilities
    {
        /// <summary>
        /// Allows web sites to register themselves as possible handlers for particular protocols.
        /// </summary>
        /// <param name="scheme">The protocol the site wishes to handle, specified as a string.</param>
        /// <param name="url">The URI to the handler as a string.</param>
        /// <param name="title">The title of the handler presented to the user as a string.</param>
        [DomName("registerProtocolHandler")]
        void RegisterProtocolHandler(String scheme, String url, String title);

        /// <summary>
        /// Allows web sites to register themselves as possible handlers for content of a particular MIME type.
        /// </summary>
        /// <param name="mimeType">The desired MIME type as a string.</param>
        /// <param name="url">The URI to the handler as a string.</param>
        /// <param name="title">The title of the handler presented to the user as a string.</param>
        [DomName("registerContentHandler")]
        void RegisterContentHandler(String mimeType, String url, String title);

        /// <summary>
        /// Checks if any handler is registered at the given URI for the specified scheme.
        /// </summary>
        /// <param name="scheme">The scheme to check for.</param>
        /// <param name="url">The URI to the handler.</param>
        /// <returns>True if a handler is registered, otherwise false.</returns>
        [DomName("isProtocolHandlerRegistered")]
        Boolean IsProtocolHandlerRegistered(String scheme, String url);

        /// <summary>
        /// Checks if any handler is registered at the given URI for the specified mime-type.
        /// </summary>
        /// <param name="mimeType">The mime-type to check for.</param>
        /// <param name="url">The URI to the handler.</param>
        /// <returns>True if a handler is registered, otherwise false.</returns>
        [DomName("isContentHandlerRegistered")]
        Boolean IsContentHandlerRegistered(String mimeType, String url);

        /// <summary>
        /// Removes the specified protocol handler, if any.
        /// </summary>
        /// <param name="scheme">The name of the handled scheme to remove.</param>
        /// <param name="url">The URI to the handler of the scheme.</param>
        [DomName("unregisterProtocolHandler")]
        void UnregisterProtocolHandler(String scheme, String url);

        /// <summary>
        /// Removes the specified content handler, if any.
        /// </summary>
        /// <param name="mimeType">The name of the handled mime-type to remove.</param>
        /// <param name="url">The URI to the handler of the mime-type.</param>
        [DomName("unregisterContentHandler")]
        void UnregisterContentHandler(String mimeType, String url);
    }
}
