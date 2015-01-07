namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-collapse
    /// </summary>
    sealed class CSSBorderCollapseProperty : CSSProperty, ICssBorderCollapseProperty
    {
        #region Fields

        internal static readonly Boolean Default = true;
        internal static readonly IValueConverter<Boolean> Converter = Converters.Toggle(Keywords.Separate, Keywords.Collapse);
        Boolean _separate;

        #endregion

        #region ctor

        internal CSSBorderCollapseProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderCollapse, rule, PropertyFlags.Inherited)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the use of the separated-border table rendering model.
        /// Otherwise the collapsed-border table rendering model is used.
        /// </summary>
        public Boolean IsSeparated
        {
            get { return _separate; }
        }

        #endregion

        #region Methods

        public void SetSeparated(Boolean separate)
        {
            _separate = separate;
        }

        internal override void Reset()
        {
            _separate = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetSeparated);
        }

        #endregion
    }
}
