namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class PeriodicValueConverter : IValueConverter
    {
        readonly IValueConverter _converter;

        public PeriodicValueConverter(IValueConverter converter)
        {
            _converter = converter;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var list = new List<CssToken>(value);
            var options = new IPropertyValue[4];

            for (int i = 0; i < options.Length && list.Count != 0; i++)
            {
                options[i] = _converter.VaryStart(list);

                if (options[i] == null)
                    return null;
            }

            return list.Count == 0 ? new PeriodicValue(options, value) : null;
        }

        public IPropertyValue Construct(CssProperty[] properties)
        {
            if (properties.Length == 4)
            {
                var options = new IPropertyValue[4];
                options[0] = _converter.Construct(properties.Where(m => m.Name.Contains("top")).Take(1).ToArray());
                options[1] = _converter.Construct(properties.Where(m => m.Name.Contains("right")).Take(1).ToArray());
                options[2] = _converter.Construct(properties.Where(m => m.Name.Contains("bottom")).Take(1).ToArray());
                options[3] = _converter.Construct(properties.Where(m => m.Name.Contains("left")).Take(1).ToArray());
                return options[0] != null && options[1] != null && options[2] != null && options[3] != null ? 
                    new PeriodicValue(options, Enumerable.Empty<CssToken>()) : null;
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

            public PeriodicValue(IPropertyValue[] options, IEnumerable<CssToken> tokens)
            {
                _top = options[0];
                _right = options[1] ?? _top;
                _bottom = options[2] ?? _top;
                _left = options[3] ?? _right;
                _original = new CssValue(tokens);
            }

            public String[] Values
            {
                get
                {
                    var top = _top.CssText;
                    var right = _right.CssText;
                    var bottom = _bottom.CssText;
                    var left = _left.CssText;

                    if (right == left)
                    {
                        if (top == bottom)
                        {
                            if (right == top)
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
                if (name.Contains("top"))
                    return _top.Original;
                else if (name.Contains("left"))
                    return _left.Original;
                else if (name.Contains("right"))
                    return _right.Original;
                else if (name.Contains("bottom"))
                    return _bottom.Original;

                return null;
            }
        }
    }
}
