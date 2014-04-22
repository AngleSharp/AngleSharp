namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// The abstract base class for all border-style sub-properties.
    /// </summary>
    public abstract class CSSBorderPartStyleProperty : CSSProperty
    {
        #region Fields

        LineStyle _style;

        #endregion

        #region ctor

        protected CSSBorderPartStyleProperty(String name)
            : base(name)
        {
            _style = LineStyle.None;
            _inherited = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected style for the border.
        /// </summary>
        public LineStyle Style
        {
            get { return _style; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var style = value.ToLineStyle();

            if (style.HasValue)
                _style = style.Value;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
