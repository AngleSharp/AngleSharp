namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/counter-reset
    /// </summary>
    sealed class CssCounterResetProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<KeyValuePair<String, Int32>[]> CounterConverter = 
            Converters.WithOrder(
                Converters.WithOrder(
                    Converters.IdentifierConverter.Required(),
                    Converters.IntegerConverter.Option(0)).To(
                m => new KeyValuePair<String, Int32>(m.Item1, m.Item2)));

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
            // Default: Nothing
            get { return CounterConverter; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CssValue value)
        {
            // Former way of computing for IElement element:
            /*
                var pairs = CounterConverter.Convert(Value);

                if (pairs.Length == 0)
                    return null;

                return pairs[0];
            */
            return CounterConverter.Validate(value);
        }

        #endregion
    }
}
