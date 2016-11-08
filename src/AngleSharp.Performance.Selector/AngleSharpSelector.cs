namespace AngleSharp.Performance.Selector
{
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using System;

    class AngleSharpSelector : ITestee
    {
        private static readonly IConfiguration configuration = new Configuration();
        private static readonly HtmlParser parser = new HtmlParser(BrowsingContext.New(configuration));

        private IDocument document;

        public AngleSharpSelector(String source)
        {
            document = parser.Parse(source);
        }

        public String Name
        {
            get { return "AngleSharp"; }
        }

        public Type Library
        {
            get { return typeof(HtmlParser); }
        }

        public void Run(String selector)
        {
            document.QuerySelectorAll(selector);
        }
    }
}
