namespace AngleSharp.Css.ValueConverters
{
    using AngleSharp.Dom.Css;
    using System;

    sealed class ClassValueConverter<T> : IValueConverter<T>
        where T : class
    {
        readonly Func<ICssValue, T> _converter;

        public ClassValueConverter(Func<ICssValue, T> converter)
        {
            _converter = converter;
        }

        public Boolean TryConvert(ICssValue value, Action<T> setResult)
        {
            var result = _converter(value);

            if (result == null)
                return false;

            setResult(result);
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            return _converter(value) != null;
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
