namespace AngleSharp.Extensions
{
    using AngleSharp.Dom.Css;
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
            return primitive != null && primitive.Value.Equals(identifier, StringComparison.OrdinalIgnoreCase);
        }

        public static Boolean TryGetValue<T>(this Dictionary<String, T> obj, ICssValue value, out T mode)
        {
            var primitive = value as CssIdentifier;
            mode = default(T);
            return primitive != null && obj.TryGetValue(primitive.Value, out mode);
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

        public static CssValueList Copy(this CssValueList original)
        {
            var list = new CssValueList();

            for (int i = 0; i < original.Length; i++)
                list.Add(original[i]);

            return list;
        }

        public static CssValueList CopyExcept(this CssValueList original, Int32 index)
        {
            var list = new CssValueList();

            for (int i = 0; i < original.Length; i++)
            {
                if (i != index)
                    list.Add(original[i]);
            }

            return list;
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

        public static CssUrl ToUri(this ICssValue value)
        {
            return value as CssUrl;
        }

        public static Length? ToBorderSlice(this ICssValue value)
        {
            var percent = value.ToPercent();

            if (percent.HasValue)
                return new Length(percent.Value.Value, Length.Unit.Percent);

            var number = value.ToSingle();

            if (number.HasValue)
                return new Length(number.Value, Length.Unit.Px);

            return null;
        }

        public static Length? ToLineHeight(this ICssValue value)
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

        public static Length? ToDistance(this ICssValue value)
        {
            var percent = value as Percent?;

            if (percent == null)
            {
                var number = value as Number?;

                if (number.HasValue && number.Value == Number.Zero)
                    return Length.Zero;
            }
            else
                return new Length(percent.Value.Value, Length.Unit.Percent);

            return value as Length?;
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

        public static Percent? ToPercent(this ICssValue value)
        {
            return value as Percent?;
        }

        public static String ToCssString(this ICssValue value)
        {
            var primitive = value as CssString;

            if (primitive != null)
                return primitive.Value;

            return null;
        }

        public static String ToIdentifier(this ICssValue value)
        {
            var primitive = value as CssIdentifier;

            if (primitive != null)
                return primitive.Value;

            return null;
        }

        public static String ToAnimatableIdentifier(this ICssValue value)
        {
            var identifier = value.ToIdentifier();

            if (identifier != null && (identifier.Equals(Keywords.All, StringComparison.OrdinalIgnoreCase) || Factory.Properties.IsAnimatable(identifier)))
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
            var primitive = value as Number?;

            if (primitive != null && primitive.Value.IsInteger)
                return (Int32)primitive.Value.Value;

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

        public static Frequency? ToFrequency(this ICssValue value)
        {
            return value as Frequency?;
        }

        public static Length? ToLength(this ICssValue value)
        {
            var length = value as Length?;

            if (length == null)
            {
                var number = value as Number?;

                if (number.HasValue && number.Value == Number.Zero)
                    return Length.Zero;
            }

            return length;
        }

        public static Resolution? ToResolution(this ICssValue value)
        {
            return value as Resolution?;
        }

        public static Time? ToTime(this ICssValue value)
        {
            return value as Time?;
        }

        public static Length? ToImageBorderWidth(this ICssValue value)
        {
            if (value.Is(Keywords.Auto))
                return new Length(100f, Length.Unit.Percent);

            var multiple = value.ToSingle();

            if (multiple.HasValue)
                return new Length(multiple.Value * 100f, Length.Unit.Percent);

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
