namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using AngleSharp.Dom.Css;

    sealed class ContinuousValueConverter : IValueConverter
    {
        readonly IValueConverter _converter;

        public ContinuousValueConverter(IValueConverter converter)
        {
            _converter = converter;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var list = new List<CssToken>(value);
            var options = new List<IPropertyValue>();

            if (list.Count > 0)
            {
                while (list.Count != 0)
                {
                    var option = _converter.VaryStart(list);

                    if (option == null)
                    {
                        return null;
                    }

                    options.Add(option);
                }

                return new OptionsValue(options.ToArray(), value);
            }

            return null;
        }

        public IPropertyValue Construct(CssProperty[] properties)
        {
            return properties.Guard<OptionsValue>();
        }

        sealed class OptionsValue : IPropertyValue
        {
            readonly IPropertyValue[] _options;
            readonly CssValue _value;

            public OptionsValue(IPropertyValue[] options, IEnumerable<CssToken> tokens)
            {
                _options = options;
                _value = new CssValue(tokens);
            }

            public String CssText
            {
                get { return String.Join(" ", _options.Where(m => !String.IsNullOrEmpty(m.CssText)).Select(m => m.CssText)); }
            }

            public CssValue Original
            {
                get { return _value; }
            }

            public CssValue ExtractFor(String name)
            {
                var tokens = new List<CssToken>();

                foreach (var option in _options)
                {
                    var extracted = option.ExtractFor(name);

                    if (extracted != null)
                    {
                        if (tokens.Count > 0)
                        {
                            tokens.Add(CssToken.Whitespace);
                        }

                        tokens.AddRange(extracted);
                    }
                }

                return new CssValue(tokens);
            }
        }
    }
}
