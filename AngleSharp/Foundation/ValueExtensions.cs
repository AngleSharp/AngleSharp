namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A bunch of useful extension methods.
    /// </summary>
    static class ValueExtensions
    {
        static readonly Dictionary<String, LineStyle> lineStyles = new Dictionary<String, LineStyle>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, BoxModel> boxModels = new Dictionary<String, BoxModel>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, CSSTimingValue> timingFunctions = new Dictionary<String, CSSTimingValue>(StringComparer.OrdinalIgnoreCase);

        static ValueExtensions()
        {
            lineStyles.Add("none", LineStyle.None);
            lineStyles.Add("solid", LineStyle.Solid);
            lineStyles.Add("double", LineStyle.Double);
            lineStyles.Add("dotted", LineStyle.Dotted);
            lineStyles.Add("dashed", LineStyle.Dashed);
            lineStyles.Add("inset", LineStyle.Inset);
            lineStyles.Add("outset", LineStyle.Outset);
            lineStyles.Add("ridge", LineStyle.Ridge);
            lineStyles.Add("groove", LineStyle.Groove);
            lineStyles.Add("hidden", LineStyle.Hidden);

            boxModels.Add("border-box", BoxModel.BorderBox);
            boxModels.Add("padding-box", BoxModel.PaddingBox);
            boxModels.Add("content-box", BoxModel.ContentBox);

            timingFunctions.Add("ease", CSSTimingValue.Ease);
            timingFunctions.Add("ease-in", CSSTimingValue.EaseIn);
            timingFunctions.Add("ease-out", CSSTimingValue.EaseOut);
            timingFunctions.Add("ease-in-out", CSSTimingValue.EaseInOut);
            timingFunctions.Add("linear", CSSTimingValue.Linear);
            timingFunctions.Add("step-start", CSSTimingValue.StepStart);
            timingFunctions.Add("step-end", CSSTimingValue.StepEnd);
        }

        public static LineStyle? ToLineStyle(this CSSValue value)
        {
            LineStyle style;

            if (value is CSSIdentifierValue && lineStyles.TryGetValue(((CSSIdentifierValue)value).Value, out style))
                return style;

            return null;
        }

        public static CSSTimingValue ToTimingFunction(this CSSValue value)
        {
            CSSTimingValue function;

            if (value is CSSTimingValue)
                return (CSSTimingValue)value;
            else if (value is CSSIdentifierValue && timingFunctions.TryGetValue(((CSSIdentifierValue)value).Value, out function))
                return function;

            return null;
        }

        public static BoxModel? ToBoxModel(this CSSValue value)
        {
            BoxModel model;

            if (value is CSSIdentifierValue && boxModels.TryGetValue(((CSSIdentifierValue)value).Value, out model))
                return model;

            return null;
        }

        public static Boolean Is(this CSSValue value, String identifier)
        {
            return value is CSSIdentifierValue && ((CSSIdentifierValue)value).Value.Equals(identifier, StringComparison.OrdinalIgnoreCase);
        }

        public static Boolean IsOneOf(this CSSValue value, params String[] identifiers)
        {
            if (value is CSSIdentifierValue)
            {
                var ident = ((CSSIdentifierValue)value).Value;

                foreach (var identifier in identifiers)
                {
                    if (ident.Equals(identifier, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }

            return false;
        }

        public static Location ToUri(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Location>)
                return ((CSSPrimitiveValue<Location>)value).Value;

            return null;
        }

        public static String ToContent(this CSSValue value)
        {
            if (value is CSSStringValue)
                return ((CSSStringValue)value).Value;

            return null;
        }

        public static CSSPrimitiveValue<Color> AsColor(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Color>)
                return (CSSPrimitiveValue<Color>)value;
            else if (value is CSSIdentifierValue)
            {
                var color = Color.FromName(((CSSIdentifierValue)value).Value);

                if (color.HasValue)
                    return new CSSPrimitiveValue<Color>(color.Value);
            }

            return null;
        }

        public static CSSCalcValue AsCalc(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Percent>)
                return CSSCalcValue.FromPercent(((CSSPrimitiveValue<Percent>)value).Value);
            else if (value is CSSPrimitiveValue<Length>)
                return CSSCalcValue.FromLength(((CSSPrimitiveValue<Length>)value).Value);
            else if (value is CSSCalcValue)
                return (CSSCalcValue)value;
            else if (value is CSSPrimitiveValue<Number> && ((CSSPrimitiveValue<Number>)value).Value == Number.Zero)
                return CSSCalcValue.Zero;

            return null;
        }

        public static List<T> AsList<T>(this CSSValue value, Func<CSSValue, T> transformer = null)
            where T : CSSValue
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

        public static CSSImageValue AsImage(this CSSValue value)
        {
            if (value is CSSImageValue)
                return (CSSImageValue)value;
            else if (value is CSSPrimitiveValue<Location>)
                return CSSImageValue.FromUrl(((CSSPrimitiveValue<Location>)value).Value);
            else if (value.Is("none"))
                return CSSImageValue.None;

            return null;
        }

        public static Percent? ToPercent(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Percent>)
                return ((CSSPrimitiveValue<Percent>)value).Value;

            return null;
        }

        public static Single? ToNumber(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Number>)
                return (Single)((CSSPrimitiveValue<Number>)value).Value;

            return null;
        }

        public static Int32? ToInteger(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Number>)
                return (Int32)((CSSPrimitiveValue<Number>)value).Value;

            return null;
        }

        public static Byte? ToByte(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Number>)
                return (Byte)Math.Min(Math.Max((Int32)((CSSPrimitiveValue<Number>)value).Value, 0), 255);

            return null;
        }

        public static Angle? ToAngle(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Angle>)
                return ((CSSPrimitiveValue<Angle>)value).Value;
            else if (value is CSSValueList)
            {
                var values = (CSSValueList)value;

                if (values.Length == 2 && values[0].Is("to"))
                {
                    if (values[1].Is("bottom"))
                        return new Angle(180f, Angle.Unit.Deg);
                    else if (values[1].Is("right"))
                        return new Angle(90f, Angle.Unit.Deg);
                    else if (values[1].Is("left"))
                        return new Angle(270f, Angle.Unit.Deg);
                    else if (values[1].Is("top"))
                        return new Angle(0f, Angle.Unit.Deg);
                }
            }

            return null;
        }

        public static Length? ToLength(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Length>)
                return ((CSSPrimitiveValue<Length>)value).Value;
            else if (value is CSSPrimitiveValue<Number> && ((CSSPrimitiveValue<Number>)value).Value == Number.Zero)
                return Length.Zero;

            return null;
        }

        public static Time? ToTime(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Time>)
                return ((CSSPrimitiveValue<Time>)value).Value;

            return null;
        }

        public static Length? ToBorderWidth(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Length>)
                return ((CSSPrimitiveValue<Length>)value).Value;
            else if (value is CSSPrimitiveValue<Number> && ((CSSPrimitiveValue<Number>)value).Value == Number.Zero)
                return Length.Zero;
            else if (value.Is("thin"))
                return Length.Thin;
            else if (value.Is("medium"))
                return Length.Medium;
            else if (value.Is("thick"))
                return Length.Thick;

            return null;
        }

        public static Color? ToColor(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Color>)
                return ((CSSPrimitiveValue<Color>)value).Value;
            else if (value is CSSIdentifierValue)
                return Color.FromName(((CSSIdentifierValue)value).Value);

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

        public static Shadow ToShadow(this CSSValueList item)
        {
            if (item.Length < 2)
                return null;

            var inset = item[0].Is("inset");
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
    }
}
