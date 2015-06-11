namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-count
    /// Gets the number of columns.
    /// </summary>
    sealed class CssColumnCountProperty : CssProperty
    {
        #region ctor

        internal CssColumnCountProperty()
            : base(PropertyNames.ColumnCount, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.OptionalIntegerConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return null;
        }

        #endregion
    }
}
