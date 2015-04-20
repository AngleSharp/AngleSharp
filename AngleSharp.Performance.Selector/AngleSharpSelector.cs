namespace AngleSharp.Performance.Selector
{
    using System;
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Parser.Html;

    class AngleSharpSelector : ITestee
    {
        static readonly IConfiguration configuration = new Configuration();

        IDocument document;

        public AngleSharpSelector(String source)
        {
            document = new HtmlParser(source, configuration).Parse();
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
