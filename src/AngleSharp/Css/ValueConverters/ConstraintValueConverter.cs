namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;

    sealed class ConstraintValueConverter : IValueConverter
    {
        readonly IValueConverter _converter;
        readonly String[] _labels;

        public ConstraintValueConverter(IValueConverter converter, String[] labels)
        {
            _converter = converter;
            _labels = labels;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var result = _converter.Convert(value);
            return result != null ? new TransformationValueConverter(result, _labels) : null;
        }

        public IPropertyValue Construct(CssProperty[] properties)
        {
            var filtered = properties.Where(m => _labels.Contains(m.Name));
            var existing = default(String);

            foreach (var filter in filtered)
            {
                var value = filter.Value;

                if (existing != null && value != existing)
                {
                    return null;
                }
                
                existing = value;
            }

            var result = _converter.Construct(filtered.Take(1).ToArray());
            return result != null ? new TransformationValueConverter(result, _labels) : null;
        }

        sealed class TransformationValueConverter : IPropertyValue
        {
            readonly IPropertyValue _value;
            readonly String[] _labels;

            public TransformationValueConverter(IPropertyValue value, String[] labels)
            {
                _value = value;
                _labels = labels;
            }

            public String CssText
            {
                get { return _value.CssText; }
            }

            public CssValue Original
            {
                get { return _value.Original; }
            }

            public CssValue ExtractFor(String name)
            {
                return _labels.Contains(name) ? _value.ExtractFor(name) : null;
            }
        }
    }
}
