namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-fill-mode
    /// </summary>
    sealed class CSSAnimationFillModeProperty : CSSProperty, ICssAnimationFillModeProperty
    {
        #region Fields

        List<AnimationFillStyle> _fillModes;

        #endregion

        #region ctor

        internal CSSAnimationFillModeProperty()
            : base(PropertyNames.AnimationFillMode)
        {
            _fillModes = new List<AnimationFillStyle>();
            _fillModes.Add(AnimationFillStyle.None);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an iteration over all defined fill modes.
        /// </summary>
        public IEnumerable<AnimationFillStyle> FillModes
        {
            get { return _fillModes; }
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
            var values = value.AsList<CSSPrimitiveValue>();

            if (values != null)
            {
                var fillModes = new List<AnimationFillStyle>();

                foreach (var item in values)
                {
                    var mode = item.ToFillMode();

                    if (!mode.HasValue)
                        return false;

                    fillModes.Add(mode.Value);
                }

                _fillModes = fillModes;
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
