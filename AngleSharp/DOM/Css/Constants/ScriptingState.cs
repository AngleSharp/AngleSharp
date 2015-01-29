namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// Enumeration to describe the current scripting state.
    /// </summary>
    public enum ScriptingState
    {
        /// <summary>
        /// Scripts will not run for this document; either it doesn't
        /// support a scripting language, or the support isn't active
        /// for the current document.
        /// </summary>
        None,
        /// <summary>
        /// Scripting is enabled during the initial page load, but is
        /// not supported afterwards, e.g., printed pages.
        /// </summary>
        InitialOnly,
        /// <summary>
        /// Scripting of the page is supported in general and is active
        /// for the current document.
        /// </summary>
        Enabled
    }
}
