namespace AngleSharp.Performance.Html
{
    using AngleSharp.Html.Parser;
    using System;

    class AngleSharpParser : ITestee
    {
        private static readonly HtmlParser parser = new HtmlParser();

        public String Name => "AngleSharp";

        public Type Library => typeof(HtmlParser);

        public void Run(String source)
        {
            parser.ParseDocument(source);
        }
    }
}
