namespace AngleSharp.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;

    /// <summary>
    /// A bunch of useful extension methods.
    /// </summary>
    [DebuggerStepThrough]
    static class ValueExtensions
    {
        #region Transformers

        public static Boolean Is(this IEnumerable<CssToken> value, String expected)
        {
            var identifier = value.ToIdentifier();
            return identifier != null && identifier.Equals(expected, StringComparison.OrdinalIgnoreCase);
        }

        public static Boolean TryGetValue<T>(this Dictionary<String, T> obj, IEnumerable<CssToken> value, out T mode)
        {
            var identifier = value.ToIdentifier();
            mode = default(T);
            return identifier != null && obj.TryGetValue(identifier, out mode);
        }

        public static String ToUri(this IEnumerable<CssToken> value)
        {
            if (value.Count() == 1 && value.First().Type == CssTokenType.Url)
                return value.First().Data;

            return null;
        }

        public static Length? ToBorderSlice(this IEnumerable<CssToken> value)
        {
            var percent = value.ToPercent();

            if (percent.HasValue)
                return new Length(percent.Value.Value, Length.Unit.Percent);

            var number = value.ToSingle();

            if (number.HasValue)
                return new Length(number.Value, Length.Unit.Px);

            return null;
        }

        public static Length? ToLineHeight(this IEnumerable<CssToken> value)
        {
            var distance = value.ToDistance();

            if (distance != null)
                return distance;
            else if (value.Is(Keywords.Normal))
                return new Length(120f, Length.Unit.Percent);

            var val = value.ToSingle();

            if (val.HasValue)
                return new Length(val.Value * 100f, Length.Unit.Percent);

            return null;
        }

        public static Length? ToDistance(this IEnumerable<CssToken> value)
        {
            var percent = value.ToPercent();

            if (percent.HasValue)
                return new Length(percent.Value.Value, Length.Unit.Percent);

            return value.ToLength();
        }

        public static Length ToLength(this FontSize fontSize)
        {
            switch (fontSize)
            {
                case FontSize.Big://1.5em
                    return new Length(1.5f, Length.Unit.Em);
                case FontSize.Huge://2em
                    return new Length(2f, Length.Unit.Em);
                case FontSize.Large://1.2em
                    return new Length(1.2f, Length.Unit.Em);
                case FontSize.Larger://*120%
                    return new Length(120f, Length.Unit.Percent);
                case FontSize.Little://0.75em
                    return new Length(0.75f, Length.Unit.Em);
                case FontSize.Small://8/9em
                    return new Length(8f / 9f, Length.Unit.Em);
                case FontSize.Smaller://*80%
                    return new Length(80f, Length.Unit.Percent);
                case FontSize.Tiny://0.6em
                    return new Length(0.6f, Length.Unit.Em);
                default://1em
                    return new Length(1f, Length.Unit.Em);
            }
        }

        public static Percent? ToPercent(this IEnumerable<CssToken> value)
        {
            if (value.Count() == 1 && value.First().Type == CssTokenType.Percentage)
                return new Percent(((CssUnitToken)value.First()).Value);

            return null;
        }

        public static String ToCssString(this IEnumerable<CssToken> value)
        {
            if (value.Count() == 1 && value.First().Type == CssTokenType.String)
                return value.First().Data;

            return null;
        }

        public static String ToIdentifier(this IEnumerable<CssToken> value)
        {
            if (value.Count() == 1 && value.First().Type == CssTokenType.Ident)
                return value.First().Data.ToLowerInvariant();

            return null;
        }

        public static String ToAnimatableIdentifier(this IEnumerable<CssToken> value)
        {
            var identifier = value.ToIdentifier();

            if (identifier != null && (identifier.Equals(Keywords.All, StringComparison.OrdinalIgnoreCase) || Factory.Properties.IsAnimatable(identifier)))
                return identifier;

            return null;
        }

        public static Single? ToSingle(this IEnumerable<CssToken> value)
        {
            if (value.Count() == 1 && value.First().Type == CssTokenType.Number)
                return ((CssNumberToken)value.First()).Value;

            return null;
        }

        public static Int32? ToInteger(this IEnumerable<CssToken> value)
        {
            if (value.Count() == 1 && value.First().Type == CssTokenType.Number && ((CssNumberToken)value.First()).IsInteger)
                return (Int32)((CssNumberToken)value.First()).Value;

            return null;
        }

        public static Byte? ToByte(this IEnumerable<CssToken> value)
        {
            var val = value.ToInteger();

            if (val.HasValue)
                return (Byte)Math.Min(Math.Max(val.Value, 0), 255);

            return null;
        }

        public static Angle? ToAngle(this IEnumerable<CssToken> value)
        {
            if (value.Count() == 1 && value.First().Type == CssTokenType.Dimension)
            {
                var unit = (CssUnitToken)value.First();

                switch (unit.Unit)
                {
                    case "deg":
                        return new Angle(unit.Value, Angle.Unit.Deg);
                    case "grad":
                        return new Angle(unit.Value, Angle.Unit.Grad);
                    case "turn":
                        return new Angle(unit.Value, Angle.Unit.Turn);
                    case "rad":
                        return new Angle(unit.Value, Angle.Unit.Rad);
                }
            }

            return null;
        }

        public static Frequency? ToFrequency(this IEnumerable<CssToken> value)
        {
            if (value.Count() == 1 && value.First().Type == CssTokenType.Dimension)
            {
                var unit = (CssUnitToken)value.First();

                switch (unit.Unit)
                {
                    case "hz":
                        return new Frequency(unit.Value, Frequency.Unit.Hz);
                    case "khz":
                        return new Frequency(unit.Value, Frequency.Unit.Khz);
                }
            }

            return null;
        }

        public static Length? ToLength(this IEnumerable<CssToken> value)
        {
            if (value.Count() == 1)
            {
                var val = value.First();

                if (val.Type == CssTokenType.Dimension)
                {
                    var unit = (CssUnitToken)val;

                    switch (unit.Unit)
                    {
                        case "ch":
                            return new Length(unit.Value, Length.Unit.Ch);
                        case "cm":
                            return new Length(unit.Value, Length.Unit.Cm);
                        case "em":
                            return new Length(unit.Value, Length.Unit.Em);
                        case "ex":
                            return new Length(unit.Value, Length.Unit.Ex);
                        case "in":
                            return new Length(unit.Value, Length.Unit.In);
                        case "mm":
                            return new Length(unit.Value, Length.Unit.Mm);
                        case "pc":
                            return new Length(unit.Value, Length.Unit.Pc);
                        case "pt":
                            return new Length(unit.Value, Length.Unit.Pt);
                        case "px":
                            return new Length(unit.Value, Length.Unit.Px);
                        case "rem":
                            return new Length(unit.Value, Length.Unit.Rem);
                        case "vh":
                            return new Length(unit.Value, Length.Unit.Vh);
                        case "vmax":
                            return new Length(unit.Value, Length.Unit.Vmax);
                        case "vmin":
                            return new Length(unit.Value, Length.Unit.Vmin);
                        case "vw":
                            return new Length(unit.Value, Length.Unit.Vw);
                    }
                }
                else if (val.Type == CssTokenType.Number && ((CssNumberToken)val).Value == 0f)
                {
                    return Length.Zero;
                }
            }

            return null;
        }

        public static Resolution? ToResolution(this IEnumerable<CssToken> value)
        {
            if (value.Count() == 1 && value.First().Type == CssTokenType.Dimension)
            {
                var unit = (CssUnitToken)value.First();

                switch (unit.Unit)
                {
                    case "dpcm":
                        return new Resolution(unit.Value, Resolution.Unit.Dpcm);
                    case "dpi":
                        return new Resolution(unit.Value, Resolution.Unit.Dpi);
                    case "dppx":
                        return new Resolution(unit.Value, Resolution.Unit.Dppx);
                }
            }

            return null;
        }

        public static Time? ToTime(this IEnumerable<CssToken> value)
        {
            if (value.Count() == 1 && value.First().Type == CssTokenType.Dimension)
            {
                var unit = (CssUnitToken)value.First();

                switch (unit.Unit)
                {
                    case "s":
                        return new Time(unit.Value, Time.Unit.Ms);
                    case "ms":
                        return new Time(unit.Value, Time.Unit.Ms);
                }
            }

            return null;
        }

        public static Length? ToImageBorderWidth(this IEnumerable<CssToken> value)
        {
            if (value.Is(Keywords.Auto))
                return new Length(100f, Length.Unit.Percent);

            var multiple = value.ToSingle();

            if (multiple.HasValue)
                return new Length(multiple.Value * 100f, Length.Unit.Percent);

            return value.ToDistance();
        }

        public static Length? ToBorderWidth(this IEnumerable<CssToken> value)
        {
            var length = value.ToLength();

            if (length != null)
                return length;
            else if (value.Is(Keywords.Thin))
                return Length.Thin;
            else if (value.Is(Keywords.Medium))
                return Length.Medium;
            else if (value.Is(Keywords.Thick))
                return Length.Thick;

            return length;
        }

        public static Color? ToColor(this IEnumerable<CssToken> value)
        {
            var val = value as CssValue;

            if (val != null && val.Count > 0)
            {
                var colorName = value.ToIdentifier();

                if (colorName != null)
                    return Color.FromName(colorName);

                var colorCode = val.CssText;
                var color = Color.Black;

                if (colorCode[0] == Symbols.Num && Color.TryFromHex(colorCode.Substring(1), out color))
                    return color;
            }

            return null;
        }

        #endregion

        #region Value Calculation

        public static Length Add(this Length a, Length b)
        {
            //TODO return new Compute(a, b, '+');
            return Length.Zero;
        }

        #endregion
    }
}
