namespace AngleSharp.Dom
{
    using System;

    /// <summary>
    /// Setting used to restrict the abilities that potentially
    /// untrusted resources have.
    /// </summary>
    [Flags]
    public enum Sandboxes
    {
        /// <summary>
        /// No flag is set, everything is accepted.
        /// </summary>
        None = 0,
        /// <summary>
        /// This flag prevents content from navigating browsing contexts
        /// other than the sandboxed browsing context itself (or browsing
        /// contexts further nested inside it), auxiliary browsing contexts
        /// (which are protected by the sandboxed auxiliary navigation
        /// browsing context flag defined next), and the top-level browsing
        /// context (which is protected by the sandboxed top-level navigation
        /// browsing context flag defined below).
        /// </summary>
        Navigation = 0x1,
        /// <summary>
        /// This flag prevents content from creating new auxiliary browsing
        /// contexts, e.g. using the target attribute, or the window.open()
        /// method.
        /// </summary>
        AuxiliaryNavigation = 0x2,
        /// <summary>
        /// This flag prevents content from navigating their top-level browsing
        /// context and prevents content from closing their top-level browsing
        /// context.
        /// </summary>
        TopLevelNavigation = 0x4,
        /// <summary>
        /// This flag prevents content from instantiating plugins, whether using
        /// the embed element, the object element, the applet element, or through
        /// navigation of a nested browsing context, unless those plugins can be
        /// secured.
        /// </summary>
        Plugins = 0x8,
        /// <summary>
        /// This flag forces content into a unique origin, thus preventing it
        /// from accessing other content from the same origin.
        /// </summary>
        Origin = 0x10,
        /// <summary>
        /// This flag blocks form submission.
        /// </summary>
        Forms = 0x20,
        /// <summary>
        /// This flag disables the Pointer Lock API.
        /// </summary>
        PointerLock = 0x40,
        /// <summary>
        /// This flag blocks script execution.
        /// </summary>
        Scripts = 0x80,
        /// <summary>
        /// This flag blocks features that trigger automatically, such as automatically
        /// playing a video or automatically focusing a form control.
        /// </summary>
        AutomaticFeatures = 0x100,
        /// <summary>
        /// This flag prevents content from using the requestFullscreen() method.
        /// </summary>
        Fullscreen = 0x200,
        /// <summary>
        /// This flag prevents content from using the document.domain feature to change
        /// the effective script origin.
        /// </summary>
        DocumentDomain = 0x400
    }
}
