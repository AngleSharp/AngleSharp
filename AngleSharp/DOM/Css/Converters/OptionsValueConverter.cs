namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using System;

    sealed class OptionsValueConverter<T1, T2> : IValueConverter<Tuple<T1, T2>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly Tuple<T1, T2> _defaults;

        public OptionsValueConverter(IValueConverter<T1> first, IValueConverter<T2> second, Tuple<T1, T2> defaults)
        {
            _first = first;
            _second = second;
            _defaults = defaults;
        }

        public Boolean TryConvert(ICssValue value, Action<Tuple<T1, T2>> setResult)
        {
            var items = value.CopyToList();

            if (items.Length > 2)
                return false;

            var t1 = _first.TryAll(items, _defaults.Item1);
            var t2 = _second.TryAll(items, _defaults.Item2);

            if (items.Length > 0)
                return false;

            setResult(Tuple.Create(t1, t2));
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            var items = value.CopyToList();

            if (items.Length > 2)
                return false;

            var validators = new IValueConverter[] { _first, _second };

            for (int i = 0; i < validators.Length; i++)
            {
                for (int j = 0; j < items.Length; j++)
                {
                    if (validators[i].Validate(items[j]))
                    {
                        items.RemoveAt(j);
                        break;
                    }
                }
            }

            return items.Length == 0;
        }
    }

    sealed class OptionsValueConverter<T1, T2, T3> : IValueConverter<Tuple<T1, T2, T3>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly IValueConverter<T3> _third;
        readonly Tuple<T1, T2, T3> _defaults;

        public OptionsValueConverter(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, Tuple<T1, T2, T3> defaults)
        {
            _first = first;
            _second = second;
            _third = third;
            _defaults = defaults;
        }

        public Boolean TryConvert(ICssValue value, Action<Tuple<T1, T2, T3>> setResult)
        {
            var items = value.CopyToList();

            if (items.Length > 3)
                return false;

            var t1 = _first.TryAll(items, _defaults.Item1);
            var t2 = _second.TryAll(items, _defaults.Item2);
            var t3 = _third.TryAll(items, _defaults.Item3);

            if (items.Length > 0)
                return false;

            setResult(Tuple.Create(t1, t2, t3));
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            var items = value.CopyToList();

            if (items.Length > 3)
                return false;

            var validators = new IValueConverter[] { _first, _second, _third };

            for (int i = 0; i < validators.Length; i++)
            {
                for (int j = 0; j < items.Length; j++)
                {
                    if (validators[i].Validate(items[j]))
                    {
                        items.RemoveAt(j);
                        break;
                    }
                }
            }

            return items.Length == 0;
        }
    }

    sealed class OptionsValueConverter<T1, T2, T3, T4> : IValueConverter<Tuple<T1, T2, T3, T4>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly IValueConverter<T3> _third;
        readonly IValueConverter<T4> _fourth;
        readonly Tuple<T1, T2, T3, T4> _defaults;

        public OptionsValueConverter(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, Tuple<T1, T2, T3, T4> defaults)
        {
            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
            _defaults = defaults;
        }

        public Boolean TryConvert(ICssValue value, Action<Tuple<T1, T2, T3, T4>> setResult)
        {
            var items = value.CopyToList();

            if (items.Length > 4)
                return false;

            var t1 = _first.TryAll(items, _defaults.Item1);
            var t2 = _second.TryAll(items, _defaults.Item2);
            var t3 = _third.TryAll(items, _defaults.Item3);
            var t4 = _fourth.TryAll(items, _defaults.Item4);

            if (items.Length > 0)
                return false;

            setResult(Tuple.Create(t1, t2, t3, t4));
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            var items = value.CopyToList();

            if (items.Length > 4)
                return false;

            var validators = new IValueConverter[] { _first, _second, _third, _fourth };

            for (int i = 0; i < validators.Length; i++)
            {
                for (int j = 0; j < items.Length; j++)
                {
                    if (validators[i].Validate(items[j]))
                    {
                        items.RemoveAt(j);
                        break;
                    }
                }
            }

            return items.Length == 0;
        }
    }

    sealed class OptionsValueConverter<T1, T2, T3, T4, T5, T6, T7, T8> : IValueConverter<Tuple<Tuple<T1, T2, T3, T4>, Tuple<T5, T6, T7, T8>>>
    {
        readonly IValueConverter<T1> _first;
        readonly IValueConverter<T2> _second;
        readonly IValueConverter<T3> _third;
        readonly IValueConverter<T4> _fourth;
        readonly IValueConverter<T5> _fifth;
        readonly IValueConverter<T6> _sixth;
        readonly IValueConverter<T7> _seventh;
        readonly IValueConverter<T8> _eigth;
        readonly Tuple<Tuple<T1, T2, T3, T4>, Tuple<T5, T6, T7, T8>> _defaults;

        public OptionsValueConverter(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth, IValueConverter<T6> sixth, IValueConverter<T7> seventh, IValueConverter<T8> eighth, Tuple<Tuple<T1, T2, T3, T4>, Tuple<T5, T6, T7, T8>> defaults)
        {
            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
            _fifth = fifth;
            _sixth = sixth;
            _seventh = seventh;
            _eigth = eighth;
            _defaults = defaults;
        }

        public Boolean TryConvert(ICssValue value, Action<Tuple<Tuple<T1, T2, T3, T4>, Tuple<T5, T6, T7, T8>>> setResult)
        {
            var items = value.CopyToList();

            if (items.Length > 8)
                return false;

            var t1 = _first.TryAll(items, _defaults.Item1.Item1);
            var t2 = _second.TryAll(items, _defaults.Item1.Item2);
            var t3 = _third.TryAll(items, _defaults.Item1.Item3);
            var t4 = _fourth.TryAll(items, _defaults.Item1.Item4);
            var t5 = _fifth.TryAll(items, _defaults.Item2.Item1);
            var t6 = _sixth.TryAll(items, _defaults.Item2.Item2);
            var t7 = _seventh.TryAll(items, _defaults.Item2.Item3);
            var t8 = _eigth.TryAll(items, _defaults.Item2.Item4);

            if (items.Length > 0)
                return false;

            setResult(Tuple.Create(Tuple.Create(t1, t2, t3, t4), Tuple.Create(t5, t6, t7, t8)));
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            var items = value.CopyToList();

            if (items.Length > 8)
                return false;

            var validators = new IValueConverter[] { _first, _second, _third, _fourth, _fifth, _sixth, _seventh, _eigth };

            for (int i = 0; i < validators.Length; i++)
            {
                for (int j = 0; j < items.Length; j++)
                {
                    if (validators[i].Validate(items[j]))
                    {
                        items.RemoveAt(j);
                        break;
                    }
                }
            }

            return items.Length == 0;
        }
    }
}
