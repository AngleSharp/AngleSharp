namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using System;

    /// <summary>
    /// Represents the parser for a selector.
    /// </summary>
    public interface ICssSelectorParser
    {
        /// <summary>
        /// Takes a string and transforms it into a selector object.
        /// </summary>
        ISelector ParseSelector(String selectorText);
    }
}
