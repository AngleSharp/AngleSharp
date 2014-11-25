namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using System;
    using System.Linq;

    sealed class ListValueConverter<T> : IValueConverter<T[]>
    {
        readonly IValueConverter<T> _converter;

        public ListValueConverter(IValueConverter<T> converter)
        {
            _converter = converter;
        }

        public Boolean TryConvert(CSSValue value, Action<T[]> setResult)
        {
            var items = (value as CSSValueList ?? new CSSValueList(value)).ToList();
            var targets = new T[items.Count];

            for (var i = 0; i < items.Count; i++)
            {
                if (!_converter.TryConvert(items[i].Reduce(), nv => targets[i] = nv))
                    return false;
            }

            setResult(targets);
            return true;
        }

        public Boolean Validate(CSSValue value)
        {
            var items = (value as CSSValueList ?? new CSSValueList(value)).ToList();

            foreach (var item in items)
            {
                if (!_converter.Validate(item))
                    return false;
            }

            return true;
        }
    }
}
