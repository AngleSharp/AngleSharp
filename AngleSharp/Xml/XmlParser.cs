using AngleSharp.DOM;
using AngleSharp.DOM.Xml;
using System;
using System.Collections.Generic;
using System.IO;
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
        Boolean started;
        XMLDocument doc;
        List<Element> open;
        XmlTreeMode insert;
        TaskCompletionSource<Boolean> tcs;

        #endregion

        #region Events

        /// <summary>
        /// This event is raised once a parser error occured.
        /// </summary>
        public event EventHandler<ParseErrorEventArgs> ErrorOccurred;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new instance of the XML parser with an new document
        /// based on the given source.
        /// </summary>
        /// <param name="source">The source code as a string.</param>
        public XmlParser(String source)
            : this(new XMLDocument(), new SourceManager(source))
        {
        }

        /// <summary>
        /// Creates a new instance of the XML parser with an new document
        /// based on the given stream.
        /// </summary>
        /// <param name="stream">The stream to use as source.</param>
        public XmlParser(Stream stream)
            : this(new XMLDocument(), new SourceManager(stream))
        {
        }

        /// <summary>
        /// Creates a new instance of the XML parser with the specified document
        /// based on the given source.
        /// </summary>
        /// <param name="document">The document instance to be constructed.</param>
        /// <param name="source">The source code as a string.</param>
        public XmlParser(XMLDocument document, String source)
            : this(document, new SourceManager(source))
        {
        }

        /// <summary>
        /// Creates a new instance of the XML parser with the specified document
        /// based on the given stream.
        /// </summary>
        /// <param name="document">The document instance to be constructed.</param>
        /// <param name="stream">The stream to use as source.</param>
        public XmlParser(XMLDocument document, Stream stream)
            : this(document, new SourceManager(stream))
        {
        }

        /// <summary>
        /// Creates a new instance of the XML parser with the specified document
        /// based on the given source manager.
        /// </summary>
        /// <param name="document">The document instance to be constructed.</param>
        /// <param name="source">The source to use.</param>
        internal XmlParser(XMLDocument document, SourceManager source)
        {
            tokenizer = new XmlTokenizer(source);

            tokenizer.ErrorOccurred += (s, ev) =>
            {
                if (ErrorOccurred != null)
                    ErrorOccurred(this, ev);
            };

            started = false;
            doc = document;
            open = new List<Element>();
            insert = XmlTreeMode.Initial;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current node.
        /// </summary>
        internal Element CurrentNode
        {
            get { return open.Count > 0 ? open[open.Count - 1] : null; }
        }

        /// <summary>
        /// Gets the (maybe intermediate) result of the parsing process.
        /// </summary>
        public XMLDocument Result
        {
            get
            {
                Parse();
                return doc;
            }
        }

        /// <summary>
        /// Gets if the parser has been started asynchronously.
        /// </summary>
        public Boolean IsAsync
        {
            get { return tcs != null; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses the given source and creates the document.
        /// </summary>
        public void Parse()
        {
            if (!started)
            {
                started = true;
                XmlToken token;

                do
                {
                    token = tokenizer.Get();
                    Consume(token);
                }
                while (token.Type != XmlTokenType.EOF);
            }
        }

        /// <summary>
        /// Parses the given source asynchronously and creates the document.
        /// WARNING: This method is not yet implemented.
        /// </summary>
        /// <returns>The task which could be awaited or continued differently.</returns>
        public Task ParseAsync()
        {
            if (!started)
            {
                started = true;
                tcs = new TaskCompletionSource<bool>();
                //TODO
                return tcs.Task;
            }
            else if (tcs == null)
            {
                var temp = new TaskCompletionSource<bool>();
                temp.SetResult(true);
                return temp.Task;
            }

            return tcs.Task;
        }

        /// <summary>
        /// Consumes a token and processes it.
        /// </summary>
        /// <param name="token">The token to consume.</param>
        void Consume(XmlToken token)
        {
            switch (insert)
            {
                case XmlTreeMode.Initial:
                    Initial(token);
                    break;
                case XmlTreeMode.Prolog:
                    BeforeDoctype(token);
                    break;
                case XmlTreeMode.Body:
                    InBody(token);
                    break;
            }
        }

        #endregion

        #region States

        void Initial(XmlToken token)
        {
            if (token.Type == XmlTokenType.Declaration)
            {
                //The declaration token
            }
            else if (!token.IsIgnorable)
            {
                //Error
                insert = XmlTreeMode.Prolog;
                BeforeDoctype(token);
            }
        }

        void BeforeDoctype(XmlToken token)
        {
            if (token.Type == XmlTokenType.DOCTYPE)
            {
                //Add doctype
                insert = XmlTreeMode.Body;
            }
            else if (token.Type == XmlTokenType.ProcessingInstruction)
            {
                var tok = (XmlPIToken)token;
                var pi = doc.CreateProcessingInstruction(tok.Target, tok.Content);
                doc.AppendChild(pi);
            }
            else if (token.Type == XmlTokenType.Comment)
            {
                var tok = (XmlCommentToken)token;
                var com = doc.CreateComment(tok.Data);
                doc.AppendChild(com);
            }
            else if (!token.IsIgnorable)
            {
                insert = XmlTreeMode.Body;
                InBody(token);
            }
        }

        void InBody(XmlToken token)
        {
            switch (token.Type)
            {
                case XmlTokenType.StartTag:
                    break;
                case XmlTokenType.EndTag:
                    break;
                case XmlTokenType.Comment:
                    //Append comment to node
                    break;
                case XmlTokenType.ProcessingInstruction:
                    //Add processing instruction
                    break;
                case XmlTokenType.Character:
                    //Append character to node
                    break;
                case XmlTokenType.EOF:
                    //Close open tags
                    //If tags are still open --> error
                    break;
                case XmlTokenType.DOCTYPE:
                    //Ignore
                    //Error
                    break;
                case XmlTokenType.Declaration:
                    //Ignore
                    //Error
                    break;
            }
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
