namespace AngleSharp.Performance.Html
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
            parser.Parse();
        }
    }
}
