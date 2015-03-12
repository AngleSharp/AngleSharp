namespace AngleSharp.Css
{
    using AngleSharp.Css.ValueConverters;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;

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
        public static readonly IValueConverter<Length> LineWidthConverter = new StructValueConverter<Length>(ValueExtensions.ToBorderWidth);

        /// <summary>
        /// Represents a distance object with line-height additions.
        /// http://www.w3.org/TR/CSS2/visudet.html#propdef-line-height
        /// </summary>
        public static readonly IValueConverter<Length> LineHeightConverter = new StructValueConverter<Length>(ValueExtensions.ToLineHeight);

        /// <summary>
        /// Represents a length object that is based on percentage or number.
        /// http://dev.w3.org/csswg/css-backgrounds/#border-image-slice
        /// </summary>
        public static readonly IValueConverter<Length> BorderSliceConverter = new StructValueConverter<Length>(ValueExtensions.ToBorderSlice);

        /// <summary>
        /// Represents a length object that is based on percentage, length or number.
        /// http://dev.w3.org/csswg/css-backgrounds/#border-image-width
        /// </summary>
        public static readonly IValueConverter<Length> ImageBorderWidthConverter = new StructValueConverter<Length>(ValueExtensions.ToImageBorderWidth);

        /// <summary>
        /// Represents a length object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/length
        /// </summary>
        public static readonly IValueConverter<Length> LengthConverter = new StructValueConverter<Length>(ValueExtensions.ToLength);

        /// <summary>
        /// Represents a resolution object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/resolution
        /// </summary>
        public static readonly IValueConverter<Resolution> ResolutionConverter = new StructValueConverter<Resolution>(ValueExtensions.ToResolution);

        /// <summary>
        /// Represents a frequency object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/frequency
        /// </summary>
        public static readonly IValueConverter<Frequency> FrequencyConverter = new StructValueConverter<Frequency>(ValueExtensions.ToFrequency);

        /// <summary>
        /// Represents a time object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/time
        /// </summary>
        public static readonly IValueConverter<Time> TimeConverter = new StructValueConverter<Time>(ValueExtensions.ToTime);

        /// <summary>
        /// Represents an URL object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/uri
        /// </summary>
        public static readonly IValueConverter<CssUrl> UrlConverter = new ClassValueConverter<CssUrl>(ValueExtensions.ToUri);

        /// <summary>
        /// Represents a string object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/string
        /// </summary>
        public static readonly IValueConverter<String> StringConverter = new ClassValueConverter<String>(ValueExtensions.ToCssString);

        /// <summary>
        /// Represents an identifier object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/user-ident
        /// </summary>
        public static readonly IValueConverter<String> IdentifierConverter = new ClassValueConverter<String>(ValueExtensions.ToIdentifier);

        /// <summary>
        /// Represents an identifier object that matches the production rules of a single transition property.
        /// http://dev.w3.org/csswg/css-transitions/#single-transition-property
        /// </summary>
        public static readonly IValueConverter<String> AnimatableConverter = new ClassValueConverter<String>(ValueExtensions.ToAnimatableIdentifier);

        /// <summary>
        /// Represents an integer object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/integer
        /// </summary>
        public static readonly IValueConverter<Int32> IntegerConverter = new StructValueConverter<Int32>(ValueExtensions.ToInteger);

        /// <summary>
        /// Represents an angle object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/angle
        /// </summary>
        public static readonly IValueConverter<Angle> AngleConverter = new StructValueConverter<Angle>(ValueExtensions.ToAngle);

        /// <summary>
        /// Represents a number object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/number
        /// </summary>
        public static readonly IValueConverter<Single> NumberConverter = new StructValueConverter<Single>(ValueExtensions.ToSingle);

        /// <summary>
        /// Represents a percentage object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/percentage
        /// </summary>
        public static readonly IValueConverter<Percent> PercentConverter = new StructValueConverter<Percent>(ValueExtensions.ToPercent);

        /// <summary>
        /// Represents an integer object reduced to [0, 255].
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/integer
        /// </summary>
        public static readonly IValueConverter<Byte> ByteConverter = new StructValueConverter<Byte>(ValueExtensions.ToByte);

        /// <summary>
        /// Represents an color object (usually hex or name).
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color
        /// </summary>
        public static readonly IValueConverter<Color> PureColorConverter = new StructValueConverter<Color>(ValueExtensions.ToColor);

        /// <summary>
        /// Represents a distance object (either Length or Percent).
        /// </summary>
        public static readonly IValueConverter<Length> LengthOrPercentConverter = new StructValueConverter<Length>(ValueExtensions.ToDistance);

        #endregion

        #region Functions

        /// <summary>
        /// Represents the angle to a side or corner of a box.
        /// http://dev.w3.org/csswg/css-images-3/#typedef-side-or-corner
        /// </summary>
        public static readonly IValueConverter<Angle> SideOrCornerConverter = WithAny(
            Assign(Keywords.Left, -1.0).Or(Keywords.Right, 1.0).Option(0.0),
            Assign(Keywords.Top, 1.0).Or(Keywords.Bottom, -1.0).Option(0.0)
        ).To(m => new Angle((Single)(Math.Atan2(m.Item1, m.Item2) * 180.0 / Math.PI), Angle.Unit.Deg));

        /// <summary>
        /// Represents a position object.
        /// http://www.w3.org/TR/css3-background/#ltpositiongt
        /// </summary>
        public static readonly IValueConverter<Point> PointConverter = Construct(() =>
        {
            var hi = Assign(Keywords.Left, Length.Zero).Or(Keywords.Right, new Length(100f, Length.Unit.Percent)).Or(Keywords.Center, new Length(50f, Length.Unit.Percent));
            var vi = Assign(Keywords.Top, Length.Zero).Or(Keywords.Bottom, new Length(100f, Length.Unit.Percent)).Or(Keywords.Center, new Length(50f, Length.Unit.Percent));
            var h = hi.Or(LengthOrPercentConverter).Required();
            var v = vi.Or(LengthOrPercentConverter).Required();

            return LengthOrPercentConverter.To(m => new Point(m, Length.Half)).Or(
                   Toggle(Keywords.Left, Keywords.Right).To(m => new Point(m ? Length.Zero : Length.Full, Length.Half))).Or(
                   Toggle(Keywords.Top, Keywords.Bottom).To(m => new Point(Length.Half, m ? Length.Zero : Length.Full))).Or(
                   Keywords.Center, Point.Center).Or(
                   WithArgs(h, v, m => new Point(m.Item1, m.Item2))).Or(
                   WithArgs(v, h, m => new Point(m.Item2, m.Item1))).Or(
                   WithArgs(hi, vi, LengthOrPercentConverter, m => new Point(m.Item1, m.Item2.Add(m.Item3)))).Or(
                   WithArgs(hi, LengthOrPercentConverter, vi, m => new Point(m.Item1.Add(m.Item2), m.Item3))).Or(
                   WithArgs(hi, LengthOrPercentConverter, vi, LengthOrPercentConverter, m => new Point(m.Item1.Add(m.Item2), m.Item3.Add(m.Item4))));
        });

        /// <summary>
        /// Represents an attribute retriever object.
        /// http://dev.w3.org/csswg/css-values/#funcdef-attr
        /// </summary>
        public static readonly IValueConverter<String> AttrConverter = new FunctionValueConverter<String>(
            FunctionNames.Attr, WithArg(StringConverter.Or(IdentifierConverter)));

        /// <summary>
        /// Represents a steps timing-function object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/timing-function
        /// </summary>
        public static readonly IValueConverter<StepsTimingFunction> StepsConverter = new FunctionValueConverter<StepsTimingFunction>(
            FunctionNames.Steps, WithArgs(
                IntegerConverter.Required(), 
                Assign(Keywords.Start, true).Or(Keywords.End, false).Option(false), 
            m => new StepsTimingFunction(m.Item1, m.Item2)));

        /// <summary>
        /// Represents a cubic-bezier timing-function object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/timing-function
        /// </summary>
        public static readonly IValueConverter<CubicBezierTimingFunction> CubicBezierConverter = Construct(() =>
        {
            var number = NumberConverter.Required();
            return new FunctionValueConverter<CubicBezierTimingFunction>(FunctionNames.CubicBezier,
                    WithArgs(number, number, number, number, m => new CubicBezierTimingFunction(m.Item1, m.Item2, m.Item3, m.Item4)));
        });

        /// <summary>
        /// Represents a counter object.
        /// http://www.w3.org/TR/CSS2/syndata.html#value-def-counter
        /// </summary>
        public static readonly IValueConverter<Counter> CounterConverter = Construct(() =>
        {
            var name = IdentifierConverter.Required();
            var kind = IdentifierConverter.Option(Keywords.Decimal);
            var def = StringConverter.Required();
            return new FunctionValueConverter<Counter>(FunctionNames.Counter,
                        WithArgs(name, kind, m => new Counter(m.Item1, m.Item2, null))).Or(
                   new FunctionValueConverter<Counter>(FunctionNames.Counters,
                        WithArgs(name, def, kind, m => new Counter(m.Item1, m.Item3, m.Item2))));
        });

        /// <summary>
        /// Represents a shape object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/shape
        /// </summary>
        public static readonly IValueConverter<Shape> ShapeConverter = Construct(() =>
        {
            var length = LengthConverter.Required();
            return new FunctionValueConverter<Shape>(FunctionNames.Rect,
                        WithArgs(length, length, length, length, m => new Shape(m.Item1, m.Item2, m.Item3, m.Item4)).Or(
                        WithArg(LengthConverter.Many(4, 4).To(m => new Shape(m[0], m[1], m[2], m[3])))));
        }).OrDefault();

        /// <summary>
        /// Represents a linear-gradient object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/linear-gradient
        /// </summary>
        public static readonly IValueConverter<LinearGradient> LinearGradientConverter = Construct(() =>
        {
            var side = SideOrCornerConverter.StartsWithKeyword(Keywords.To);
            var angle = AngleConverter.Or(side);
            var gradient = new GradientConverter<Angle>(angle, Angle.Half);

            return new FunctionValueConverter<LinearGradient>(FunctionNames.LinearGradient,
                        gradient.To(m => new LinearGradient(m.Item1.Value, m.Item2, false))).Or(
                   new FunctionValueConverter<LinearGradient>(FunctionNames.RepeatingLinearGradient,
                        gradient.To(m => new LinearGradient(m.Item1.Value, m.Item2, true))));
        });

        /// <summary>
        /// Represents a radial-gradient object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/radial-gradient
        /// </summary>
        public static readonly IValueConverter<RadialGradient> RadialGradientConverter = Construct(() =>
        {
            var position = PointConverter.StartsWithKeyword(Keywords.At).Option(Point.Center);
            var defaultValue = new { Width = Length.Zero, Height = Length.Zero, SizeMode = RadialGradient.SizeMode.FarthestCorner };
            var circle = WithOrder(WithAny(Assign(Keywords.Circle, true).Option(true), LengthConverter.To(
                m => new { Width = m, Height = m, SizeMode = RadialGradient.SizeMode.None }).Option(defaultValue)), position);
            var ellipse = WithOrder(WithAny(Assign(Keywords.Ellipse, false).Option(false), LengthOrPercentConverter.Many(2, 2).To(
                m => new { Width = m[0], Height = m[1], SizeMode = RadialGradient.SizeMode.None }).Option(defaultValue)), position);
            var extents = WithOrder(WithAny(Toggle(Keywords.Circle, Keywords.Ellipse).Option(false), Map.RadialGradientSizeModes.ToConverter().To(
                m => new { Width = Length.Zero, Height = Length.Zero, SizeMode = m })), position);

            var gradient = new GradientConverter<Tuple<Boolean, Length, Length, RadialGradient.SizeMode, Point>>(
                circle.Or(ellipse.Or(extents)).To(m => Tuple.Create(m.Item1.Item1, m.Item1.Item2.Width, m.Item1.Item2.Height, m.Item1.Item2.SizeMode, m.Item2)), 
                Tuple.Create(false, defaultValue.Width, defaultValue.Height, defaultValue.SizeMode, Point.Center));

            return new FunctionValueConverter<RadialGradient>(FunctionNames.RadialGradient,
                        gradient.To(m => new RadialGradient(m.Item1.Item1, m.Item1.Item5, m.Item1.Item2, m.Item1.Item3, m.Item1.Item4, m.Item2, false))).Or(
                   new FunctionValueConverter<RadialGradient>(FunctionNames.RepeatingRadialGradient,
                        gradient.To(m => new RadialGradient(m.Item1.Item1, m.Item1.Item5, m.Item1.Item2, m.Item1.Item3, m.Item1.Item4, m.Item2, true))));
        });

        /// <summary>
        /// Represents a color object (RGB function).
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color_value
        /// </summary>
        public static readonly IValueConverter<Color> RgbColorConverter = Construct(() =>
        {
            var value = ByteConverter.Required();
            return new FunctionValueConverter<Color>(FunctionNames.Rgb, WithArgs(value, value, value, m => new Color(m.Item1, m.Item2, m.Item3)));
        });

        /// <summary>
        /// Represents a color object (RGBA function).
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color_value
        /// </summary>
        public static readonly IValueConverter<Color> RgbaColorConverter = Construct(() =>
        {
            var value = ByteConverter.Required();
            var number = NumberConverter.Required();
            return new FunctionValueConverter<Color>(FunctionNames.Rgba, WithArgs(value, value, value, number, m => new Color(m.Item1, m.Item2, m.Item3, m.Item4)));
        });

        /// <summary>
        /// Represents a color object (HSL function).
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color_value
        /// </summary>
        public static readonly IValueConverter<Color> HslColorConverter = Construct(() =>
        {
            var percent = PercentConverter.Required();
            var number = NumberConverter.Required();
            return new FunctionValueConverter<Color>(FunctionNames.Hsl, WithArgs(number, percent, percent, m => Color.FromHsl(m.Item1 / 360f, m.Item2.NormalizedValue, m.Item3.NormalizedValue)));
        });

        /// <summary>
        /// Represents a color object (HSLA function).
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color_value
        /// </summary>
        public static readonly IValueConverter<Color> HslaColorConverter = Construct(() =>
        {
            var percent = PercentConverter.Required();
            var number = NumberConverter.Required();
            return new FunctionValueConverter<Color>(FunctionNames.Hsla, WithArgs(number, percent, percent, number, m => Color.FromHsla(m.Item1 / 360f, m.Item2.NormalizedValue, m.Item3.NormalizedValue, m.Item4)));
        });

        /// <summary>
        /// A perspective for 3D transformations.s
        /// http://www.w3.org/TR/css3-transforms/#funcdef-perspective
        /// </summary>
        public static readonly IValueConverter<PerspectiveTransform> PerspectiveConverter = Construct(() =>
        {
            return new FunctionValueConverter<PerspectiveTransform>(FunctionNames.Perspective,
                        WithArg(LengthConverter.To(m => new PerspectiveTransform(m))));
        });

        /// <summary>
        /// Sets the transformation matrix explicitely.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-matrix3d
        /// </summary>
        public static readonly IValueConverter<MatrixTransform> MatrixTransformConverter = Construct(() =>
        {
            return new FunctionValueConverter<MatrixTransform>(FunctionNames.Matrix,
                        WithArgs(NumberConverter, 6, m => new MatrixTransform(new[] { m[0], m[1], 0f, 0f, m[2], m[3], 0f, 0f, 0f, 1f, 0f, m[4], m[5], 0f, 1f }))).Or(
                   new FunctionValueConverter<MatrixTransform>(FunctionNames.Matrix3d,
                        WithArgs(NumberConverter, 16, m => new MatrixTransform(m))));
        });

        /// <summary>
        /// A broad variety of translate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-translate3d
        /// </summary>
        public static readonly IValueConverter<TranslateTransform> TranslateTransformConverter = Construct(() =>
        {
            var distance = LengthOrPercentConverter.Required();
            var option = LengthOrPercentConverter.Option(Length.Zero);
            return new FunctionValueConverter<TranslateTransform>(FunctionNames.Translate,
                        WithArgs(distance, option, m => new TranslateTransform(m.Item1, m.Item2, Length.Zero))).Or(
                   new FunctionValueConverter<TranslateTransform>(FunctionNames.Translate3d,
                        WithArgs(distance, option, option, m => new TranslateTransform(m.Item1, m.Item2, m.Item3)))).Or(
                   new FunctionValueConverter<TranslateTransform>(FunctionNames.TranslateX,
                        WithArg(LengthOrPercentConverter.To(m => new TranslateTransform(m, Length.Zero, Length.Zero))))).Or(
                   new FunctionValueConverter<TranslateTransform>(FunctionNames.TranslateY,
                        WithArg(LengthOrPercentConverter.To(m => new TranslateTransform(Length.Zero, m, Length.Zero))))).Or(
                   new FunctionValueConverter<TranslateTransform>(FunctionNames.TranslateZ,
                        WithArg(LengthOrPercentConverter.To(m => new TranslateTransform(Length.Zero, Length.Zero, m)))));
        });

        /// <summary>
        /// A broad variety of scale transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-scale3d
        /// </summary>
        public static readonly IValueConverter<ScaleTransform> ScaleTransformConverter = Construct(() =>
        {
            var number = NumberConverter.Required();
            var option = NumberConverter.Option(Single.NaN);
            return new FunctionValueConverter<ScaleTransform>(FunctionNames.Scale,
                        WithArgs(number, option, m => new ScaleTransform(m.Item1, Single.IsNaN(m.Item2) ? m.Item1 : m.Item2, 1f))).Or(
                   new FunctionValueConverter<ScaleTransform>(FunctionNames.Scale3d,
                        WithArgs(number, option, option, m => new ScaleTransform(m.Item1, Single.IsNaN(m.Item2) ? m.Item1 : m.Item2, Single.IsNaN(m.Item3) ? m.Item1 : m.Item3)))).Or(
                   new FunctionValueConverter<ScaleTransform>(FunctionNames.ScaleX,
                        WithArg(NumberConverter.To(m => new ScaleTransform(m, 1f, 1f))))).Or(
                   new FunctionValueConverter<ScaleTransform>(FunctionNames.ScaleY,
                        WithArg(NumberConverter.To(m => new ScaleTransform(1f, m, 1f))))).Or(
                   new FunctionValueConverter<ScaleTransform>(FunctionNames.ScaleZ,
                        WithArg(NumberConverter.To(m => new ScaleTransform(1f, 1f, m)))));
        });

        /// <summary>
        /// A broad variety of rotate transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-rotate3d
        /// </summary>
        public static readonly IValueConverter<RotateTransform> RotateTransformConverter = Construct(() =>
        {
            var number = NumberConverter.Required();
            return new FunctionValueConverter<RotateTransform>(FunctionNames.Rotate,
                        WithArg(AngleConverter.To(m => RotateTransform.RotateZ(m.Value)))).Or(
                   new FunctionValueConverter<RotateTransform>(FunctionNames.Rotate3d,
                        WithArgs(number, number, number, AngleConverter.Required(), m => new RotateTransform(m.Item1, m.Item2, m.Item3, m.Item4.Value)))).Or(
                   new FunctionValueConverter<RotateTransform>(FunctionNames.RotateX,
                        WithArg(AngleConverter.To(m => RotateTransform.RotateX(m.Value))))).Or(
                   new FunctionValueConverter<RotateTransform>(FunctionNames.RotateY,
                        WithArg(AngleConverter.To(m => RotateTransform.RotateY(m.Value))))).Or(
                   new FunctionValueConverter<RotateTransform>(FunctionNames.RotateZ,
                        WithArg(AngleConverter.To(m => RotateTransform.RotateZ(m.Value)))));
        });

        /// <summary>
        /// A broad variety of skew transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-skew
        /// </summary>
        public static readonly IValueConverter<SkewTransform> SkewTransformConverter = Construct(() =>
        {
            var angle = AngleConverter.Required();
            return new FunctionValueConverter<SkewTransform>(FunctionNames.Skew,
                        WithArgs(angle, angle, m => new SkewTransform(m.Item1.Value, m.Item2.Value))).Or(
                   new FunctionValueConverter<SkewTransform>(FunctionNames.SkewX,
                        WithArg(AngleConverter.To(m => new SkewTransform(m.Value, 0f))))).Or(
                   new FunctionValueConverter<SkewTransform>(FunctionNames.SkewY,
                        WithArg(AngleConverter.To(m => new SkewTransform(0f, m.Value)))));
        });

        #endregion

        #region Composed

        /// <summary>
        /// Represents a timing-function object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/timing-function
        /// </summary>
        public static readonly IValueConverter<ITimingFunction> TransitionConverter = new DictionaryValueConverter<ITimingFunction>(Map.TimingFunctions).Or(
            StepsConverter.To(m => (ITimingFunction)m)).Or(
            CubicBezierConverter.To(m => (ITimingFunction)m));

        /// <summary>
        /// Represents a gradient object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/gradient
        /// </summary>
        public static readonly IValueConverter<IImageSource> GradientConverter = LinearGradientConverter.To(m => (IImageSource)m).Or(
            RadialGradientConverter.To(m => (IImageSource)m));

        /// <summary>
        /// Represents a transform function.
        /// http://www.w3.org/TR/css3-transforms/#typedef-transform-function
        /// </summary>
        public static readonly IValueConverter<ITransform> TransformConverter = MatrixTransformConverter.To(m => (ITransform)m).Or(
            ScaleTransformConverter.To(m => (ITransform)m)).Or(
            RotateTransformConverter.To(m => (ITransform)m)).Or(
            TranslateTransformConverter.To(m => (ITransform)m)).Or(
            SkewTransformConverter.To(m => (ITransform)m)).Or(
            PerspectiveConverter.To(m => (ITransform)m));

        /// <summary>
        /// Represents a color object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color_value
        /// </summary>
        public static readonly IValueConverter<Color> ColorConverter = PureColorConverter.Or(
            RgbColorConverter.Or(RgbaColorConverter)).Or(
            HslColorConverter.Or(HslaColorConverter));

        /// <summary>
        /// Represents a color object or, alternatively, the current color.
        /// </summary>
        public static readonly IValueConverter<Color> CurrentColorConverter = ColorConverter.WithCurrentColor();

        /// <summary>
        /// Represents a color object, the current color, or the inverted current color.
        /// </summary>
        public static readonly IValueConverter<Color?> InvertedColorConverter = CurrentColorConverter.ToNullable().Or(
            Keywords.Invert, null);

        /// <summary>
        /// Represents a ratio object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/ratio
        /// </summary>
        public static readonly IValueConverter<Tuple<Int32, Int32>> RatioConverter = WithOrder(
            IntegerConverter.Required(), 
            IntegerConverter.StartsWithDelimiter().Required());

        /// <summary>
        /// Represents a shadow object.
        /// http://dev.w3.org/csswg/css-backgrounds/#shadow
        /// </summary>
        public static readonly IValueConverter<Shadow> ShadowConverter = WithAny(
            Assign(Keywords.Inset, true).Option(false),
            LengthConverter.Many(2, 4).Required(),
            ColorConverter.WithCurrentColor().Option(Color.Black)).To(
            m => new Shadow(m.Item1, m.Item2[0], m.Item2[1], Get(m.Item2, 2, Length.Zero), Get(m.Item2, 3, Length.Zero), m.Item3));

        /// <summary>
        /// Represents multiple shadow objects.
        /// </summary>
        public static readonly IValueConverter<Shadow[]> MultipleShadowConverter = ShadowConverter.FromList().Or(
            Keywords.None, new Shadow[0]);

        /// <summary>
        /// Represents an image source object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/image
        /// </summary>
        public static readonly IValueConverter<IImageSource> ImageSourceConverter = UrlConverter.To(m => (IImageSource)m).Or(
            GradientConverter);

        /// <summary>
        /// Represents an optional image source object.
        /// </summary>
        public static readonly IValueConverter<IImageSource> OptionalImageSourceConverter = ImageSourceConverter.Or(
            Keywords.None, null);

        /// <summary>
        /// Represents multiple image source object.
        /// </summary>
        public static readonly IValueConverter<IImageSource[]> MultipleImageSourceConverter = ImageSourceConverter.FromList().Or(
            Keywords.None, new IImageSource[0]);

        /// <summary>
        /// Represents the border-radius (horizontal / vertical; radius) converter.
        /// </summary>
        public static readonly IValueConverter<Tuple<Length, Length?>> BorderRadiusConverter = WithOrder(
            LengthOrPercentConverter.Required(),
            LengthOrPercentConverter.ToNullable().Option(null));

        #endregion

        #region Specific

        /// <summary>
        /// Represents an optional integer object.
        /// </summary>
        public static readonly IValueConverter<Int32?> OptionalIntegerConverter = IntegerConverter.OrNullDefault();

        /// <summary>
        /// Represents an integer object that is zero or greater.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/integer
        /// </summary>
        public static readonly IValueConverter<Int32> PositiveIntegerConverter = IntegerConverter.Constraint(
            m => m >= 0);

        /// <summary>
        /// Represents a positive or infinite number object.
        /// </summary>
        public static readonly IValueConverter<Single> PositiveOrInfiniteNumberConverter = NumberConverter.Constraint(
            m => m >= 0f).Or(Keywords.Infinite, Single.PositiveInfinity);

        /// <summary>
        /// Represents a positive or infinite number object.
        /// </summary>
        public static readonly IValueConverter<Single?> OptionalNumberConverter = NumberConverter.ToNullable().Or(
            Keywords.None, null);

        /// <summary>
        /// Represents a length object or alternatively a fixed length when "normal" is given.
        /// </summary>
        public static readonly IValueConverter<Length> LengthOrNormalConverter = LengthConverter.Or(
            Keywords.Normal, new Length(1f, Length.Unit.Em));

        /// <summary>
        /// Represents a length object or null, when "normal" is given.
        /// </summary>
        public static readonly IValueConverter<Length?> OptionalLengthConverter = LengthConverter.ToNullable().Or(
            Keywords.Normal, null);

        /// <summary>
        /// Represents a length (or default).
        /// </summary>
        public static readonly IValueConverter<Length?> AutoLengthConverter = LengthConverter.OrNullDefault();

        /// <summary>
        /// Represents a distance object (either Length or Percent) or none.
        /// </summary>
        public static readonly IValueConverter<Length?> OptionalLengthOrPercentConverter = LengthOrPercentConverter.ToNullable().Or(
            Keywords.None, null);

        /// <summary>
        /// Represents a distance object (or default).
        /// </summary>
        public static readonly IValueConverter<Length?> AutoLengthOrPercentConverter = LengthOrPercentConverter.OrNullDefault();

        /// <summary>
        /// Represents a length for a font size.
        /// </summary>
        public static readonly IValueConverter<Length> FontSizeConverter = LengthOrPercentConverter.Or(
            Map.FontSizes.ToConverter().To(m => m.ToLength()));

        #endregion

        #region Maps

        /// <summary>
        /// Represents a converter for the LineStyle enumeration.
        /// </summary>
        public static readonly IValueConverter<LineStyle> LineStyleConverter = Map.LineStyles.ToConverter();

        /// <summary>
        /// Represents a converter for the BackgroundAttachment enumeration.
        /// </summary>
        public static readonly IValueConverter<BackgroundAttachment> BackgroundAttachmentConverter = Map.BackgroundAttachments.ToConverter();

        /// <summary>
        /// Represents a converter for the BoxModel enumeration.
        /// </summary>
        public static readonly IValueConverter<BoxModel> BoxModelConverter = Map.BoxModels.ToConverter();

        /// <summary>
        /// Represents a converter for the AnimationDirection enumeration.
        /// </summary>
        public static readonly IValueConverter<AnimationDirection> AnimationDirectionConverter = Map.AnimationDirections.ToConverter();

        /// <summary>
        /// Represents a converter for the AnimationFillStyle enumeration.
        /// </summary>
        public static readonly IValueConverter<AnimationFillStyle> AnimationFillStyleConverter = Map.AnimationFillStyles.ToConverter();

        /// <summary>
        /// Represents a converter for the TextDecorationStyle enumeration.
        /// </summary>
        public static readonly IValueConverter<TextDecorationStyle> TextDecorationStyleConverter = Map.TextDecorationStyles.ToConverter();

        /// <summary>
        /// Represents a converter for the ListPosition enumeration.
        /// </summary>
        public static readonly IValueConverter<ListPosition> ListPositionConverter = Map.ListPositions.ToConverter();

        /// <summary>
        /// Represents a converter for the ListStyle enumeration.
        /// </summary>
        public static readonly IValueConverter<ListStyle> ListStyleConverter = Map.ListStyles.ToConverter();

        /// <summary>
        /// Represents a converter for the BreakMode enumeration.
        /// </summary>
        public static readonly IValueConverter<BreakMode> BreakModeConverter = Map.BreakModes.ToConverter();

        /// <summary>
        /// Represents a converter for the BreakMode enumeration (constraint to the inside values).
        /// </summary>
        public static readonly IValueConverter<BreakMode> BreakInsideModeConverter = Map.BreakInsideModes.ToConverter();

        /// <summary>
        /// Represents a converter for the BreakMode enumeration (constraint to the page values).
        /// </summary>
        public static readonly IValueConverter<BreakMode> PageBreakModeConverter = Map.PageBreakModes.ToConverter();

        /// <summary>
        /// Represents a converter for the UnicodeMode enumeration.
        /// </summary>
        public static readonly IValueConverter<UnicodeMode> UnicodeModeConverter = Map.UnicodeModes.ToConverter();

        /// <summary>
        /// Represents a converter for the Visibility enumeration.
        /// </summary>
        public static readonly IValueConverter<Visibility> VisibilityConverter = Map.Visibilities.ToConverter();

        /// <summary>
        /// Represents a converter for the PlayState enumeration.
        /// </summary>
        public static readonly IValueConverter<PlayState> PlayStateConverter = Converters.Assign(
            Keywords.Running, PlayState.Running).Or(Keywords.Paused, PlayState.Paused);

        /// <summary>
        /// Represents a converter for the FontVariant enumeration.
        /// </summary>
        public static readonly IValueConverter<FontVariant> FontVariantConverter = Converters.Assign(
            Keywords.Normal, FontVariant.Normal).Or(Keywords.SmallCaps, FontVariant.SmallCaps);

        /// <summary>
        /// Represents a converter for the DirectionMode enumeration.
        /// </summary>
        public static readonly IValueConverter<DirectionMode> DirectionModeConverter = Converters.Assign(
            Keywords.Ltr, DirectionMode.Ltr).Or(Keywords.Rtl, DirectionMode.Rtl);

        /// <summary>
        /// Represents a converter for the HorizontalAlignment enumeration.
        /// </summary>
        public static readonly IValueConverter<HorizontalAlignment> HorizontalAlignmentConverter = Map.HorizontalAlignments.ToConverter();

        /// <summary>
        /// Represents a converter for the VerticalAlignment enumeration.
        /// </summary>
        public static readonly IValueConverter<VerticalAlignment> VerticalAlignmentConverter = Map.VerticalAlignments.ToConverter();

        /// <summary>
        /// Represents a converter for the Whitespace enumeration.
        /// </summary>
        public static readonly IValueConverter<Whitespace> WhitespaceConverter = Map.WhitespaceModes.ToConverter();

        /// <summary>
        /// Represents a converter for the TextTransform enumeration.
        /// </summary>
        public static readonly IValueConverter<TextTransform> TextTransformConverter = Map.TextTransforms.ToConverter();

        /// <summary>
        /// Represents a converter for the ObjectFitting enumeration.
        /// </summary>
        public static readonly IValueConverter<ObjectFitting> ObjectFittingConverter = Map.ObjectFittings.ToConverter();

        /// <summary>
        /// Represents a converter for the PositionMode enumeration.
        /// </summary>
        public static readonly IValueConverter<PositionMode> PositionModeConverter = Map.PositionModes.ToConverter();

        /// <summary>
        /// Represents a converter for the OverflowMode enumeration.
        /// </summary>
        public static readonly IValueConverter<OverflowMode> OverflowModeConverter = Map.OverflowModes.ToConverter();

        /// <summary>
        /// Represents a converter for the Floating enumeration.
        /// </summary>
        public static readonly IValueConverter<Floating> FloatingConverter = Map.FloatingModes.ToConverter();

        /// <summary>
        /// Represents a converter for the DisplayMode enumeration.
        /// </summary>
        public static readonly IValueConverter<DisplayMode> DisplayModeConverter = Map.DisplayModes.ToConverter();

        /// <summary>
        /// Represents a converter for the ClearMode enumeration.
        /// </summary>
        public static readonly IValueConverter<ClearMode> ClearModeConverter = Map.ClearModes.ToConverter();

        /// <summary>
        /// Represents a converter for the FontStretch enumeration.
        /// </summary>
        public static readonly IValueConverter<FontStretch> FontStretchConverter = Map.FontStretches.ToConverter();

        /// <summary>
        /// Represents a converter for the FontStyle enumeration.
        /// </summary>
        public static readonly IValueConverter<FontStyle> FontStyleConverter = Map.FontStyles.ToConverter();

        #endregion

        #region Toggles

        /// <summary>
        /// Represents a converter for the table layout mode.
        /// </summary>
        public static readonly IValueConverter<Boolean> TableLayoutConverter = Toggle(Keywords.Fixed, Keywords.Auto);

        /// <summary>
        /// Represents a converter for the empty cells mode.
        /// </summary>
        public static readonly IValueConverter<Boolean> EmptyCellsConverter = Toggle(Keywords.Show, Keywords.Hide);

        /// <summary>
        /// Represents a converter for the caption side mode.
        /// </summary>
        public static readonly IValueConverter<Boolean> CaptionSideConverter = Toggle(Keywords.Top, Keywords.Bottom);

        /// <summary>
        /// Represents a converter for the backface visibility mode.
        /// </summary>
        public static readonly IValueConverter<Boolean> BackfaceVisibilityConverter = Toggle(Keywords.Visible, Keywords.Hidden);

        /// <summary>
        /// Represents a converter for the border collapse mode.
        /// </summary>
        public static readonly IValueConverter<Boolean> BorderCollapseConverter = Toggle(Keywords.Separate, Keywords.Collapse);

        /// <summary>
        /// Represents a converter for the box decoration break mode.
        /// </summary>
        public static readonly IValueConverter<Boolean> BoxDecorationConverter = Toggle(Keywords.Clone, Keywords.Slice);

        /// <summary>
        /// Represents a converter for the column span mode.
        /// </summary>
        public static readonly IValueConverter<Boolean> ColumnSpanConverter = Toggle(Keywords.All, Keywords.None);

        /// <summary>
        /// Represents a converter for the column fill mode.
        /// </summary>
        public static readonly IValueConverter<Boolean> ColumnFillConverter = Toggle(Keywords.Balance, Keywords.Auto);

        #endregion

        #region Misc

        /// <summary>
        /// Creates a new converter by assigning the given identifier to a fixed result.
        /// </summary>
        /// <typeparam name="T">The type of the result.</typeparam>
        /// <param name="identifier">The identifier (keyword) to use.</param>
        /// <param name="result">The fixed result that is returned if the identifier is found.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter<T> Assign<T>(String identifier, T result)
        {
            return new IdentifierValueConverter<T>(identifier, result);
        }

        /// <summary>
        /// Creates a new boolean converter that toggles between the two given keywords.
        /// </summary>
        /// <param name="on">The keyword to use for returning true.</param>
        /// <param name="off">The keyword to use for returning false.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter<Boolean> Toggle(String on, String off)
        {
            return Assign(on, true).Or(off, false);
        }

        #endregion

        #region Order

        /// <summary>
        /// Uses the provided converter successively in order.
        /// </summary>
        /// <typeparam name="T">The type of the converter.</typeparam>
        /// <param name="converter">The converter that should be used successively.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter<T[]> WithOrder<T>(IValueConverter<T> converter)
        {
            return new OrderedOptionsConverter<T>(converter);
        }

        /// <summary>
        /// Uses the two converters in their order to convert provided values.
        /// </summary>
        /// <typeparam name="T1">The type of the first converter.</typeparam>
        /// <typeparam name="T2">The type of the second converter.</typeparam>
        /// <param name="first">The first converter to be applied.</param>
        /// <param name="second">The second converter to be applied.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter<Tuple<T1, T2>> WithOrder<T1, T2>(IValueConverter<T1> first, IValueConverter<T2> second)
        {
            return new OrderedOptionsConverter<T1, T2>(first, second);
        }

        /// <summary>
        /// Uses the three converters in their order to convert provided values.
        /// </summary>
        /// <typeparam name="T1">The type of the first converter.</typeparam>
        /// <typeparam name="T2">The type of the second converter.</typeparam>
        /// <typeparam name="T3">The type of the third converter.</typeparam>
        /// <param name="first">The first converter to be applied.</param>
        /// <param name="second">The second converter to be applied.</param>
        /// <param name="third">The third converter to be applied.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter<Tuple<T1, T2, T3>> WithOrder<T1, T2, T3>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third)
        {
            return new OrderedOptionsConverter<T1, T2, T3>(first, second, third);
        }

        #endregion

        #region Unordered

        /// <summary>
        /// Uses the two converters in any order to convert provided values.
        /// </summary>
        /// <typeparam name="T1">The type of the first converter.</typeparam>
        /// <typeparam name="T2">The type of the second converter.</typeparam>
        /// <param name="first">The first value converter to try.</param>
        /// <param name="second">The second value converter to try.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter<Tuple<T1, T2>> WithAny<T1, T2>(IValueConverter<T1> first, IValueConverter<T2> second)
        {
            return new UnorderedOptionsConverter<T1, T2>(first, second);
        }

        /// <summary>
        /// Uses the two converters in any order to convert provided values.
        /// </summary>
        /// <typeparam name="T1">The type of the first converter.</typeparam>
        /// <typeparam name="T2">The type of the second converter.</typeparam>
        /// <typeparam name="T3">The type of the third converter.</typeparam>
        /// <param name="first">The first value converter to try.</param>
        /// <param name="second">The second value converter to try.</param>
        /// <param name="third">The third value converter to try.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter<Tuple<T1, T2, T3>> WithAny<T1, T2, T3>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third)
        {
            return new UnorderedOptionsConverter<T1, T2, T3>(first, second, third);
        }

        /// <summary>
        /// Uses the two converters in any order to convert provided values.
        /// </summary>
        /// <typeparam name="T1">The type of the first converter.</typeparam>
        /// <typeparam name="T2">The type of the second converter.</typeparam>
        /// <typeparam name="T3">The type of the third converter.</typeparam>
        /// <typeparam name="T4">The type of the fourth converter.</typeparam>
        /// <param name="first">The first value converter to try.</param>
        /// <param name="second">The second value converter to try.</param>
        /// <param name="third">The third value converter to try.</param>
        /// <param name="fourth">The fourth value converter to try.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter<Tuple<T1, T2, T3, T4>> WithAny<T1, T2, T3, T4>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth)
        {
            return new UnorderedOptionsConverter<T1, T2, T3, T4>(first, second, third, fourth);
        }

        /// <summary>
        /// Uses the two converters in any order to convert provided values.
        /// </summary>
        /// <typeparam name="T1">The type of the first converter.</typeparam>
        /// <typeparam name="T2">The type of the second converter.</typeparam>
        /// <typeparam name="T3">The type of the third converter.</typeparam>
        /// <typeparam name="T4">The type of the fourth converter.</typeparam>
        /// <typeparam name="T5">The type of the fifth converter.</typeparam>
        /// <param name="first">The first value converter to try.</param>
        /// <param name="second">The second value converter to try.</param>
        /// <param name="third">The third value converter to try.</param>
        /// <param name="fourth">The fourth value converter to try.</param>
        /// <param name="fifth">The fifth value converter to try.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter<Tuple<T1, T2, T3, T4, T5>> WithAny<T1, T2, T3, T4, T5>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth)
        {
            return new UnorderedOptionsConverter<T1, T2, T3, T4, T5>(first, second, third, fourth, fifth);
        }

        /// <summary>
        /// Uses the two converters in any order to convert provided values.
        /// </summary>
        /// <typeparam name="T1">The type of the first converter.</typeparam>
        /// <typeparam name="T2">The type of the second converter.</typeparam>
        /// <typeparam name="T3">The type of the third converter.</typeparam>
        /// <typeparam name="T4">The type of the fourth converter.</typeparam>
        /// <typeparam name="T5">The type of the fifth converter.</typeparam>
        /// <typeparam name="T6">The type of the sixth converter.</typeparam>
        /// <param name="first">The first value converter to try.</param>
        /// <param name="second">The second value converter to try.</param>
        /// <param name="third">The third value converter to try.</param>
        /// <param name="fourth">The fourth value converter to try.</param>
        /// <param name="fifth">The fifth value converter to try.</param>
        /// <param name="sixth">The sixth value converter to try.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter<Tuple<T1, T2, T3, T4, T5, T6>> WithAny<T1, T2, T3, T4, T5, T6>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth, IValueConverter<T6> sixth)
        {
            return new UnorderedOptionsConverter<T1, T2, T3, T4, T5, T6>(first, second, third, fourth, fifth, sixth);
        }

        /// <summary>
        /// Uses the two converters in any order to convert provided values.
        /// </summary>
        /// <typeparam name="T1">The type of the first converter.</typeparam>
        /// <typeparam name="T2">The type of the second converter.</typeparam>
        /// <typeparam name="T3">The type of the third converter.</typeparam>
        /// <typeparam name="T4">The type of the fourth converter.</typeparam>
        /// <typeparam name="T5">The type of the fifth converter.</typeparam>
        /// <typeparam name="T6">The type of the sixth converter.</typeparam>
        /// <typeparam name="T7">The type of the seventh converter.</typeparam>
        /// <param name="first">The first value converter to try.</param>
        /// <param name="second">The second value converter to try.</param>
        /// <param name="third">The third value converter to try.</param>
        /// <param name="fourth">The fourth value converter to try.</param>
        /// <param name="fifth">The fifth value converter to try.</param>
        /// <param name="sixth">The sixth value converter to try.</param>
        /// <param name="seventh">The seventh value converter to try.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter<Tuple<T1, T2, T3, T4, T5, T6, T7>> WithAny<T1, T2, T3, T4, T5, T6, T7>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth, IValueConverter<T6> sixth, IValueConverter<T7> seventh)
        {
            return new UnorderedOptionsConverter<T1, T2, T3, T4, T5, T6, T7>(first, second, third, fourth, fifth, sixth, seventh);
        }

        /// <summary>
        /// Uses the two converters in any order to convert provided values.
        /// </summary>
        /// <typeparam name="T1">The type of the first converter.</typeparam>
        /// <typeparam name="T2">The type of the second converter.</typeparam>
        /// <typeparam name="T3">The type of the third converter.</typeparam>
        /// <typeparam name="T4">The type of the fourth converter.</typeparam>
        /// <typeparam name="T5">The type of the fifth converter.</typeparam>
        /// <typeparam name="T6">The type of the sixth converter.</typeparam>
        /// <typeparam name="T7">The type of the seventh converter.</typeparam>
        /// <typeparam name="T8">The type of the eighth converter.</typeparam>
        /// <param name="first">The first value converter to try.</param>
        /// <param name="second">The second value converter to try.</param>
        /// <param name="third">The third value converter to try.</param>
        /// <param name="fourth">The fourth value converter to try.</param>
        /// <param name="fifth">The fifth value converter to try.</param>
        /// <param name="sixth">The sixth value converter to try.</param>
        /// <param name="seventh">The seventh value converter to try.</param>
        /// <param name="eighth">The eighth value converter to try.</param>
        /// <returns>The new converter.</returns>
        public static IValueConverter<Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>> WithAny<T1, T2, T3, T4, T5, T6, T7, T8>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth, IValueConverter<T6> sixth, IValueConverter<T7> seventh, IValueConverter<T8> eighth)
        {
            return new UnorderedOptionsConverter<T1, T2, T3, T4, T5, T6, T7, T8>(first, second, third, fourth, fifth, sixth, seventh, eighth);
        }

        #endregion

        #region Helper

        static T Get<T>(T[] array, Int32 index, T defaultValue)
        {
            return array.Length > index ? array[index] : defaultValue;
        }

        static IValueConverter<T> Construct<T>(Func<IValueConverter<T>> f)
        {
            return f();
        }

        static IValueConverter<T> WithArg<T>(IValueConverter<T> converter)
        {
            return converter.Atomic();
        }

        static IValueConverter<T> WithArgs<T1, T>(IValueConverter<T1> converter, Int32 arguments, Func<T1[], T> transform)
        {
            return new ArgumentsValueConverter<T1>(converter, arguments).To(transform);
        }

        static IValueConverter<T> WithArgs<T1, T2, T>(IValueConverter<T1> first, IValueConverter<T2> second, Func<Tuple<T1, T2>, T> converter)
        {
            return new ArgumentsValueConverter<T1, T2>(first, second).To(converter);
        }

        static IValueConverter<T> WithArgs<T1, T2, T3, T>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, Func<Tuple<T1, T2, T3>, T> converter)
        {
            return new ArgumentsValueConverter<T1, T2, T3>(first, second, third).To(converter);
        }

        static IValueConverter<T> WithArgs<T1, T2, T3, T4, T>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, Func<Tuple<T1, T2, T3, T4>, T> converter)
        {
            return new ArgumentsValueConverter<T1, T2, T3, T4>(first, second, third, fourth).To(converter);
        }

        #endregion
    }
}
