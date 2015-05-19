namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class GradientConverter<T> : IValueConverter<Tuple<T, GradientStop[]>>
    {
        readonly IValueConverter<T> _arguments;
        readonly T _default;

        public GradientConverter(IValueConverter<T> arguments, T defaultValue)
        {
            _default = defaultValue;
            _arguments = arguments;
        }

        public static GradientStop[] ToGradientStops(List<List<CssToken>> values, Int32 offset)
        {
            var stops = new GradientStop[values.Count - offset];

            for (int i = offset, k = 0; i < values.Count; i++, k++)
            {
                var location = Length.Missing;

                if (k == 0)
                    location = Length.Zero;
                else if (k == stops.Length - 1)
                    location = Length.Full;

                var stop = ToGradientStop(values[i], location);

                if (stop == null)
                    return null;

                stops[k] = stop.Value;
            }

            return stops;
        }

        public static GradientStop? ToGradientStop(List<CssToken> value, Length? location)
        {
            var color = Color.Transparent;
            var position = value.Skip(value.Count - 1).ToDistance();

            if (position != null)
            {
                value.RemoveAt(value.Count - 1);
                value.Trim();
            }
            else
            {
                position = location;
            }

            if (Converters.ColorConverter.TryConvert(value, m => color = m))
            {
                return new GradientStop(color, location.Value);
            }

            return null;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<Tuple<T, GradientStop[]>> setResult)
        {
            var args = value.ToList();
            var offset = 1;
            var core = default(T);

            if (_arguments.TryConvert(args[0], m => core = m) == false)
            {
                offset = 0;
                core = _default;
            }

            var stops = ToGradientStops(args, offset);

            if (stops != null)
            {
                setResult(Tuple.Create(core, stops));
                return true;
            }

            return false;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var args = value.ToList();
            var offset = 0;

            if (args.Count != 0 && _arguments.Validate(args[0]))
                offset = 1;

            return ToGradientStops(args, offset) != null;
        }
    }
}
