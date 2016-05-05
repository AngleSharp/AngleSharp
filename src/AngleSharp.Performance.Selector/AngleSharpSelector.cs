namespace AngleSharp.Performance.Selector
{
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Parser.Html;
    using System;

    class AngleSharpSelector : ITestee
    {
        static readonly IConfiguration configuration = new Configuration();
        static readonly HtmlParser parser = new HtmlParser(configuration);

        IDocument document;

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
