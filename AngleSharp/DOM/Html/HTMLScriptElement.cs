using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents an HTML script element.
    /// </summary>
    public sealed class HTMLScriptElement : HTMLRawtextElement
    {
        #region Constant

        /// <summary>
        /// The script tag.
        /// </summary>
        internal const string Tag = "script";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML script element.
        /// </summary>
        internal HTMLScriptElement()
        {
            _name = Tag;
            IsAsyncForced = true;
        }

        #endregion

        #region Internal Properties

        internal bool IsAsyncForced
        {
            get;
            set;
        }

        internal bool IsParserInserted
        {
            get;
            set;
        }

        internal bool IsAlreadyStarted
        {
            get;
            set;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets athe address of the resource.
        /// </summary>
        public string Src
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of an embedded resource.
        /// </summary>
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the character encoding of the external script resource.
        /// </summary>
        public string Charset
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if script should execute asynchronously.
        /// </summary>
        public bool Async
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the script should be deferred.
        /// </summary>
        public bool Defer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets how the element handles crossorigin requests.
        /// </summary>
        public CORSSettings CrossOrigin
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the text in the script element.
        /// </summary>
        public string Text
        {
            get { return TextContent; }
            set { TextContent = value; }
        }

        #endregion

        #region Protected properties

        protected internal override bool IsSpecial
        {
            get { return true; }
        }

        #endregion

        #region Internal Methods

        internal void Prepare()
        {
            //TODO
            //Prepare the script. This might cause some script to execute, which might cause new characters to be inserted into the tokenizer, and
            //might cause the tokenizer to output more tokens, resulting in a reentrant invocation of the parser.
        }

        internal void Execute()
        {
            //TODO
            //Execute the script
        }

        #endregion
    }
}
