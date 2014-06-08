namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents an HTML script element.
    /// </summary>
    [DomName("HTMLScriptElement")]
    public sealed class HTMLScriptElement : HTMLElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML script element.
        /// </summary>
        internal HTMLScriptElement()
        {
            _name = Tags.Script;
            Async = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets athe address of the resource.
        /// </summary>
        [DomName("src")]
        public String Src
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of an embedded resource.
        /// </summary>
        [DomName("type")]
        public String Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the character encoding of the external script resource.
        /// </summary>
        [DomName("charset")]
        public String Charset
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if script should execute asynchronously.
        /// </summary>
        [DomName("async")]
        public Boolean Async
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the script should be deferred.
        /// </summary>
        [DomName("defer")]
        public Boolean Defer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets how the element handles crossorigin requests.
        /// </summary>
        [DomName("crossOrigin")]
        public CORSSettings CrossOrigin
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the text in the script element.
        /// </summary>
        [DomName("text")]
        public String Text
        {
            get { return TextContent; }
            set { TextContent = value; }
        }

        #endregion

        #region Protected properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// More information available at:
        /// http://www.w3.org/TR/html5/scripting-1.html#execute-the-script-block
        /// </summary>
        internal void Run()
        {
            //TODO Try to find implementation of language and execute script
        }

        /// <summary>
        /// More information available at:
        /// http://www.w3.org/TR/html5/scripting-1.html#prepare-a-script
        /// </summary>
        internal void Prepare()
        {
            //TODO
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a special textual representation of the node.
        /// </summary>
        /// <returns>A string containing only (rendered) text.</returns>
        public override String ToText()
        {
            return String.Empty;
        }

        #endregion
    }
}
