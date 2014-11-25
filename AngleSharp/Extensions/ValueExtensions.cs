namespace AngleSharp.Extensions
{
    using AngleSharp.Css;
    using AngleSharp.DOM;
    using AngleSharp.DOM.Css;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    /// A bunch of useful extension methods.
    /// </summary>
    [DebuggerStepThrough]
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
        static readonly Dictionary<String, FontStyle> fontStyles = new Dictionary<String, FontStyle>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, FontStretch> fontStretches = new Dictionary<String, FontStretch>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, BreakMode> breakModes = new Dictionary<String, BreakMode>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, BreakMode> pageBreakModes = new Dictionary<String, BreakMode>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, BreakMode> breakInsideModes = new Dictionary<String, BreakMode>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, Single> horizontalModes = new Dictionary<String, Single>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, Single> verticalModes = new Dictionary<String, Single>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, UnicodeMode> unicodeBidis = new Dictionary<String, UnicodeMode>(StringComparer.OrdinalIgnoreCase);

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

            fontStyles.Add(Keywords.Normal, FontStyle.Normal);
            fontStyles.Add(Keywords.Italic, FontStyle.Italic);
            fontStyles.Add(Keywords.Oblique, FontStyle.Oblique);

            fontStretches.Add(Keywords.Normal, FontStretch.Normal);
            fontStretches.Add(Keywords.UltraCondensed, FontStretch.UltraCondensed);
            fontStretches.Add(Keywords.ExtraCondensed, FontStretch.ExtraCondensed);
            fontStretches.Add(Keywords.Condensed, FontStretch.Condensed);
            fontStretches.Add(Keywords.SemiCondensed, FontStretch.SemiCondensed);
            fontStretches.Add(Keywords.SemiExpanded, FontStretch.SemiExpanded);
            fontStretches.Add(Keywords.Expanded, FontStretch.Expanded);
            fontStretches.Add(Keywords.ExtraExpanded, FontStretch.ExtraExpanded);
            fontStretches.Add(Keywords.UltraExpanded, FontStretch.UltraExpanded);

            breakModes.Add(Keywords.Auto, BreakMode.Auto);
            breakModes.Add(Keywords.Always, BreakMode.Always);
            breakModes.Add(Keywords.Avoid, BreakMode.Avoid);
            breakModes.Add(Keywords.Left, BreakMode.Left);
            breakModes.Add(Keywords.Right, BreakMode.Right);
            breakModes.Add(Keywords.Page, BreakMode.Page);
            breakModes.Add(Keywords.Column, BreakMode.Column);
            breakModes.Add(Keywords.AvoidPage, BreakMode.AvoidPage);
            breakModes.Add(Keywords.AvoidColumn, BreakMode.AvoidColumn);

            pageBreakModes.Add(Keywords.Auto, BreakMode.Auto);
            pageBreakModes.Add(Keywords.Always, BreakMode.Always);
            pageBreakModes.Add(Keywords.Avoid, BreakMode.Avoid);
            pageBreakModes.Add(Keywords.Left, BreakMode.Left);
            pageBreakModes.Add(Keywords.Right, BreakMode.Right);

            breakInsideModes.Add(Keywords.Auto, BreakMode.Auto);
            breakInsideModes.Add(Keywords.Avoid, BreakMode.Avoid);
            breakInsideModes.Add(Keywords.AvoidPage, BreakMode.AvoidPage);
            breakInsideModes.Add(Keywords.AvoidColumn, BreakMode.AvoidColumn);
            breakInsideModes.Add(Keywords.AvoidRegion, BreakMode.AvoidRegion);

            horizontalModes.Add(Keywords.Left, 0f);
            horizontalModes.Add(Keywords.Center, 0.5f);
            horizontalModes.Add(Keywords.Right, 1f);

            verticalModes.Add(Keywords.Top, 0f);
            verticalModes.Add(Keywords.Center, 0.5f);
            verticalModes.Add(Keywords.Bottom, 1f);

            unicodeBidis.Add(Keywords.Normal, UnicodeMode.Normal);
            unicodeBidis.Add(Keywords.Embed, UnicodeMode.Embed);
            unicodeBidis.Add(Keywords.Isolate, UnicodeMode.Isolate);
            unicodeBidis.Add(Keywords.IsolateOverride, UnicodeMode.IsolateOverride);
            unicodeBidis.Add(Keywords.BidiOverride, UnicodeMode.BidiOverride);
            unicodeBidis.Add(Keywords.Plaintext, UnicodeMode.Plaintext);
        }

        #endregion

        #region Dictionary Lookups

        public static AnimationDirection? ToDirection(this CSSValue value)
        {
            return directions.GetValueOrDefault(value);
        }

        public static UnicodeMode? ToUnicodeBidi(this CSSValue value)
        {
            return unicodeBidis.GetValueOrDefault(value);
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

        public static BreakMode? ToBreakMode(this CSSValue value)
        {
            return breakModes.GetValueOrDefault(value);
        }

        public static BreakMode? ToBreakInsideMode(this CSSValue value)
        {
            return breakInsideModes.GetValueOrDefault(value);
        }

        public static BreakMode? ToPageBreakMode(this CSSValue value)
        {
            return pageBreakModes.GetValueOrDefault(value);
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

        public static FontStyle? ToFontStyle(this CSSValue value)
        {
            return fontStyles.GetValueOrDefault(value);
        }

        public static FontStretch? ToFontStretch(this CSSValue value)
        {
            return fontStretches.GetValueOrDefault(value);
        }

        public static FontVariant? ToFontVariant(this CSSValue value)
        {
            if (value.Is(Keywords.Normal))
                return FontVariant.Normal;
            else if (value.Is(Keywords.SmallCaps))
                return FontVariant.SmallCaps;

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

        public static List<T> AsList<T>(this CSSValue value, Func<CSSValue, T> transformer = null)
            where T : class
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

        #region Value Converters

        public static IValueConverter<LineStyle> WithLineStyle(this CSSProperty property)
        {
            return new DictionaryValueConverter<LineStyle>(lineStyles);
        }

        public static IValueConverter<TextDecorationStyle> WithDecorationStyle(this CSSProperty property)
        {
            return new DictionaryValueConverter<TextDecorationStyle>(decorationStyles);
        }

        public static IValueConverter<TextDecorationLine> WithDecorationLine(this CSSProperty property)
        {
            return new DictionaryValueConverter<TextDecorationLine>(decorationLines);
        }

        public static IValueConverter<Visibility> WithVisibility(this CSSProperty property)
        {
            return new DictionaryValueConverter<Visibility>(visibilities);
        }

        public static IValueConverter<UnicodeMode> WithUnicodeMode(this CSSProperty property)
        {
            return new DictionaryValueConverter<UnicodeMode>(unicodeBidis);
        }

        public static IValueConverter<T> From<T>(this CSSProperty property, Dictionary<String, T> values)
        {
            return new DictionaryValueConverter<T>(values);
        }

        public static IValueConverter<Angle> WithSideOrCorner(this CSSProperty property)
        {
            return new StructValueConverter<Angle>(ToSideOrCorner);
        }

        public static IValueConverter<Length> WithBorderWidth(this CSSProperty property)
        {
            return new StructValueConverter<Length>(ToBorderWidth);
        }

        public static IValueConverter<Length> WithLength(this CSSProperty property)
        {
            return new StructValueConverter<Length>(ToLength);
        }

        public static IValueConverter<Time> WithTime(this CSSProperty property)
        {
            return new StructValueConverter<Time>(ToTime);
        }

        public static IValueConverter<IDistance> WithDistance(this CSSProperty property)
        {
            return new ClassValueConverter<IDistance>(ToDistance);
        }

        public static IValueConverter<CssUrl> WithUrl(this CSSProperty property)
        {
            return new ClassValueConverter<CssUrl>(ToUri);
        }

        public static IValueConverter<String> WithString(this CSSProperty property)
        {
            return new ClassValueConverter<String>(ToCssString);
        }

        public static IValueConverter<String> WithIdentifier(this CSSProperty property)
        {
            return new ClassValueConverter<String>(ToIdentifier);
        }

        public static IValueConverter<String> WithAnimatableIdentifier(this CSSProperty property)
        {
            return new ClassValueConverter<String>(ToAnimatableIdentifier);
        }

        public static IValueConverter<CssAttr> WithAttr(this CSSProperty property)
        {
            return new FunctionValueConverter<CssAttr>(FunctionNames.Attr, property.WithString().Or(property.WithIdentifier()).To(m => new CssAttr(m)));
        }

        public static IValueConverter<T> WithArgs<T1, T>(this CSSProperty property, IValueConverter<T1> first, Int32 arguments, Func<T1[], T> converter)
        {
            return new ArgumentsValueConverter<T1>(first, arguments).To(converter);
        }

        public static IValueConverter<T> WithArgs<T1, T2, T>(this CSSProperty property, IValueConverter<T1> first, IValueConverter<T2> second, Func<Tuple<T1, T2>, T> converter)
        {
            return new ArgumentsValueConverter<T1, T2>(first, second).To(converter);
        }

        public static IValueConverter<T> WithArgs<T1, T2, T3, T>(this CSSProperty property, IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, Func<Tuple<T1, T2, T3>, T> converter)
        {
            return new ArgumentsValueConverter<T1, T2, T3>(first, second, third).To(converter);
        }

        public static IValueConverter<T> WithArgs<T1, T2, T3, T4, T>(this CSSProperty property, IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, Func<Tuple<T1, T2, T3, T4>, T> converter)
        {
            return new ArgumentsValueConverter<T1, T2, T3, T4>(first, second, third, fourth).To(converter);
        }

        public static IValueConverter<Tuple<T1, T2>> WithOptions<T1, T2>(this CSSProperty property, IValueConverter<T1> first, IValueConverter<T2> second, Tuple<T1, T2> defaults)
        {
            return new OptionsValueConverter<T1, T2>(first, second, defaults);
        }

        public static IValueConverter<Tuple<T1, T2, T3>> WithOptions<T1, T2, T3>(this CSSProperty property, IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, Tuple<T1, T2, T3> defaults)
        {
            return new OptionsValueConverter<T1, T2, T3>(first, second, third, defaults);
        }

        public static IValueConverter<Tuple<T1, T2, T3, T4>> WithOptions<T1, T2, T3, T4>(this CSSProperty property, IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, Tuple<T1, T2, T3, T4> defaults)
        {
            return new OptionsValueConverter<T1, T2, T3, T4>(first, second, third, fourth, defaults);
        }

        public static IValueConverter<TransitionFunction> WithTransition(this CSSProperty p)
        {
            return new DictionaryValueConverter<TransitionFunction>(timingFunctions).
                Or(new FunctionValueConverter<TransitionFunction>(FunctionNames.Steps,
                        p.WithInteger().To(m => (TransitionFunction)new StepsTransitionFunction(m)).Or(
                        p.WithArgs(p.WithInteger(), p.TakeOne(Keywords.Start, true).Or(p.TakeOne(Keywords.End, false)), m => (TransitionFunction)new StepsTransitionFunction(m.Item1, m.Item2))))).
                Or(new FunctionValueConverter<TransitionFunction>(FunctionNames.CubicBezier,
                        p.WithArgs(p.WithNumber(), p.WithNumber(), p.WithNumber(), p.WithNumber(), m => (TransitionFunction)new CubicBezierTransitionFunction(m.Item1, m.Item2, m.Item3, m.Item4))));
        }

        public static IValueConverter<Counter> WithCounter(this CSSProperty p)
        {
            return new FunctionValueConverter<Counter>(FunctionNames.Counter,
                        p.WithIdentifier().To(m => new Counter(m, Keywords.Decimal, null)).Or(
                        p.WithArgs(p.WithIdentifier(), p.WithIdentifier(), m => new Counter(m.Item1, m.Item2, null)))).
                Or(new FunctionValueConverter<Counter>(FunctionNames.Counters,
                        p.WithArgs(p.WithIdentifier(), p.WithString(), m => new Counter(m.Item1, Keywords.Decimal, m.Item2)).Or(
                        p.WithArgs(p.WithIdentifier(), p.WithString(), p.WithIdentifier(), m => new Counter(m.Item1, m.Item3, m.Item2)))));
        }

        public static IValueConverter<Int32> WithInteger(this CSSProperty property)
        {
            return new StructValueConverter<Int32>(ToInteger);
        }

        public static IValueConverter<Byte> WithByte(this CSSProperty property)
        {
            return new StructValueConverter<Byte>(ToByte);
        }

        public static IValueConverter<GradientStop[]> WithGradientStops(this CSSProperty property)
        {
            return new ClassValueConverter<GradientStop[]>(ToGradientStops);
        }

        public static IValueConverter<T> FirstArg<T>(this CSSProperty property, IValueConverter<T> converter)
        {
            return new SubsetValueConverter<T>(converter, 0, 1);
        }

        public static IValueConverter<T> RestArgs<T>(this CSSProperty property, IValueConverter<T> converter, Int32 start)
        {
            return new SubsetValueConverter<T>(converter, start, Int32.MaxValue);
        }

        public static IValueConverter<LinearGradient> WithLinearGradient(this CSSProperty p)
        {
            var args = p.FirstArg(p.WithAngle()).And(p.RestArgs(p.WithGradientStops(), 1)).
               Or(p.WithGradientStops().To(m => Tuple.Create(Angle.Zero, m)));

            return new FunctionValueConverter<LinearGradient>(FunctionNames.LinearGradient,
                        args.To(m => new LinearGradient(m.Item1, m.Item2, false))).
                Or(new FunctionValueConverter<LinearGradient>(FunctionNames.RepeatingLinearGradient,
                        args.To(m => new LinearGradient(m.Item1, m.Item2, true))));
        }

        public static IValueConverter<RadialGradient> WithRadialGradient(this CSSProperty p)
        {
            //TODO
            //Determine first argument (if any):
            // [ <ending-shape> || <size> ]? [ at <position> ]?
            //where:
            // <size> = [ <predefined> | <length> | [ <length> | <percentage> ]{2} ]
            // <ending-shape> = [ ellipse | circle ]
            // <predefined> = [ closest-side | closest-corner | farthest-side | farthest-corner ]
            var args = p.FirstArg(p.WithAngle()).And(p.RestArgs(p.WithGradientStops(), 1)).
               Or(p.WithGradientStops().To(m => Tuple.Create(Angle.Zero, m)));

            return new FunctionValueConverter<RadialGradient>(FunctionNames.RadialGradient,
                        args.To(m => new RadialGradient(Percent.Fifty, Percent.Fifty, Percent.Hundred, Percent.Hundred, m.Item2, false))).
                Or(new FunctionValueConverter<RadialGradient>(FunctionNames.RepeatingRadialGradient,
                        args.To(m => new RadialGradient(Percent.Fifty, Percent.Fifty, Percent.Hundred, Percent.Hundred, m.Item2, true))));
        }

        public static IValueConverter<CssImages> WithImages(this CSSProperty property)
        {
            return new FunctionValueConverter<CssImages>(FunctionNames.Image,
                        property.TakeList(property.WithUrl().To(m => new Url(m))).To(m => new CssImages(m)));
        }

        public static IValueConverter<Color> WithColor(this CSSProperty p)
        {
            const Single hnorm = 1f / 360f;

            return new StructValueConverter<Color>(ToColor).
                Or(new FunctionValueConverter<Color>(FunctionNames.Rgb,
                        p.WithArgs(p.WithByte(), p.WithByte(), p.WithByte(), m => new Color(m.Item1, m.Item2, m.Item3)))).
                Or(new FunctionValueConverter<Color>(FunctionNames.Rgba,
                        p.WithArgs(p.WithByte(), p.WithByte(), p.WithByte(), p.WithNumber().Constraint(m => m >= 0f && m <= 1f), m => new Color(m.Item1, m.Item2, m.Item3, m.Item4)))).
                Or(new FunctionValueConverter<Color>(FunctionNames.Hsl,
                        p.WithArgs(p.WithNumber(), p.WithPercent(), p.WithPercent(), m => Color.FromHsl(hnorm * m.Item1, m.Item2.NormalizedValue, m.Item3.NormalizedValue)))).
                Or(new FunctionValueConverter<Color>(FunctionNames.Hsla,
                        p.WithArgs(p.WithNumber(), p.WithPercent(), p.WithPercent(), p.WithNumber().Constraint(m => m >= 0f && m <= 1f), m => Color.FromHsla(hnorm * m.Item1, m.Item2.NormalizedValue, m.Item3.NormalizedValue, m.Item4))));
        }

        public static IValueConverter<ITransform> WithTransform(this CSSProperty p)
        {
            return new FunctionValueConverter<ITransform>(FunctionNames.Matrix,
                        p.WithArgs(p.WithNumber(), 6, m => (ITransform)new MatrixTransform(m[0], m[1], m[2], m[3], m[4], m[5]))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.Matrix3d,
                        p.WithArgs(p.WithNumber(), 12, m => (ITransform)new Matrix3DTransform(m[0], m[1], m[2], m[3], m[4], m[5], m[6], m[7], m[8], m[9], m[10], m[11])))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.Translate, //TODO 2nd arg. optional => Length.Zero
                        p.WithArgs(p.WithDistance(), 2, m => (ITransform)new TranslateTransform(m[0], m[1])))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.Translate3d, //TODO 2nd and 3rd arg. optional => Length.Zero
                        p.WithArgs(p.WithDistance(), 3, m => (ITransform)new Translate3DTransform(m[0], m[1], m[2])))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.TranslateX,
                        p.WithDistance().To(m => (ITransform)new TranslateXTransform(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.TranslateY,
                        p.WithDistance().To(m => (ITransform)new TranslateYTransform(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.TranslateZ,
                        p.WithDistance().To(m => (ITransform)new TranslateZTransform(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.Scale, //TODO 2nd arg. optional => same as 1st
                        p.WithArgs(p.WithNumber(), 2, m => (ITransform)new ScaleTransform(m[0], m[1])))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.Scale3d, //TODO if only 1 arg. => all three same
                        p.WithArgs(p.WithNumber(), 3, m => (ITransform)new Scale3DTransform(m[0], m[1], m[2])))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.ScaleX,
                        p.WithNumber().To(m => (ITransform)new ScaleXTransform(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.ScaleY,
                        p.WithNumber().To(m => (ITransform)new ScaleYTransform(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.ScaleZ,
                        p.WithNumber().To(m => (ITransform)new ScaleZTransform(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.Rotate,
                        p.WithAngle().To(m => (ITransform)new RotateTransform(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.Rotate3d,
                        p.WithArgs(p.WithNumber(), p.WithNumber(), p.WithNumber(), p.WithAngle(), m => (ITransform)new Rotate3DTransform(m.Item1, m.Item2, m.Item3, m.Item4)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.RotateX,
                        p.WithAngle().To(m => (ITransform)Rotate3DTransform.RotateX(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.RotateY,
                        p.WithAngle().To(m => (ITransform)Rotate3DTransform.RotateY(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.RotateZ,
                        p.WithAngle().To(m => (ITransform)Rotate3DTransform.RotateZ(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.Skew,
                        p.WithArgs(p.WithAngle(), 2, m => (ITransform)new SkewTransform(m[0], m[1])))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.SkewX,
                        p.WithAngle().To(m => (ITransform)new SkewXTransform(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.SkewY,
                       p.WithAngle().To(m => (ITransform)new SkewYTransform(m))));
        }

        public static IValueConverter<Boolean> Toggle(this CSSProperty property, String on, String off)
        {
            return property.TakeOne(on, true).Or(property.TakeOne(off, false));
        }

        public static IValueConverter<Tuple<Length, LineStyle, Color>> ValidateBorderPart(this CSSProperty property)
        {
            return property.WithOptions(property.WithBorderWidth(), property.WithLineStyle(), property.WithColor(), Tuple.Create(Length.Medium, LineStyle.None, Color.Transparent));
        }

        public static IValueConverter<Shape> WithShape(this CSSProperty p)
        {
            return new FunctionValueConverter<Shape>(FunctionNames.Rect,
                       p.WithArgs(p.WithLength(), p.WithLength(), p.WithLength(), p.WithLength(), m => new Shape(m.Item1, m.Item2, m.Item3, m.Item4)));
        }

        public static IValueConverter<Angle> WithAngle(this CSSProperty property)
        {
            return new StructValueConverter<Angle>(ToAngle);
        }

        public static IValueConverter<Single> WithNumber(this CSSProperty property)
        {
            return new StructValueConverter<Single>(ToSingle);
        }

        public static IValueConverter<Percent> WithPercent(this CSSProperty property)
        {
            return new StructValueConverter<Percent>(ToPercent);
        }

        public static IValueConverter<T> TakeOne<T>(this CSSProperty property, String identifier, T result)
        {
            return new IdentifierValueConverter<T>(identifier, result);
        }

        public static IValueConverter<T[]> TakeMany<T>(this CSSProperty property, IValueConverter<T> converter)
        {
            return new OneOrMoreValueConverter<T>(converter);
        }

        public static IValueConverter<T[]> TakeList<T>(this CSSProperty property, params IValueConverter<T>[] converters)
        {
            return new ListValueConverter<T>(converters);
        }

        #endregion

        #region Transformation Classes

        /// <summary>
        /// Represents the matrix transformation.
        /// </summary>
        sealed class MatrixTransform : ITransform, ICssObject
        {
            readonly TransformMatrix _matrix;

            internal MatrixTransform(Single m11, Single m12, Single m21, Single m22, Single tx, Single ty)
            {
                _matrix = new TransformMatrix(m11, m12, 0f, m21, m22, 0f, 0f, 0f, 1f, tx, ty, 0f);
            }

            /// <summary>
            /// Returns the matrix transformation.
            /// </summary>
            /// <returns>The stored matrix.</returns>
            public TransformMatrix ComputeMatrix()
            {
                return _matrix;
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Matrix, _matrix.M11.ToString(CultureInfo.InvariantCulture), _matrix.M12.ToString(CultureInfo.InvariantCulture), _matrix.M21.ToString(CultureInfo.InvariantCulture), _matrix.M22.ToString(CultureInfo.InvariantCulture), _matrix.Tx.ToString(CultureInfo.InvariantCulture), _matrix.Ty.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Represents the matrix3d transformation.
        /// </summary>
        sealed class Matrix3DTransform : ITransform, ICssObject
        {
            readonly TransformMatrix _matrix;

            internal Matrix3DTransform(Single m11, Single m12, Single m13, Single m21, Single m22, Single m23, Single m31, Single m32, Single m33, Single tx, Single ty, Single tz)
            {
                _matrix = new TransformMatrix(m11, m12, m13, m21, m22, m23, m31, m32, m33, tx, ty, tz);
            }

            /// <summary>
            /// Returns the stored matrix.
            /// </summary>
            /// <returns>The current transformation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                return _matrix;
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return _matrix.ToCss();
            }
        }

        /// <summary>
        /// Represents the translate transformation.
        /// </summary>
        sealed class TranslateTransform : ITransform, ICssObject
        {
            readonly IDistance _y;
            readonly IDistance _x;

            internal TranslateTransform(IDistance x, IDistance y)
            {
                _x = x;
                _y = y;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var dx = _x.ToPixel();
                var dy = _y.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, dx, dy, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Translate, _x.ToCss(), _y.ToCss());
            }
        }

        /// <summary>
        /// Represents the translate-x transformation.
        /// </summary>
        sealed class TranslateXTransform : ITransform, ICssObject
        {
            readonly IDistance _x;

            internal TranslateXTransform(IDistance x)
            {
                _x = x;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var dx = _x.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, dx, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.TranslateX, _x.ToCss());
            }
        }

        /// <summary>
        /// Represents the translate-y transformation.
        /// </summary>
        sealed class TranslateYTransform : ITransform, ICssObject
        {
            readonly IDistance _y;

            internal TranslateYTransform(IDistance y)
            {
                _y = y;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var dy = _y.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, dy, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.TranslateY, _y.ToCss());
            }
        }

        /// <summary>
        /// Represents the translate-z transformation.
        /// </summary>
        sealed class TranslateZTransform : ITransform, ICssObject
        {
            readonly IDistance _z;

            internal TranslateZTransform(IDistance z)
            {
                _z = z;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var dz = _z.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, dz);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.TranslateZ, _z.ToCss());
            }
        }

        /// <summary>
        /// Represents the translate3d transformation.
        /// </summary>
        sealed class Translate3DTransform : ITransform, ICssObject
        {
            readonly IDistance _x;
            readonly IDistance _y;
            readonly IDistance _z;

            internal Translate3DTransform(IDistance x, IDistance y, IDistance z)
            {
                _x = x;
                _y = y;
                _z = z;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var dx = _x.ToPixel();
                var dy = _y.ToPixel();
                var dz = _z.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, dx, dy, dz);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Translate3d, _x.ToCss(), _y.ToCss(), _z.ToCss());
            }
        }

        /// <summary>
        /// Represents the rotate transformation.
        /// </summary>
        sealed class RotateTransform : ITransform, ICssObject
        {
            readonly Angle _angle;

            internal RotateTransform(Angle angle)
            {
                _angle = angle;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var sina = _angle.Sin();
                var cosa = _angle.Cos();
                return new TransformMatrix(cosa, sina, 0f, -sina, cosa, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Rotate, _angle.ToCss());
            }
        }

        /// <summary>
        /// Represents the rotate3d transformation.
        /// </summary>
        sealed class Rotate3DTransform : ITransform, ICssObject
        {
            readonly Single _x;
            readonly Single _y;
            readonly Single _z;
            readonly Angle _angle;

            internal Rotate3DTransform(Single x, Single y, Single z, Angle angle)
            {
                _x = x;
                _y = y;
                _z = z;
                _angle = angle;
            }

            /// <summary>
            /// Constructs a rotate 3D transformation around the x-axis.
            /// </summary>
            /// <param name="angle">The angle to rotate.</param>
            /// <returns>The rotate 3D transformation.</returns>
            public static Rotate3DTransform RotateX(Angle angle)
            {
                return new Rotate3DTransform(1f, 0f, 0f, angle);
            }

            /// <summary>
            /// Constructs a rotate 3D transformation around the y-axis.
            /// </summary>
            /// <param name="angle">The angle to rotate.</param>
            /// <returns>The rotate 3D transformation.</returns>
            public static Rotate3DTransform RotateY(Angle angle)
            {
                return new Rotate3DTransform(0f, 1f, 0f, angle);
            }

            /// <summary>
            /// Constructs a rotate 3D transformation around the z-axis.
            /// </summary>
            /// <param name="angle">The angle to rotate.</param>
            /// <returns>The rotate 3D transformation.</returns>
            public static Rotate3DTransform RotateZ(Angle angle)
            {
                return new Rotate3DTransform(0f, 0f, 1f, angle);
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var norm = 1f / (Single)Math.Sqrt(_x * _x + _y * _y + _z * _z);
                var sina = _angle.Sin();
                var cosa = _angle.Cos();
                var l = _x * norm;
                var m = _y * norm;
                var n = _z * norm;
                var omc = (1f - cosa);
                return new TransformMatrix(
                    l * l * omc + cosa, m * l * omc - n * sina, n * l * omc + m * sina,
                    l * m * omc + n * sina, m * m * omc + cosa, n * m * omc - l * sina,
                    l * n * omc - m * sina, m * n * omc + l * sina, n * n * omc + cosa,
                    0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Rotate3d, _x.ToString(CultureInfo.InvariantCulture), _y.ToString(CultureInfo.InvariantCulture), _z.ToString(CultureInfo.InvariantCulture), _angle.ToCss());
            }
        }

        /// <summary>
        /// Represents the scale transformation.
        /// </summary>
        sealed class ScaleTransform : ITransform, ICssObject
        {
            readonly Single _sx;
            readonly Single _sy;

            internal ScaleTransform(Single scale)
            {
                _sx = scale;
                _sy = scale;
            }

            internal ScaleTransform(Single sx, Single sy)
            {
                _sx = sx;
                _sy = sy;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(_sx, 0f, 0f, 0f, _sy, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                if (_sx == _sy)
                    return FunctionNames.Build(FunctionNames.Scale, _sx.ToString(CultureInfo.InvariantCulture));

                return FunctionNames.Build(FunctionNames.Scale, _sx.ToString(CultureInfo.InvariantCulture), _sy.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Represents the scale-x transformation.
        /// </summary>
        sealed class ScaleXTransform : ITransform, ICssObject
        {
            readonly Single _scale;

            internal ScaleXTransform(Single scale)
            {
                _scale = scale;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(_scale, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.ScaleX, _scale.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Represents the scale-y transformation.
        /// </summary>
        sealed class ScaleYTransform : ITransform, ICssObject
        {
            readonly Single _scale;

            internal ScaleYTransform(Single scale)
            {
                _scale = scale;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(1f, 0f, 0f, 0f, _scale, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.ScaleY, _scale.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Represents the scale-z transformation.
        /// </summary>
        sealed class ScaleZTransform : ITransform, ICssObject
        {
            readonly Single _scale;

            internal ScaleZTransform(Single scale)
            {
                _scale = scale;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, _scale, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.ScaleZ, _scale.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Represents the scale3d transformation.
        /// </summary>
        sealed class Scale3DTransform : ITransform, ICssObject
        {
            readonly Single _sx;
            readonly Single _sy;
            readonly Single _sz;

            internal Scale3DTransform(Single sx, Single sy, Single sz)
            {
                _sx = sx;
                _sy = sy;
                _sz = sz;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(_sx, 0f, 0f, 0f, _sy, 0f, 0f, 0f, _sz, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Scale3d, _sx.ToString(CultureInfo.InvariantCulture), _sy.ToString(CultureInfo.InvariantCulture), _sz.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Represents the skew transformation.
        /// </summary>
        sealed class SkewTransform : ITransform, ICssObject
        {
            readonly Angle _alpha;
            readonly Angle _beta;

            internal SkewTransform(Angle alpha, Angle beta)
            {
                _alpha = alpha;
                _beta = beta;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var a = _alpha.Tan();
                var b = _beta.Tan();
                return new TransformMatrix(1f, a, 0f, b, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Skew, _alpha.ToCss(), _beta.ToCss());
            }
        }

        /// <summary>
        /// Represents the skew-x transformation.
        /// </summary>
        sealed class SkewXTransform : ITransform, ICssObject
        {
            readonly Angle _angle;

            internal SkewXTransform(Angle alpha)
            {
                _angle = alpha;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var a = _angle.Tan();
                return new TransformMatrix(1f, a, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.SkewX, _angle.ToCss());
            }
        }

        /// <summary>
        /// Represents the skew-y transformation.
        /// </summary>
        sealed class SkewYTransform : ITransform, ICssObject
        {
            readonly Angle _angle;

            internal SkewYTransform(Angle beta)
            {
                _angle = beta;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public TransformMatrix ComputeMatrix()
            {
                var b = _angle.Tan();
                return new TransformMatrix(1f, 0f, 0f, b, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public String ToCss()
            {
                return FunctionNames.Build(FunctionNames.SkewY, _angle.ToCss());
            }
        }

        #endregion
    }
}
