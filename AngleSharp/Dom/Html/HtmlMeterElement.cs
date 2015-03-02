namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents the HTML meter element.
    /// https://html.spec.whatwg.org/multipage/forms.html#dom-meter-low
    /// </summary>
    sealed class HtmlMeterElement : HtmlElement, ILabelabelElement, IHtmlMeterElement
    {
        #region Fields

        readonly NodeList labels;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML meter element.
        /// </summary>
        public HtmlMeterElement(Document owner)
            : base(owner, Tags.Meter)
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

        public INodeList Labels
        {
            get { return labels; }
        }

        public Double Value
        {
            get { return GetAttribute(String.Empty, AttributeNames.Value).ToDouble(0.0); }
            set { SetAttribute(String.Empty, AttributeNames.Value, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        public Double Maximum
        {
            get { return GetAttribute(String.Empty, AttributeNames.Max).ToDouble(1.0); }
            set { SetAttribute(String.Empty, AttributeNames.Max, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        public Double Minimum
        {
            get { return GetAttribute(String.Empty, AttributeNames.Min).ToDouble(1.0); }
            set { SetAttribute(String.Empty, AttributeNames.Min, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        public Double Low
        {
            get { return GetAttribute(String.Empty, AttributeNames.Low).ToDouble(1.0); }
            set { SetAttribute(String.Empty, AttributeNames.Low, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        public Double High
        {
            get { return GetAttribute(String.Empty, AttributeNames.High).ToDouble(1.0); }
            set { SetAttribute(String.Empty, AttributeNames.High, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        public Double Optimum
        {
            get { return GetAttribute(String.Empty, AttributeNames.Optimum).ToDouble(1.0); }
            set { SetAttribute(String.Empty, AttributeNames.Optimum, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        #endregion
    }
}
