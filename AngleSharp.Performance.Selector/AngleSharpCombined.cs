namespace AngleSharp.Performance.Selector
{
    using AngleSharp;
    using AngleSharp.Parser.Html;
    using System;

    class AngleSharpParser : ITestee
    {
        static readonly IConfiguration configuration = new Configuration();
        static readonly HtmlParser parser = new HtmlParser(configuration);

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
            var document = parser.Parse(source);
            // Even using two kinds of selectors -- measure perf.
            document.QuerySelectorAll("a[href]");
            document.QuerySelectorAll("div > p > a");
        }
    }
}
