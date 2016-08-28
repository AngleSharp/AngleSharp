namespace AngleSharp.Network
{
    /// <summary>
    /// CORS settings attributes (see 2.6.6).
    /// </summary>
    public enum CorsSetting : byte
    {
        /// <summary>
        /// The default (missing) value.
        /// </summary>
        None,
        /// <summary>
        /// Cross-origin CORS requests for the element will have the omit 
        /// credentials flag set. 
        /// </summary>
        Anonymous,
        /// <summary>
        /// Cross-origin CORS requests for the element will not have the omit 
        /// credentials flag set
        /// </summary>
        UseCredentials
    }
}
