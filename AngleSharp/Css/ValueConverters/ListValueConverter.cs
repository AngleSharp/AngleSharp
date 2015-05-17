namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class ListValueConverter<T> : IValueConverter<T[]>
    {
        readonly IValueConverter<T> _converter;

        public ListValueConverter(IValueConverter<T> converter)
        {
            _converter = converter;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<T[]> setResult)
        {
            var items = value.ToList();
            var targets = new T[items.Count];

            for (var i = 0; i < items.Count; i++)
            {
                if (!_converter.TryConvert(items[i], nv => targets[i] = nv))
                {
                    return false;
                }
            }

            setResult(targets);
            return true;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var items = value.ToList();

            foreach (var item in items)
            {
                if (!_converter.Validate(item))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
