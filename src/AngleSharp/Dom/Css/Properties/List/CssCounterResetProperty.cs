namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using static AngleSharp.Css.Converters;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/counter-reset
    /// </summary>
    sealed class CssCounterResetProperty : CssProperty
    {
        #region Fields

        // Former way of computing for IElement element:
        /*
            var pairs = CounterConverter.Convert(Value);

            if (pairs.Length == 0)
                return null;

            return pairs[0];
        */
        static readonly IValueConverter StyleConverter = Continuous(
            WithOrder(IdentifierConverter.Required(), IntegerConverter.Option(0))).OrDefault();

        #endregion

        #region ctor

        internal CssCounterResetProperty()
            : base(PropertyNames.CounterReset)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion
    }
}
