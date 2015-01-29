namespace AngleSharp.Infrastructure
{
    using AngleSharp.Dom;

    /// <summary>
    /// A shared set of options. Just plain data transfer.
    /// </summary>
    public class BaseOptions
    {
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
