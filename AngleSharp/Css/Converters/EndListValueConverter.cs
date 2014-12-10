namespace AngleSharp.Css
{
    using AngleSharp.DOM.Css;
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
            var values = value as CssValueList;
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
                values = new CssValueList();
                end = value;
            }

            T[] v1 = default(T[]);
            U v2 = default(U);

            if (!_endConverter.TryConvert(end, m => v2 = m))
                return false;

            if (values.Length != 0 && !_listConverter.TryConvert(values, m => v1 = m))
                return false;
            else if (values.Length == 0)
                v1 = new T[0];

            setResult(Tuple.Create(v1, v2));
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            var values = value as CssValueList;
            var end = default(ICssValue);

            if (values != null)
            {
                var items = values.ToList();
                end = items[items.Count - 1].Reduce();
                items.RemoveAt(items.Count - 1);
                values = new CssValueList(items.Select(m => m.Reduce()).ToList());
            }
            else
            {
                values = new CssValueList();
                end = value;
            }

            return _endConverter.Validate(end) && (values.Length == 0 || _listConverter.Validate(values));
        }

        public Int32 MinArgs
        {
            get { return _endConverter.MinArgs; }
        }

        public Int32 MaxArgs
        {
            get { return _listConverter.MaxArgs + _endConverter.MaxArgs; }
        }
    }
}
