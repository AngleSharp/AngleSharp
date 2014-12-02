namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using System;
    using System.Linq;

    sealed class OneOrMoreValueConverter<T> : IValueConverter<T[]>
    {
        readonly IValueConverter<T> _converter;

        public OneOrMoreValueConverter(IValueConverter<T> converter)
        {
            _converter = converter;
        }

        public Boolean TryConvert(ICssValue value, Action<T[]> setResult)
        {
            var items = value.AsEnumeration().ToArray();
            var targets = new T[items.Length];

            for (var i = 0; i < items.Length; i++)
            {
                if (!_converter.TryConvert(items[i], nv => targets[i] = nv))
                    return false;
            }

            setResult(targets);
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            var items = value.AsEnumeration();

            foreach (var item in items)
            {
                if (!_converter.Validate(item))
                    return false;
            }

            return true;
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
