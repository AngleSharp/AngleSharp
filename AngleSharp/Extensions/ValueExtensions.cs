namespace AngleSharp.Extensions
{
    using AngleSharp.Css;
    using AngleSharp.DOM.Css;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// A bunch of useful extension methods.
    /// </summary>
    [DebuggerStepThrough]
    static class ValueExtensions
    {
        #region Transformers

        public static Boolean Is(this ICssValue value, String identifier)
        {
            var primitive = value as CssIdentifier;
            return primitive != null && ((String)primitive).Equals(identifier, StringComparison.OrdinalIgnoreCase);
        }

        public static Boolean TryGetValue<T>(this Dictionary<String, T> obj, ICssValue value, out T mode)
        {
            var primitive = value as CssIdentifier;
            mode = default(T);
            return primitive != null && obj.TryGetValue(primitive, out mode);
        }

        public static String GetIdentifier<T>(this Dictionary<String, T> obj, T value)
        {
            foreach (var pair in obj)
            {
                if (pair.Value.Equals(value))
                    return pair.Key;
            }

            return null;
        }

        public static CssValueList CopyToList(this ICssValue value)
        {
            var original = value as CssValueList;

            if (original == null)
                return new CssValueList(value);

            var newList = new CssValueList();

            foreach (var item in original)
                newList.Add(item);

            return newList;
        }

        public static CssUrl ToUri(this ICssValue value)
        {
            return value as CssUrl;
        }

        public static IEnumerable<ICssValue> AsEnumeration(this ICssValue value)
        {
            var list = value as CssValueList;

            if (list != null)
                return list;

            return new ICssValue[1] { value };
        }

        public static ICssValue Reduce(this CssValueList list)
        {
            if (list.Length == 0)
                return null;
            else if (list.Length == 1)
                return list[0];

            return list;
        }

        public static IDistance ToBorderSlice(this ICssValue value)
        {
            var percent = value.ToPercent();

            if (percent.HasValue)
                return percent.Value;

            var number = value.ToSingle();

            if (number.HasValue)
                return new Length(number.Value, Length.Unit.Px);

            return null;
        }

        public static IDistance ToLineHeight(this ICssValue value)
        {
            var distance = value.ToDistance();

            if (distance != null)
                return distance;
            else if (value.Is(Keywords.Normal))
                return new Percent(120f);

            var val = value.ToSingle();

            if (val.HasValue)
                return new Percent(val.Value * 100f);

            return null;
        }

        public static IDistance ToDistance(this ICssValue value)
        {
            var primitive = value as IDistance;

            if (primitive == null)
            {
                var number = value as Number?;

                if (number.HasValue && number.Value == Number.Zero)
                    return Length.Zero;
            }

            return primitive;
        }

        public static IDistance ToDistance(this FontSize fontSize)
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
                    return new Percent(120f);
                case FontSize.Little://0.75em
                    return new Length(0.75f, Length.Unit.Em);
                case FontSize.Small://8/9em
                    return new Length(8f / 9f, Length.Unit.Em);
                case FontSize.Smaller://*80%
                    return new Percent(80f);
                case FontSize.Tiny://0.6em
                    return new Length(0.6f, Length.Unit.Em);
                default://1em
                    return new Length(1f, Length.Unit.Em);
            }
        }

        public static Percent? ToPercent(this ICssValue value)
        {
            return value as Percent?;
        }

        public static String ToCssString(this ICssValue value)
        {
            var primitive = value as CssString;

            if (primitive != null)
                return (String)primitive;

            return null;
        }

        public static String ToIdentifier(this ICssValue value)
        {
            var primitive = value as CssIdentifier;

            if (primitive != null)
                return (String)primitive;

            return null;
        }

        public static String ToAnimatableIdentifier(this ICssValue value)
        {
            var identifier = value.ToIdentifier();

            if (identifier != null && (identifier.Equals(Keywords.All, StringComparison.OrdinalIgnoreCase) || CssPropertyFactory.IsAnimatable(identifier)))
                return identifier;

            return null;
        }

        public static Single? ToSingle(this ICssValue value)
        {
            var primitive = value as Number?;

            if (primitive != null)
                return primitive.Value.Value;

            return null;
        }

        public static Int32? ToInteger(this ICssValue value)
        {
            var val = value.ToSingle();

            if (val.HasValue)
                return (Int32)val.Value;

            return null;
        }

        public static Byte? ToByte(this ICssValue value)
        {
            var val = value.ToInteger();

            if (val.HasValue)
                return (Byte)Math.Min(Math.Max(val.Value, 0), 255);

            return null;
        }

        public static Angle? ToAngle(this ICssValue value)
        {
            return value as Angle?;
        }

        public static String ToFontFamily(this ICssValue value)
        {
            var values = value as CssValueList;

            if (values == null)
            {
                var ident = value as CssIdentifier;

                if (ident != null)
                {
                    String family;

                    if (Map.DefaultFontFamilies.TryGetValue(ident, out family))
                        return family;

                    return ident;
                }

                var str = value as CssString;

                if (str != null)
                    return str;
            }
            else
            {
                var names = new String[values.Length];

                for (var i = 0; i < names.Length; i++)
                {
                    var ident = values[i] as CssIdentifier;

                    if (ident == null)
                        return null;

                    names[i] = ident;
                }

                return String.Join(" ", names);
            }

            return null;
        }

        public static Frequency? ToFrequency(this ICssValue value)
        {
            return value as Frequency?;
        }

        public static Length? ToLength(this ICssValue value)
        {
            var primitive = value as Length?;

            if (primitive == null)
            {
                var number = value as Number?;

                if (number.HasValue && number.Value == Number.Zero)
                    return Length.Zero;
            }

            return primitive;
        }

        public static Resolution? ToResolution(this ICssValue value)
        {
            return value as Resolution?;
        }

        public static Time? ToTime(this ICssValue value)
        {
            return value as Time?;
        }

        public static GradientStop[] ToGradientStops(this ICssValue value)
        {
            var values = value as CssValueList;

            if (value == null || values.Length < 2)
                return null;

            var stops = new GradientStop[values.Length];

            var perStop = 100f / (values.Length - 1);

            for (int i = 0, k = 0; i < values.Length; i++, k++)
            {
                var stop = values[i].ToGradientStop(perStop * k);

                if (stop == null)
                    return null;

                stops[k] = stop.Value;
            }

            return stops;
        }

        public static GradientStop? ToGradientStop(this ICssValue value, Single defaultStop = 0f)
        {
            var list = value as CssValueList;
            Color? color = null;
            IDistance location = new Percent(defaultStop);

            if (list != null)
            {
                if (list.Length != 2)
                    return null;

                color = list[0].ToColor();
                location = list[1].ToDistance();
            }
            else
                color = value.ToColor();

            if (color == null || location == null)
                return null;

            return new GradientStop(color.Value, location);
        }

        public static Single? ToAspectRatio(this ICssValue value)
        {
            var values = value as CssValueList;

            if (values != null && values.Length == 3 && values[1] == CssValue.Delimiter)
            {
                var w = values[0].ToInteger();
                var h = values[2].ToInteger();

                if (w.HasValue && h.HasValue && w.Value > 0 && h.Value > 0)
                    return (Single)w.Value / (Single)h.Value;
            }

            return null;
        }

        public static IDistance ToImageBorderWidth(this ICssValue value)
        {
            if (value.Is(Keywords.Auto))
                return Percent.Hundred;

            var multiple = value.ToSingle();

            if (multiple.HasValue)
                return new Percent(multiple.Value * 100f);

            return value.ToDistance();
        }

        public static Length? ToBorderWidth(this ICssValue value)
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

        public static Color? ToColor(this ICssValue value)
        {
            var primitive = value as Color?;

            if (primitive == null)
            {
                var colorName = value.ToIdentifier();

                if (colorName != null)
                    return Color.FromName(colorName);
            }

            return primitive;
        }

        public static CssValueList Subset(this CssValueList values, Int32 start = 0, Int32 end = -1)
        {
            if (end == -1)
                end = values.Length;

            var list = new List<ICssValue>();

            for (var i = start; i < end; i++)
                list.Add(values[i]);

            return new CssValueList(list);
        }

        public static List<CssValueList> ToList(this CssValueList values)
        {
            var list = new List<CssValueList>();

            for (int i = 0; i < values.Length; i++)
            {
                var entry = new CssValueList();

                for (int j = i; j < values.Length; j++)
                {
                    if (values[j] == CssValue.Separator)
                        break;

                    entry.Add(values[j]);
                    i++;
                }

                list.Add(entry);
            }

            return list;
        }

        public static Point ToPoint(this ICssValue value)
        {
            var values = value as CssValueList;

            if (values == null || values.Length == 1)
            {
                var calc = value.ToDistance();

                if (calc != null)
                    return new Point(calc);
                else if (value.Is(Keywords.Left))
                    return new Point(x: Percent.Zero);
                else if (value.Is(Keywords.Right))
                    return new Point(x: Percent.Hundred);
                else if (value.Is(Keywords.Top))
                    return new Point(y: Percent.Zero);
                else if (value.Is(Keywords.Bottom))
                    return new Point(y: Percent.Hundred);
                else if (value.Is(Keywords.Center))
                    return Point.Centered;
            }
            else if (values.Length == 2)
            {
                var left = values[0];
                var right = values[1];
                var horizontal = left.ToDistance();
                var vertical = right.ToDistance();

                if (horizontal == null)
                {
                    if (left.Is(Keywords.Left))
                        horizontal = Percent.Zero;
                    else if (left.Is(Keywords.Right))
                        horizontal = Percent.Hundred;
                    else if (left.Is(Keywords.Center))
                        horizontal = Percent.Fifty;
                    else if (left.Is(Keywords.Top))
                    {
                        horizontal = vertical;
                        vertical = Percent.Zero;
                    }
                    else if (left.Is(Keywords.Bottom))
                    {
                        horizontal = vertical;
                        vertical = Percent.Hundred;
                    }
                }

                if (vertical == null)
                {
                    if (right.Is(Keywords.Top))
                        vertical = Percent.Zero;
                    else if (right.Is(Keywords.Bottom))
                        vertical = Percent.Hundred;
                    else if (right.Is(Keywords.Center))
                        vertical = Percent.Fifty;
                    else if (right.Is(Keywords.Left))
                    {
                        vertical = horizontal;
                        horizontal = Percent.Zero;
                    }
                    else if (right.Is(Keywords.Right))
                    {
                        vertical = horizontal;
                        horizontal = Percent.Hundred;
                    }
                }

                if (horizontal != null && vertical != null)
                    return new Point(horizontal, vertical);
            }
            else if (values.Length > 2)
            {
                IDistance shift = null;
                IDistance horizontal = Percent.Fifty;
                IDistance vertical = Percent.Fifty;
                var index = 0;
                value = values[index];

                if (value.Is(Keywords.Left))
                {
                    horizontal = Percent.Zero;
                    shift = values[index + 1].ToDistance();
                }
                else if (value.Is(Keywords.Right))
                {
                    horizontal = Percent.Hundred;
                    shift = values[index + 1].ToDistance();
                }
                else if (!value.Is(Keywords.Center))
                    return null;

                if (shift != null)
                {
                    index++;
                    horizontal = horizontal.Add(shift);
                    shift = Percent.Zero;
                }

                value = values[++index];

                if (value.Is(Keywords.Top))
                {
                    vertical = Percent.Zero;

                    if (index + 1 < values.Length)
                        shift = values[index + 1].ToDistance();
                }
                else if (value.Is(Keywords.Bottom))
                {
                    vertical = Percent.Hundred;

                    if (index + 1 < values.Length)
                        shift = values[index + 1].ToDistance();
                }
                else if (!value.Is(Keywords.Center))
                    return null;

                if (shift != null)
                {
                    vertical = vertical.Add(shift);
                    return new Point(horizontal, vertical);
                }
            }

            return null;
        }

        public static Shadow ToShadow(this ICssValue value)
        {
            var item = value as CssValueList;

            if (item == null || item.Length < 2)
                return null;

            var inset = item[0].Is(Keywords.Inset);
            var offset = inset ? 1 : 0;

            if (inset && item.Length < 3)
                return null;

            var offsetX = item[offset++].ToLength();
            var offsetY = item[offset++].ToLength();

            if (!offsetX.HasValue || !offsetY.HasValue)
                return null;

            var blurRadius = Length.Zero;
            var spreadRadius = Length.Zero;
            var color = Color.Black;

            if (item.Length > offset)
            {
                var blur = item[offset].ToLength();

                if (blur.HasValue)
                {
                    blurRadius = blur.Value;
                    offset++;
                }
            }

            if (item.Length > offset)
            {
                var spread = item[offset].ToLength();

                if (spread.HasValue)
                {
                    spreadRadius = spread.Value;
                    offset++;
                }
            }

            if (item.Length > offset)
            {
                CSSProperty.WithColor().TryConvert(item[offset], v => 
                {
                    color = v;
                    offset++;
                });
            }

            if (item.Length > offset)
                return null;

            return new Shadow(inset, offsetX.Value, offsetY.Value, blurRadius, spreadRadius, color);
        }

        public static CssValueList ToSeparatedList(this List<CssValueList> list)
        {
            var values = new CssValueList();

            if (list.Count > 0)
                values.Add(list[0].Reduce());

            for (int i = 1; i < list.Count; i++)
            {
                values.Add(CssValue.Separator);
                values.Add(list[i].Reduce());
            }

            return values;
        }

        #endregion

        #region Value Calculation

        public static IDistance Add(this IDistance a, IDistance b)
        {
            return new Compute(a, b, '+');
        }

        sealed class Compute : IDistance
        {
            readonly IDistance _left;
            readonly IDistance _right;
            readonly Char _op;

            public Compute(IDistance left, IDistance right, Char op)
            {
                _left = left;
                _right = right;
                _op = op;
            }

            public Single ToPixel()
            {
                var left = _left.ToPixel();
                var right = _right.ToPixel();

                switch (_op)
                {
                    case '+': return left + right;
                    case '-': return left - right;
                    case '*': return left * right;
                    case '/': return left / right;
                }

                return 0f;
            }

            public CssValueType Type
            {
                get { return CssValueType.Primitive; }
            }

            public String CssText
            {
                get { return String.Concat(_left.CssText, _op.ToString(), _right.CssText); }
            }
        }

        #endregion
    }
}
