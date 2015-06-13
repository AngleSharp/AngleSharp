namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

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

            if (list.Count == 0)
                return null;

            while (list.Count != 0)
            {
                var option = _converter.VaryStart(list);

                if (option == null)
                    return null;

                options.Add(option);
            }

            return new OptionsValue(options.ToArray());
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
                get { return String.Join(" ", _options.Where(m => !String.IsNullOrEmpty(m.CssText)).Select(m => m.CssText)); }
            }
        }
    }
}
