namespace AngleSharp.Infrastructure
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Html;

    /// <summary>
    /// Transport object for running scripts.
    /// </summary>
    public class ScriptOptions
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
    }
}
