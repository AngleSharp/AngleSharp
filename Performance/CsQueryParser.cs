namespace Performance
{
    using CsQuery;
    using CsQuery.ExtensionMethods.Internal;
    using CsQuery.HtmlParser;
    using System;
    using System.Text;

    class CsQueryParser : IHtmlParser
    {
        public String Name
        {
            get { return "CsQuery"; }
        }

        public void Parse(String source)
        {
            var factory = new ElementFactory(DomIndexProviders.Simple);

            using (var stream = source.ToStream())
            {
                var document = factory.Parse(stream, Encoding.UTF8);
            }
        }
    }
}
