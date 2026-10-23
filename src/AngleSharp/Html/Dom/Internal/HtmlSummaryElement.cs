namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the HTML summary element. The specification gives it no interface of its own
    /// (it is an HTMLElement), only the activation behavior of toggling its details element.
    /// </summary>
    sealed class HtmlSummaryElement : HtmlElement
    {
        #region ctor

        public HtmlSummaryElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Summary, prefix, NodeFlags.Special)
        {
        }

        #endregion

        #region Helpers

        internal override async Task ClickAsync()
        {
            var cancelled = await IsClickedCancelled().ConfigureAwait(false);

            if (!cancelled && Parent is HtmlDetailsElement details && IsSummaryFor(details))
            {
                details.IsOpen = !details.IsOpen;
            }
        }

        /// <summary>
        /// Only the first summary child of a details element is its summary; any other one has
        /// no activation behavior.
        /// https://html.spec.whatwg.org/multipage/interactive-elements.html#summary-for-its-parent-details
        /// </summary>
        private Boolean IsSummaryFor(HtmlDetailsElement details)
        {
            foreach (var child in details.ChildNodes)
            {
                if (child is HtmlSummaryElement summary)
                {
                    return Object.ReferenceEquals(summary, this);
                }
            }

            return false;
        }

        #endregion
    }
}
