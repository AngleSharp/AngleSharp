namespace AngleSharp.Css.ValueConverters
{
    using AngleSharp.Dom.Css;
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

        public Boolean TryConvert(ICssValue value, Action<T[]> setResult)
        {
            var items = (value as CssValueList ?? new CssValueList(value)).ToList();
            var targets = new T[items.Count];

            for (var i = 0; i < items.Count; i++)
            {
                if (!_converter.TryConvert(items[i].Reduce(), nv => targets[i] = nv))
                    return false;
            }

            setResult(targets);
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            var items = (value as CssValueList ?? new CssValueList(value)).ToList();

            foreach (var item in items)
            {
                if (!_converter.Validate(item.Reduce()))
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
            get { return UInt16.MaxValue; }
        }
    }
}
