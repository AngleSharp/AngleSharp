namespace AngleSharp.DOM.Css
{
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

        public Boolean TryConvert(CSSValue value, Action<Tuple<T, U>> setResult)
        {
            var values = value as CSSValueList;
            var v1 = default(T);
            var v2 = default(U);

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if (_optionConverter.TryConvert(values[i], m => v2 = m))
                    {
                        value = CopyExcept(values, i);
                        break;
                    }
                }
            }

            if (!_listConverter.TryConvert(value, m => v1 = m))
                return false;

            setResult(Tuple.Create(v1, v2));
            return true;
        }

        public Boolean Validate(CSSValue value)
        {
            var values = value as CSSValueList;

            if (values != null)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if (_optionConverter.Validate(values[i]))
                    {
                        value = CopyExcept(values, i);
                        break;
                    }
                }
            }

            return _listConverter.Validate(value);
        }

        static CSSValueList CopyExcept(CSSValueList original, Int32 index)
        {
            var list = new CSSValueList();

            for (int i = 0; i < original.Length; i++)
            {
                if (i != index)
                    list.Add(original[i]);
            }

            return list;
        }
    }
}
