namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-position
    /// Gets the selected position.
    /// </summary>
    sealed class CssListStylePositionProperty : CssProperty
    {
        #region ctor

        internal CssListStylePositionProperty()
            : base(PropertyNames.ListStylePosition, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.ListPositionConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return ListPosition.Outside;
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.ListPositionConverter.Validate(value);
        }

        #endregion
    }
}
