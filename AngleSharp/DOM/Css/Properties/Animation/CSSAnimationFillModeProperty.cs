namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-fill-mode
    /// </summary>
    sealed class CSSAnimationFillModeProperty : CSSProperty, ICssAnimationFillModeProperty
    {
        #region Fields

        readonly List<AnimationFillStyle> _fillModes;

        #endregion

        #region ctor

        internal CSSAnimationFillModeProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.AnimationFillMode, rule)
        {
            _fillModes = new List<AnimationFillStyle>();
            Reset();
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

        internal override void Reset()
        {
            _fillModes.Clear();
            _fillModes.Add(AnimationFillStyle.None);
        }

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

                    if (mode == null)
                        return false;

                    fillModes.Add(mode.Value);
                }

                _fillModes.Clear();
                _fillModes.AddRange(fillModes);
                return true;
            }
            
            return false;
        }

        #endregion
    }
}
