namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a document CSS rule.
    /// </summary>
    [DomName("CSSDocumentRule")]
    public interface ICssDocumentRule : ICssConditionRule
    {
        /// <summary>
        /// Gets the functions to evaluate as conditions.
        /// </summary>
        IEnumerable<IDocumentFunction> Conditions { get; }
    }
}
