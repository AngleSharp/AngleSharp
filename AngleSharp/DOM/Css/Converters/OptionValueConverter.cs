namespace AngleSharp.DOM.Css
{
    using System;

    sealed class OptionValueConverter<T> : IValueConverter<T>
    {
        readonly IValueConverter<T> _converter;
        readonly T _default;

        public OptionValueConverter(IValueConverter<T> converter, T @default)
        {
            _converter = converter;
            _default = @default;
        }

        public Boolean TryConvert(ICssValue value, Action<T> setResult)
        {
            if (value == null || !_converter.TryConvert(value, setResult))
                setResult(_default);

            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            return true;
        }

        public Int32 MinArgs
        {
            get { return 0; }
        }

        public Int32 MaxArgs
        {
            get { return _converter.MaxArgs; }
        }
    }
}
