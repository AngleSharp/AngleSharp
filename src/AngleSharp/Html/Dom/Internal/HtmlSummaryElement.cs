namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
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

        internal override Boolean HasActivationBehavior => Parent is HtmlDetailsElement details && IsSummaryFor(details);

        internal override async Task ClickAsync(Boolean immediate, Event? dispatched = null, IEventTarget? target = null)
        {
            var cancelled = await IsClickedCancelled(immediate, dispatched, target).ConfigureAwait(false);

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
