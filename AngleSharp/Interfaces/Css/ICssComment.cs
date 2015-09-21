namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Represents a comment in the CSSOM.
    /// </summary>
    public interface ICssComment : ICssNode
    {
        /// <summary>
        /// Gets the contained comment data.
        /// </summary>
        String Data { get; }
    }
}
