namespace AngleSharp.Performance.Css
{
    using AngleSharp;
    using AngleSharp.Parser.Css;
    using System;

    class AngleSharpParser : ITestee
    {
        static readonly IConfiguration configuration = new Configuration().WithCss();

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
            var parser = new CssParser(source, configuration);
            parser.Parse(new CssParserOptions
            {
                IsIncludingUnknownDeclarations = true,
                IsIncludingUnknownRules = true,
                IsToleratingInvalidConstraints = true,
                IsToleratingInvalidValues = true
            });
        }
    }
}
