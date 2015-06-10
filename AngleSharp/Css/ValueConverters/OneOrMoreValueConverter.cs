namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Extensions;
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

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var items = value.ToItems();

            if (items.Count >= _minimum && items.Count <= _maximum)
            {
                foreach (var item in items)
                {
                    if (!_converter.Validate(item))
                        return false;
                }

                return true;
            }

            return false;
        }
    }
}
