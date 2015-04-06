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
        public HtmlMeterElement(Document owner, String prefix = null)
            : base(owner, Tags.Meter, prefix)
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
            get { return GetOwnAttribute(AttributeNames.Value).ToDouble(0.0).Constraint(Minimum, Maximum); }
            set { SetOwnAttribute(AttributeNames.Value, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        public Double Maximum
        {
            get { return GetOwnAttribute(AttributeNames.Max).ToDouble(1.0).Constraint(Minimum, Double.PositiveInfinity); }
            set { SetOwnAttribute(AttributeNames.Max, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        public Double Minimum
        {
            get { return GetOwnAttribute(AttributeNames.Min).ToDouble(0.0); }
            set { SetOwnAttribute(AttributeNames.Min, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        public Double Low
        {
            get { return GetOwnAttribute(AttributeNames.Low).ToDouble(Minimum).Constraint(Minimum, Maximum); }
            set { SetOwnAttribute(AttributeNames.Low, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        public Double High
        {
            get { return GetOwnAttribute(AttributeNames.High).ToDouble(Maximum).Constraint(Low, Maximum); }
            set { SetOwnAttribute(AttributeNames.High, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        public Double Optimum
        {
            get { return GetOwnAttribute(AttributeNames.Optimum).ToDouble((Maximum + Minimum) * 0.5).Constraint(Minimum, Maximum); }
            set { SetOwnAttribute(AttributeNames.Optimum, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        #endregion
    }
}
