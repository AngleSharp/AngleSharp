namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-span
    /// Gets if the element should span across all columns.
    /// </summary>
    sealed class CssColumnSpanProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<Boolean> Converter = 
            Converters.Toggle(Keywords.All, Keywords.None);

        #endregion

        #region ctor

        internal CssColumnSpanProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ColumnSpan, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return false;
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
