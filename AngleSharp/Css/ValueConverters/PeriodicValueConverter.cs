namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
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

            return list.Count == 0 ? new PeriodicValue(options[0], options[1], options[2], options[3]) : null;
        }

        sealed class PeriodicValue : IPropertyValue
        {
            readonly IPropertyValue _top;
            readonly IPropertyValue _right; 
            readonly IPropertyValue _bottom;
            readonly IPropertyValue _left;

            public PeriodicValue(IPropertyValue top, IPropertyValue right, IPropertyValue bottom, IPropertyValue left)
            {
                _top = top;
                _right = right ?? _top;
                _bottom = bottom ?? _top;
                _left = left ?? _right;
            }

            public String CssText
            {
                get { return String.Join(" ", new []{ _top.CssText, _right.CssText, _bottom.CssText, _left.CssText }); }
            }
        }
    }
}
