namespace AngleSharp.Css.ValueConverters
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    sealed class OrderedOptionsConverter<T> : IValueConverter<T[]>
    {
        readonly IValueConverter<T> _converter;

        public OrderedOptionsConverter(IValueConverter<T> converter)
        {
            _converter = converter;
        }

        public Int32 MaxArgs
        {
            get { return UInt16.MaxValue; }
        }

        public Int32 MinArgs
        {
            get { return _converter.MinArgs; }
        }

        public Boolean TryConvert(ICssValue value, Action<T[]> setResult)
        {
            var values = value as CssValueList;

            if (values != null)
                values = values.Copy();
            else if (value != null)
                values = new CssValueList(value);
            else
                values = new CssValueList();

            if (values.Length < MinArgs)
                return false;

            var items = new List<T>();

            while (values.Length != 0)
            {
                if (!_converter.VaryStart(values, m => items.Add(m)))
                    return false;
            }

            setResult(items.ToArray());
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

            if (values.Length < MinArgs)
                return false;

            while (values.Length != 0)
            {
                if (!_converter.VaryStart(values))
                    return false;
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

        public Int32 MaxArgs
        {
            get { return _first.MaxArgs + _second.MaxArgs; }
        }

        public Int32 MinArgs
        {
            get { return _first.MinArgs + _second.MinArgs; }
        }

        public Boolean TryConvert(ICssValue value, Action<Tuple<T1, T2>> setResult)
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

            if (!_first.VaryStart(values, m => t1 = m) ||
                !_second.VaryStart(values, m => t2 = m) ||
                values.Length != 0)
                return false;

            setResult(Tuple.Create(t1, t2));
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

            return _first.VaryStart(values) && _second.VaryStart(values) &&
                   values.Length == 0;
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

            if (!_first.VaryStart(values, m => t1 = m) ||
                !_second.VaryStart(values, m => t2 = m) ||
                !_third.VaryStart(values, m => t3 = m) ||
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

            return _first.VaryStart(values) && _second.VaryStart(values) &&
                   _third.VaryStart(values) && values.Length == 0;
        }
    }
}
