namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-transform
    /// </summary>
    sealed class CssTextTransformProperty : CssProperty, ICssTextTransformProperty
    {
        #region Fields

        internal static readonly TextTransform Default = TextTransform.None;
        internal static readonly IValueConverter<TextTransform> Converter = Map.TextTransforms.ToConverter();
        TextTransform _mode;

        #endregion

        #region ctor

        internal CssTextTransformProperty(CssStyleDeclaration rule)
            : base(PropertyNames.TextTransform, rule, PropertyFlags.Inherited)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected text transformation mode.
        /// </summary>
        public TextTransform Transform
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        public void SetTransform(TextTransform mode)
        {
            _mode = mode;
        }

        internal override void Reset()
        {
            _mode = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetTransform);
        }

        #endregion
    }
}
