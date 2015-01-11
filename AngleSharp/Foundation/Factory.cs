namespace AngleSharp
{
    using AngleSharp.Factories;

    static class Factory
    {
        public static readonly HtmlElementFactory HtmlElements = new HtmlElementFactory();

        public static readonly MathElementFactory MathElements = new MathElementFactory();

        public static readonly SvgElementFactory SvgElements = new SvgElementFactory();

        public static readonly EventFactory Events = new EventFactory();

        public static readonly CssUnitFactory Units = new CssUnitFactory();

        public static readonly CssPropertyFactory Properties = new CssPropertyFactory();

        public static readonly InputTypeFactory InputTypes = new InputTypeFactory();

        public static readonly MediaFeatureFactory MediaFeatures = new MediaFeatureFactory();
    }
}
