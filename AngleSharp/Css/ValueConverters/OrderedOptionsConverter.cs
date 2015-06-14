namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using AngleSharp.Dom.Css;

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

            return list.Count == 0 ? new OptionsValue(options, value) : null;
        }

        sealed class OptionsValue : IPropertyValue
        {
            readonly IPropertyValue[] _options;
            readonly CssValue _original;

            public OptionsValue(IPropertyValue[] options, IEnumerable<CssToken> tokens)
            {
                _options = options;
                _original = new CssValue(tokens);
            }

            public String CssText
            {
                get { return String.Join(" ", _options.Where(m => !String.IsNullOrEmpty(m.CssText)).Select(m => m.CssText)); }
            }

            public CssValue Original
            {
                get { return _original; }
            }
        }
    }
}
