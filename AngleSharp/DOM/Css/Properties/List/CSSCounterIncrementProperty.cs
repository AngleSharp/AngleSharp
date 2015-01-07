namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/counter-increment
    /// </summary>
    sealed class CSSCounterIncrementProperty : CSSCounterProperty, ICssCounterIncrementProperty
    {
        #region Fields

        internal static readonly IValueConverter<KeyValuePair<String, Int32>[]> Converter = Converters.WithOrder(
            Converters.WithOrder(
                Converters.IdentifierConverter.Required(),
                Converters.IntegerConverter.Option(1)).To(
            m => new KeyValuePair<String, Int32>(m.Item1, m.Item2)));

        #endregion

        #region ctor

        internal CSSCounterIncrementProperty(CssStyleDeclaration rule)
            : base(PropertyNames.CounterIncrement, rule)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetCounters);
        }

        #endregion
    }
}
