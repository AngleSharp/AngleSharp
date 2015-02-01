namespace AngleSharp.Performance.Html
{
    using AngleSharp;
    using AngleSharp.Parser.Html;
    using System;

    class AngleSharpParser : ITestee
    {
        static readonly IConfiguration configuration = new Configuration { IsStyling = false, IsScripting = false };

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
