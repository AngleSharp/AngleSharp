namespace AngleSharp.DOM.Css
{
    using System;

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

        public Boolean TryConvert(CSSValue value, Action<Tuple<T1, T2, T3>> setResult)
        {
            var items = value as CSSValueList ?? new CSSValueList(value);

            if (items.Length > 3)
                return false;

            var t1 = TryAll(items, _first, _defaults.Item1);
            var t2 = TryAll(items, _second, _defaults.Item2);
            var t3 = TryAll(items, _third, _defaults.Item3);

            if (items.Length > 0)
                return false;

            setResult(Tuple.Create(t1, t2, t3));
            return true;
        }

        static T TryAll<T>(CSSValueList list, IValueConverter<T> converter, T defaultValue)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (converter.TryConvert(list[i], tmp => defaultValue = tmp))
                {
                    list.Remove(list[i]);
                    break;
                }
            }

            return defaultValue;
        }

        public Boolean Validate(CSSValue value)
        {
            var items = value as CSSValueList ?? new CSSValueList(value);

            if (items.Length > 3)
                return false;

            var validators = new IValueConverter[] { _first, _second, _third };

            for (int i = 0; i < validators.Length; i++)
            {
                for (int j = 0; j < items.Length; j++)
                {
                    if (validators[i].Validate(items[j]))
                    {
                        items.Remove(items[j]);
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

        public Boolean TryConvert(CSSValue value, Action<Tuple<T1, T2, T3, T4>> setResult)
        {
            var items = value as CSSValueList ?? new CSSValueList(value);

            if (items.Length > 4)
                return false;

            var t1 = TryAll(items, _first, _defaults.Item1);
            var t2 = TryAll(items, _second, _defaults.Item2);
            var t3 = TryAll(items, _third, _defaults.Item3);
            var t4 = TryAll(items, _fourth, _defaults.Item4);

            if (items.Length > 0)
                return false;

            setResult(Tuple.Create(t1, t2, t3, t4));
            return true;
        }

        static T TryAll<T>(CSSValueList list, IValueConverter<T> converter, T defaultValue)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (converter.TryConvert(list[i], tmp => defaultValue = tmp))
                {
                    list.Remove(list[i]);
                    break;
                }
            }

            return defaultValue;
        }

        public Boolean Validate(CSSValue value)
        {
            var items = value as CSSValueList ?? new CSSValueList(value);

            if (items.Length > 4)
                return false;

            var validators = new IValueConverter[] { _first, _second, _third, _fourth };

            for (int i = 0; i < validators.Length; i++)
            {
                for (int j = 0; j < items.Length; j++)
                {
                    if (validators[i].Validate(items[j]))
                    {
                        items.Remove(items[j]);
                        break;
                    }
                }
            }

            return items.Length == 0;
        }
    }
}
