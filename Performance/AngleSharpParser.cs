namespace Performance
{
    using AngleSharp;
    using AngleSharp.Parser.Html;
    using System;

    class AngleSharpParser : IHtmlParser
    {
        static readonly IConfiguration configuration = new Configuration { AllowRequests = false, IsStyling = false, IsScripting = false };

        public String Name
        {
            get { return "AngleSharp"; }
        }

        public void Parse(String source)
        {
            var parser = new HtmlParser(source, configuration);
            parser.Parse();
        }
    }
}
