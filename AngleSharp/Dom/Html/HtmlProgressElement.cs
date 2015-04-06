namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents the HTML progress element.
    /// https://html.spec.whatwg.org/multipage/forms.html#the-progress-element
    /// </summary>
    sealed class HtmlProgressElement : HtmlElement, ILabelabelElement, IHtmlProgressElement
    {
        #region Fields

        readonly NodeList labels;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML progress element.
        /// </summary>
        public HtmlProgressElement(Document owner, String prefix = null)
            : base(owner, Tags.Progress, prefix)
        {
            labels = new NodeList();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if labels are supported.
        /// </summary>
        public Boolean SupportsLabels
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the list of assigned labels.
        /// </summary>
        public INodeList Labels
        {
            get { return labels; }
        }

        /// <summary>
        /// Gets if the progress bar is determinate. Otherwise it is considered
        /// to be an indeterminate progress bar.
        /// </summary>
        public Boolean IsDeterminate
        {
            get { return !String.IsNullOrEmpty(GetOwnAttribute(AttributeNames.Value)); }
        }

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        public Double Value
        {
            get { return GetOwnAttribute(AttributeNames.Value).ToDouble(0.0); }
            set { SetOwnAttribute(AttributeNames.Value, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        public Double Maximum
        {
            get { return GetOwnAttribute(AttributeNames.Max).ToDouble(1.0); }
            set { SetOwnAttribute(AttributeNames.Max, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        public Double Position
        {
            get { return IsDeterminate ? Math.Max(Math.Min(Value / Maximum, 1.0), 0.0) : -1.0; }
        }

        #endregion
    }
}
