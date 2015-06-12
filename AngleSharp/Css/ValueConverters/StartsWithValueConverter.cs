namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Parser.Css;

    sealed class StartsWithValueConverter : IValueConverter
    {
        readonly Predicate<CssToken> _condition;
        readonly IValueConverter _converter;

        public StartsWithValueConverter(Predicate<CssToken> condition, IValueConverter converter)
        {
            _condition = condition;
            _converter = converter;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var rest = Transform(value);
            return rest != null ? _converter.Convert(rest) : null;
        }

        List<CssToken> Transform(IEnumerable<CssToken> values)
        {
            var enumerator = values.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Type != CssTokenType.Whitespace)
                    break;
            }

            if (_condition(enumerator.Current))
            {
                var list = new List<CssToken>();

                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.Type == CssTokenType.Whitespace && list.Count == 0)
                        continue;

                    list.Add(enumerator.Current);
                }

                return list;
            }

            return null;
        }
    }
}
