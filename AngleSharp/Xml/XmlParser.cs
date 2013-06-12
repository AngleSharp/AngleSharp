using System;
using System.Threading.Tasks;

namespace AngleSharp.Xml
{
    public class XmlParser : IParser
    {
        //TODO
        //http://www.w3.org/html/wg/drafts/html/master/the-xhtml-syntax.html#xml-parser

        //http://www.w3.org/TR/xml11/

        public void Parse()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<ParseErrorEventArgs> ErrorOccurred;

        public Boolean IsAsync
        {
            get { throw new NotImplementedException(); }
        }

        public Task ParseAsync()
        {
            throw new NotImplementedException();
        }
    }
}
