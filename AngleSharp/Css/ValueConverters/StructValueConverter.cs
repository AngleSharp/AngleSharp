namespace AngleSharp.Css.ValueConverters
{
    using AngleSharp.Dom.Css;
    using System;

    sealed class StructValueConverter<T> : IValueConverter<T>
        where T : struct
    {
        readonly Func<ICssValue, T?> _converter;

        public StructValueConverter(Func<ICssValue, T?> converter)
        {
            _converter = converter;
        }

        public Boolean TryConvert(ICssValue value, Action<T> setResult)
        {
            var result = _converter(value);

            if (!result.HasValue)
                return false;

            setResult(result.Value);
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            return _converter(value).HasValue;
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
