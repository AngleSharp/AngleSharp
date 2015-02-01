namespace AngleSharp.Performance.Html
{
    using CsQuery;
    using CsQuery.ExtensionMethods.Internal;
    using CsQuery.HtmlParser;
    using System;
    using System.Text;

    class CsQueryParser : ITestee
    {
        public String Name
        {
            get { return "CsQuery"; }
        }

        public Type Library
        {
            get { return typeof(ElementFactory); }
        }

        public void Run(String source)
        {
            var factory = new ElementFactory(DomIndexProviders.Simple);

            using (var stream = source.ToStream())
            {
                var document = factory.Parse(stream, Encoding.UTF8);
            }
        }
    }
}
