namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// The same as TokenList, except that it allows the
    /// underlying string to be directly changed.
    /// </summary>
    [DOM("DOMSettableTokenList")]
    public interface ISettableTokenList : ITokenList
    {
        /// <summary>
        /// Gets or sets the underlying string.
        /// </summary>
        [DOM("value")]
        String Value { get; set; }
    }
}