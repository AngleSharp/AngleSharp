namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/visibility
    /// </summary>
    sealed class CSSVisibilityProperty : CSSProperty, ICssVisibilityProperty
    {
        #region Fields

        internal static readonly IValueConverter<Visibility> Converter = From(Map.Visibilities);
        Visibility _visiblity;

        #endregion

        #region ctor

        internal CSSVisibilityProperty(CSSStyleDeclaration rule)
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
            _visiblity = Visibility.Visible;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return Converter.TryConvert(value, SetVisibility);
        }

        #endregion
    }
}
