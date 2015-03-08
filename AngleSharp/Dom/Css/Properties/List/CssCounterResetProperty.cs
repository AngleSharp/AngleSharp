namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/counter-reset
    /// </summary>
    sealed class CssCounterResetProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<KeyValuePair<String, Int32>[]> Converter = 
            Converters.WithOrder(
                Converters.WithOrder(
                    Converters.IdentifierConverter.Required(),
                    Converters.IntegerConverter.Option(0)).To(
                m => new KeyValuePair<String, Int32>(m.Item1, m.Item2)));

        #endregion

        #region ctor

        internal CssCounterResetProperty(CssStyleDeclaration rule)
            : base(PropertyNames.CounterReset, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return null;
        }

        protected override Object Compute(IElement element)
        {
            var pairs = Converter.Convert(Value);

            if (pairs.Length == 0)
                return null;

            return pairs[0];
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}
