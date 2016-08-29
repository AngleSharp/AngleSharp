namespace AngleSharp.Css.ValueConverters
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static Converters;

    sealed class BorderRadiusConverter : IValueConverter
    {
        readonly IValueConverter _converter = LengthOrPercentConverter.Periodic(
            PropertyNames.BorderTopLeftRadius, PropertyNames.BorderTopRightRadius, PropertyNames.BorderBottomRightRadius, PropertyNames.BorderBottomLeftRadius);

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var front = new List<CssToken>();
            var back = new List<CssToken>();
            var current = front;

            foreach (var token in value)
            {
                if (token.Type == CssTokenType.Delim && token.Data.Is("/"))
                {
                    if (current == back)
                    {
                        return null;
                    }

                    current = back;
                }
                else
                {
                    current.Add(token);
                }
            }

            var horizontal = _converter.Convert(front);

            if (horizontal == null)
            {
                return null;
            }

            var vertical = current == back ? _converter.Convert(back) : horizontal;

            if (vertical == null)
            {
                return null;
            }

            return new BorderRadiusValue(horizontal, vertical, new CssValue(value));
        }

        public IPropertyValue Construct(CssProperty[] properties)
        {
            if (properties.Length == 4)
            {
                var front = new List<CssToken>();
                var back = new List<CssToken>();
                var props = new List<CssProperty>
                                {
                                    properties.First(m => m.Name.Is(PropertyNames.BorderTopLeftRadius)),
                                    properties.First(m => m.Name.Is(PropertyNames.BorderTopRightRadius)),
                                    properties.First(
                                        m => m.Name.Is(PropertyNames.BorderBottomRightRadius)),
                                    properties.First(
                                        m => m.Name.Is(PropertyNames.BorderBottomLeftRadius))
                                };

                for (var i = 0; i < props.Count; i++)
                {
                    var dpv = props[i].DeclaredValue as IEnumerable<IPropertyValue>;

                    if (dpv == null)
                    {
                        return null;
                    }

                    var first = dpv.First().Original;
                    var second = dpv.Last().Original;

                    if (i != 0)
                    {
                        front.Add(CssToken.Whitespace);
                        back.Add(CssToken.Whitespace);
                    }

                    front.AddRange(first);
                    back.AddRange(second);
                }

                var h = _converter.Convert(front);
                var v = _converter.Convert(back);
                var o = front.Concat(new CssToken(CssTokenType.Delim, "/", TextPosition.Empty)).Concat(back);

                return new BorderRadiusValue(h, v, new CssValue(o));
            }

            return null;
        }

        sealed class BorderRadiusValue : IPropertyValue
        {
            readonly IPropertyValue _horizontal;
            readonly IPropertyValue _vertical;
            readonly CssValue _original;

            public BorderRadiusValue(IPropertyValue horizontal, IPropertyValue vertical, CssValue original)
            {
                _horizontal = horizontal;
                _vertical = vertical;
                _original = original;
            }

            public String CssText
            {
                get
                {
                    var horizontal = _horizontal.CssText;

                    if (_vertical != null)
                    {
                        var vertical = _vertical.CssText;

                        if (horizontal != vertical)
                        {
                            return String.Concat(horizontal, " / ", vertical);
                        }
                    }

                    return horizontal;
                }
            }

            public CssValue Original
            {
                get { return _original; }
            }

            public CssValue ExtractFor(String name)
            {
                var h = _horizontal.ExtractFor(name);
                var v = _vertical.ExtractFor(name);
                return new CssValue(h.Concat(CssToken.Whitespace).Concat(v));
            }
        }
    }
}
