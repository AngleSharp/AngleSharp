namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class DictionaryValueConverter<T> : IValueConverter<T>
    {
        readonly Dictionary<String, T> _values;

        public DictionaryValueConverter(Dictionary<String, T> values)
        {
            _values = values;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<T> setResult)
        {
            var temp = default(T);

            if (_values.TryGetValue(value, out temp))
            {
                setResult(temp);
                return true;
            }

            return false;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var mode = default(T);
            return _values.TryGetValue(value, out mode);
        }
    }
}
