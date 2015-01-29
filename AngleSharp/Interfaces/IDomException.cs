namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Defines how a DOMException should look like.
    /// </summary>
    [DomName("DOMException")]
    public interface IDomException
    {
        /// <summary>
        /// Gets the error code for this exception.
        /// </summary>
        [DomName("code")]
        Int32 Code { get; }
    }
}
