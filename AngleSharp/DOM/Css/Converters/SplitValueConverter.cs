namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    sealed class SplitValueConverter<T, U> : IValueConverter<U[]>
    {
        readonly IValueConverter<T> _condition;
        readonly IValueConverter<U> _converter;
        readonly Boolean _include;

        public SplitValueConverter(IValueConverter<T> condition, IValueConverter<U> converter, Boolean include = true)
        {
            _condition = condition;
            _converter = converter;
            _include = include;
        }

        public Boolean TryConvert(ICssValue value, Action<U[]> setResult)
        {
            var values = value as CssValueList;
            var results = new List<U>();

            if (values != null)
            {
                var list = new CssValueList();
                list.Add(values[0]);

                for (int i = 1; i < values.Length; i++)
                {
                    if (_condition.Validate(values[i]))
                    {
                        if (!_converter.TryConvert(list, m => results.Add(m)))
                            return false;

                        list = new CssValueList();

                        if (!_include)
                            continue;
                    }

                    list.Add(values[i]);
                }

                value = list;
            }
            else
                value = new CssValueList(value);

            if (!_converter.TryConvert(value, m => results.Add(m)))
                return false;

            setResult(results.ToArray());
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            var values = value as CssValueList;

            if (values != null)
            {
                var list = new CssValueList();
                list.Add(values[0]);

                for (int i = 1; i < values.Length; i++)
                {
                    if (_condition.Validate(values[i]))
                    {
                        if (!_converter.Validate(list))
                            return false;

                        list = new CssValueList();

                        if (!_include)
                            continue;
                    }

                    list.Add(values[i]);
                }
                value = list;
            }
            else
                value = new CssValueList(value);

            return _converter.Validate(value);
        }

        public Int32 MinArgs
        {
            get { return _converter.MinArgs; }
        }

        public Int32 MaxArgs
        {
            get { return Int32.MaxValue; }
        }
    }
}
