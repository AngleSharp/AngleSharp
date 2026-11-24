namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Html.Parser;
    using System;

    /// <summary>
    /// Setting markup like innerHTML, but with declarative shadow roots (HTML 8.5.1).
    /// </summary>
    [DomExposed("Element")]
    [DomExposed("ShadowRoot")]
    public static class HtmlUnsafeExtensions
    {
        /// <summary>
        /// Replaces the children of the element with the parsed markup, in which templates with
        /// a shadowrootmode attach a shadow root to their parent.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="html">The markup.</param>
        [DomName("setHTMLUnsafe")]
        public static void SetHtmlUnsafe(this IElement element, String html) =>
            WithDeclarativeShadowRoots(() => element.InnerHtml = html);

        /// <summary>
        /// Replaces the children of the shadow root with the parsed markup, in which templates
        /// with a shadowrootmode attach a shadow root to their parent.
        /// </summary>
        /// <param name="shadowRoot">The shadow root.</param>
        /// <param name="html">The markup.</param>
        [DomName("setHTMLUnsafe")]
        public static void SetHtmlUnsafe(this IShadowRoot shadowRoot, String html) =>
            WithDeclarativeShadowRoots(() => shadowRoot.InnerHtml = html);

        private static void WithDeclarativeShadowRoots(Action action)
        {
            var allowed = HtmlDomBuilder.AllowsDeclarativeShadowRoots;
            HtmlDomBuilder.AllowsDeclarativeShadowRoots = true;

            try
            {
                action.Invoke();
            }
            finally
            {
                HtmlDomBuilder.AllowsDeclarativeShadowRoots = allowed;
            }
        }
    }
}
