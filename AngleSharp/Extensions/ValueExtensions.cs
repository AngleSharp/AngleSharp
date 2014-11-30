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
            return primitive != null && primitive.Unit == UnitType.Ident && primitive.GetString().Equals(identifier, StringComparison.OrdinalIgnoreCase);
        }

        public static Boolean TryGetValue<T>(this Dictionary<String, T> obj, CSSValue value, out T mode)
        {
            var primitive = value as CSSPrimitiveValue;
            mode = default(T);
            return primitive != null && primitive.Unit == UnitType.Ident && obj.TryGetValue(primitive.GetString(), out mode);
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
                if (primitive.Unit == UnitType.Number && primitive.ToSingle() == 0f)
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

            if (primitive != null && primitive.Unit == UnitType.String)
                return primitive.GetString();

            return null;
        }

        public static String ToIdentifier(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null && primitive.Unit == UnitType.Ident)
                return primitive.GetString();

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

            if (primitive != null && primitive.Unit == UnitType.Number)
                return primitive.GetNumber(UnitType.Number);

            return null;
        }

        public static Int32? ToInteger(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null && primitive.Unit == UnitType.Number)
                return (Int32)primitive.GetNumber(UnitType.Number);

            return null;
        }

        public static Byte? ToByte(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null && primitive.Unit == UnitType.Number)
                return (Byte)Math.Min(Math.Max((Int32)primitive.GetNumber(UnitType.Number), 0), 255);

            return null;
        }

        public static Angle? ToAngle(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null)
                return primitive.Value as Angle?;

            return null;
        }

        public static Angle? ToSideOrCorner(this CSSValue value)
        {
            var values = value as CSSValueList;

            if (values != null && values.Length > 1 && values[0].Is(Keywords.To))
            {
                var horizontalModes = Map.HorizontalModes;
                var verticalModes = Map.VerticalModes;

                if (values.Length == 2)
                {
                    var val = 0f;

                    if (horizontalModes.TryGetValue(values[1], out val))
                        return new Angle(270f - val * 180f, Angle.Unit.Deg);
                    else if (verticalModes.TryGetValue(values[1], out val))
                        return new Angle(val * 180f, Angle.Unit.Deg);
                }
                else if (values.Length == 3)
                {
                    var h = 0f;
                    var v = 0f;

                    if ((horizontalModes.TryGetValue(values[1], out h) && verticalModes.TryGetValue(values[2], out v)) || 
                        (horizontalModes.TryGetValue(values[2], out h) && verticalModes.TryGetValue(values[1], out v)))
                        return new Angle((Single)(Math.Atan2(h - 0.5, 0.5 - v) * 180.0 / Math.PI), Angle.Unit.Deg);
                }
            }

            return null;
        }

        public static String ToFontFamily(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null)
            {
                if (primitive.Unit == UnitType.Ident)
                {
                    String family;
                    var name = primitive.GetString();

                    if (Map.DefaultFontFamilies.TryGetValue(name, out family))
                        return family;

                    return name;
                }
                else if (primitive.Unit == UnitType.String)
                {
                    return primitive.GetString();
                }
            }
            else if (value is CSSValueList)
            {
                var values = (CSSValueList)value;
                var names = new String[values.Length];

                for (var i = 0; i < names.Length; i++)
                {
                    var ident = values[i] as CSSPrimitiveValue;

                    if (ident == null || ident.Unit != UnitType.Ident)
                        return null;

                    names[i] = ident.GetString();
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
                if (primitive.Unit == UnitType.Number && primitive.ToSingle() == 0f)
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

            if (primitive != null && primitive.Unit == UnitType.Transform)
                return (ITransform)primitive.Value;

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
                var w = values[0].ToSingle();
                var h = values[2].ToSingle();

                if (w.HasValue && h.HasValue)
                    return w.Value / h.Value;
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

            if (primitive != null && primitive.Unit == UnitType.RgbColor)
                return primitive.GetColor();
            else if (primitive != null && primitive.Unit == UnitType.Ident)
                return Color.FromName(primitive.GetString());

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
