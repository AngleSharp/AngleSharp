namespace AngleSharp.Performance.Css
{
    using AngleSharp;
    using AngleSharp.Css.Parser;
    using System;

    class AngleSharpParser : ITestee
    {
        static readonly IConfiguration configuration = new Configuration().WithCss();
        static readonly CssParserOptions options = new CssParserOptions
        {
            IsIncludingUnknownDeclarations = true,
            IsIncludingUnknownRules = true,
            IsToleratingInvalidConstraints = true,
            IsToleratingInvalidValues = true
        };
        static readonly CssParser parser = new CssParser(options, configuration);

        public String Name
        {
            get { return "AngleSharp"; }
        }

        public Type Library
        {
            get { return typeof(BrowsingContext); }
        }

        public void Run(String source)
        {
            parser.ParseStylesheet(source);
        }
    }

    //TODO:
    // Remove both after AngleSharp.Css is available
    static class ConfigurationMockExtensions
    {
        public static IConfiguration WithCss(this IConfiguration configuration)
        {
            return configuration;
        }
    }

    sealed class CssParser
    {
        public CssParser(CssParserOptions options, IConfiguration configuration)
        {
        }

        public void ParseStylesheet(String source)
        {
        }
    }
}
