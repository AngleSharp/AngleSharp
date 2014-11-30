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

        public static Boolean Is(this CSSValue value, String identifier)
        {
            var primitive = value as CSSPrimitiveValue;
            return primitive != null && (primitive.Value as CssIdentifier).GetString().Equals(identifier, StringComparison.OrdinalIgnoreCase);
        }

        static String GetString(this CssIdentifier identifier)
        {
            if (identifier == null)
                return String.Empty;

            return identifier;
        }

        public static Boolean TryGetValue<T>(this Dictionary<String, T> obj, CSSValue value, out T mode)
        {
            var primitive = value as CSSPrimitiveValue;
            mode = default(T);
            return primitive != null && obj.TryGetValue((primitive.Value as CssIdentifier).GetString(), out mode);
        }

        public static CssIdentifier GetIdentifier<T>(this Dictionary<String, T> obj, T value)
        {
            foreach (var pair in obj)
            {
                if (pair.Value.Equals(value))
                    return new CssIdentifier(pair.Key);
            }

            return null;
        }

        public static CSSValueList CopyToList(this CSSValue value)
        {
            var original = value as CSSValueList;

            if (original == null)
                return new CSSValueList(value);

            var newList = new CSSValueList();

            foreach (var item in original)
                newList.Add(item);

            return newList;
        }

        public static CssUrl ToUri(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null)
                return primitive.Value as CssUrl;

            return null;
        }

        public static IEnumerable<CSSValue> AsEnumeration(this CSSValue value)
        {
            if (value.Type == CssValueType.List)
                return (CSSValueList)value;

            return new CSSValue[1] { value };
        }

        public static CSSValue Reduce(this CSSValueList list)
        {
            if (list.Length == 0)
                return null;
            else if (list.Length == 1)
                return list[0];

            return list;
        }

        public static IDistance ToBorderSlice(this CSSValue value)
        {
            var percent = value.ToPercent();

            if (percent.HasValue)
                return percent.Value;

            var number = value.ToSingle();

            if (number.HasValue)
                return new Length(number.Value, Length.Unit.Px);

            return null;
        }

        public static IDistance ToLineHeight(this CSSValue value)
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

        public static IDistance ToDistance(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null)
            {
                var number = primitive.Value as Number?;

                if (number.HasValue && number.Value == Number.Zero)
                    return Length.Zero;

                return primitive.Value as IDistance;
            }

            return null;
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

        public static Percent? ToPercent(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null)
                return primitive.Value as Percent?;

            return null;
        }

        public static String ToCssString(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null && primitive.Value is CssString)
                return (CssString)primitive.Value;

            return null;
        }

        public static String ToIdentifier(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null && primitive.Value is CssIdentifier)
                return (CssIdentifier)primitive.Value;

            return null;
        }

        public static String ToAnimatableIdentifier(this CSSValue value)
        {
            var identifier = value.ToIdentifier();

            if (identifier != null && (identifier.Equals(Keywords.All, StringComparison.OrdinalIgnoreCase) || CssPropertyFactory.IsAnimatable(identifier)))
                return identifier;

            return null;
        }

        public static Single? ToSingle(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null && primitive.Value is Number)
                return ((Number)primitive.Value).Value;

            return null;
        }

        public static Int32? ToInteger(this CSSValue value)
        {
            var val = value.ToSingle();

            if (val.HasValue)
                return (Int32)val.Value;

            return null;
        }

        public static Byte? ToByte(this CSSValue value)
        {
            var val = value.ToInteger();

            if (val.HasValue)
                return (Byte)Math.Min(Math.Max(val.Value, 0), 255);

            return null;
        }

        public static Angle? ToAngle(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null)
                return primitive.Value as Angle?;

            return null;
        }

        public static String ToFontFamily(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null)
            {
                var ident = primitive.Value as CssIdentifier;

                if (ident != null)
                {
                    String family;

                    if (Map.DefaultFontFamilies.TryGetValue(ident, out family))
                        return family;

                    return ident;
                }

                else if (primitive.Value is CssString)
                {
                    return (CssString)primitive.Value;
                }
            }
            else if (value is CSSValueList)
            {
                var values = (CSSValueList)value;
                var names = new String[values.Length];

                for (var i = 0; i < names.Length; i++)
                {
                    var ident = values[i] as CSSPrimitiveValue;

                    if (ident == null || String.IsNullOrEmpty(names[i] = (ident.Value as CssIdentifier).GetString()))
                        return null;
                }

                return String.Join(" ", names);
            }

            return null;
        }

        public static Frequency? ToFrequency(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null)
                return primitive.Value as Frequency?;

            return null;
        }

        public static Length? ToLength(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null)
            {
                var number = primitive.Value as Number?;

                if (number.HasValue && number.Value == Number.Zero)
                    return Length.Zero;

                return primitive.Value as Length?;
            }

            return null;
        }

        public static Resolution? ToResolution(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null)
                return primitive.Value as Resolution?;

            return null;
        }

        public static ITransform ToTransform(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null)
                return primitive.Value as ITransform;

            return null;
        }

        public static Time? ToTime(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null)
                return primitive.Value as Time?;

            return null;
        }

        public static GradientStop[] ToGradientStops(this CSSValue value)
        {
            var values = value as CSSValueList;

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

        public static GradientStop? ToGradientStop(this CSSValue value, Single defaultStop = 0f)
        {
            var list = value as CSSValueList;
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

        public static Single? ToAspectRatio(this CSSValue value)
        {
            var values = value as CSSValueList;

            if (values != null && values.Length == 3 && values[1] == CSSValueList.Delimiter)
            {
                var w = values[0].ToInteger();
                var h = values[2].ToInteger();

                if (w.HasValue && h.HasValue && w.Value > 0 && h.Value > 0)
                    return (Single)w.Value / (Single)h.Value;
            }

            return null;
        }

        public static IDistance ToImageBorderWidth(this CSSValue value)
        {
            if (value.Is(Keywords.Auto))
                return Percent.Hundred;

            var multiple = value.ToSingle();

            if (multiple.HasValue)
                return new Percent(multiple.Value * 100f);

            return value.ToDistance();
        }

        public static Length? ToBorderWidth(this CSSValue value)
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

            return null;
        }

        public static Color? ToColor(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null && primitive.Value is Color)
                return (Color)primitive.Value;
            else if (primitive != null)
                return Color.FromName((primitive.Value as CssIdentifier).GetString());

            return null;
        }

        public static CSSValueList Subset(this CSSValueList values, Int32 start = 0, Int32 end = -1)
        {
            if (end == -1)
                end = values.Length;

            var list = new List<CSSValue>();

            for (var i = start; i < end; i++)
                list.Add(values[i]);

            return new CSSValueList(list);
        }

        public static List<CSSValueList> ToList(this CSSValueList values)
        {
            var list = new List<CSSValueList>();

            for (int i = 0; i < values.Length; i++)
            {
                var entry = new CSSValueList();

                for (int j = i; j < values.Length; j++)
                {
                    if (values[j] == CSSValue.Separator)
                        break;

                    entry.Add(values[j]);
                    i++;
                }

                list.Add(entry);
            }

            return list;
        }

        public static Point ToPoint(this CSSValue value)
        {
            var values = value as CSSValueList;

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

        public static Shadow ToShadow(this CSSValue value)
        {
            var item = value as CSSValueList;

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
                var col = item[offset].ToColor();

                if (col.HasValue)
                {
                    color = col.Value;
                    offset++;
                }
            }

            if (item.Length > offset)
                return null;

            return new Shadow(inset, offsetX.Value, offsetY.Value, blurRadius, spreadRadius, color);
        }

        public static CSSValueList ToSeparatedList(this List<CSSValueList> list)
        {
            var values = new CSSValueList();

            if (list.Count > 0)
                values.Add(list[0].Reduce());

            for (int i = 1; i < list.Count; i++)
            {
                values.Add(CSSValue.Separator);
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

            public String ToCss()
            {
                return String.Concat(_left.ToCss(), _op.ToString(), _right.ToCss());
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
        }

        #endregion
    }
}
