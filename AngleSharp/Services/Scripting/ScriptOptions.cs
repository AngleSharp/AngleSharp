namespace AngleSharp.Services.Scripting
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
        /// Creates new script options for the given document.
        /// </summary>
        /// <param name="document">The document to use.</param>
        public ScriptOptions(IDocument document)
        {
            Document = document;
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
        /// Gets the parent document of the executing script.
        /// </summary>
        public IDocument Document
        {
            get;
            private set;
        }
    }
}
