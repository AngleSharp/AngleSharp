namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class ArgumentsValueConverter : IValueConverter
    {
        readonly IValueConverter[] _converters;

        public ArgumentsValueConverter(params IValueConverter[] converters)
        {
            _converters = converters;
        }
        
        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var items = value.ToList();
            var n = _converters.Length;

            if (items.Count <= n)
            {
                var args = new IPropertyValue[n];

                for (int i = 0; i < n; i++)
                {
                    var item = i < items.Count ? items[i] : Enumerable.Empty<CssToken>();
                    args[i] = _converters[i].Convert(item);

                    if (args[i] == null)
                        return null;
                }

                return new ArgumentsValue(args);
            }

            return null;
        }

        sealed class ArgumentsValue : IPropertyValue
        {
            readonly IPropertyValue[] _arguments;

            public ArgumentsValue(IPropertyValue[] arguments)
            {
                _arguments = arguments;
            }

            public String CssText
            {
                get { return String.Join(", ", _arguments.Where(m => !String.IsNullOrEmpty(m.CssText)).Select(m => m.CssText)); }
            }
        }
    }
}
