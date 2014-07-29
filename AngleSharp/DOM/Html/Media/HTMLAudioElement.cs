namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML audio element.
    /// </summary>
    sealed class HTMLAudioElement : HTMLMediaElement, IHtmlAudioElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML audio element.
        /// </summary>
        internal HTMLAudioElement()
            : base(Tags.Audio)
        {
        }

        #endregion
    }
}
