namespace AngleSharp.Css.ValueConverters
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class PeriodicValueConverter : IValueConverter
    {
        readonly IValueConverter _converter;
        readonly String[] _labels;

        public PeriodicValueConverter(IValueConverter converter, String[] labels)
        {
            _converter = converter;
            _labels = labels.Length == 4 ? labels : Enumerable.Repeat(String.Empty, 4).ToArray();
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var list = new List<CssToken>(value);
            var options = new IPropertyValue[4];

            if (list.Count == 0)
            {
                return null;
            }

            for (var i = 0; i < options.Length && list.Count != 0; i++)
            {
                options[i] = _converter.VaryStart(list);

                if (options[i] == null)
                {
                    return null;
                }
            }

            return list.Count == 0 ? new PeriodicValue(options, value, _labels) : null;
        }

        public IPropertyValue Construct(CssProperty[] properties)
        {
            if (properties.Length == 4)
            {
                var options = new IPropertyValue[4];
                options[0] = _converter.Construct(properties.Where(m => m.Name == _labels[0]).ToArray());
                options[1] = _converter.Construct(properties.Where(m => m.Name == _labels[1]).ToArray());
                options[2] = _converter.Construct(properties.Where(m => m.Name == _labels[2]).ToArray());
                options[3] = _converter.Construct(properties.Where(m => m.Name == _labels[3]).ToArray());
                return options[0] != null && options[1] != null && options[2] != null && options[3] != null ? 
                    new PeriodicValue(options, Enumerable.Empty<CssToken>(), _labels) : null;
            }

            return null;
        }

        sealed class PeriodicValue : IPropertyValue
        {
            readonly IPropertyValue _top;
            readonly IPropertyValue _right; 
            readonly IPropertyValue _bottom;
            readonly IPropertyValue _left;
            readonly CssValue _original;
            readonly String[] _labels;

            public PeriodicValue(IPropertyValue[] options, IEnumerable<CssToken> tokens, String[] labels)
            {
                _top = options[0];
                _right = options[1] ?? _top;
                _bottom = options[2] ?? _top;
                _left = options[3] ?? _right;
                _original = new CssValue(tokens);
                _labels = labels;
            }

            public String[] Values
            {
                get
                {
                    var top = _top.CssText;
                    var right = _right.CssText;
                    var bottom = _bottom.CssText;
                    var left = _left.CssText;

                    if (right.Is(left))
                    {
                        if (top.Is(bottom))
                        {
                            if (right.Is(top))
                            {
                                return new[] { top };
                            }

                            return new[] { top, right };
                        }

                        return new[] { top, right, bottom };
                    }
                    
                    return new[] { top, right, bottom, left };
                }
            }

            public String CssText
            {
                get {  return String.Join(" ", Values); }
            }

            public CssValue Original
            {
                get { return _original; }
            }

            public CssValue ExtractFor(String name)
            {
                if (name.Is(_labels[0]))
                {
                    return _top.Original;
                }
                else if (name.Is(_labels[1]))
                {
                    return _right.Original;
                }
                else if (name.Is(_labels[2]))
                {
                    return _bottom.Original;
                }
                else if (name.Is(_labels[3]))
                {
                    return _left.Original;
                }

                return null;
            }
        }
    }
}
