namespace AngleSharp.Scripting
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using System.Text;

    /// <summary>
    /// Transport object for running scripts.
    /// </summary>
    public sealed class ScriptOptions
    {
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
        /// Gets or sets the context of the document.
        /// </summary>
        public IWindow Context
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the assigned document.
        /// </summary>
        public IDocument Document
        {
            get;
            set;
        }
    }
}
