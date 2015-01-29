namespace AngleSharp.Css.ValueConverters
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    sealed class DictionaryValueConverter<T> : IValueConverter<T>
    {
        readonly Dictionary<String, T> _values;

        public DictionaryValueConverter(Dictionary<String, T> values)
        {
            _values = values;
        }

        public Boolean TryConvert(ICssValue value, Action<T> setResult)
        {
            var temp = default(T);

            if (!_values.TryGetValue(value, out temp))
                return false;

            setResult(temp);
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            var mode = default(T);
            return _values.TryGetValue(value, out mode);
        }

        public Int32 MinArgs
        {
            get { return 1; }
        }

        public Int32 MaxArgs
        {
            get { return 1; }
        }
    }
}
