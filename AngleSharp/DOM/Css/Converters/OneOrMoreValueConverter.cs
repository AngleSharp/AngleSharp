namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using System;
    using System.Linq;

    sealed class OneOrMoreValueConverter<T> : IValueConverter<T[]>
    {
        readonly IValueConverter<T>[] _converters;

        public OneOrMoreValueConverter(IValueConverter<T>[] converters)
        {
            _converters = converters;
        }

        public Boolean TryConvert(CSSValue value, Action<T[]> setResult)
        {
            var items = value.AsEnumeration().ToArray();
            var targets = new T[items.Length];

            for (var i = 0; i < items.Length; i++)
            {
                var invalid = true;

                foreach (var converter in _converters)
                {
                    if (converter.TryConvert(items[i], nv => targets[i] = nv))
                    {
                        invalid = false;
                        break;
                    }
                }

                if (invalid)
                    return false;
            }

            setResult(targets);
            return true;
        }

        public Boolean Validate(CSSValue value)
        {
            var items = value.AsEnumeration();

            foreach (var item in items)
            {
                var invalid = true;

                foreach (var converter in _converters)
                {
                    if (converter.Validate(item))
                    {
                        invalid = false;
                        break;
                    }
                }

                if (invalid)
                    return false;
            }

            return true;
        }
    }
}
