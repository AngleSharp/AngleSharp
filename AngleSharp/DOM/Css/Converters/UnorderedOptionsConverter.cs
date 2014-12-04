namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using System;

    sealed class UnorderedOptionsConverter<T1, T2, T3> : IValueConverter<Tuple<T1, T2, T3>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly IValueConverter<T3> _third;

        public UnorderedOptionsConverter(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third)
        {
            _first = first;
            _second = second;
            _third = third;
        }

        public Int32 MaxArgs
        {
            get { return _first.MaxArgs + _second.MaxArgs + _third.MaxArgs; }
        }

        public Int32 MinArgs
        {
            get { return _first.MinArgs + _second.MinArgs + _third.MinArgs; }
        }

        public Boolean TryConvert(ICssValue value, Action<Tuple<T1, T2, T3>> setResult)
        {
            var values = value as CssValueList;

            if (values != null)
                values = values.Copy();
            else if (value != null)
                values = new CssValueList(value);
            else
                values = new CssValueList();

            if (values.Length < MinArgs && values.Length > MaxArgs)
                return false;

            var t1 = default(T1);
            var t2 = default(T2);
            var t3 = default(T3);

            if (!_first.VaryAll(values, m => t1 = m) ||
                !_second.VaryAll(values, m => t2 = m) ||
                !_third.VaryAll(values, m => t3 = m) ||
                values.Length != 0)
                return false;

            setResult(Tuple.Create(t1, t2, t3));
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            var values = value as CssValueList;

            if (values != null)
                values = values.Copy();
            else if (value != null)
                values = new CssValueList(value);
            else
                values = new CssValueList();

            if (values.Length < MinArgs && values.Length > MaxArgs)
                return false;

            return values.Length == 0;
        }
    }

    sealed class UnorderedOptionsConverter<T1, T2, T3, T4, T5> : IValueConverter<Tuple<T1, T2, T3, T4, T5>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly IValueConverter<T3> _third;
        readonly IValueConverter<T4> _fourth;
        readonly IValueConverter<T5> _fifth;

        public UnorderedOptionsConverter(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth)
        {
            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
            _fifth = fifth;
        }

        public Int32 MaxArgs
        {
            get { return _first.MaxArgs + _second.MaxArgs + _third.MaxArgs + _fourth.MaxArgs + _fifth.MaxArgs; }
        }

        public Int32 MinArgs
        {
            get { return _first.MinArgs + _second.MinArgs + _third.MinArgs + _fourth.MinArgs + _fifth.MinArgs; }
        }

        public Boolean TryConvert(ICssValue value, Action<Tuple<T1, T2, T3, T4, T5>> setResult)
        {
            var values = value as CssValueList;

            if (values != null)
                values = values.Copy();
            else if (value != null)
                values = new CssValueList(value);
            else
                values = new CssValueList();

            if (values.Length < MinArgs && values.Length > MaxArgs)
                return false;

            var t1 = default(T1);
            var t2 = default(T2);
            var t3 = default(T3);
            var t4 = default(T4);
            var t5 = default(T5);

            if (!_first.VaryAll(values, m => t1 = m) ||
                !_second.VaryAll(values, m => t2 = m) ||
                !_third.VaryAll(values, m => t3 = m) ||
                !_fourth.VaryAll(values, m => t4 = m) ||
                !_fifth.VaryAll(values, m => t5 = m) ||
                values.Length != 0)
                return false;

            setResult(Tuple.Create(t1, t2, t3, t4, t5));
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            var values = value as CssValueList;

            if (values != null)
                values = values.Copy();
            else if (value != null)
                values = new CssValueList(value);
            else
                values = new CssValueList();

            if (values.Length < MinArgs && values.Length > MaxArgs)
                return false;

            return values.Length == 0;
        }
    }
}
