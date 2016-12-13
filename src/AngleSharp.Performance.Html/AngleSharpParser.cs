namespace AngleSharp.Performance.Html
{
    using AngleSharp.Html.Parser;
    using System;

    class AngleSharpParser : ITestee
    {
        private static readonly HtmlParser parser = new HtmlParser();

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
            parser.ParseDocument(source);
        }
    }
}
