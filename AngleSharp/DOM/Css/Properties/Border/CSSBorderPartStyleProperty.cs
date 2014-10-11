namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// The abstract base class for all border-style sub-properties.
    /// </summary>
    abstract class CSSBorderPartStyleProperty : CSSProperty
    {
        #region Fields

        LineStyle _style;

        #endregion

        #region ctor

        internal CSSBorderPartStyleProperty(String name)
            : base(name)
        {
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

        internal override void Reset()
        {
            _style = LineStyle.None;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var style = value.ToLineStyle();

            if (style.HasValue)
            {
                _style = style.Value;
                return true;
            }
            
            return false;
        }

        #endregion
    }
}
