namespace AngleSharp.Css.ValueConverters
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class UnorderedOptionsConverter<T1, T2> : IValueConverter<Tuple<T1, T2>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;

        public UnorderedOptionsConverter(IValueConverter<T1> first, IValueConverter<T2> second)
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

            if (!_first.VaryAll(values, m => t1 = m) ||
                !_second.VaryAll(values, m => t2 = m) ||
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

            return _first.VaryAll(values) && _second.VaryAll(values) && values.Length == 0;
        }
    }

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

            return _first.VaryAll(values) && _second.VaryAll(values) &&
                   _third.VaryAll(values) && values.Length == 0;
        }
    }

    sealed class UnorderedOptionsConverter<T1, T2, T3, T4> : IValueConverter<Tuple<T1, T2, T3, T4>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly IValueConverter<T3> _third;
        readonly IValueConverter<T4> _fourth;

        public UnorderedOptionsConverter(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth)
        {
            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
        }

        public Int32 MaxArgs
        {
            get { return _first.MaxArgs + _second.MaxArgs + _third.MaxArgs + _fourth.MaxArgs; }
        }

        public Int32 MinArgs
        {
            get { return _first.MinArgs + _second.MinArgs + _third.MinArgs + _fourth.MinArgs; }
        }

        public Boolean TryConvert(ICssValue value, Action<Tuple<T1, T2, T3, T4>> setResult)
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

            if (!_first.VaryAll(values, m => t1 = m) ||
                !_second.VaryAll(values, m => t2 = m) ||
                !_third.VaryAll(values, m => t3 = m) ||
                !_fourth.VaryAll(values, m => t4 = m) ||
                values.Length != 0)
                return false;

            setResult(Tuple.Create(t1, t2, t3, t4));
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

            return _first.VaryAll(values) && _second.VaryAll(values) &&
                   _third.VaryAll(values) && _fourth.VaryAll(values) &&
                   values.Length == 0;
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

            return _first.VaryAll(values) && _second.VaryAll(values) &&
                   _third.VaryAll(values) && _fourth.VaryAll(values) &&
                   _fifth.VaryAll(values) && values.Length == 0;
        }
    }

    sealed class UnorderedOptionsConverter<T1, T2, T3, T4, T5, T6> : IValueConverter<Tuple<T1, T2, T3, T4, T5, T6>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly IValueConverter<T3> _third;
        readonly IValueConverter<T4> _fourth;
        readonly IValueConverter<T5> _fifth;
        readonly IValueConverter<T6> _sixth;

        public UnorderedOptionsConverter(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth, IValueConverter<T6> sixth)
        {
            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
            _fifth = fifth;
            _sixth = sixth;
        }

        public Int32 MaxArgs
        {
            get { return _first.MaxArgs + _second.MaxArgs + _third.MaxArgs + _fourth.MaxArgs + _fifth.MaxArgs + _sixth.MaxArgs; }
        }

        public Int32 MinArgs
        {
            get { return _first.MinArgs + _second.MinArgs + _third.MinArgs + _fourth.MinArgs + _fifth.MinArgs + _sixth.MinArgs; }
        }

        public Boolean TryConvert(ICssValue value, Action<Tuple<T1, T2, T3, T4, T5, T6>> setResult)
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
            var t6 = default(T6);

            if (!_first.VaryAll(values, m => t1 = m) ||
                !_second.VaryAll(values, m => t2 = m) ||
                !_third.VaryAll(values, m => t3 = m) ||
                !_fourth.VaryAll(values, m => t4 = m) ||
                !_fifth.VaryAll(values, m => t5 = m) ||
                !_sixth.VaryAll(values, m => t6 = m) ||
                values.Length != 0)
                return false;

            setResult(Tuple.Create(t1, t2, t3, t4, t5, t6));
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

            return _first.VaryAll(values) && _second.VaryAll(values) && 
                   _third.VaryAll(values) && _fourth.VaryAll(values) && 
                   _fifth.VaryAll(values) && _sixth.VaryAll(values) && 
                   values.Length == 0;
        }
    }

    sealed class UnorderedOptionsConverter<T1, T2, T3, T4, T5, T6, T7> : IValueConverter<Tuple<T1, T2, T3, T4, T5, T6, T7>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly IValueConverter<T3> _third;
        readonly IValueConverter<T4> _fourth;
        readonly IValueConverter<T5> _fifth;
        readonly IValueConverter<T6> _sixth;
        readonly IValueConverter<T7> _seventh;

        public UnorderedOptionsConverter(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth, IValueConverter<T6> sixth, IValueConverter<T7> seventh)
        {
            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
            _fifth = fifth;
            _sixth = sixth;
            _seventh = seventh;
        }

        public Int32 MaxArgs
        {
            get { return _first.MaxArgs + _second.MaxArgs + _third.MaxArgs + _fourth.MaxArgs + _fifth.MaxArgs + _sixth.MaxArgs + _seventh.MaxArgs; }
        }

        public Int32 MinArgs
        {
            get { return _first.MinArgs + _second.MinArgs + _third.MinArgs + _fourth.MinArgs + _fifth.MinArgs + _sixth.MinArgs + _seventh.MinArgs; }
        }

        public Boolean TryConvert(ICssValue value, Action<Tuple<T1, T2, T3, T4, T5, T6, T7>> setResult)
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
            var t6 = default(T6);
            var t7 = default(T7);

            if (!_first.VaryAll(values, m => t1 = m) ||
                !_second.VaryAll(values, m => t2 = m) ||
                !_third.VaryAll(values, m => t3 = m) ||
                !_fourth.VaryAll(values, m => t4 = m) ||
                !_fifth.VaryAll(values, m => t5 = m) ||
                !_sixth.VaryAll(values, m => t6 = m) ||
                !_seventh.VaryAll(values, m => t7 = m) ||
                values.Length != 0)
                return false;

            setResult(Tuple.Create(t1, t2, t3, t4, t5, t6, t7));
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

            return _first.VaryAll(values) && _second.VaryAll(values) &&
                   _third.VaryAll(values) && _fourth.VaryAll(values) &&
                   _fifth.VaryAll(values) && _sixth.VaryAll(values) &&
                   _seventh.VaryAll(values) && values.Length == 0;
        }
    }

    sealed class UnorderedOptionsConverter<T1, T2, T3, T4, T5, T6, T7, T8> : IValueConverter<Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly IValueConverter<T3> _third;
        readonly IValueConverter<T4> _fourth;
        readonly IValueConverter<T5> _fifth;
        readonly IValueConverter<T6> _sixth;
        readonly IValueConverter<T7> _seventh;
        readonly IValueConverter<T8> _eighth;

        public UnorderedOptionsConverter(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth, IValueConverter<T6> sixth, IValueConverter<T7> seventh, IValueConverter<T8> eighth)
        {
            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
            _fifth = fifth;
            _sixth = sixth;
            _seventh = seventh;
            _eighth = eighth;
        }

        public Int32 MaxArgs
        {
            get { return _first.MaxArgs + _second.MaxArgs + _third.MaxArgs + _fourth.MaxArgs + _fifth.MaxArgs + _sixth.MaxArgs + _seventh.MaxArgs + _eighth.MaxArgs; }
        }

        public Int32 MinArgs
        {
            get { return _first.MinArgs + _second.MinArgs + _third.MinArgs + _fourth.MinArgs + _fifth.MinArgs + _sixth.MinArgs + _seventh.MinArgs + _eighth.MinArgs; }
        }

        public Boolean TryConvert(ICssValue value, Action<Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>> setResult)
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
            var t6 = default(T6);
            var t7 = default(T7);
            var t8 = default(T8);

            if (!_first.VaryAll(values, m => t1 = m) ||
                !_second.VaryAll(values, m => t2 = m) ||
                !_third.VaryAll(values, m => t3 = m) ||
                !_fourth.VaryAll(values, m => t4 = m) ||
                !_fifth.VaryAll(values, m => t5 = m) ||
                !_sixth.VaryAll(values, m => t6 = m) ||
                !_seventh.VaryAll(values, m => t7 = m) ||
                !_eighth.VaryAll(values, m => t8 = m) ||
                values.Length != 0)
                return false;

            setResult(Tuple.Create(t1, t2, t3, t4, t5, t6, t7, t8));
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

            return _first.VaryAll(values) && _second.VaryAll(values) &&
                   _third.VaryAll(values) && _fourth.VaryAll(values) &&
                   _fifth.VaryAll(values) && _sixth.VaryAll(values) &&
                   _seventh.VaryAll(values) && _eighth.VaryAll(values) &&
                   values.Length == 0;
        }
    }
}
