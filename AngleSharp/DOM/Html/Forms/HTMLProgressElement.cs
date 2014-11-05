namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML progress element.
    /// </summary>
    sealed class HTMLProgressElement : HTMLElement, ILabelabelElement, IHtmlProgressElement
    {
        #region Fields

        readonly NodeList labels;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML progress element.
        /// </summary>
        internal HTMLProgressElement()
            : base(Tags.Progress)
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
            get { return !String.IsNullOrEmpty(GetAttribute(AttributeNames.Value)); }
        }

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        public Double Value
        {
            get { return GetAttribute(AttributeNames.Value).ToDouble(0.0); }
            set { SetAttribute(AttributeNames.Value, value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo)); }
        }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        public Double Max
        {
            get { return GetAttribute(AttributeNames.Max).ToDouble(1.0); }
            set { SetAttribute(AttributeNames.Max, value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo)); }
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        public Double Position
        {
            get { return IsDeterminate ? Math.Max(Math.Min(Value / Max, 1.0), 0.0) : -1.0; }
        }

        #endregion
    }
}
