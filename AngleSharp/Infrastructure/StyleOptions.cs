namespace AngleSharp.Infrastructure
{
    using AngleSharp.DOM;

    /// <summary>
    /// Transport object for evaluating stylesheets.
    /// </summary>
    public sealed class StyleOptions : BaseOptions
    {
        /// <summary>
        /// Gets or sets the element that triggered the evaluation.
        /// </summary>
        public IElement Element
        {
            get;
            set;
        }
    }
}
