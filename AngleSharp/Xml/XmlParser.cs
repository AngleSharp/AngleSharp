using System;
using System.Threading.Tasks;

namespace AngleSharp.Xml
{
    /// <summary>
    /// WARNING: This class is not yet implemented.
    /// See http://www.w3.org/TR/xml11/ and 
    /// http://www.w3.org/html/wg/drafts/html/master/the-xhtml-syntax.html#xml-parser
    /// for more details.
    /// </summary>
    public class XmlParser : IParser
    {
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
