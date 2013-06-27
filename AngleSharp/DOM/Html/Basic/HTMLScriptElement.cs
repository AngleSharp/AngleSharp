using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents an HTML script element.
    /// </summary>
    [DOM("HTMLScriptElement")]
    public sealed class HTMLScriptElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The script tag.
        /// </summary>
        internal const String Tag = "script";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML script element.
        /// </summary>
        internal HTMLScriptElement()
        {
            _name = Tag;
            Async = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets athe address of the resource.
        /// </summary>
        [DOM("src")]
        public String Src
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of an embedded resource.
        /// </summary>
        [DOM("type")]
        public String Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the character encoding of the external script resource.
        /// </summary>
        [DOM("charset")]
        public String Charset
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if script should execute asynchronously.
        /// </summary>
        [DOM("async")]
        public Boolean Async
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the script should be deferred.
        /// </summary>
        [DOM("defer")]
        public Boolean Defer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets how the element handles crossorigin requests.
        /// </summary>
        [DOM("crossOrigin")]
        public CORSSettings CrossOrigin
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the text in the script element.
        /// </summary>
        [DOM("text")]
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
    }
}
