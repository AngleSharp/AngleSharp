namespace AngleSharp.DOM.Navigator
{
    using System;

    [DomName("NavigatorContentUtils")]
    interface INavigatorContentUtilities
    {
        [DomName("registerProtocolHandler")]
        void RegisterProtocolHandler(String scheme, String url, String title);

        [DomName("registerContentHandler")]
        void RegisterContentHandler(String mimeType, String url, String title);

        [DomName("isProtocolHandlerRegistered")]
        String IsProtocolHandlerRegistered(String scheme, String url);

        [DomName("isContentHandlerRegistered")]
        String IsContentHandlerRegistered(String mimeType, String url);

        [DomName("unregisterProtocolHandler")]
        void UnregisterProtocolHandler(String scheme, String url);

        [DomName("unregisterContentHandler")]
        void UnregisterContentHandler(String mimeType, String url);
    }
}
