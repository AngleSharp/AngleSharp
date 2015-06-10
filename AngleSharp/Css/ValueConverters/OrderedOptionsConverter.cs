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

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var list = new List<CssToken>(value);
            return _first.VaryStart(list) && _second.VaryStart(list) && _third.VaryStart(list) && list.Count == 0;
        }
    }

    sealed class OrderedOptionsConverter<T1, T2, T3, T4> : IValueConverter<Tuple<T1, T2, T3, T4>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly IValueConverter<T3> _third;
        readonly IValueConverter<T4> _fourth;

        public OrderedOptionsConverter(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth)
        {
            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var list = new List<CssToken>(value);
            return _first.VaryStart(list) && _second.VaryStart(list) &&
                _third.VaryStart(list) && _fourth.VaryStart(list) && list.Count == 0;
        }
    }
}
