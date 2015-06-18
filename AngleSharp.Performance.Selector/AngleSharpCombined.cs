namespace AngleSharp.Performance.Selector
{
    using System;
    using AngleSharp;
    using AngleSharp.Parser.Html;

    class AngleSharpParser : ITestee
    {
        static readonly IConfiguration configuration = new Configuration();

        public String Name
        {
            get { return "AngleSharp"; }
        }

        public Type Library
        {
            get { return typeof(HtmlParser); }
        }

        public void Run(String source)
        {
            var parser = new HtmlParser(source, configuration);
            var document = parser.Parse();
            // Even using two kinds of selectors -- measure perf.
            document.QuerySelectorAll("a[href]");
            document.QuerySelectorAll("div > p > a");
        }
    }
}
