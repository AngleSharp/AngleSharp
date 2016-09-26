namespace AngleSharp.Css
{
	using AngleSharp.Css.ValueConverters;
	using AngleSharp.Css.Values;
	using AngleSharp.Extensions;
	using System;
	using System.Linq;

	/// <summary>
	/// A set of already constructed CSS value converters.
	/// </summary>
	static class Converters
    {
        #region Elementary

        /// <summary>
        /// Represents a length object with line-width additions.
        /// http://dev.w3.org/csswg/css-backgrounds/#line-width
        /// </summary>
        public static readonly IValueConverter LineWidthConverter = new StructValueConverter<Length>(ValueExtensions.ToBorderWidth);

        /// <summary>
        /// Represents a length object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/length
        /// </summary>
        public static readonly IValueConverter LengthConverter = new StructValueConverter<Length>(ValueExtensions.ToLength);

        /// <summary>
        /// Represents a resolution object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/resolution
        /// </summary>
        public static readonly IValueConverter ResolutionConverter = new StructValueConverter<Resolution>(ValueExtensions.ToResolution);

        /// <summary>
        /// Represents a frequency object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/frequency
        /// </summary>
        public static readonly IValueConverter FrequencyConverter = new StructValueConverter<Frequency>(ValueExtensions.ToFrequency);

        /// <summary>
        /// Represents a time object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/time
        /// </summary>
        public static readonly IValueConverter TimeConverter = new StructValueConverter<Time>(ValueExtensions.ToTime);

        /// <summary>
        /// Represents an URL object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/uri
        /// </summary>
        public static readonly IValueConverter UrlConverter = new UrlValueConverter();

        /// <summary>
        /// Represents a string object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/string
        /// </summary>
        public static readonly IValueConverter StringConverter = new StringValueConverter();

        /// <summary>
        /// Represents many string objects, but always divisible by 2.
        /// </summary>
        public static readonly IValueConverter EvenStringsConverter = new StringsValueConverter();

        /// <summary>
        /// Represents a string object from many identifiers.
        /// </summary>
        public static readonly IValueConverter LiteralsConverter = new IdentifierValueConverter(ValueExtensions.ToLiterals);

        /// <summary>
        /// Represents an identifier object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/user-ident
        /// </summary>
        public static readonly IValueConverter IdentifierConverter = new IdentifierValueConverter(ValueExtensions.ToIdentifier);

        /// <summary>
        /// Represents an identifier object that matches the production rules of a single transition property.
        /// http://dev.w3.org/csswg/css-transitions/#single-transition-property
        /// </summary>
        public static readonly IValueConverter AnimatableConverter = new IdentifierValueConverter(ValueExtensions.ToAnimatableIdentifier);

        /// <summary>
        /// Represents an integer object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/integer
        /// </summary>
        public static readonly IValueConverter IntegerConverter = new StructValueConverter<Int32>(ValueExtensions.ToInteger);

        /// <summary>
        /// Represents an integer object that is zero or greater.
        /// </summary>
        public static readonly IValueConverter NaturalIntegerConverter = new StructValueConverter<Int32>(ValueExtensions.ToNaturalInteger);

        /// <summary>
        /// Represents an integer object that only allows values \in { 100, 200, ..., 900 }.
        /// </summary>
        public static readonly IValueConverter WeightIntegerConverter = new StructValueConverter<Int32>(ValueExtensions.ToWeightInteger);

        /// <summary>
        /// Represents an integer object that is greater tha zero.
        /// </summary>
        public static readonly IValueConverter PositiveIntegerConverter = new StructValueConverter<Int32>(ValueExtensions.ToPositiveInteger);

        /// <summary>
        /// Represents an integer object with 0 or 1.
        /// </summary>
        public static readonly IValueConverter BinaryConverter = new StructValueConverter<Int32>(ValueExtensions.ToBinary);

        /// <summary>
        /// Represents an angle object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/angle
        /// </summary>
        public static readonly IValueConverter AngleConverter = new StructValueConverter<Angle>(ValueExtensions.ToAngle);

        /// <summary>
        /// Represents a number object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/number
        /// </summary>
        public static readonly IValueConverter NumberConverter = new StructValueConverter<Single>(ValueExtensions.ToSingle);

        /// <summary>
        /// Represents an number object that is zero or greater.
        /// </summary>
        public static readonly IValueConverter NaturalNumberConverter = new StructValueConverter<Single>(ValueExtensions.ToNaturalSingle);

		/// <summary>
		/// Represents a percentage object.
		/// https://developer.mozilla.org/en-US/docs/Web/CSS/percentage
		/// </summary>
		public static readonly IValueConverter PercentConverter = new StructValueConverter<Percent>(ValueExtensions.ToPercent);

        /// <summary>
        /// Represents an integer object reduced to [0, 255] or percent.
        /// https://drafts.csswg.org/css-color/#rgb-functions
        /// </summary>
        public static readonly IValueConverter RgbComponentConverter = new StructValueConverter<Byte>(ValueExtensions.ToRgbComponent);

        /// <summary>
        /// Represents an number reduced to [0, 1] or percent.
        /// https://drafts.csswg.org/css-color/#rgb-functions
        /// </summary>
        public static readonly IValueConverter AlphaValueConverter = new StructValueConverter<Single>(ValueExtensions.ToAlphaValue);

        /// <summary>
        /// Represents an color object (usually hex or name).
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color
        /// </summary>
        public static readonly IValueConverter PureColorConverter = new StructValueConverter<Color>(ValueExtensions.ToColor);

        /// <summary>
        /// Represents a distance object (either Length or Percent).
        /// </summary>
        public static readonly IValueConverter LengthOrPercentConverter = new StructValueConverter<Length>(ValueExtensions.ToDistance);

        /// <summary>
        /// Represents an number object that is a valid angle number.
        /// </summary>
        public static readonly IValueConverter AngleNumberConverter = new StructValueConverter<Angle>(ValueExtensions.ToAngleNumber);

        #endregion

        #region Functions

        /// <summary>
        /// Represents the angle to a side or corner of a box.
        /// http://dev.w3.org/csswg/css-images-3/#typedef-side-or-corner
        /// </summary>
        public static readonly IValueConverter SideOrCornerConverter = WithAny(
            Assign(Keywords.Left, -1.0).Or(Keywords.Right, 1.0).Option(0.0),
            Assign(Keywords.Top, 1.0).Or(Keywords.Bottom, -1.0).Option(0.0)
        );

        /// <summary>
        /// Represents a position object.
        /// http://www.w3.org/TR/css3-background/#ltpositiongt
        /// </summary>
        public static readonly IValueConverter PointConverter = Construct(() =>
        {
            var hi = Assign(Keywords.Left, Length.Zero).Or(Keywords.Right, new Length(100f, Length.Unit.Percent)).Or(Keywords.Center, new Length(50f, Length.Unit.Percent));
            var vi = Assign(Keywords.Top, Length.Zero).Or(Keywords.Bottom, new Length(100f, Length.Unit.Percent)).Or(Keywords.Center, new Length(50f, Length.Unit.Percent));
            var h = hi.Or(LengthOrPercentConverter).Required();
            var v = vi.Or(LengthOrPercentConverter).Required();

            return LengthOrPercentConverter.Or(
                   Toggle(Keywords.Left, Keywords.Right)).Or(
                   Toggle(Keywords.Top, Keywords.Bottom)).Or(
                   Keywords.Center, Point.Center).Or(
                   WithOrder(h, v)).Or(
                   WithOrder(v, h)).Or(
                   WithOrder(hi, vi, LengthOrPercentConverter)).Or(
                   WithOrder(hi, LengthOrPercentConverter, vi)).Or(
                   WithOrder(hi, LengthOrPercentConverter, vi, LengthOrPercentConverter));
        });

        /// <summary>
        /// Represents an attribute retriever object.
        /// http://dev.w3.org/csswg/css-values/#funcdef-attr
        /// </summary>
        public static readonly IValueConverter AttrConverter = new FunctionValueConverter(
            FunctionNames.Attr, WithArgs(StringConverter.Or(IdentifierConverter)));

        /// <summary>
        /// Represents a steps timing-function object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/timing-function
        /// </summary>
        public static readonly IValueConverter StepsConverter = new FunctionValueConverter(
            FunctionNames.Steps, WithArgs(
                IntegerConverter.Required(), 
                Assign(Keywords.Start, true).Or(Keywords.End, false).Option(false)));

        /// <summary>
        /// Represents a cubic-bezier timing-function object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/timing-function
        /// </summary>
        public static readonly IValueConverter CubicBezierConverter = Construct(() =>
        {
            var number = NumberConverter.Required();
            return new FunctionValueConverter(FunctionNames.CubicBezier,
                    WithArgs(number, number, number, number));
        });

        /// <summary>
        /// Represents a counter object.
        /// http://www.w3.org/TR/CSS2/syndata.html#value-def-counter
        /// </summary>
        public static readonly IValueConverter CounterConverter = Construct(() =>
        {
            var name = IdentifierConverter.Required();
            var kind = IdentifierConverter.Option(Keywords.Decimal);
            var def = StringConverter.Required();
            return new FunctionValueConverter(FunctionNames.Counter,
                        WithArgs(name, kind).Or(
                   new FunctionValueConverter(FunctionNames.Counters,
                        WithArgs(name, def, kind))));
        });

        /// <summary>
        /// Represents a shape object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/shape
        /// </summary>
        public static readonly IValueConverter ShapeConverter = Construct(() =>
        {
            var length = LengthConverter.Required();
            return new FunctionValueConverter(FunctionNames.Rect,
                        WithArgs(length, length, length, length).Or(
                        WithArgs(LengthConverter.Many(4, 4))));
        }).OrAuto();

        /// <summary>
        /// Represents a linear-gradient object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/linear-gradient
        /// </summary>
        public static readonly IValueConverter LinearGradientConverter = Construct(() =>
        {
            return new FunctionValueConverter(FunctionNames.LinearGradient, new LinearGradientConverter(false)).Or(
                   new FunctionValueConverter(FunctionNames.RepeatingLinearGradient, new LinearGradientConverter(true)));
        });

        /// <summary>
        /// Represents a radial-gradient object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/radial-gradient
        /// </summary>
        public static readonly IValueConverter RadialGradientConverter = Construct(() =>
        {            
            return new FunctionValueConverter(FunctionNames.RadialGradient, new RadialGradientConverter(false)).Or(
                   new FunctionValueConverter(FunctionNames.RepeatingRadialGradient, new RadialGradientConverter(true)));
        });

        /// <summary>
        /// Represents a color object (RGB function).
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color_value
        /// </summary>
        public static readonly IValueConverter RgbColorConverter = Construct(() =>
        {
            var number = RgbComponentConverter.Required();
            return new FunctionValueConverter(FunctionNames.Rgb, WithArgs(number, number, number));
        });

        /// <summary>
        /// Represents a color object (RGBA function).
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color_value
        /// </summary>
        public static readonly IValueConverter RgbaColorConverter = Construct(() =>
        {
            var value = RgbComponentConverter.Required();
            var alpha = AlphaValueConverter.Required();
            return new FunctionValueConverter(FunctionNames.Rgba, WithArgs(value, value, value, alpha));
        });

        /// <summary>
        /// Represents a color object (HSL function).
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color_value
        /// </summary>
        public static readonly IValueConverter HslColorConverter = Construct(() =>
        {
            var hue = AngleNumberConverter.Required();
            var percent = PercentConverter.Required();
            return new FunctionValueConverter(FunctionNames.Hsl, WithArgs(hue, percent, percent));
        });

        /// <summary>
        /// Represents a color object (HSLA function).
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color_value
        /// </summary>
        public static readonly IValueConverter HslaColorConverter = Construct(() =>
        {
            var hue = AngleNumberConverter.Required();
            var percent = PercentConverter.Required();
            var alpha = AlphaValueConverter.Required();
            return new FunctionValueConverter(FunctionNames.Hsla, WithArgs(hue, percent, percent, alpha));
        });

        /// <summary>
        /// Represents a color object (GRAY function).
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color_value
        /// </summary>
        public static readonly IValueConverter GrayColorConverter = Construct(() =>
        {
            var value = RgbComponentConverter.Required();
            var alpha = AlphaValueConverter.Option(1f);
            return new FunctionValueConverter(FunctionNames.Gray, WithArgs(value, alpha));
        });

        /// <summary>
        /// Represents a color object (HWB function).
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color_value
        /// </summary>
        public static readonly IValueConverter HwbColorConverter = Construct(() =>
        {
            var hue = AngleNumberConverter.Required();
            var percent = PercentConverter.Required();
            var alpha = AlphaValueConverter.Option(1f);
            return new FunctionValueConverter(FunctionNames.Hwb, WithArgs(hue, percent, percent, alpha));
        });

        /// <summary>
        /// A perspective for 3D transformations.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-perspective
        /// </summary>
        public static readonly IValueConverter PerspectiveConverter = Construct(() =>
        {
            return new FunctionValueConverter(FunctionNames.Perspective, WithArgs(LengthConverter));
        });

        /// <summary>
        /// Sets the transformation matrix explicitly.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-matrix3d
        /// </summary>
        public static readonly IValueConverter MatrixTransformConverter = Construct(() =>
        {
            return new FunctionValueConverter(FunctionNames.Matrix, WithArgs(NumberConverter, 6)).Or(
                   new FunctionValueConverter(FunctionNames.Matrix3d, WithArgs(NumberConverter, 16)));
        });

        /// <summary>
        /// A broad variety of translate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-translate3d
        /// </summary>
        public static readonly IValueConverter TranslateTransformConverter = Construct(() =>
        {
            var distance = LengthOrPercentConverter.Required();
            var option = LengthOrPercentConverter.Option(Length.Zero);
            return new FunctionValueConverter(FunctionNames.Translate, WithArgs(distance, option)).Or(
                   new FunctionValueConverter(FunctionNames.Translate3d, WithArgs(distance, option, option))).Or(
                   new FunctionValueConverter(FunctionNames.TranslateX, WithArgs(LengthOrPercentConverter))).Or(
                   new FunctionValueConverter(FunctionNames.TranslateY, WithArgs(LengthOrPercentConverter))).Or(
                   new FunctionValueConverter(FunctionNames.TranslateZ, WithArgs(LengthOrPercentConverter)));
        });

        /// <summary>
        /// A broad variety of scale transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-scale3d
        /// </summary>
        public static readonly IValueConverter ScaleTransformConverter = Construct(() =>
        {
            var number = NumberConverter.Required();
            var option = NumberConverter.Option(Single.NaN);
            return new FunctionValueConverter(FunctionNames.Scale, WithArgs(number, option)).Or(
                   new FunctionValueConverter(FunctionNames.Scale3d, WithArgs(number, option, option))).Or(
                   new FunctionValueConverter(FunctionNames.ScaleX, WithArgs(NumberConverter))).Or(
                   new FunctionValueConverter(FunctionNames.ScaleY, WithArgs(NumberConverter))).Or(
                   new FunctionValueConverter(FunctionNames.ScaleZ, WithArgs(NumberConverter)));
        });

        /// <summary>
        /// A broad variety of rotate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-rotate3d
        /// </summary>
        public static readonly IValueConverter RotateTransformConverter = Construct(() =>
        {
            var number = NumberConverter.Required();
            return new FunctionValueConverter(FunctionNames.Rotate, WithArgs(AngleConverter)).Or(
                   new FunctionValueConverter(FunctionNames.Rotate3d, WithArgs(number, number, number, AngleConverter.Required()))).Or(
                   new FunctionValueConverter(FunctionNames.RotateX, WithArgs(AngleConverter))).Or(
                   new FunctionValueConverter(FunctionNames.RotateY, WithArgs(AngleConverter))).Or(
                   new FunctionValueConverter(FunctionNames.RotateZ, WithArgs(AngleConverter)));
        });

        /// <summary>
        /// A broad variety of skew transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-skew
        /// </summary>
        public static readonly IValueConverter SkewTransformConverter = Construct(() =>
        {
            var angle = AngleConverter.Required();
            return new FunctionValueConverter(FunctionNames.Skew, WithArgs(angle, angle)).Or(
                   new FunctionValueConverter(FunctionNames.SkewX, WithArgs(AngleConverter))).Or(
                   new FunctionValueConverter(FunctionNames.SkewY, WithArgs(AngleConverter)));
        });

        #endregion

        #region Maps

        /// <summary>
        /// Represents a converter for the default font families.
        /// </summary>
        public static readonly IValueConverter DefaultFontFamiliesConverter = Map.DefaultFontFamilies.ToConverter();

        /// <summary>
        /// Represents a converter for the LineStyle enumeration.
        /// </summary>
        public static readonly IValueConverter LineStyleConverter = Map.LineStyles.ToConverter();

        /// <summary>
        /// Represents a converter for the BackgroundAttachment enumeration.
        /// </summary>
        public static readonly IValueConverter BackgroundAttachmentConverter = Map.BackgroundAttachments.ToConverter();

        /// <summary>
        /// Represents a converter for the BackgroundRepeat enumeration.
        /// </summary>
        public static readonly IValueConverter BackgroundRepeatConverter = Map.BackgroundRepeats.ToConverter();

        /// <summary>
        /// Represents a converter for the BoxModel enumeration.
        /// </summary>
        public static readonly IValueConverter BoxModelConverter = Map.BoxModels.ToConverter();

        /// <summary>
        /// Represents a converter for the AnimationDirection enumeration.
        /// </summary>
        public static readonly IValueConverter AnimationDirectionConverter = Map.AnimationDirections.ToConverter();

        /// <summary>
        /// Represents a converter for the AnimationFillStyle enumeration.
        /// </summary>
        public static readonly IValueConverter AnimationFillStyleConverter = Map.AnimationFillStyles.ToConverter();

        /// <summary>
        /// Represents a converter for the TextDecorationStyle enumeration.
        /// </summary>
        public static readonly IValueConverter TextDecorationStyleConverter = Map.TextDecorationStyles.ToConverter();

        /// <summary>
        /// Represents a converter for the TextDecorationLine enumeration, 
        /// taking many values or none.
        /// </summary>
        public static readonly IValueConverter TextDecorationLinesConverter = Map.TextDecorationLines.ToConverter().Many().OrNone();

        /// <summary>
        /// Represents a converter for the ListPosition enumeration.
        /// </summary>
        public static readonly IValueConverter ListPositionConverter = Map.ListPositions.ToConverter();

        /// <summary>
        /// Represents a converter for the ListStyle enumeration.
        /// </summary>
        public static readonly IValueConverter ListStyleConverter = Map.ListStyles.ToConverter();

        /// <summary>
        /// Represents a converter for the BreakMode enumeration.
        /// </summary>
        public static readonly IValueConverter BreakModeConverter = Map.BreakModes.ToConverter();

        /// <summary>
        /// Represents a converter for the BreakMode enumeration (constraint to the inside values).
        /// </summary>
        public static readonly IValueConverter BreakInsideModeConverter = Map.BreakInsideModes.ToConverter();

        /// <summary>
        /// Represents a converter for the BreakMode enumeration (constraint to the page values).
        /// </summary>
        public static readonly IValueConverter PageBreakModeConverter = Map.PageBreakModes.ToConverter();

        /// <summary>
        /// Represents a converter for the UnicodeMode enumeration.
        /// </summary>
        public static readonly IValueConverter UnicodeModeConverter = Map.UnicodeModes.ToConverter();

        /// <summary>
        /// Represents a converter for the Visibility enumeration.
        /// </summary>
        public static readonly IValueConverter VisibilityConverter = Map.Visibilities.ToConverter();

        /// <summary>
        /// Represents a converter for the PlayState enumeration.
        /// </summary>
        public static readonly IValueConverter PlayStateConverter = Map.PlayStates.ToConverter();

        /// <summary>
        /// Represents a converter for the FontVariant enumeration.
        /// </summary>
        public static readonly IValueConverter FontVariantConverter = Map.FontVariants.ToConverter();

        /// <summary>
        /// Represents a converter for the DirectionMode enumeration.
        /// </summary>
        public static readonly IValueConverter DirectionModeConverter = Map.DirectionModes.ToConverter();

        /// <summary>
        /// Represents a converter for the HorizontalAlignment enumeration.
        /// </summary>
        public static readonly IValueConverter HorizontalAlignmentConverter = Map.HorizontalAlignments.ToConverter();

        /// <summary>
        /// Represents a converter for the VerticalAlignment enumeration.
        /// </summary>
        public static readonly IValueConverter VerticalAlignmentConverter = Map.VerticalAlignments.ToConverter();

        /// <summary>
        /// Represents a converter for the Whitespace enumeration.
        /// </summary>
        public static readonly IValueConverter WhitespaceConverter = Map.WhitespaceModes.ToConverter();

        /// <summary>
        /// Represents a converter for the TextTransform enumeration.
        /// </summary>
        public static readonly IValueConverter TextTransformConverter = Map.TextTransforms.ToConverter();

		/// <summary>
		/// Represents a converter for the TextTAligLast enumeration.
		/// </summary>
		public static readonly IValueConverter TextAlignLastConverter = Map.TextAlignmentsLast.ToConverter();
		
		/// <summary>
		/// Represents a converter for the TextAnchor enumeration.
		/// </summary>
		public static readonly IValueConverter TextAnchorConverter = Map.TextAnchors.ToConverter();

		/// <summary>
		/// Represents a converter for the TextJustify enumeration.
		/// </summary>
		public static readonly IValueConverter TextJustifyConverter = Map.TextJustifyOptions.ToConverter();

		/// <summary>
		/// Represents a converter for the ObjectFitting enumeration.
		/// </summary>
		public static readonly IValueConverter ObjectFittingConverter = Map.ObjectFittings.ToConverter();

        /// <summary>
        /// Represents a converter for the PositionMode enumeration.
        /// </summary>
        public static readonly IValueConverter PositionModeConverter = Map.PositionModes.ToConverter();

        /// <summary>
        /// Represents a converter for the OverflowMode enumeration.
        /// </summary>
        public static readonly IValueConverter OverflowModeConverter = Map.OverflowModes.ToConverter();

        /// <summary>
        /// Represents a converter for the Floating enumeration.
        /// </summary>
        public static readonly IValueConverter FloatingConverter = Map.FloatingModes.ToConverter();

        /// <summary>
        /// Represents a converter for the DisplayMode enumeration.
        /// </summary>
        public static readonly IValueConverter DisplayModeConverter = Map.DisplayModes.ToConverter();

        /// <summary>
        /// Represents a converter for the ClearMode enumeration.
        /// </summary>
        public static readonly IValueConverter ClearModeConverter = Map.ClearModes.ToConverter();

        /// <summary>
        /// Represents a converter for the FontStretch enumeration.
        /// </summary>
        public static readonly IValueConverter FontStretchConverter = Map.FontStretches.ToConverter();

        /// <summary>
        /// Represents a converter for the FontStyle enumeration.
        /// </summary>
        public static readonly IValueConverter FontStyleConverter = Map.FontStyles.ToConverter();

        /// <summary>
        /// Represents a converter for the FontWeight enumeration.
        /// </summary>
        public static readonly IValueConverter FontWeightConverter = Map.FontWeights.ToConverter();

        /// <summary>
        /// Represents a converter for the SystemFont enumeration.
        /// </summary>
        public static readonly IValueConverter SystemFontConverter = Map.SystemFonts.ToConverter();

		/// <summary>
		/// Represents a converter for the StrokeLinecap enumeration.
		/// </summary>
		public static readonly IValueConverter StrokeLinecapConverter = Map.StrokeLinecaps.ToConverter();
		
		/// <summary>
		/// Represents a converter for the StrokeLinejoin enumeration.
		/// </summary>
		public static readonly IValueConverter StrokeLinejoinConverter = Map.StrokeLinejoins.ToConverter();

		/// <summary>
		/// Represents a converter for the WordBreak enumeration.
		/// </summary>
		public static readonly IValueConverter WordBreakConverter = Map.WordBreaks.ToConverter();

		/// <summary>
		/// Represents a converter for the OverflowWrap enumeration.
		/// </summary>
		public static readonly IValueConverter OverflowWrapConverter = Map.OverflowWraps.ToConverter();

		#endregion

		#region Specific

		/// <summary>
		/// Represents an optional integer object.
		/// </summary>
		public static readonly IValueConverter OptionalIntegerConverter = IntegerConverter.OrAuto();

        /// <summary>
        /// Represents a positive or infinite number object.
        /// </summary>
        public static readonly IValueConverter PositiveOrInfiniteNumberConverter = NaturalNumberConverter.Or(Keywords.Infinite, Single.PositiveInfinity);

        /// <summary>
        /// Represents a positive or infinite number object.
        /// </summary>
        public static readonly IValueConverter OptionalNumberConverter = NumberConverter.OrNone();

        /// <summary>
        /// Represents a length object or alternatively a fixed length when "normal" is given.
        /// </summary>
        public static readonly IValueConverter LengthOrNormalConverter = LengthConverter.Or(Keywords.Normal, new Length(1f, Length.Unit.Em));

        /// <summary>
        /// Represents a length object or null, when "normal" is given.
        /// </summary>
        public static readonly IValueConverter OptionalLengthConverter = LengthConverter.Or(Keywords.Normal);

        /// <summary>
        /// Represents a length (or default).
        /// </summary>
        public static readonly IValueConverter AutoLengthConverter = LengthConverter.OrAuto();

        /// <summary>
        /// Represents a distance object (either Length or Percent) or none.
        /// </summary>
        public static readonly IValueConverter OptionalLengthOrPercentConverter = LengthOrPercentConverter.OrNone();

        /// <summary>
        /// Represents a distance object (or default).
        /// </summary>
        public static readonly IValueConverter AutoLengthOrPercentConverter = LengthOrPercentConverter.OrAuto();

        /// <summary>
        /// Represents a length for a font size.
        /// </summary>
        public static readonly IValueConverter FontSizeConverter = LengthOrPercentConverter.Or(Map.FontSizes.ToConverter());

        #endregion

        #region Composed

        /// <summary>
        /// Represents a distance object with line-height additions.
        /// http://www.w3.org/TR/CSS2/visudet.html#propdef-line-height
        /// </summary>
        public static readonly IValueConverter LineHeightConverter = LengthOrPercentConverter.Or(NumberConverter).Or(Keywords.Normal);

        /// <summary>
        /// Represents a length object that is based on percentage or number.
        /// http://dev.w3.org/csswg/css-backgrounds/#border-image-slice
        /// </summary>
        public static readonly IValueConverter BorderSliceConverter = PercentConverter.Or(NumberConverter);

        /// <summary>
        /// Represents a length object that is based on percentage, length or number.
        /// http://dev.w3.org/csswg/css-backgrounds/#border-image-width
        /// </summary>
        public static readonly IValueConverter ImageBorderWidthConverter = LengthOrPercentConverter.Or(NumberConverter).Or(Keywords.Auto);

        /// <summary>
        /// Represents a timing-function object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/timing-function
        /// </summary>
        public static readonly IValueConverter TransitionConverter = new DictionaryValueConverter<ITimingFunction>(Map.TimingFunctions).Or(
            StepsConverter).Or(CubicBezierConverter);

        /// <summary>
        /// Represents a gradient object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/gradient
        /// </summary>
        public static readonly IValueConverter GradientConverter = LinearGradientConverter.Or(RadialGradientConverter);

        /// <summary>
        /// Represents a transform function.
        /// http://www.w3.org/TR/css3-transforms/#typedef-transform-function
        /// </summary>
        public static readonly IValueConverter TransformConverter = MatrixTransformConverter.Or(
            ScaleTransformConverter).Or(
            RotateTransformConverter).Or(
            TranslateTransformConverter).Or(
            SkewTransformConverter).Or(
            PerspectiveConverter);

        /// <summary>
        /// Represents a color object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color_value
        /// </summary>
        public static readonly IValueConverter ColorConverter = PureColorConverter.Or(
            RgbColorConverter.Or(RgbaColorConverter)).Or(
            HslColorConverter.Or(HslaColorConverter)).Or(
            GrayColorConverter.Or(HwbColorConverter));

        /// <summary>
        /// Represents a color object or, alternatively, the current color.
        /// </summary>
        public static readonly IValueConverter CurrentColorConverter = ColorConverter.WithCurrentColor();

        /// <summary>
        /// Represents a color object, the current color, or the inverted current color.
        /// </summary>
        public static readonly IValueConverter InvertedColorConverter = CurrentColorConverter.Or(Keywords.Invert);

		/// <summary>
		/// Represents a paint object.
		/// </summary>
		public static readonly IValueConverter PaintConverter = UrlConverter.Or(CurrentColorConverter.OrNone());

		/// <summary>
		/// Represents a converter for Stroke Dasharray property
		/// taking many values or none.
		/// </summary>
		public static readonly IValueConverter StrokeDasharrayConverter = LengthOrPercentConverter.Or(NumberConverter).Many().OrNone();

		/// <summary>
		/// Represents a converter for the StrokeMiterlimit enumeration.
		/// </summary>
		public static readonly IValueConverter StrokeMiterlimitConverter = new StructValueConverter<Single>(ValueExtensions.ToGreaterOrEqualOneSingle);

		/// <summary>
		/// Represents a ratio object.
		/// https://developer.mozilla.org/en-US/docs/Web/CSS/ratio
		/// </summary>
		public static readonly IValueConverter RatioConverter = WithOrder(
            IntegerConverter.Required(), 
            IntegerConverter.StartsWithDelimiter().Required());

        /// <summary>
        /// Represents a shadow object.
        /// http://dev.w3.org/csswg/css-backgrounds/#shadow
        /// </summary>
        public static readonly IValueConverter ShadowConverter = WithAny(
            Assign(Keywords.Inset, true).Option(false),
            LengthConverter.Many(2, 4).Required(),
            ColorConverter.WithCurrentColor().Option(Color.Black));

        /// <summary>
        /// Represents multiple shadow objects.
        /// </summary>
        public static readonly IValueConverter MultipleShadowConverter = ShadowConverter.FromList().OrNone();

        /// <summary>
        /// Represents an image source object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/image
        /// </summary>
        public static readonly IValueConverter ImageSourceConverter = UrlConverter.Or(GradientConverter);

        /// <summary>
        /// Represents an optional image source object.
        /// </summary>
        public static readonly IValueConverter OptionalImageSourceConverter = ImageSourceConverter.OrNone();

        /// <summary>
        /// Represents multiple image source object.
        /// </summary>
        public static readonly IValueConverter MultipleImageSourceConverter = OptionalImageSourceConverter.FromList();

        /// <summary>
        /// Represents the border-radius (h h h h / v v v v) converter.
        /// </summary>
        public static readonly IValueConverter BorderRadiusShorthandConverter = new BorderRadiusConverter();

        /// <summary>
        /// Represents the border-radius (horizontal / vertical; radius) converter.
        /// </summary>
        public static readonly IValueConverter BorderRadiusConverter = WithOrder(
            LengthOrPercentConverter.Required(), LengthOrPercentConverter.Option());

        /// <summary>
        /// Represents a converter for font families.
        /// </summary>
        public static readonly IValueConverter FontFamiliesConverter = DefaultFontFamiliesConverter.Or(StringConverter).Or(LiteralsConverter).FromList();

        /// <summary>
        /// Represents a converter for background size.
        /// </summary>
        public static readonly IValueConverter BackgroundSizeConverter = AutoLengthOrPercentConverter.Or(
            Keywords.Cover).Or(Keywords.Contain).Or(
            WithOrder(AutoLengthOrPercentConverter.Required(), AutoLengthOrPercentConverter.Required()));

        /// <summary>
        /// Represents a converter for background repeat.
        /// </summary>
        public static readonly IValueConverter BackgroundRepeatsConverter = BackgroundRepeatConverter.Or(Keywords.RepeatX).Or(Keywords.RepeatY).Or(
            WithOrder(BackgroundRepeatConverter.Required(), BackgroundRepeatConverter.Required()));

        #endregion

        #region Toggles

        /// <summary>
        /// Represents a converter for the table layout mode.
        /// </summary>
        public static readonly IValueConverter TableLayoutConverter = Toggle(Keywords.Fixed, Keywords.Auto);

        /// <summary>
        /// Represents a converter for the empty cells mode.
        /// </summary>
        public static readonly IValueConverter EmptyCellsConverter = Toggle(Keywords.Show, Keywords.Hide);

        /// <summary>
        /// Represents a converter for the caption side mode.
        /// </summary>
        public static readonly IValueConverter CaptionSideConverter = Toggle(Keywords.Top, Keywords.Bottom);

        /// <summary>
        /// Represents a converter for the backface visibility mode.
        /// </summary>
        public static readonly IValueConverter BackfaceVisibilityConverter = Toggle(Keywords.Visible, Keywords.Hidden);

        /// <summary>
        /// Represents a converter for the border collapse mode.
        /// </summary>
        public static readonly IValueConverter BorderCollapseConverter = Toggle(Keywords.Separate, Keywords.Collapse);

        /// <summary>
        /// Represents a converter for the box decoration break mode.
        /// </summary>
        public static readonly IValueConverter BoxDecorationConverter = Toggle(Keywords.Clone, Keywords.Slice);

        /// <summary>
        /// Represents a converter for the column span mode.
        /// </summary>
        public static readonly IValueConverter ColumnSpanConverter = Toggle(Keywords.All, Keywords.None);

        /// <summary>
        /// Represents a converter for the column fill mode.
        /// </summary>
        public static readonly IValueConverter ColumnFillConverter = Toggle(Keywords.Balance, Keywords.Auto);

        #endregion

        #region Misc

        /// <summary>
        /// Represents a converter for anything. Just copies the tokens.
        /// </summary>
        public static IValueConverter Any = new AnyValueConverter();

        /// <summary>
        /// Creates a new converter by assigning the given identifier to a fixed result.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="identifier">The identifier (keyword) to use.</param>
        /// <param name="result">The fixed result that is returned if the identifier is found.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter Assign<T>(String identifier, T result)
        {
            return new IdentifierValueConverter<T>(identifier, result);
        }

        /// <summary>
        /// Creates a new boolean converter that toggles between the two given keywords.
        /// </summary>
        /// <param name="on">The keyword to use for returning true.</param>
        /// <param name="off">The keyword to use for returning false.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter Toggle(String on, String off)
        {
            return Assign(on, true).Or(off, false);
        }

        #endregion

        #region Order / Unordered

        /// <summary>
        /// Uses the provided converters successively in order.
        /// </summary>
        /// <param name="converters">The converters that are used.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter WithOrder(params IValueConverter[] converters)
        {
            return new OrderedOptionsConverter(converters);
        }

        /// <summary>
        /// Uses the converters in any order to convert provided values.
        /// </summary>
        /// <param name="converters">The converters that are used.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter WithAny(params IValueConverter[] converters)
        {
            return new UnorderedOptionsConverter(converters);
        }

        /// <summary>
        /// Uses the provided converter for the whole value.
        /// </summary>
        /// <param name="converter">The converter that is used.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter Continuous(IValueConverter converter)
        {
            return new ContinuousValueConverter(converter);
        }

        #endregion

        #region Helper

        static IValueConverter Construct(Func<IValueConverter> f)
        {
            return f();
        }

        static IValueConverter WithArgs(IValueConverter converter, Int32 arguments)
        {
            var converters = Enumerable.Repeat(converter, arguments).ToArray();
            return WithArgs(converters);
        }

        static IValueConverter WithArgs(IValueConverter converter)
        {
            return new ArgumentsValueConverter(converter);
        }

        static IValueConverter WithArgs(params IValueConverter[] converters)
        {
            return new ArgumentsValueConverter(converters);
        }

        #endregion
    }
}
