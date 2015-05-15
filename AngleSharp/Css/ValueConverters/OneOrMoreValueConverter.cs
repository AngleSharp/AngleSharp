namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Parser.Css;

    sealed class OneOrMoreValueConverter<T> : IValueConverter<T[]>
    {
        readonly IValueConverter<T> _converter;
        readonly Int32 _minimum;
        readonly Int32 _maximum;

        public OneOrMoreValueConverter(IValueConverter<T> converter, Int32 minimum, Int32 maximum)
        {
            _converter = converter;
            _minimum = minimum;
            _maximum = maximum;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<T[]> setResult)
        {
            //var items = value.AsEnumeration().ToArray();

            //if (items.Length < _minimum || items.Length > _maximum)
            //    return false;

            //var targets = new T[items.Length];

            //for (var i = 0; i < items.Length; i++)
            //{
            //    if (!_converter.TryConvert(items[i], nv => targets[i] = nv))
            //        return false;
            //}

            //setResult(targets);
            return true;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            //var items = value.AsEnumeration().ToArray();

            //if (items.Length < _minimum || items.Length > _maximum)
            //    return false;

            //for (var i = 0; i < items.Length; i++)
            //{
            //    if (!_converter.Validate(items[i]))
            //        return false;
            //}

            return true;
        }

        public Int32 MinArgs
        {
            get { return _minimum; }
        }

        public Int32 MaxArgs
        {
            get { return _maximum; }
        }
    }
}
