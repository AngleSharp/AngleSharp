namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-iteration-count
    /// </summary>
    public sealed class CSSAnimationIterationCountProperty : CSSProperty
    {
        #region Fields

        List<Single> _iterations;

        #endregion

        #region ctor

        internal CSSAnimationIterationCountProperty()
            : base(PropertyNames.AnimationIterationCount)
        {
            _inherited = false;
            _iterations = new List<Single>();
            _iterations.Add(1f);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the iteration count of the covered animations.
        /// </summary>
        public List<Single> Iterations
        {
            get { return _iterations; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var values = value.AsList(ToNumber);

            if (values != null)
            {
                _iterations.Clear();

                foreach (var v in values)
                    _iterations.Add(v.Value.Value);
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        static CSSPrimitiveValue<Number> ToNumber(CSSValue value)
        {
            if (value is CSSPrimitiveValue<Number>)
                return (CSSPrimitiveValue<Number>)value;
            else if (value.Is("infinite"))
                return new CSSPrimitiveValue<Number>(Number.Infinite);

            return null;
        }

        #endregion
    }
}
