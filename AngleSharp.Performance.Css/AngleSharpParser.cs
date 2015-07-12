namespace AngleSharp.Performance.Css
{
    using AngleSharp;
    using AngleSharp.Parser.Css;
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
            get { return typeof(CssParser); }
        }

        public void Run(String source)
        {
            parser.ParseStylesheet(source);
        }
    }
}
