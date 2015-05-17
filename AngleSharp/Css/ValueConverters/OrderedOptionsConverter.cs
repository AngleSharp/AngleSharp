namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class OrderedOptionsConverter<T> : IValueConverter<T[]>
    {
        readonly IValueConverter<T> _converter;

        public OrderedOptionsConverter(IValueConverter<T> converter)
        {
            _converter = converter;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<T[]> setResult)
        {
            var list = new List<CssToken>(value);
            var items = new List<T>();

            while (list.Count != 0)
            {
                if (_converter.VaryStart(list, m => items.Add(m)) == false)
                {
                    return false;
                }
            }

            setResult(items.ToArray());
            return true;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var list = new List<CssToken>(value);

            while (list.Count != 0)
            {
                if (_converter.VaryStart(list) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }

    sealed class OrderedOptionsConverter<T1, T2> : IValueConverter<Tuple<T1, T2>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;

        public OrderedOptionsConverter(IValueConverter<T1> first, IValueConverter<T2> second)
        {
            _first = first;
            _second = second;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<Tuple<T1, T2>> setResult)
        {
            var list = new List<CssToken>(value);
            var t1 = default(T1);
            var t2 = default(T2);

            if (_first.VaryStart(list, m => t1 = m) && _second.VaryStart(list, m => t2 = m) && list.Count == 0)
            {
                setResult(Tuple.Create(t1, t2));
                return true;
            }

            return false;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var list = new List<CssToken>(value);
            return _first.VaryStart(list) && _second.VaryStart(list) && list.Count == 0;
        }
    }

    sealed class OrderedOptionsConverter<T1, T2, T3> : IValueConverter<Tuple<T1, T2, T3>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly IValueConverter<T3> _third;

        public OrderedOptionsConverter(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third)
        {
            _first = first;
            _second = second;
            _third = third;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<Tuple<T1, T2, T3>> setResult)
        {
            var list = new List<CssToken>(value);
            var t1 = default(T1);
            var t2 = default(T2);
            var t3 = default(T3);

            if (_first.VaryStart(list, m => t1 = m) && _second.VaryStart(list, m => t2 = m) && _third.VaryStart(list, m => t3 = m) && list.Count == 0)
            {
                setResult(Tuple.Create(t1, t2, t3));
                return true;
            }

            return false;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var list = new List<CssToken>(value);
            return _first.VaryStart(list) && _second.VaryStart(list) && _third.VaryStart(list) && list.Count == 0;
        }
    }
}
