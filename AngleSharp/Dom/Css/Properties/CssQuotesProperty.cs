namespace AngleSharp.Dom.Css
{
    using System;
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

        static readonly IValueConverter<Tuple<String, String>[]> StyleConverter = 
            Converters.StringConverter.Many().Constraint(m => m.Length % 2 == 0).
                To(TransformArray).Or(Keywords.None, new Tuple<String, String>[0]);

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
            // Default: Tuple.Create("«", "»")
            get { return StyleConverter; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CssValue value)
        {
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
            return StyleConverter.Validate(value);
        }

        #endregion

        #region Helpers

        static Tuple<String, String>[] TransformArray(String[] arrays)
        {
            var tuples = new Tuple<String, String>[arrays.Length / 2];

            for (int i = 0, k = 0; i < arrays.Length; i += 2, k++)
                tuples[k] = Tuple.Create(arrays[i], arrays[i + 1]);

            return tuples;
        }

        #endregion
    }
}
