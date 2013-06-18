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
        /// <summary>
        /// This method is not yet implemented.
        /// </summary>
        public void Parse()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This event is raised once a parser error occured.
        /// </summary>
        public event EventHandler<ParseErrorEventArgs> ErrorOccurred;

        /// <summary>
        /// Gets the status if the parsing process is asynchronous.
        /// </summary>
        public Boolean IsAsync
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// This method is not yet implemented.
        /// </summary>
        /// <returns>A task that can be used to determine the status.</returns>
        public Task ParseAsync()
        {
            throw new NotImplementedException();
        }
    }
}
