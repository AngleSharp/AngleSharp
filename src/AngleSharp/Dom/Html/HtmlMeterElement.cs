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
    sealed class HtmlMeterElement : HtmlElement, IHtmlMeterElement
    {
        #region Fields

        private readonly NodeList _labels;

        #endregion

        #region ctor

        public HtmlMeterElement(Document owner, String prefix = null)
            : base(owner, TagNames.Meter, prefix)
        {
            _labels = new NodeList();
        }

        #endregion

        #region Properties

        public INodeList Labels
        {
            get { return _labels; }
        }

        public Double Value
        {
            get { return this.GetOwnAttribute(AttributeNames.Value).ToDouble(0.0).Constraint(Minimum, Maximum); }
            set { this.SetOwnAttribute(AttributeNames.Value, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        public Double Maximum
        {
            get { return this.GetOwnAttribute(AttributeNames.Max).ToDouble(1.0).Constraint(Minimum, Double.PositiveInfinity); }
            set { this.SetOwnAttribute(AttributeNames.Max, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        public Double Minimum
        {
            get { return this.GetOwnAttribute(AttributeNames.Min).ToDouble(0.0); }
            set { this.SetOwnAttribute(AttributeNames.Min, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        public Double Low
        {
            get { return this.GetOwnAttribute(AttributeNames.Low).ToDouble(Minimum).Constraint(Minimum, Maximum); }
            set { this.SetOwnAttribute(AttributeNames.Low, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        public Double High
        {
            get { return this.GetOwnAttribute(AttributeNames.High).ToDouble(Maximum).Constraint(Low, Maximum); }
            set { this.SetOwnAttribute(AttributeNames.High, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        public Double Optimum
        {
            get { return this.GetOwnAttribute(AttributeNames.Optimum).ToDouble((Maximum + Minimum) * 0.5).Constraint(Minimum, Maximum); }
            set { this.SetOwnAttribute(AttributeNames.Optimum, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        #endregion
    }
}
