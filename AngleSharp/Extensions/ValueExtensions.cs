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

        public static CssToken OnlyOrDefault(this IEnumerable<CssToken> value)
        {
            var result = default(CssToken);

            foreach (var item in value)
            {
                if (result == null)
                {
                    result = item;
                    continue;
                }

                result = default(CssToken);
                break;
            }

            return result;
        }

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
            var element = value.OnlyOrDefault();

            if (element != null && element.Type == CssTokenType.Url)
                return element.Data;

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
            var element = value.OnlyOrDefault();

            if (element != null && element.Type == CssTokenType.Percentage)
                return new Percent(((CssUnitToken)element).Value);

            return null;
        }

        public static String ToCssString(this IEnumerable<CssToken> value)
        {
            var element = value.OnlyOrDefault();

            if (element != null && element.Type == CssTokenType.String)
                return element.Data;

            return null;
        }

        public static String ToIdentifier(this IEnumerable<CssToken> value)
        {
            var element = value.OnlyOrDefault();

            if (element != null && element.Type == CssTokenType.Ident)
                return element.Data.ToLowerInvariant();

            return null;
        }

        public static String ToAnimatableIdentifier(this IEnumerable<CssToken> value)
        {
            var identifier = value.ToIdentifier();

            if (identifier != null && 
                (identifier.Equals(Keywords.All, StringComparison.OrdinalIgnoreCase) || 
                 Factory.Properties.IsAnimatable(identifier)))
                return identifier;

            return null;
        }

        public static Single? ToSingle(this IEnumerable<CssToken> value)
        {
            var element = value.OnlyOrDefault();

            if (element != null && element.Type == CssTokenType.Number)
                return ((CssNumberToken)element).Value;

            return null;
        }

        public static Int32? ToInteger(this IEnumerable<CssToken> value)
        {
            var element = value.OnlyOrDefault();

            if (element != null && element.Type == CssTokenType.Number && ((CssNumberToken)element).IsInteger)
                return ((CssNumberToken)element).IntegerValue;

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
            var element = value.OnlyOrDefault();

            if (element != null && element.Type == CssTokenType.Dimension)
            {
                var unit = (CssUnitToken)element;

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
            var element = value.OnlyOrDefault();

            if (element != null && element.Type == CssTokenType.Dimension)
            {
                var unit = (CssUnitToken)element;

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
            var element = value.OnlyOrDefault();

            if (element != null)
            {
                if (element.Type == CssTokenType.Dimension)
                {
                    var unit = (CssUnitToken)element;

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
                else if (element.Type == CssTokenType.Number && ((CssNumberToken)element).Value == 0f)
                {
                    return Length.Zero;
                }
            }

            return null;
        }

        public static Resolution? ToResolution(this IEnumerable<CssToken> value)
        {
            var element = value.OnlyOrDefault();

            if (element != null && element.Type == CssTokenType.Dimension)
            {
                var unit = (CssUnitToken)element;

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
            var element = value.OnlyOrDefault();

            if (element != null && element.Type == CssTokenType.Dimension)
            {
                var unit = (CssUnitToken)element;

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

        public static List<List<CssToken>> ToItems(this IEnumerable<CssToken> value)
        {
            var list = new List<List<CssToken>>();
            var current = new List<CssToken>();
            var nested = 0;
            list.Add(current);

            foreach (var token in value)
            {
                var whitespace = token.Type == CssTokenType.Whitespace;
                var newitem = token.Type == CssTokenType.String || token.Type == CssTokenType.Url || token.Type == CssTokenType.Function;

                if (nested == 0 && (whitespace || newitem))
                {
                    if (current.Count != 0)
                    {
                        current = new List<CssToken>();
                        list.Add(current);
                    }

                    if (whitespace)
                        continue;
                }
                else if (token.Type == CssTokenType.RoundBracketOpen)
                {
                    nested++;
                }
                else if (token.Type == CssTokenType.RoundBracketClose)
                {
                    nested--;
                }

                current.Add(token);
            }

            return list;
        }

        public static void Trim(this List<CssToken> value)
        {
            var begin = 0;
            var end = value.Count - 1;

            while (begin < end)
            {
                if (value[begin].Type == CssTokenType.Whitespace)
                    begin++;
                else if (value[end].Type == CssTokenType.Whitespace)
                    end--;
                else
                    break;
            }

            value.RemoveRange(++end, value.Count - end);
            value.RemoveRange(0, begin);
        }

        public static List<List<CssToken>> ToArgs(this IEnumerable<CssToken> value, Int32 size)
        {
            var args = value.ToList();

            if (args.Count > size)
                return null;

            while (args.Count < size)
                args.Add(new List<CssToken>());

            return args;
        }

        public static List<List<CssToken>> ToList(this IEnumerable<CssToken> value)
        {
            var list = new List<List<CssToken>>();
            var current = new List<CssToken>();
            var nested = 0;
            list.Add(current);

            foreach (var token in value)
            {
                if (nested == 0 && token.Type == CssTokenType.Comma)
                {
                    current = new List<CssToken>();
                    list.Add(current);
                    continue;
                }
                else if (token.Type == CssTokenType.RoundBracketOpen)
                {
                    nested++;
                }
                else if (token.Type == CssTokenType.RoundBracketClose)
                {
                    nested--;
                }
                else if (token.Type == CssTokenType.Whitespace && current.Count == 0)
                {
                    continue;
                }

                current.Add(token);
            }

            for (var i = 0; i < list.Count; i++)
            {
                list[i].Trim();
            }

            return list;
        }

        public static String ToText(this IEnumerable<CssToken> value)
        {
            return String.Join(String.Empty, value.Select(m => m.ToValue()));
        }

        public static Color? ToColor(this IEnumerable<CssToken> value)
        {
            var colorName = value.ToIdentifier();

            if (colorName != null)
            {
                return Color.FromName(colorName); 
            }

            var colorCode = value.ToText();
            var color = Color.Black;

            if (String.IsNullOrEmpty(colorCode) == false && colorCode[0] == Symbols.Num &&
                (colorCode.Length == 4 || colorCode.Length == 7) &&
                Color.TryFromHex(colorCode.Substring(1), out color))
            {
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
