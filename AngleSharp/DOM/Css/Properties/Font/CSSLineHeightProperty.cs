namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/line-height
    /// </summary>
    sealed class CSSLineHeightProperty : CSSProperty, ICssLineHeightProperty
    {
        #region Fields

        IDistance _height;

        #endregion

        #region ctor

        internal CSSLineHeightProperty()
            : base(PropertyNames.LineHeight, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        public IDistance Height
        {
            get { return _height; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _height = new Percent(120f);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var distance = value.ToLineHeight();

            if (distance != null)
            {
                _height = distance;
                return true;
            }

            return false;
        }

        #endregion
    }
}
