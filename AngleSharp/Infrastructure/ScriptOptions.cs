namespace AngleSharp.Infrastructure
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Html;
    using System;
    using System.Text;

    /// <summary>
    /// Transport object for running scripts.
    /// </summary>
    public sealed class ScriptOptions
    {
        /// <summary>
        /// Gets or sets the context in which the script should run.
        /// </summary>
        public IWindow Context
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the assigned document of the script.
        /// </summary>
        public IDocument Document
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the script element that triggered the invocation.
        /// </summary>
        public IHtmlScriptElement Element
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the encoding that has been selected for the script.
        /// </summary>
        public Encoding Encoding
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the base path of the script.
        /// </summary>
        public String BaseUri
        {
            get;
            set;
        }
    }
}
