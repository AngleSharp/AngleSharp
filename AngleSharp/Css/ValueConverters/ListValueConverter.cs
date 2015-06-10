namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class ListValueConverter<T> : IValueConverter<T[]>
    {
        readonly IValueConverter<T> _converter;

        public ListValueConverter(IValueConverter<T> converter)
        {
            _converter = converter;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var items = value.ToList();

            foreach (var item in items)
            {
                if (!_converter.Validate(item))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
