namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;

    sealed class ConstraintValueConverter : IValueConverter
    {
        readonly IValueConverter _converter;
        readonly String _label;

        public ConstraintValueConverter(IValueConverter converter, String label)
        {
            _converter = converter;
            _label = label;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var result = _converter.Convert(value);
            return result != null ? new TransformationValueConverter(result, _label) : null;
        }

        sealed class TransformationValueConverter : IPropertyValue
        {
            readonly IPropertyValue _value;
            readonly String _label;

            public TransformationValueConverter(IPropertyValue value, String label)
            {
                _value = value;
                _label = label;
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
                return _label == name ? _value.ExtractFor(name) : null;
            }
        }
    }
}
