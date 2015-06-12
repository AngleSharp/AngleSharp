namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    abstract class GradientConverter : IValueConverter
    {
        readonly IValueConverter _arguments;

        public GradientConverter(IValueConverter arguments)
        {
            _arguments = arguments;
        }

        public static IPropertyValue[] ToGradientStops(List<List<CssToken>> values, Int32 offset)
        {
            var stops = new IPropertyValue[values.Count - offset];

            for (int i = offset, k = 0; i < values.Count; i++, k++)
            {
                stops[k] = ToGradientStop(values[i]);

                if (stops[k] == null)
                    return null;
            }

            return stops;
        }

        public static IPropertyValue ToGradientStop(List<CssToken> value)
        {
            var color = default(IPropertyValue);
            var position = default(IPropertyValue);
            var items = value.ToItems();

            if (items.Count != 0)
            {
                position = Converters.LengthOrPercentConverter.Convert(items[items.Count - 1]);

                if (position != null)
                    items.RemoveAt(items.Count - 1);
            }

            if (items.Count != 0)
            {
                color = Converters.ColorConverter.Convert(items[items.Count - 1]);

                if (color != null)
                    items.RemoveAt(items.Count - 1);
            }

            return items.Count == 0 ? new StopValue(color, position) : null;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var args = value.ToList();
            var initial = args.Count != 0 ? _arguments.Convert(args[0]) : null;
            var offset = initial != null ? 1 : 0;
            var stops = ToGradientStops(args, offset);
            return stops != null ? new GradientValue(initial, stops) : null;
        }

        sealed class GradientValue : IPropertyValue
        {
            readonly IPropertyValue _initial;
            readonly IPropertyValue[] _stops;

            public GradientValue(IPropertyValue initial, IPropertyValue[] stops)
            {
                _initial = initial;
                _stops = stops;
            }

            public String CssText
            {
                get { throw new NotImplementedException(); }
            }
        }

        sealed class StopValue : IPropertyValue
        {
            readonly IPropertyValue _color;
            readonly IPropertyValue _position;

            public StopValue(IPropertyValue color, IPropertyValue position)
            {
                _color = color;
                _position = position;
            }

            public String CssText
            {
                get { throw new NotImplementedException(); }
            }
        }
    }
}
