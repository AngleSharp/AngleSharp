namespace AngleSharp.Performance.Selector
{
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Parser.Html;
    using System;

    class AngleSharpSelector : ITestee
    {
        static readonly IConfiguration configuration = new Configuration { IsStyling = false };

        IDocument document;

        public AngleSharpSelector(String source)
        {
            document = DocumentBuilder.Html(source, configuration);
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
