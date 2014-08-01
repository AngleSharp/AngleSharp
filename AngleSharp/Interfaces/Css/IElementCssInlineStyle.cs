namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Used to mark elements that may have inline style,
    /// usually set and defined over an attribute.
    /// </summary>
    [DomName("ElementCSSInlineStyle")]
    public interface IElementCssInlineStyle
    {
        /// <summary>
        /// Gets an object representing the declarations of an element's style attributes.
        /// </summary>
        [DomName("style")]
        ICssStyleDeclaration Style { get; }
    }
}
