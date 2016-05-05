namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/quotes
    /// Gets the enumeration with pairs of values for open-quote and
    /// close-quote. The first pair represents the outer level of quotation,
    /// the second pair is for the first nested level, next pair for third
    /// level and so on.
    /// </summary>
    sealed class CssQuotesProperty : CssProperty
    {
        #region Fields

        // Style of computing quotes for an IElement element:
        /*
            var pairs = StyleConverter.Convert(Value);

            if (element is IHtmlQuoteElement && pairs.Length > 0)
            {
                var nesting = element.GetAncestors().OfType<IHtmlQuoteElement>().Count();
                var index = Math.Min(pairs.Length - 1, nesting);
                return pairs[index];
            }

            return Tuple.Create("", "");
        */
        static readonly IValueConverter StyleConverter = Converters.EvenStringsConverter.OrNone().OrDefault(new[] { "«", "»" });

        #endregion

        #region ctor

        internal CssQuotesProperty()
            : base(PropertyNames.Quotes, PropertyFlags.Inherited)
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
