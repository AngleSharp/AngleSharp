using Majestic13;
using System;

namespace Performance
{
    class MajesticParser : IHtmlParser
    {
        public String Name
        {
            get { return "Majestic"; }
        }

        public void Parse(String source)
        {
            HtmlParser parser = new HtmlParser();
            var node = parser.Parse(source);
        }
    }
}
