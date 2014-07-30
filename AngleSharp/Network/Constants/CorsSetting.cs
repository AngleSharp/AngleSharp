namespace AngleSharp.Network
{
    /// <summary>
    /// CORS settings attributes (2.6.6).
    /// </summary>
    enum CorsSetting
    {
        /// <summary>
        /// The default (missing) value.
        /// </summary>
        None,
        /// <summary>
        /// Cross-origin CORS requests for the element will have the
        /// omit credentials flag set. 
        /// </summary>
        [DomName("anonymous")]
        Anonymous,
        /// <summary>
        /// Cross-origin CORS requests for the element will not have
        /// the omit credentials flag set
        /// </summary>
        [DomName("use-credentials")]
        UseCredentials
    }
}
