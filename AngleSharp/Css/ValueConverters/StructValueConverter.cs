namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Parser.Css;

    sealed class StructValueConverter<T> : IValueConverter<T>
        where T : struct
    {
        readonly Func<IEnumerable<CssToken>, T?> _converter;

        public StructValueConverter(Func<IEnumerable<CssToken>, T?> converter)
        {
            _converter = converter;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<T> setResult)
        {
            var result = _converter(value);

            if (!result.HasValue)
                return false;

            setResult(result.Value);
            return true;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            return _converter(value).HasValue;
        }
    }
}
