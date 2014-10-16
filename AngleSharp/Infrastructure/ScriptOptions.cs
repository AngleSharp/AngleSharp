namespace AngleSharp.Infrastructure
{
    using AngleSharp.DOM.Html;
    using System.Text;

    /// <summary>
    /// Transport object for running scripts.
    /// </summary>
    public sealed class ScriptOptions : BaseOptions
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
    }
}
