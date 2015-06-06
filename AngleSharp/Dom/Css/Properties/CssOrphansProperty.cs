namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/orphans
    /// Gets the minimum number of lines in a block container
    /// that must be left at the bottom of the page. 
    /// </summary>
    sealed class CssOrphansProperty : CssProperty
    {
        #region ctor

        internal CssOrphansProperty()
            : base(PropertyNames.Orphans, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return 2;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.PositiveIntegerConverter.Convert(Value);
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.PositiveIntegerConverter.Validate(value);
        }

        #endregion
    }
}
