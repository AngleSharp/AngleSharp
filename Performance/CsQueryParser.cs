using System;
using CsQuery;

namespace Performance
{
    class CsQueryParser : IHtmlParser
    {
        public String Name
        {
            get { return "CsQuery"; }
        }

        public void Parse(String source)
        {
            var document = CQ.CreateDocument(source);
        }
    }
}
