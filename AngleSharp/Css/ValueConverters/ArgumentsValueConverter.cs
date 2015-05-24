namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class ArgumentsValueConverter<T> : IValueConverter<T[]>
    {
        readonly IValueConverter<T> _converter;
        readonly Int32 _arguments;

        public ArgumentsValueConverter(IValueConverter<T> converter, Int32 arguments)
        {
            _converter = converter;
            _arguments = arguments;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<T[]> setResult)
        {
            var items = value.ToList();

            if (items.Count == _arguments)
            {
                var array = new T[_arguments];

                for (int i = 0; i < _arguments; i++)
                {
                    if (!_converter.TryConvert(items[i], m => array[i] = m))
                    {
                        return false;
                    }
                }

                setResult(array);
                return true;
            }

            return false;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var items = value.ToList();

            if (items.Count == _arguments)
            {
                for (int i = 0; i < _arguments; i++)
                {
                    if (!_converter.Validate(items[i]))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }
    }

    sealed class ArgumentsValueConverter<T1, T2> : IValueConverter<Tuple<T1, T2>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;

        public ArgumentsValueConverter(IValueConverter<T1> first, IValueConverter<T2> second)
        {
            _first = first;
            _second = second;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<Tuple<T1, T2>> setResult)
        {
            var items = value.ToArgs(2);
            var t1 = default(T1);
            var t2 = default(T2);

            if (items != null && 
                _first.TryConvert(items[0], t => t1 = t) && _second.TryConvert(items[1], t => t2 = t))
            {
                setResult(Tuple.Create(t1, t2));
                return true;
            }

            return false;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var items = value.ToArgs(2);
            return items != null && 
                _first.Validate(items[0]) && _second.Validate(items[1]);
        }
    }

    sealed class ArgumentsValueConverter<T1, T2, T3> : IValueConverter<Tuple<T1, T2, T3>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly IValueConverter<T3> _third;

        public ArgumentsValueConverter(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third)
        {
            _first = first;
            _second = second;
            _third = third;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<Tuple<T1, T2, T3>> setResult)
        {
            var items = value.ToArgs(3);
            var t1 = default(T1);
            var t2 = default(T2);
            var t3 = default(T3);

            if (items != null && _first.TryConvert(items[0], t => t1 = t) && 
                _second.TryConvert(items[1], t => t2 = t) && _third.TryConvert(items[2], t => t3 = t))
            {
                setResult(Tuple.Create(t1, t2, t3));
                return true;
            }

            return false;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var items = value.ToArgs(3);
            return items != null && _first.Validate(items[0]) && 
                _second.Validate(items[1]) && _third.Validate(items[2]);
        }
    }

    sealed class ArgumentsValueConverter<T1, T2, T3, T4> : IValueConverter<Tuple<T1, T2, T3, T4>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly IValueConverter<T3> _third;
        readonly IValueConverter<T4> _fourth;

        public ArgumentsValueConverter(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth)
        {
            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<Tuple<T1, T2, T3, T4>> setResult)
        {
            var items = value.ToArgs(4);
            var t1 = default(T1);
            var t2 = default(T2);
            var t3 = default(T3);
            var t4 = default(T4);

            if (items != null && 
                _first.TryConvert(items[0], t => t1 = t) && _second.TryConvert(items[1], t => t2 = t) && 
                _third.TryConvert(items[2], t => t3 = t) && _fourth.TryConvert(items[3], t => t4 = t))
            {
                setResult(Tuple.Create(t1, t2, t3, t4));
                return true;
            }

            return false;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var items = value.ToArgs(4);
            return items != null && 
                _first.Validate(items[0]) && _second.Validate(items[1]) && 
                _third.Validate(items[2]) && _fourth.Validate(items[3]);
        }
    }
}
