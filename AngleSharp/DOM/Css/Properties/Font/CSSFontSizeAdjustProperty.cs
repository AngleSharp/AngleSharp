namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information:
    /// http://dev.w3.org/csswg/css-fonts/#propdef-font-size-adjust
    /// </summary>
    sealed class CSSFontSizeAdjustProperty : CSSProperty
    {
        #region Fields

        Single? _aspectValue;

        #endregion

        #region ctor

        internal CSSFontSizeAdjustProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.FontSizeAdjust, rule, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the aspect value specified by the property, if any.
        /// </summary>
        public Single? AspectValue
        {
            get { return _aspectValue; }
        }

        #endregion

        #region Methods

        public void SetAspectValue(Single? aspectValue)
        {
            _aspectValue = aspectValue;
        }

        internal override void Reset()
        {
            _aspectValue = null;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return this.TakeOne(Keywords.None, (Single?)null).Or(this.WithNumber().To(m => new Single?(m))).TryConvert(value, SetAspectValue);
        }

        #endregion
    }
}
