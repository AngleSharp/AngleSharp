namespace AngleSharp.Performance.Html
{
    using Majestic13;
    using System;

    class MajesticParser : ITestee
    {
        public String Name
        {
            get { return "Majestic"; }
        }

        public Type Library
        {
            get { return typeof(HtmlParser); }
        }

        public void Run(String source)
        {
            HtmlParser parser = new HtmlParser();
            var node = parser.Parse(source);
        }
    }
}
