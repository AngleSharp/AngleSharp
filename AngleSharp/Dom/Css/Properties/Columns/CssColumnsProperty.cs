namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/columns
    /// </summary>
    sealed class CssColumnsProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.WithAny(
            Converters.AutoLengthConverter.Option(),
            Converters.OptionalIntegerConverter.Option());

        #endregion

        #region ctor

        internal CssColumnsProperty()
            : base(PropertyNames.Columns, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CssValue value)
        {
            return StyleConverter.Convert(value) != null;
            //TODO Convert instead of validate
            /*, m =>
            {
                Get<CssColumnWidthProperty>().TrySetValue(m.Item1);
                Get<CssColumnCountProperty>().TrySetValue(m.Item2);
            });*/
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var width = properties.OfType<CssColumnWidthProperty>().FirstOrDefault();
            var count = properties.OfType<CssColumnCountProperty>().FirstOrDefault();

            if (width == null || count == null)
                return String.Empty;
            else if (!width.HasValue || !count.HasValue)
                return String.Empty;

            return String.Concat(width.SerializeValue(), " ", count.SerializeValue());
        }

        #endregion
    }
}
