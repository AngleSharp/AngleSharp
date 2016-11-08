namespace AngleSharp.Performance.Html
{
    using AngleSharp;
    using AngleSharp.Html.Parser;
    using System;

    class AngleSharpParser : ITestee
    {
        private static readonly IConfiguration configuration = new Configuration();
        private static readonly HtmlParser parser = new HtmlParser(BrowsingContext.New(configuration));

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
            parser.Parse(source);
        }
    }
}
