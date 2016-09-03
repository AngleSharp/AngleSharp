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
    sealed class HtmlProgressElement : HtmlElement, IHtmlProgressElement
    {
        #region Fields

        private readonly NodeList _labels;

        #endregion

        #region ctor

        public HtmlProgressElement(Document owner, String prefix = null)
            : base(owner, TagNames.Progress, prefix)
        {
            _labels = new NodeList();
        }

        #endregion

        #region Properties

        public INodeList Labels
        {
            get { return _labels; }
        }

        public Boolean IsDeterminate
        {
            get { return !String.IsNullOrEmpty(this.GetOwnAttribute(AttributeNames.Value)); }
        }

        public Double Value
        {
            get { return this.GetOwnAttribute(AttributeNames.Value).ToDouble(0.0); }
            set { this.SetOwnAttribute(AttributeNames.Value, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        public Double Maximum
        {
            get { return this.GetOwnAttribute(AttributeNames.Max).ToDouble(1.0); }
            set { this.SetOwnAttribute(AttributeNames.Max, value.ToString(NumberFormatInfo.InvariantInfo)); }
        }

        public Double Position
        {
            get { return IsDeterminate ? Math.Max(Math.Min(Value / Maximum, 1.0), 0.0) : -1.0; }
        }

        #endregion
    }
}
