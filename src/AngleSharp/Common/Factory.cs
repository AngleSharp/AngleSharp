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
        public static readonly DefaultEventFactory Events = new DefaultEventFactory();

        /// <summary>
        /// The HTML input type provider factory.
        /// </summary>
        public static readonly DefaultInputTypeFactory InputTypes = new DefaultInputTypeFactory();

        /// <summary>
        /// The CSS attribute selector factory.
        /// </summary>
        public static readonly DefaultAttributeSelectorFactory AttributeSelector = new DefaultAttributeSelectorFactory();

        /// <summary>
        /// The CSS pseudo element selector factory.
        /// </summary>
        public static readonly DefaultPseudoElementSelectorFactory PseudoElementSelector = new DefaultPseudoElementSelectorFactory();

        /// <summary>
        /// The CSS pseudo class selector factory.
        /// </summary>
        public static readonly DefaultPseudoClassSelectorFactory PseudoClassSelector = new DefaultPseudoClassSelectorFactory();

        /// <summary>
        /// The link rel type factory.
        /// </summary>
        public static readonly DefaultLinkRelationFactory LinkRelations = new DefaultLinkRelationFactory();

        /// <summary>
        /// The document factory.
        /// </summary>
        public static readonly DefaultDocumentFactory Document = new DefaultDocumentFactory();
    }
}
