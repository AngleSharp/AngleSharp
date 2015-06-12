namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class OrderedOptionsConverter: IValueConverter
    {
        readonly IValueConverter[] _converters;

        public OrderedOptionsConverter(params IValueConverter[] converters)
        {
            _converters = converters;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var list = new List<CssToken>(value);
            var options = new IPropertyValue[_converters.Length];

            for (int i = 0; i < _converters.Length; i++)
            {
                options[i] = _converters[i].VaryStart(list);

                if (options[i] == null)
                    return null;
            }

            return list.Count == 0 ? new OptionsValue(options) : null;
        }

        sealed class OptionsValue : IPropertyValue
        {
            readonly IPropertyValue[] _options;

            public OptionsValue(IPropertyValue[] options)
            {
                _options = options;
            }

            public String CssText
            {
                get { return String.Join(" ", _options.Select(m => m.CssText)); }
            }
        }
    }
}
