namespace AngleSharp
{
    using AngleSharp.Factories;

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
        /// The CSS unit value factory.
        /// </summary>
        public static readonly CssUnitFactory Units = new CssUnitFactory();

        /// <summary>
        /// The CSS property factory.
        /// </summary>
        public static readonly CssPropertyFactory Properties = new CssPropertyFactory();

        /// <summary>
        /// The HTML input type provider factory.
        /// </summary>
        public static readonly InputTypeFactory InputTypes = new InputTypeFactory();

        /// <summary>
        /// The CSS media feature factory.
        /// </summary>
        public static readonly MediaFeatureFactory MediaFeatures = new MediaFeatureFactory();
    }
}
