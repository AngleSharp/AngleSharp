namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-fill-mode
    /// </summary>
    public sealed class CSSAnimationFillModeProperty : CSSProperty
    {
        #region Fields

        List<AnimationFillMode> _fillModes;

        #endregion

        #region ctor

        internal CSSAnimationFillModeProperty()
            : base(PropertyNames.AnimationFillMode)
        {
            _inherited = false;
            _fillModes = new List<AnimationFillMode>();
            _fillModes.Add(AnimationFillMode.None);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an iteration over all defined fill modes.
        /// </summary>
        public IEnumerable<AnimationFillMode> FillModes
        {
            get { return _fillModes; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var values = value.AsList<CSSIdentifierValue>();

            if (values != null)
            {
                var fillModes = new List<AnimationFillMode>();

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
