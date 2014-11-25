namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using System;
    using System.Linq;

    sealed class ListValueConverter<T> : IValueConverter<T[]>
    {
        readonly IValueConverter<T>[] _converters;

        public ListValueConverter(IValueConverter<T>[] converters)
        {
            _converters = converters;
        }

        public Boolean TryConvert(CSSValue value, Action<T[]> setResult)
        {
            var items = (value as CSSValueList ?? new CSSValueList(value)).ToList();
            var targets = new T[items.Count];

            for (var i = 0; i < items.Count; i++)
            {
                var invalid = true;

                foreach (var converter in _converters)
                {
                    if (converter.TryConvert(items[i].Reduce(), nv => targets[i] = nv))
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
