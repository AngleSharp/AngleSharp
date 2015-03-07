namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/float
    /// </summary>
    sealed class CssFloatProperty : CssProperty
    {
        #region Fields

        internal static readonly Floating Default = Floating.None;
        internal static readonly IValueConverter<Floating> Converter = Map.FloatingModes.ToConverter();
        Floating _mode;

        #endregion

        #region ctor

        internal CssFloatProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Float, rule)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the floating property.
        /// </summary>
        public Floating State
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        public void SetState(Floating mode)
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
            return Converter.TryConvert(value, SetState);
        }

        #endregion
    }
}
