namespace AngleSharp.Css.ValueConverters
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

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

            for (var i = 0; i < _converters.Length; i++)
            {
                options[i] = _converters[i].VaryStart(list);

                if (options[i] == null)
                {
                    return null;
                }
            }

            return list.Count == 0 ? new OptionsValue(options, value) : null;
        }

        public IPropertyValue Construct(CssProperty[] properties)
        {
            var result = properties.Guard<OptionsValue>();

            if (result == null)
            {
                var values = new IPropertyValue[_converters.Length];

                for (var i = 0; i < _converters.Length; i++)
                {
                    var value = _converters[i].Construct(properties);

                    if (value == null)
                    {
                        return null;
                    }

                    values[i] = value;
                }

                result = new OptionsValue(values, Enumerable.Empty<CssToken>());
            }

            return result;
        }

        sealed class OptionsValue : IPropertyValue, IEnumerable<IPropertyValue>
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

            public CssValue ExtractFor(String name)
            {
                var tokens = new List<CssToken>();

                foreach (var option in _options)
                {
                    var extracted = option.ExtractFor(name);

                    if (extracted != null && extracted.Count > 0)
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

            IEnumerator<IPropertyValue> IEnumerable<IPropertyValue>.GetEnumerator()
            {
                foreach (var option in _options)
                {
                    yield return option;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _options.GetEnumerator();
            }
        }
    }
}
