namespace AngleSharp.Performance.Selector
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using System;

    class AngleSharpSelector : ITestee
    {
        private static readonly HtmlParser parser = new HtmlParser();

        private IDocument document;

        public AngleSharpSelector(String source)
        {
            document = parser.ParseDocument(source);
        }

        public String Name => "AngleSharp";

        public Type Library => typeof(HtmlParser);

        public void Run(String selector)
        {
            document.QuerySelectorAll(selector);
        }
    }
}
