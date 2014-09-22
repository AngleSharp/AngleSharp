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
        static readonly Dictionary<String, TransformFunction> timingFunctions = new Dictionary<String, TransformFunction>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, AnimationFillStyle> fillModes = new Dictionary<String, AnimationFillStyle>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, AnimationDirection> directions = new Dictionary<String, AnimationDirection>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, Visibility> visibilities = new Dictionary<String, Visibility>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, ListStyle> listStyles = new Dictionary<String, ListStyle>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, ListPosition> listPositions = new Dictionary<String, ListPosition>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, FontSize> fontSizes = new Dictionary<String, FontSize>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, TextDecorationStyle> decorationStyles = new Dictionary<String, TextDecorationStyle>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, TextDecorationLine> decorationLines = new Dictionary<String, TextDecorationLine>(StringComparer.OrdinalIgnoreCase);

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

            timingFunctions.Add(Keywords.Ease, TransformFunction.Ease);
            timingFunctions.Add(Keywords.EaseIn, TransformFunction.EaseIn);
            timingFunctions.Add(Keywords.EaseOut, TransformFunction.EaseOut);
            timingFunctions.Add(Keywords.EaseInOut, TransformFunction.EaseInOut);
            timingFunctions.Add(Keywords.Linear, TransformFunction.Linear);
            timingFunctions.Add(Keywords.StepStart, TransformFunction.StepStart);
            timingFunctions.Add(Keywords.StepEnd, TransformFunction.StepEnd);

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
        }

        #endregion

        #region Dictionary Lookups

        public static AnimationDirection? ToDirection(this CSSValue value)
        {
            AnimationDirection direction;

            if (directions.TryGetValue(value, out direction))
                return direction;

            return null;
        }

        public static TextDecorationStyle? ToDecorationStyle(this CSSValue value)
        {
            TextDecorationStyle style;

            if (decorationStyles.TryGetValue(value, out style))
                return style;

            return null;
        }

        public static TextDecorationLine? ToDecorationLine(this CSSValue value)
        {
            TextDecorationLine line;

            if (decorationLines.TryGetValue(value, out line))
                return line;

            return null;
        }

        public static AnimationFillStyle? ToFillMode(this CSSValue value)
        {
            AnimationFillStyle fillMode;

            if (fillModes.TryGetValue(value, out fillMode))
                return fillMode;

            return null;
        }

        public static LineStyle? ToLineStyle(this CSSValue value)
        {
            LineStyle style;

            if (lineStyles.TryGetValue(value, out style))
                return style;

            return null;
        }

        public static Visibility? ToVisibility(this CSSValue value)
        {
            Visibility visibility;

            if (visibilities.TryGetValue(value, out visibility))
                return visibility;

            return null;
        }

        public static TransformFunction ToTimingFunction(this CSSValue value)
        {
            TransformFunction function;

            if (timingFunctions.TryGetValue(value, out function))
                return function;

            var primitive = value as CSSPrimitiveValue;

            if (primitive != null && primitive.Unit == UnitType.Timing)
                return primitive.Value as TransformFunction;

            return null;
        }

        public static BoxModel? ToBoxModel(this CSSValue value)
        {
            BoxModel model;

            if (boxModels.TryGetValue(value, out model))
                return model;

            return null;
        }

        public static ListStyle? ToListStyle(this CSSValue value)
        {
            ListStyle style;

            if (listStyles.TryGetValue(value, out style))
                return style;

            return null;
        }

        public static ListPosition? ToListPosition(this CSSValue value)
        {
            ListPosition position;

            if (listPositions.TryGetValue(value, out position))
                return position;

            return null;
        }

        public static FontSize? ToFontSize(this CSSValue value)
        {
            FontSize size;

            if (fontSizes.TryGetValue(value, out size))
                return size;

            return null;
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

        public static CSSCalcValue AsCalc(this CSSValue value)
        {
            if (value is CSSCalcValue)
                return (CSSCalcValue)value;

            var percent = value.ToPercent();

            if (percent != null)
                return CSSCalcValue.FromPercent(percent.Value);

            var length = value.ToLength();

            if (length != null)
                return CSSCalcValue.FromLength(length.Value);

            return null;
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

        public static Shape ToShape(this CSSValue value)
        {
            var primitive = value as CSSPrimitiveValue;

            if (primitive != null && primitive.Unit == UnitType.Rect)
                return primitive.Value as Shape;

            return null;
        }

        public static ICssObject ToImage(this CSSValue value)
        {
            if (value.Is(Keywords.None))
                return Color.Transparent;

            var primitive = value as CSSPrimitiveValue;

            if (primitive != null)
            {
                switch (primitive.Unit)
                {
                    case UnitType.ImageList:
                    case UnitType.RgbColor:
                    case UnitType.Uri:
                    case UnitType.Gradient:
                        return primitive.Value;
                }
            }

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

        public static Point2d ToPoint(this CSSValueList values)
        {
            if (values.Length == 1)
            {
                var value = values[0];
                var calc = value.AsCalc();

                if (calc != null)
                    return new Point2d(calc);
                else if (value.Is(Keywords.Left))
                    return new Point2d(x: CSSCalcValue.Zero);
                else if (value.Is(Keywords.Right))
                    return new Point2d(x: CSSCalcValue.Full);
                else if (value.Is(Keywords.Top))
                    return new Point2d(y: CSSCalcValue.Zero);
                else if (value.Is(Keywords.Bottom))
                    return new Point2d(y: CSSCalcValue.Full);
                else if (value.Is(Keywords.Center))
                    return Point2d.Centered;
            }
            else if (values.Length == 2)
            {
                var left = values[0];
                var right = values[1];
                var horizontal = left.AsCalc();
                var vertical = right.AsCalc();

                if (horizontal == null)
                {
                    if (left.Is(Keywords.Left))
                        horizontal = CSSCalcValue.Zero;
                    else if (left.Is(Keywords.Right))
                        horizontal = CSSCalcValue.Full;
                    else if (left.Is(Keywords.Center))
                        horizontal = CSSCalcValue.Center;
                    else if (left.Is(Keywords.Top))
                    {
                        horizontal = vertical;
                        vertical = CSSCalcValue.Zero;
                    }
                    else if (left.Is(Keywords.Bottom))
                    {
                        horizontal = vertical;
                        vertical = CSSCalcValue.Full;
                    }
                }

                if (vertical == null)
                {
                    if (right.Is(Keywords.Top))
                        vertical = CSSCalcValue.Zero;
                    else if (right.Is(Keywords.Bottom))
                        vertical = CSSCalcValue.Full;
                    else if (right.Is(Keywords.Center))
                        vertical = CSSCalcValue.Center;
                    else if (right.Is(Keywords.Left))
                    {
                        vertical = horizontal;
                        horizontal = CSSCalcValue.Zero;
                    }
                    else if (right.Is(Keywords.Right))
                    {
                        vertical = horizontal;
                        horizontal = CSSCalcValue.Full;
                    }
                }

                if (horizontal != null && vertical != null)
                    return new Point2d(horizontal, vertical);
            }
            else if (values.Length > 2)
            {
                var index = 0;
                var shift = CSSCalcValue.Zero;
                var horizontal = CSSCalcValue.Center;
                var vertical = CSSCalcValue.Center;
                var value = values[index];

                if (value.Is(Keywords.Left))
                {
                    horizontal = CSSCalcValue.Zero;
                    shift = values[index + 1].AsCalc();
                }
                else if (value.Is(Keywords.Right))
                {
                    horizontal = CSSCalcValue.Full;
                    shift = values[index + 1].AsCalc();
                }
                else if (!value.Is(Keywords.Center))
                    return null;

                if (shift != null && shift != CSSCalcValue.Zero)
                {
                    index++;
                    horizontal = horizontal.Add(shift);
                    shift = CSSCalcValue.Zero;
                }

                value = values[++index];

                if (value.Is(Keywords.Top))
                {
                    vertical = CSSCalcValue.Zero;

                    if (index + 1 < values.Length)
                        shift = values[index + 1].AsCalc();
                }
                else if (value.Is(Keywords.Bottom))
                {
                    vertical = CSSCalcValue.Full;

                    if (index + 1 < values.Length)
                        shift = values[index + 1].AsCalc();
                }
                else if (!value.Is(Keywords.Center))
                    return null;

                if (shift != null)
                {
                    if (shift != CSSCalcValue.Zero)
                        vertical = vertical.Add(shift);

                    return new Point2d(horizontal, vertical);
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
    }
}
