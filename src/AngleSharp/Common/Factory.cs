namespace AngleSharp.Common
{
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using AngleSharp.Html;
    using AngleSharp.Mathml;
    using AngleSharp.Svg;

    /// <summary>
    /// Bundles the available factories.
    /// </summary>
    static class Factory
    {
        /// <summary>
        /// The HTML element factory.
        /// </summary>
        public static readonly HtmlElementFactory HtmlElements = new HtmlElementFactory();

        /// <summary>
        /// The MathML element factory.
        /// </summary>
        public static readonly MathElementFactory MathElements = new MathElementFactory();

        /// <summary>
        /// The SVG element factory.
        /// </summary>
        public static readonly SvgElementFactory SvgElements = new SvgElementFactory();

        /// <summary>
        /// The DOM events factory.
        /// </summary>
        public static readonly EventFactory Events = new EventFactory();

        /// <summary>
        /// The HTML input type provider factory.
        /// </summary>
        public static readonly DefaultInputTypeFactory InputTypes = new DefaultInputTypeFactory();

        /// <summary>
        /// The CSS attribute selector factory.
        /// </summary>
        public static readonly AttributeSelectorFactory AttributeSelector = new AttributeSelectorFactory();

        /// <summary>
        /// The CSS pseudo element selector factory.
        /// </summary>
        public static readonly PseudoElementSelectorFactory PseudoElementSelector = new PseudoElementSelectorFactory();

        /// <summary>
        /// The CSS pseudo class selector factory.
        /// </summary>
        public static readonly PseudoClassSelectorFactory PseudoClassSelector = new PseudoClassSelectorFactory();

        /// <summary>
        /// The link rel type factory.
        /// </summary>
        public static readonly LinkRelationFactory LinkRelations = new LinkRelationFactory();

        /// <summary>
        /// The document factory.
        /// </summary>
        public static readonly DefaultDocumentFactory Document = new DefaultDocumentFactory();
    }
}
