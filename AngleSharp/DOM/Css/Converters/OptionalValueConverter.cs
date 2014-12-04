namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using System;

    sealed class OptionalValueConverter<T, U> : IValueConverter<Tuple<T, U>>
    {
        readonly IValueConverter<T> _listConverter;
        readonly IValueConverter<U> _optionConverter;
        readonly U _defaultValue;

        public OptionalValueConverter(IValueConverter<T> listConverter, IValueConverter<U> optionConverter, U defaultValue)
        {
            _listConverter = listConverter;
            _optionConverter = optionConverter;
            _defaultValue = defaultValue;
        }

        public Boolean TryConvert(ICssValue value, Action<Tuple<T, U>> setResult)
        {
            var values = value as CssValueList;
            var v1 = default(T);
            var v2 = default(U);

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if (_optionConverter.TryConvert(values[i], m => v2 = m))
                    {
                        value = values.CopyExcept(i);
                        break;
                    }
                }
            }

            if (!_listConverter.TryConvert(value, m => v1 = m))
                return false;

            setResult(Tuple.Create(v1, v2));
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            var values = value as CssValueList;

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if (_optionConverter.Validate(values[i]))
                    {
                        value = values.CopyExcept(i);
                        break;
                    }
                }
            }

            return _listConverter.Validate(value);
        }

        public Int32 MinArgs
        {
            get { return _listConverter.MinArgs; }
        }

        public Int32 MaxArgs
        {
            get { return _listConverter.MaxArgs + _optionConverter.MaxArgs; }
        }
    }
}
