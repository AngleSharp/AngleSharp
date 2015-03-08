namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-position
    /// Gets the selected position.
    /// </summary>
    sealed class CssListStylePositionProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<ListPosition> Converter = 
            Map.ListPositions.ToConverter();

        #endregion

        #region ctor

        internal CssListStylePositionProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ListStylePosition, rule, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return ListPosition.Outside;
        }

        protected override Object Compute(IElement element)
        {
            return Converter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}
