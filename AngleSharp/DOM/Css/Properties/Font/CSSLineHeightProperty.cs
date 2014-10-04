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

        static readonly Percent Normal = new Percent(120f);
        IDistance _height;

        #endregion

        #region ctor

        internal CSSLineHeightProperty()
            : base(PropertyNames.LineHeight, PropertyFlags.Inherited)
        {
            _height = Normal;
        }

        #endregion

        #region Properties

        public IDistance Height
        {
            get { return _height; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var distance = value.ToDistance();

            if (distance != null)
                _height = distance;
            else if (value.Is(Keywords.Normal))
                _height = Normal;
            else
            {
                var val = value.ToSingle();

                if (val.HasValue)
                    _height = new Percent(val.Value * 100f);
                else if (value != CSSValue.Inherit)
                    return false;
            }

            return true;
        }

        #endregion
    }
}
