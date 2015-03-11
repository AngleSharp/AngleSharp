namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-fill
    /// Gets if the columns should be filled uniformly.
    /// </summary>
    sealed class CssColumnFillProperty : CssProperty
    {
        #region ctor

        internal CssColumnFillProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ColumnFill, rule)
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
            return Converters.ColumnFillConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.ColumnFillConverter.Validate(value);
        }

        #endregion
    }
}
