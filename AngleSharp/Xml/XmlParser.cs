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
        #region Members

        XmlTokenizer tokenizer;

        #endregion

        #region Events

        /// <summary>
        /// This event is raised once a parser error occured.
        /// </summary>
        public event EventHandler<ParseErrorEventArgs> ErrorOccurred;

        #endregion

        #region ctor

        #endregion

        #region Properties

        /// <summary>
        /// Gets the status if the parsing process is asynchronous.
        /// </summary>
        public Boolean IsAsync
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// This method is not yet implemented.
        /// </summary>
        public void Parse()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is not yet implemented.
        /// </summary>
        /// <returns>A task that can be used to determine the status.</returns>
        public Task ParseAsync()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Fires an error occurred event.
        /// </summary>
        /// <param name="code">The associated error code.</param>
        void RaiseErrorOccurred(ErrorCode code)
        {
            if (ErrorOccurred != null)
            {
                var pck = new ParseErrorEventArgs((int)code, Errors.GetError(code));
                pck.Line = tokenizer.Stream.Line;
                pck.Column = tokenizer.Stream.Column;
                ErrorOccurred(this, pck);
            }
        }

        #endregion
    }
}
