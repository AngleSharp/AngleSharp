namespace AngleSharp.Css.ValueConverters
{
    using AngleSharp.Dom.Css;
    using System;

    sealed class ToValueConverter<T> : IValueConverter<ICssValue>
    {
        readonly IValueConverter<T> _converter;

        public ToValueConverter(IValueConverter<T> converter)
        {
            _converter = converter;
        }

        public Boolean TryConvert(ICssValue value, Action<ICssValue> setResult)
        {
            if (Validate(value))
            {
                setResult(value);
                return true;
            }

            return false;
        }

        public Boolean Validate(ICssValue value)
        {
            return _converter.Validate(value);
        }

        public Int32 MinArgs
        {
            get { return _converter.MinArgs; }
        }

        public Int32 MaxArgs
        {
            get { return _converter.MaxArgs; }
        }
    }

    sealed class ToValueConverter<T, U> : IValueConverter<U>
    {
        readonly IValueConverter<T> _converter;
        readonly Func<T, U> _next;

        public ToValueConverter(IValueConverter<T> converter, Func<T, U> next)
        {
            _converter = converter;
            _next = next;
        }

        public Boolean TryConvert(ICssValue value, Action<U> setResult)
        {
            return _converter.TryConvert(value, item => setResult(_next(item)));
        }

        public Boolean Validate(ICssValue value)
        {
            return _converter.Validate(value);
        }

        public Int32 MinArgs
        {
            get { return _converter.MinArgs; }
        }

        public Int32 MaxArgs
        {
            get { return _converter.MaxArgs; }
        }
    }
}
