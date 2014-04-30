namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-delay
    /// </summary>
    public sealed class CSSAnimationDelayProperty : CSSProperty
    {
        #region Fields

        List<Time> _times;

        #endregion

        #region ctor

        internal CSSAnimationDelayProperty()
            : base(PropertyNames.AnimationDelay)
        {
            _inherited = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the delays for the animations.
        /// </summary>
        public IEnumerable<Time> Delays
        {
            get { return _times; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var values = value.AsList<CSSPrimitiveValue<Time>>();

            if (values != null)
            {
                _times.Clear();

                foreach (var v in values)
                    _times.Add(v.Value);
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
