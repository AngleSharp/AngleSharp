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
        public HtmlProgressElement(Document owner)
            : base(owner, Tags.Progress)
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
            get { return !String.IsNullOrEmpty(GetAttribute(String.Empty, AttributeNames.Value)); }
        }

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        public Double Value
        {
            get { return GetAttribute(String.Empty, AttributeNames.Value).ToDouble(0.0); }
            set { SetAttribute(String.Empty, AttributeNames.Value, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        public Double Maximum
        {
            get { return GetAttribute(String.Empty, AttributeNames.Max).ToDouble(1.0); }
            set { SetAttribute(String.Empty, AttributeNames.Max, value.ToString(NumberFormatInfo.InvariantInfo)); }
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
