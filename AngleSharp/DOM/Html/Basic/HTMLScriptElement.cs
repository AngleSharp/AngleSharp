using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents an HTML script element.
    /// </summary>
    public sealed class HTMLScriptElement : HTMLElement
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
            Async = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets athe address of the resource.
        /// </summary>
        public String Src
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of an embedded resource.
        /// </summary>
        public String Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the character encoding of the external script resource.
        /// </summary>
        public String Charset
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if script should execute asynchronously.
        /// </summary>
        public Boolean Async
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the script should be deferred.
        /// </summary>
        public Boolean Defer
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
        protected internal override bool IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
