namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Parser.Css;

    sealed class AtomicValueConverter<T> : IValueConverter<T>
    {
        readonly IValueConverter<T> _converter;

        public AtomicValueConverter(IValueConverter<T> converter)
        {
            _converter = converter;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<T> setResult)
        {
            return _converter.TryConvert(value, setResult);
        }

        public Boolean Validate(IEnumerable<CssToken> value)
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
