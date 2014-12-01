namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using System;
    using System.Linq;

    sealed class EndListValueConverter<T, U> : IValueConverter<Tuple<T[], U>>
    {
        readonly IValueConverter<T[]> _listConverter;
        readonly IValueConverter<U> _endConverter;

        public EndListValueConverter(IValueConverter<T[]> listConverter, IValueConverter<U> endConverter)
        {
            _listConverter = listConverter;
            _endConverter = endConverter;
        }

        public Boolean TryConvert(ICssValue value, Action<Tuple<T[], U>> setResult)
        {
            var values = value as CSSValueList;
            var end = default(ICssValue);

            if (values != null)
            {
                var items = values.ToList();
                end = items[items.Count - 1].Reduce();
                items.RemoveAt(items.Count - 1);
                values = items.ToSeparatedList();
            }
            else
            {
                values = new CSSValueList();
                end = value;
            }

            T[] v1 = default(T[]);
            U v2 = default(U);

            if (!_listConverter.TryConvert(values, m => v1 = m) || !_endConverter.TryConvert(end, m => v2 = m))
                return false;

            setResult(Tuple.Create(v1, v2));
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            var values = value as CSSValueList;
            var end = default(ICssValue);

            if (values != null)
            {
                var items = values.ToList();
                end = items[items.Count - 1].Reduce();
                items.RemoveAt(items.Count - 1);
                values = new CSSValueList(items.Select(m => m.Reduce()).ToList());
            }
            else
            {
                values = new CSSValueList();
                end = value;
            }

            return _listConverter.Validate(values) && _endConverter.Validate(end);
        }
    }
}
