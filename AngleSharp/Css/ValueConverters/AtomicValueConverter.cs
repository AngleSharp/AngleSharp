namespace AngleSharp.Css.ValueConverters
{
    using AngleSharp.Dom.Css;
    using System;

    sealed class AtomicValueConverter<T> : IValueConverter<T>
    {
        readonly IValueConverter<T> _converter;

        public AtomicValueConverter(IValueConverter<T> converter)
        {
            _converter = converter;
        }

        public Boolean TryConvert(ICssValue value, Action<T> setResult)
        {
            return _converter.TryConvert(Reduce(value), setResult);
        }

        public Boolean Validate(ICssValue value)
        {
            return _converter.Validate(Reduce(value));
        }

        public Int32 MinArgs
        {
            get { return _converter.MinArgs; }
        }

        public Int32 MaxArgs
        {
            get { return _converter.MaxArgs; }
        }

        static ICssValue Reduce(ICssValue value)
        {
            var values = value as CssValueList;

            if (values != null && values.Length == 1)
                return values[0];

            return value;
        }
    }
}
