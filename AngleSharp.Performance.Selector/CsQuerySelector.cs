namespace AngleSharp.Performance.Selector
{
    using CsQuery;
    using CsQuery.HtmlParser;
    using System;

    class CsQuerySelector : ITestee
    {
        CQ document;

        public CsQuerySelector(String source)
        {
            document = CQ.CreateDocument(source);
        }

        public String Name
        {
            get { return "CsQuery"; }
        }

        public Type Library
        {
            get { return typeof(ElementFactory); }
        }

        public void Run(String selector)
        {
            document.Select(selector);
        }
    }
}
