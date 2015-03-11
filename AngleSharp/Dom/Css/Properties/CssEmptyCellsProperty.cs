namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/empty-cells
    /// Gets if borders and backgrounds should be drawn like
    /// in a normal cells. Otherwise no border or backgrounds
    /// should be drawn.
    /// </summary>
    sealed class CssEmptyCellsProperty : CssProperty
    {
        #region ctor

        internal CssEmptyCellsProperty(CssStyleDeclaration rule)
            : base(PropertyNames.EmptyCells, rule, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return true;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.EmptyCellsConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.EmptyCellsConverter.Validate(value);
        }

        #endregion
    }
}
