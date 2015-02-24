namespace AngleSharp.Css.ValueConverters
{
    using AngleSharp.Css.Values;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class GradientConverter<T> : IValueConverter<Tuple<T, GradientStop[]>>
    {
        readonly IValueConverter<T> _arguments;
        readonly T _default;

        public GradientConverter(IValueConverter<T> arguments, T defaultValue)
        {
            _default = defaultValue;
            _arguments = arguments;
        }

        public static GradientStop[] ToGradientStops(CssValueList values, Int32 offset)
        {
            var stops = new GradientStop[values.Length - offset];

            if (stops.Length < 2)
                return null;

            for (int i = offset, k = 0; i < values.Length; i++, k++)
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

        public static GradientStop? ToGradientStop(ICssValue value, Length? location)
        {
            var values = value as CssValueList;
            var color = Color.Transparent;

            if (values != null)
            {
                if (values.Length != 2)
                    return null;

                location = values[1].ToDistance();
                value = values[0];
            }

            if (!Converters.ColorConverter.TryConvert(value, m => color = m) || location == null)
                return null;

            return new GradientStop(color, location.Value);
        }

        public Boolean TryConvert(ICssValue value, Action<Tuple<T, GradientStop[]>> setResult)
        {
            var values = value as CssValueList;

            if (values == null || values.Length < MinArgs)
                return false;

            var offset = 0;
            var core = default(T);

            if (_arguments.TryConvert(values[0], m => core = m))
                offset = 1;
            else
                core = _default;

            var stops = ToGradientStops(values, offset);

            if (stops == null)
                return false;

            setResult(Tuple.Create(core, stops));
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            var values = value as CssValueList;

            if (values == null || values.Length < MinArgs)
                return false;

            var offset = _arguments.Validate(values[0]) ? 1 : 0;
            return ToGradientStops(values, offset) != null;
        }

        public Int32 MinArgs
        {
            get { return 2; }
        }

        public Int32 MaxArgs
        {
            get { return UInt16.MaxValue; }
        }
    }
}
