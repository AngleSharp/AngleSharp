namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
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

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<Tuple<T[], U>> setResult)
        {
            //var values = value as CssValueList;
            //var end = default(ICssValue);

            //if (values != null)
            //{
            //    var items = values.ToList();
            //    end = items[items.Count - 1].Reduce();
            //    items.RemoveAt(items.Count - 1);
            //    values = items.ToSeparatedList();
            //}
            //else
            //{
            //    values = new CssValueList();
            //    end = value;
            //}

            //T[] v1 = default(T[]);
            //U v2 = default(U);

            //if (!_endConverter.TryConvert(end, m => v2 = m))
            //    return false;

            //if (values.Length != 0 && !_listConverter.TryConvert(values, m => v1 = m))
            //    return false;
            //else if (values.Length == 0)
            //    v1 = new T[0];

            //setResult(Tuple.Create(v1, v2));
            return true;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            //var values = value as CssValueList;
            //var end = default(ICssValue);

            //if (values != null)
            //{
            //    var items = values.ToList();
            //    end = items[items.Count - 1].Reduce();
            //    items.RemoveAt(items.Count - 1);
            //    values = items.ToSeparatedList();
            //}
            //else
            //{
            //    values = new CssValueList();
            //    end = value;
            //}

            //if (!_endConverter.Validate(end))
            //    return false;

            //return values.Length == 0 || _listConverter.Validate(values);
            return true;
        }
    }
}
