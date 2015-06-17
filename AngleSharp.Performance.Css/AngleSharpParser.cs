namespace AngleSharp.Performance.Css
{
    using System;
    using AngleSharp;
    using AngleSharp.Parser.Css;

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
            parser.Parse();
        }
    }
}
