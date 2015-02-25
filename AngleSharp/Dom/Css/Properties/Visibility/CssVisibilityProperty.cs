namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/visibility
    /// </summary>
    sealed class CssVisibilityProperty : CssProperty, ICssVisibilityProperty
    {
        #region Fields

        internal static readonly IValueConverter<Visibility> Converter = Map.Visibilities.ToConverter();
        internal static readonly Visibility Default = Visibility.Visible;
        Visibility _visiblity;

        #endregion

        #region ctor

        internal CssVisibilityProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Visibility, rule, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the visibility mode.
        /// </summary>
        public Visibility Visibility
        {
            get { return _visiblity; }
        }

        #endregion

        #region Methods

        public void SetVisibility(Visibility visibility)
        {
            _visiblity = visibility;
        }

        internal override void Reset()
        {
            _visiblity = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetVisibility);
        }

        #endregion
    }
}
