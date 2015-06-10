namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class EndListValueConverter<T, U> : IValueConverter<Tuple<T[], U>>
    {
        readonly IValueConverter<T[]> _listConverter;
        readonly IValueConverter<U> _endConverter;

        public EndListValueConverter(IValueConverter<T[]> listConverter, IValueConverter<U> endConverter)
        {
            _listConverter = listConverter;
            _endConverter = endConverter;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var items = value.ToList();
            var end = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            return _endConverter.Validate(end) && (items.Count == 0 || _listConverter.Validate(Merge(items)));
        }

        static IEnumerable<CssToken> Merge(List<List<CssToken>> items)
        {
            var first = true;

            foreach (var item in items)
            {
                if (first)
                    first = false;
                else
                    yield return new CssToken(CssTokenType.Comma, ",", TextPosition.Empty);

                foreach (var value in item)
                    yield return value;
            }
        }
    }
}
