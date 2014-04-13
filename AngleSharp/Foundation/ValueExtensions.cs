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

        public static Uri ToUri(this CSSValue value)
        {
            if (value is CSSUriValue)
                return ((CSSUriValue)value).Uri;

            return null;
        }

        public static String ToContent(this CSSValue value)
        {
            if (value is CSSStringValue)
                return ((CSSStringValue)value).Value;

            return null;
        }

        public static CSSCalcValue ToCalc(this CSSValue value)
        {
            if (value is CSSPercentValue)
                return CSSCalcValue.FromPercent((CSSPercentValue)value);
            else if (value is CSSLengthValue)
                return CSSCalcValue.FromLength((CSSLengthValue)value);
            else if (value == CSSNumberValue.Zero)
                return CSSCalcValue.Zero;
            else if (value is CSSCalcValue)
                return (CSSCalcValue)value;

            return null;
        }

        public static CSSImageValue ToImage(this CSSValue value)
        {
            if (value is CSSImageValue)
                return (CSSImageValue)value;
            else if (value is CSSUriValue)
                return CSSImageValue.FromUrl((CSSUriValue)value);
            else if (value.Is("none"))
                return CSSImageValue.None;

            return null;
        }

        public static Single? ToNumber(this CSSValue value)
        {
            if (value is CSSNumberValue)
                return ((CSSNumberValue)value).Value;

            return null;
        }

        public static Int32? ToInteger(this CSSValue value)
        {
            if (value is CSSNumberValue)
                return (Byte)((CSSNumberValue)value).Value;

            return null;
        }

        public static Byte? ToByte(this CSSValue value)
        {
            if (value is CSSNumberValue)
                return (Byte)Math.Min(Math.Max(((CSSNumberValue)value).Value, 0), 255);

            return null;
        }

        public static Angle? ToAngle(this CSSValue value)
        {
            if (value is CSSAngleValue)
                return ((CSSAngleValue)value).Angle;

            return null;
        }

        public static Length? ToLength(this CSSValue value)
        {
            if (value is CSSLengthValue)
                return ((CSSLengthValue)value).Length;
            else if (value == CSSNumberValue.Zero)
                return Length.Zero;

            return null;
        }

        public static Length? ToBorderWidth(this CSSValue value)
        {
            if (value is CSSLengthValue)
                return ((CSSLengthValue)value).Length;
            else if (value == CSSNumberValue.Zero)
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
            if (value is CSSColorValue)
                return ((CSSColorValue)value).Color;
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
