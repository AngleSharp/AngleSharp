namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A bunch of useful extension methods.
    /// </summary>
    static class ValueExtensions
    {
        #region Dictionaries

        static readonly Dictionary<String, LineStyle> lineStyles = new Dictionary<String, LineStyle>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, BoxModel> boxModels = new Dictionary<String, BoxModel>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, TransitionFunction> timingFunctions = new Dictionary<String, TransitionFunction>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, AnimationFillStyle> fillModes = new Dictionary<String, AnimationFillStyle>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, AnimationDirection> directions = new Dictionary<String, AnimationDirection>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, Visibility> visibilities = new Dictionary<String, Visibility>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, ListStyle> listStyles = new Dictionary<String, ListStyle>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, ListPosition> listPositions = new Dictionary<String, ListPosition>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, FontSize> fontSizes = new Dictionary<String, FontSize>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, TextDecorationStyle> decorationStyles = new Dictionary<String, TextDecorationStyle>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, TextDecorationLine> decorationLines = new Dictionary<String, TextDecorationLine>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, BorderRepeat> borderRepeatModes = new Dictionary<String, BorderRepeat>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, String> defaultfamilies = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, BackgroundAttachment> backgroundAttachments = new Dictionary<String, BackgroundAttachment>(StringComparer.OrdinalIgnoreCase);

        #endregion

        #region Initial Population

        static ValueExtensions()
        {
            lineStyles.Add(Keywords.None, LineStyle.None);
            lineStyles.Add(Keywords.Solid, LineStyle.Solid);
            lineStyles.Add(Keywords.Double, LineStyle.Double);
            lineStyles.Add(Keywords.Dotted, LineStyle.Dotted);
            lineStyles.Add(Keywords.Dashed, LineStyle.Dashed);
            lineStyles.Add(Keywords.Inset, LineStyle.Inset);
            lineStyles.Add(Keywords.Outset, LineStyle.Outset);
            lineStyles.Add(Keywords.Ridge, LineStyle.Ridge);
            lineStyles.Add(Keywords.Groove, LineStyle.Groove);
            lineStyles.Add(Keywords.Hidden, LineStyle.Hidden);

            boxModels.Add(Keywords.BorderBox, BoxModel.BorderBox);
            boxModels.Add(Keywords.PaddingBox, BoxModel.PaddingBox);
            boxModels.Add(Keywords.ContentBox, BoxModel.ContentBox);

            timingFunctions.Add(Keywords.Ease, TransitionFunction.Ease);
            timingFunctions.Add(Keywords.EaseIn, TransitionFunction.EaseIn);
            timingFunctions.Add(Keywords.EaseOut, TransitionFunction.EaseOut);
            timingFunctions.Add(Keywords.EaseInOut, TransitionFunction.EaseInOut);
            timingFunctions.Add(Keywords.Linear, TransitionFunction.Linear);
            timingFunctions.Add(Keywords.StepStart, TransitionFunction.StepStart);
            timingFunctions.Add(Keywords.StepEnd, TransitionFunction.StepEnd);

            fillModes.Add(Keywords.None, AnimationFillStyle.None);
            fillModes.Add(Keywords.Forwards, AnimationFillStyle.Forwards);
            fillModes.Add(Keywords.Backwards, AnimationFillStyle.Backwards);
            fillModes.Add(Keywords.Both, AnimationFillStyle.Both);

            directions.Add(Keywords.Normal, AnimationDirection.Normal);
            directions.Add(Keywords.Reverse, AnimationDirection.Reverse);
            directions.Add(Keywords.Alternate, AnimationDirection.Alternate);
            directions.Add(Keywords.AlternateReverse, AnimationDirection.AlternateReverse);

            visibilities.Add(Keywords.Visible, Visibility.Visible);
            visibilities.Add(Keywords.Hidden, Visibility.Hidden);
            visibilities.Add(Keywords.Collapse, Visibility.Collapse);

            listStyles.Add(Keywords.Disc, ListStyle.Disc);
            listStyles.Add(Keywords.Circle, ListStyle.Circle);
            listStyles.Add(Keywords.Square, ListStyle.Square);
            listStyles.Add(Keywords.Decimal, ListStyle.Decimal);
            listStyles.Add(Keywords.DecimalLeadingZero, ListStyle.DecimalLeadingZero);
            listStyles.Add(Keywords.LowerRoman, ListStyle.LowerRoman);
            listStyles.Add(Keywords.UpperRoman, ListStyle.UpperRoman);
            listStyles.Add(Keywords.LowerGreek, ListStyle.LowerGreek);
            listStyles.Add(Keywords.LowerLatin, ListStyle.LowerLatin);
            listStyles.Add(Keywords.UpperLatin, ListStyle.UpperLatin);
            listStyles.Add(Keywords.Armenian, ListStyle.Armenian);
            listStyles.Add(Keywords.Georgian, ListStyle.Georgian);
            listStyles.Add(Keywords.LowerAlpha, ListStyle.LowerLatin);
            listStyles.Add(Keywords.UpperAlpha, ListStyle.UpperLatin);
            listStyles.Add(Keywords.None, ListStyle.None);

            listPositions.Add(Keywords.Inside, ListPosition.Inside);
            listPositions.Add(Keywords.Outside, ListPosition.Outside);

            fontSizes.Add(Keywords.XxSmall, FontSize.Tiny);
            fontSizes.Add(Keywords.XSmall, FontSize.Little);
            fontSizes.Add(Keywords.Small, FontSize.Small);
            fontSizes.Add(Keywords.Medium, FontSize.Medium);
            fontSizes.Add(Keywords.Large, FontSize.Large);
            fontSizes.Add(Keywords.XLarge, FontSize.Big);
            fontSizes.Add(Keywords.XxLarge, FontSize.Huge);
            fontSizes.Add(Keywords.Larger, FontSize.Smaller);
            fontSizes.Add(Keywords.Smaller, FontSize.Larger);

            decorationStyles.Add(Keywords.Solid, TextDecorationStyle.Solid);
            decorationStyles.Add(Keywords.Double, TextDecorationStyle.Double);
            decorationStyles.Add(Keywords.Dotted, TextDecorationStyle.Dotted);
            decorationStyles.Add(Keywords.Dashed, TextDecorationStyle.Dashed);
            decorationStyles.Add(Keywords.Wavy, TextDecorationStyle.Wavy);

            decorationLines.Add(Keywords.Underline, TextDecorationLine.Underline);
            decorationLines.Add(Keywords.Overline, TextDecorationLine.Overline);
            decorationLines.Add(Keywords.LineThrough, TextDecorationLine.LineThrough);
            decorationLines.Add(Keywords.Blink, TextDecorationLine.Blink);

            borderRepeatModes.Add(Keywords.Stretch, BorderRepeat.Stretch);
            borderRepeatModes.Add(Keywords.Repeat, BorderRepeat.Repeat);
            borderRepeatModes.Add(Keywords.Round, BorderRepeat.Round);

            defaultfamilies.Add(Keywords.Serif, "Times New Roman");
            defaultfamilies.Add(Keywords.SansSerif, "Arial");
            defaultfamilies.Add(Keywords.Monospace, "Consolas");
            defaultfamilies.Add(Keywords.Cursive, "Cursive");
            defaultfamilies.Add(Keywords.Fantasy, "Comic Sans");

            backgroundAttachments.Add(Keywords.Fixed, BackgroundAttachment.Fixed);
            backgroundAttachments.Add(Keywords.Local, BackgroundAttachment.Local);
            backgroundAttachments.Add(Keywords.Scroll, BackgroundAttachment.Scroll);
        }

        #endregion

        #region Dictionary Lookups

        public static AnimationDirection? ToDirection(this CSSValue value)
        {
            return directions.GetValueOrDefault(value);
        }

        public static TextDecorationStyle? ToDecorationStyle(this CSSValue value)
        {
            return decorationStyles.GetValueOrDefault(value);
        }

        public static TextDecorationLine? ToDecorationLine(this CSSValue value)
        {
            return decorationLines.GetValueOrDefault(value);
        }

        public static BorderRepeat? ToBorderRepeat(this CSSValue value)
        {
            return borderRepeatModes.GetValueOrDefault(value);
        }

        public static AnimationFillStyle? ToFillMode(this CSSValue value)
        {
            return fillModes.GetValueOrDefault(value);
        }

        public static LineStyle? ToLineStyle(this CSSValue value)
        {
            return lineStyles.GetValueOrDefault(value);
        }

        public static Visibility? ToVisibility(this CSSValue value)
        {
            return visibilities.GetValueOrDefault(value);
        }

        public static TransitionFunction ToTimingFunction(this CSSValue value)
        {
            TransitionFunction function;

            if (timingFunctions.TryGetValue(value, out function))
                return function;

            var primitive = value as CSSPrimitiveValue;

            if (primitive != null && primitive.Unit == UnitType.Transition)
                return primitive.Value as TransitionFunction;

            return null;
        }

        public static BoxModel? ToBoxModel(this CSSValue value)
        {
            return boxModels.GetValueOrDefault(value);
        }

        public static ListStyle? ToListStyle(this CSSValue value)
        {
            return listStyles.GetValueOrDefault(value);
        }

        public static ListPosition? ToListPosition(this CSSValue value)
        {
            return listPositions.GetValueOrDefault(value);
        }

        public static BackgroundAttachment? ToBackgroundAttachment(this CSSValue value)
        {
            return backgroundAttachments.GetValueOrDefault(value);
        }

        public static FontSize? ToFontSize(this CSSValue value)
        {
            return fontSizes.GetValueOrDefault(value);
        }

        #endregion

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

        public static T? GetValueOrDefault<T>(this Dictionary<String, T> obj, CSSValue value)
            where T : struct
        {
            T member;

            if (obj.TryGetValue(value, out member))
                return member;

            return null;
        }

        public static Boolean IsOneOf(this CSSValue value, params String[] identifiers)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null && primitive.Unit == UnitType.Ident)
            {
                var ident = primitive.GetString();

                foreach (var identifier in identifiers)
                {
                    if (ident.Equals(identifier, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }

            return false;
        }

        public static Url ToUri(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null)
                return primitive.Value as Url;

            return null;
        }

        public static IEnumerable<CSSValue> AsEnumeration(this CSSValue value)
        {
            if (value.Type == CssValueType.List)
                return (CSSValueList)value;

            return new CSSValue[1] { value };
        }

        public static List<T> AsList<T>(this CSSValue value, Func<CSSValue, T> transformer = null)
            where T : class, ICssObject
        {
            transformer = transformer ?? (v => v as T);

            if (value is CSSValueList)
            {
                var values = (CSSValueList)value;
                var list = new List<T>();

                for (int i = 0; i < values.Length; i++)
                {
                    var item = transformer(values[i++]);

                    if (item == null)
                        return null;

                    list.Add(item);

                    if (i < values.Length && values[i] != CSSValue.Separator)
                        return null;
                }

                return list;
            }
            else
            {
                var item = transformer(value);

                if (item != null)
                {
                    var list = new List<T>();
                    list.Add(item);
                    return list;
                }
            }

            return null;
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

        public static Shape ToShape(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null && primitive.Unit == UnitType.Rect)
                return primitive.Value as Shape;

            return null;
        }

        public static IBitmap ToImage(this CSSValue value)
        {
            if (value.Is(Keywords.None))
                return Color.Transparent;

            var primitive = value as CSSPrimitiveValue;

            if (primitive != null)
                return primitive.Value as IBitmap;

            return null;
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
            else if (value is CSSValueList)
            {
                var values = (CSSValueList)value;

                if (values.Length == 2 && values[0].Is(Keywords.To))
                {
                    if (values[1].Is(Keywords.Bottom))
                        return new Angle(180f, Angle.Unit.Deg);
                    else if (values[1].Is(Keywords.Right))
                        return new Angle(90f, Angle.Unit.Deg);
                    else if (values[1].Is(Keywords.Left))
                        return new Angle(270f, Angle.Unit.Deg);
                    else if (values[1].Is(Keywords.Top))
                        return new Angle(0f, Angle.Unit.Deg);
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

                    if (defaultfamilies.TryGetValue(name, out family))
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

        public static Point ToPoint(this CSSValueList values)
        {
            if (values.Length == 1)
            {
                var value = values[0];
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
                var value = values[index];

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

        public static Shadow ToShadow(this CSSValueList item)
        {
            if (item.Length < 2)
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

        #endregion

        #region Value Calculation

        public static IDistance Add(this IDistance a, IDistance b)
        {
            return a;//TODO
        }

        #endregion
    }
}
