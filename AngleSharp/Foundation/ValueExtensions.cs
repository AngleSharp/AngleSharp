namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A bunch of useful extension methods.
    /// </summary>
    static class ValueExtensions
    {
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

        public static Length? ToBorderWidth(this CSSValue value)
        {
            if (value is CSSPrimitiveValue<Length>)
                return ((CSSPrimitiveValue<Length>)value).Value;
            else if (value is CSSPrimitiveValue<Number> && ((CSSPrimitiveValue<Number>)value).Value == Number.Zero)
                return Length.Zero;
            else if (value is CSSIdentifierValue)
            {
                var ident = ((CSSIdentifierValue)value).Value;

                if (ident.Equals("thin", StringComparison.OrdinalIgnoreCase))
                    return new Length(0.5f, Length.Unit.Px);
                else if (ident.Equals("medium", StringComparison.OrdinalIgnoreCase))
                    return new Length(1f, Length.Unit.Px);
                else if (ident.Equals("thick", StringComparison.OrdinalIgnoreCase))
                    return new Length(2f, Length.Unit.Px);
            }

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
    }
}
