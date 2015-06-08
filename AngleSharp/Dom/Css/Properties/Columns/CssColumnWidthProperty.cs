namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-width
    /// Gets the width of a single columns.
    /// </summary>
    sealed class CssColumnWidthProperty : CssProperty
    {
        #region ctor

        internal CssColumnWidthProperty()
            : base(PropertyNames.ColumnWidth, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.AutoLengthConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return null;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.AutoLengthConverter.Convert(Value);
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.AutoLengthConverter.Validate(value);
        }

        #endregion
    }
}
