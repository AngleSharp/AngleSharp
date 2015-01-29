namespace AngleSharp.Css.ValueConverters
{
    using AngleSharp.Dom.Css;
    using System;

    sealed class ArgumentsValueConverter<T> : IValueConverter<T[]>
    {
        readonly IValueConverter<T> _converter;
        readonly Int32 _arguments;

        public ArgumentsValueConverter(IValueConverter<T> converter, Int32 arguments)
        {
            _converter = converter;
            _arguments = arguments;
        }

        public Boolean TryConvert(ICssValue value, Action<T[]> setResult)
        {
            var items = value as CssValueList;

            if (items == null || items.Length != _arguments)
                return false;

            var array = new T[_arguments];

            for (int i = 0; i < _arguments; i++)
            {
                if (!_converter.TryConvert(items[i], m => array[i] = m))
                    return false;
            }

            setResult(array);
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            var items = value as CssValueList;

            if (items == null || items.Length != _arguments)
                return false;

            for (int i = 0; i < _arguments; i++)
            {
                if (!_converter.Validate(items[i]))
                    return false;
            }

            return true;
        }

        public Int32 MinArgs
        {
            get { return _converter.MinArgs * _arguments; }
        }

        public Int32 MaxArgs
        {
            get { return _converter.MaxArgs * _arguments; }
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

        public Boolean TryConvert(ICssValue value, Action<Tuple<T1, T2>> setResult)
        {
            var items = value as CssValueList;

            if (items == null || items.Length > 2)
                return false;

            var t1 = default(T1);
            var t2 = default(T2);

            if (!_first.TryConvert(items[0], t => t1 = t) || !_second.TryConvert(items[1], t => t2 = t))
                return false;

            setResult(Tuple.Create(t1, t2));
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            var items = value as CssValueList;

            if (items == null)
                return false;

            return items.Length <= 2 && _first.Validate(items[0]) && _second.Validate(items[1]);
        }

        public Int32 MinArgs
        {
            get { return _first.MinArgs + _second.MinArgs; }
        }

        public Int32 MaxArgs
        {
            get { return _first.MaxArgs + _second.MaxArgs; }
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

        public Boolean TryConvert(ICssValue value, Action<Tuple<T1, T2, T3>> setResult)
        {
            var items = value as CssValueList;

            if (items == null || items.Length > 3)
                return false;

            var t1 = default(T1);
            var t2 = default(T2);
            var t3 = default(T3);

            if (!_first.TryConvert(items[0], t => t1 = t) || !_second.TryConvert(items[1], t => t2 = t) || !_third.TryConvert(items[2], t => t3 = t))
                return false;

            setResult(Tuple.Create(t1, t2, t3));
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            var items = value as CssValueList;

            if (items == null)
                return false;

            return items.Length <= 3 && _first.Validate(items[0]) && _second.Validate(items[1]) && _third.Validate(items[2]);
        }

        public Int32 MinArgs
        {
            get { return _first.MinArgs + _second.MinArgs + _third.MinArgs; }
        }

        public Int32 MaxArgs
        {
            get { return _first.MaxArgs + _second.MaxArgs + _third.MaxArgs; }
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

        public Boolean TryConvert(ICssValue value, Action<Tuple<T1, T2, T3, T4>> setResult)
        {
            var items = value as CssValueList;

            if (items == null || items.Length > 4)
                return false;

            var t1 = default(T1);
            var t2 = default(T2);
            var t3 = default(T3);
            var t4 = default(T4);

            if (!_first.TryConvert(items[0], t => t1 = t) || !_second.TryConvert(items[1], t => t2 = t) ||
                !_third.TryConvert(items[2], t => t3 = t) || !_fourth.TryConvert(items[3], t => t4 = t))
                return false;

            setResult(Tuple.Create(t1, t2, t3, t4));
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            var items = value as CssValueList;

            if (items == null)
                return false;

            return items.Length <= 4 && _first.Validate(items[0]) && _second.Validate(items[1]) && _third.Validate(items[2]) && _fourth.Validate(items[3]);
        }

        public Int32 MinArgs
        {
            get { return _first.MinArgs + _second.MinArgs + _third.MinArgs + _fourth.MinArgs; }
        }

        public Int32 MaxArgs
        {
            get { return _first.MaxArgs + _second.MaxArgs + _third.MaxArgs + _fourth.MaxArgs; }
        }
    }
}
