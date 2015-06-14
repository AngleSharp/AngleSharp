namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/counter-increment
    /// </summary>
    sealed class CssCounterIncrementProperty : CssProperty
    {
        #region Fields

        // Former way of computing for IElement element:
        /*
            var pairs = CounterConverter.Convert(Value);

            if (pairs.Length == 0)
                return null;

            return pairs[0];
        */
        static readonly IValueConverter StyleConverter = Converters.Continuous(
            Converters.WithOrder(Converters.IdentifierConverter.Required(), Converters.IntegerConverter.Option(1))).OrDefault();

        #endregion

        #region ctor

        internal CssCounterIncrementProperty()
            : base(PropertyNames.CounterIncrement)
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
