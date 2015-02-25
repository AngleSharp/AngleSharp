namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/z-index
    /// </summary>
    sealed class CssZIndexProperty : CssProperty, ICssZIndexProperty
    {
        #region Fields

        internal static readonly Int32? Default = null;
        internal static readonly IValueConverter<Int32?> Converter = Converters.IntegerConverter.OrNullDefault();
        Int32? _index;

        #endregion

        #region ctor

        internal CssZIndexProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ZIndex, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the index in the stacking order, if any.
        /// </summary>
        public Int32? Index
        {
            get { return _index; }
        }

        public void SetIndex(Int32? index)
        {
            _index = index;
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _index = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetIndex);
        }

        #endregion
    }
}
