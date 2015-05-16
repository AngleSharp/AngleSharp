namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Parser.Css;

    sealed class ClassValueConverter<T> : IValueConverter<T>
        where T : class
    {
        readonly Func<IEnumerable<CssToken>, T> _converter;

        public ClassValueConverter(Func<IEnumerable<CssToken>, T> converter)
        {
            _converter = converter;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<T> setResult)
        {
            var result = _converter(value);

            if (result == null)
                return false;

            setResult(result);
            return true;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            return _converter(value) != null;
        }
    }
}
