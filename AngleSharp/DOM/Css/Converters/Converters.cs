namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

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
        public static readonly IValueConverter<IDistance> LineHeightConverter = new ClassValueConverter<IDistance>(ValueExtensions.ToLineHeight);

        /// <summary>
        /// Represents a length object that is based on percentage or number.
        /// http://dev.w3.org/csswg/css-backgrounds/#border-image-slice
        /// </summary>
        public static readonly IValueConverter<IDistance> BorderSliceConverter = new ClassValueConverter<IDistance>(ValueExtensions.ToBorderSlice);

        /// <summary>
        /// Represents a length object that is based on percentage, length or number.
        /// http://dev.w3.org/csswg/css-backgrounds/#border-image-width
        /// </summary>
        public static readonly IValueConverter<IDistance> ImageBorderWidthConverter = new ClassValueConverter<IDistance>(ValueExtensions.ToImageBorderWidth);

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
        /// Represents an integer object that is zero or greater.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/integer
        /// </summary>
        public static readonly IValueConverter<Int32> PositiveIntegerConverter = IntegerConverter.Constraint(m => m >= 0);

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
        /// Represents a distance object (either Length or Percent).
        /// </summary>
        public static readonly IValueConverter<IDistance> DistanceConverter = new ClassValueConverter<IDistance>(ValueExtensions.ToDistance);

        /// <summary>
        /// Represents an color object (usually hex or name).
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color
        /// </summary>
        public static readonly IValueConverter<Color> PureColorConverter = new StructValueConverter<Color>(ValueExtensions.ToColor);

        #endregion

        #region Functions

        /// <summary>
        /// Represents the angle to a side or corner of a box.
        /// http://dev.w3.org/csswg/css-images-3/#typedef-side-or-corner
        /// </summary>
        public static readonly IValueConverter<Angle> SideOrCornerConverter = WithAny(
            TakeOne(Keywords.Left, -1.0).Or(TakeOne(Keywords.Right, 1.0)).Option(0.0),
            TakeOne(Keywords.Top, 1.0).Or(TakeOne(Keywords.Bottom, -1.0)).Option(0.0)
        ).To(m => new Angle((Single)(Math.Atan2(m.Item1, m.Item2) * 180.0 / Math.PI), Angle.Unit.Deg));

        /// <summary>
        /// Represents a position object.
        /// http://www.w3.org/TR/css3-background/#ltpositiongt
        /// </summary>
        public static readonly IValueConverter<Point> PointConverter = Construct(() =>
        {
            var hi = TakeOne(Keywords.Left, (IDistance)Percent.Zero).Or(TakeOne(Keywords.Right, (IDistance)Percent.Hundred)).Or(TakeOne(Keywords.Center, (IDistance)Percent.Fifty));
            var vi = TakeOne(Keywords.Top, (IDistance)Percent.Zero).Or(TakeOne(Keywords.Bottom, (IDistance)Percent.Hundred)).Or(TakeOne(Keywords.Center, (IDistance)Percent.Fifty));
            var h = hi.Or(DistanceConverter).Required();
            var v = vi.Or(DistanceConverter).Required();

            return DistanceConverter.To(m => new Point(x: m)).Or(
                   Toggle(Keywords.Left, Keywords.Right).To(m => new Point(x: m ? Percent.Zero : Percent.Hundred))).Or(
                   Toggle(Keywords.Top, Keywords.Bottom).To(m => new Point(y: m ? Percent.Zero : Percent.Hundred))).Or(
                   TakeOne(Keywords.Center, new Point())).Or(
                   WithArgs(h, v, m => new Point(m.Item1, m.Item2))).Or(
                   WithArgs(v, h, m => new Point(m.Item2, m.Item1))).Or(
                   WithArgs(hi, vi, DistanceConverter, m => new Point(m.Item1, m.Item2.Add(m.Item3)))).Or(
                   WithArgs(hi, DistanceConverter, vi, m => new Point(m.Item1.Add(m.Item2), m.Item3))).Or(
                   WithArgs(hi, DistanceConverter, vi, DistanceConverter, m => new Point(m.Item1.Add(m.Item2), m.Item3.Add(m.Item4))));
        });

        /// <summary>
        /// Represents an attribute retriever object.
        /// http://dev.w3.org/csswg/css-values/#funcdef-attr
        /// </summary>
        public static readonly IValueConverter<CssAttr> AttrConverter = new FunctionValueConverter<CssAttr>(
            FunctionNames.Attr, WithArg(StringConverter.Or(IdentifierConverter).To(m => new CssAttr(m))));

        /// <summary>
        /// Represents a steps timing-function object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/timing-function
        /// </summary>
        public static readonly IValueConverter<StepsTransitionFunction> StepsConverter = new FunctionValueConverter<StepsTransitionFunction>(
            FunctionNames.Steps, WithArgs(
                IntegerConverter.Required(), 
                TakeOne(Keywords.Start, true).Or(TakeOne(Keywords.End, false)).Option(false), 
            m => new StepsTransitionFunction(m.Item1, m.Item2)));

        /// <summary>
        /// Represents a cubic-bezier timing-function object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/timing-function
        /// </summary>
        public static readonly IValueConverter<CubicBezierTransitionFunction> CubicBezierConverter = Construct(() =>
        {
            var number = NumberConverter.Required();
            return new FunctionValueConverter<CubicBezierTransitionFunction>(FunctionNames.CubicBezier,
                    WithArgs(number, number, number, number, m => new CubicBezierTransitionFunction(m.Item1, m.Item2, m.Item3, m.Item4)));
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
                        WithArg(TakeMany(LengthConverter, 4, 4).To(m => new Shape(m[0], m[1], m[2], m[3])))));
        });

        /// <summary>
        /// Represents a linear-gradient object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/linear-gradient
        /// </summary>
        public static readonly IValueConverter<LinearGradient> LinearGradientConverter = Construct(() =>
        {
            var side = SideOrCornerConverter.StartsWithKeyword(Keywords.To);
            var angle = AngleConverter.Or(side);
            var gradient = new GradientConverter<Angle>(angle, new Angle(180f, Angle.Unit.Deg));

            return new FunctionValueConverter<LinearGradient>(FunctionNames.LinearGradient,
                        gradient.To(m => new LinearGradient(m.Item1, m.Item2, false))).Or(
                   new FunctionValueConverter<LinearGradient>(FunctionNames.RepeatingLinearGradient,
                        gradient.To(m => new LinearGradient(m.Item1, m.Item2, true))));
        });

        /// <summary>
        /// Represents a radial-gradient object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/radial-gradient
        /// </summary>
        public static readonly IValueConverter<RadialGradient> RadialGradientConverter = Construct(() =>
        {
            //TODO
            //Determine first argument (if any):
            // [ <ending-shape> || <size> ]? [ at <position> ]?
            //where:
            // <size> = [ <predefined> | <length> | [ <length> | <percentage> ]{2} ]
            // <ending-shape> = [ ellipse | circle ]
            // <predefined> = [ closest-side | closest-corner | farthest-side | farthest-corner ]

            var distance = DistanceConverter.Required();
            var center = new Point(Percent.Fifty, Percent.Fifty);
            var zeroSize = new Point(Percent.Zero, Percent.Zero);
            var defaults = Tuple.Create(zeroSize, center);
            var endingShape = Toggle(Keywords.Ellipse, Keywords.Circle).To(m => zeroSize);
            var predefinedSize = IdentifierConverter.Constraint(m => m.IsOneOf(Keywords.ClosestSide, Keywords.ClosestCorner, Keywords.FarthestSide, Keywords.FarthestCorner)).To(m => new Point(Percent.Zero, Percent.Zero));
            var size = predefinedSize.Or(LengthConverter.To(m => new Point(m, m))).Or(WithArgs(distance, distance, m => new Point(m.Item1, m.Item2)));
            var dimensions = WithOrder(WithAny(endingShape.Option(zeroSize), size.Option(zeroSize)), WithOrder(TakeOne(Keywords.At, true).Required(), PointConverter.Required()).Option(Tuple.Create(true, center))).To(m => Tuple.Create(m.Item1.Item2, m.Item2.Item2));
            var gradient = new GradientConverter<Tuple<Point, Point>>(dimensions, defaults);

            return new FunctionValueConverter<RadialGradient>(FunctionNames.RadialGradient,
                        gradient.To(m => new RadialGradient(m.Item1.Item2, m.Item1.Item1, m.Item2, false))).Or(
                   new FunctionValueConverter<RadialGradient>(FunctionNames.RepeatingRadialGradient,
                        gradient.To(m => new RadialGradient(m.Item1.Item2, m.Item1.Item1, m.Item2, true))));
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
            var distance = DistanceConverter.Required();
            var option = DistanceConverter.Option(Length.Zero);
            return new FunctionValueConverter<TranslateTransform>(FunctionNames.Translate,
                        WithArgs(distance, option, m => new TranslateTransform(m.Item1, m.Item2, Length.Zero))).Or(
                   new FunctionValueConverter<TranslateTransform>(FunctionNames.Translate3d,
                        WithArgs(distance, option, option, m => new TranslateTransform(m.Item1, m.Item2, m.Item3)))).Or(
                   new FunctionValueConverter<TranslateTransform>(FunctionNames.TranslateX,
                        WithArg(DistanceConverter.To(m => new TranslateTransform(m, Length.Zero, Length.Zero))))).Or(
                   new FunctionValueConverter<TranslateTransform>(FunctionNames.TranslateY,
                        WithArg(DistanceConverter.To(m => new TranslateTransform(Length.Zero, m, Length.Zero))))).Or(
                   new FunctionValueConverter<TranslateTransform>(FunctionNames.TranslateZ,
                        WithArg(DistanceConverter.To(m => new TranslateTransform(Length.Zero, Length.Zero, m)))));
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
                        WithArg(AngleConverter.To(m => RotateTransform.RotateZ(m)))).Or(
                   new FunctionValueConverter<RotateTransform>(FunctionNames.Rotate3d,
                        WithArgs(number, number, number, AngleConverter.Required(), m => new RotateTransform(m.Item1, m.Item2, m.Item3, m.Item4)))).Or(
                   new FunctionValueConverter<RotateTransform>(FunctionNames.RotateX,
                        WithArg(AngleConverter.To(m => RotateTransform.RotateX(m))))).Or(
                   new FunctionValueConverter<RotateTransform>(FunctionNames.RotateY,
                        WithArg(AngleConverter.To(m => RotateTransform.RotateY(m))))).Or(
                   new FunctionValueConverter<RotateTransform>(FunctionNames.RotateZ,
                        WithArg(AngleConverter.To(m => RotateTransform.RotateZ(m)))));
        });

        /// <summary>
        /// A broad variety of skew transforms.
        /// http://www.w3.org/TR/css3-transforms/#funcdef-skew
        /// </summary>
        public static readonly IValueConverter<SkewTransform> SkewTransformConverter = Construct(() =>
        {
            var angle = AngleConverter.Required();
            return new FunctionValueConverter<SkewTransform>(FunctionNames.Skew,
                        WithArgs(angle, angle, m => new SkewTransform(m.Item1, m.Item2))).Or(
                   new FunctionValueConverter<SkewTransform>(FunctionNames.SkewX,
                        WithArg(AngleConverter.To(m => new SkewTransform(m, Angle.Zero))))).Or(
                   new FunctionValueConverter<SkewTransform>(FunctionNames.SkewY,
                        WithArg(AngleConverter.To(m => new SkewTransform(Angle.Zero, m)))));
        });

        #endregion

        #region Composed

        /// <summary>
        /// Represents a timing-function object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/timing-function
        /// </summary>
        public static readonly IValueConverter<TransitionFunction> TransitionConverter = new DictionaryValueConverter<TransitionFunction>(Map.TransitionFunctions).Or(
            StepsConverter.To(m => (TransitionFunction)m)).Or(
            CubicBezierConverter.To(m => (TransitionFunction)m));

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
        /// <returns>The value converter.</returns>
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
        public static readonly IValueConverter<Shadow> ShadowConverter = WithOrder(
            TakeOne(Keywords.Inset, true).Option(false),
            TakeMany(LengthConverter, 2, 4).Required(),
            ColorConverter.Option(Color.Black)).To(
            m => new Shadow(m.Item1, m.Item2[0], m.Item2[1], Get(m.Item2, 2, Length.Zero), Get(m.Item2, 3, Length.Zero), m.Item3));


        /// <summary>
        /// Represents an image source object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/image
        /// </summary>
        /// <returns>The value converter.</returns>
        public static readonly IValueConverter<IImageSource> ImageSourceConverter = UrlConverter.To(m => (IImageSource)new ImageUrl(m)).Or(
            GradientConverter);

        #endregion

        #region Arguments

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

        #region Misc

        public static IValueConverter<Boolean> Toggle(String on, String off)
        {
            return TakeOne(on, true).Or(TakeOne(off, false));
        }

        public static IValueConverter<T> TakeOne<T>(String identifier, T result)
        {
            return new IdentifierValueConverter<T>(identifier, result);
        }

        public static IValueConverter<T[]> TakeMany<T>(IValueConverter<T> converter, Int32 min = 1, Int32 max = Int32.MaxValue)
        {
            return new OneOrMoreValueConverter<T>(converter, min, max);
        }

        public static IValueConverter<T[]> TakeList<T>(IValueConverter<T> converter)
        {
            return new ListValueConverter<T>(converter);
        }

        #endregion

        #region Order

        public static IValueConverter<Tuple<T1, T2>> WithOrder<T1, T2>(IValueConverter<T1> first, IValueConverter<T2> second)
        {
            return new OrderedOptionsConverter<T1, T2>(first, second);
        }

        public static IValueConverter<Tuple<T1, T2, T3>> WithOrder<T1, T2, T3>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third)
        {
            return new OrderedOptionsConverter<T1, T2, T3>(first, second, third);
        }

        #endregion

        #region Unordered

        public static IValueConverter<Tuple<T1, T2>> WithAny<T1, T2>(IValueConverter<T1> first, IValueConverter<T2> second)
        {
            return new UnorderedOptionsConverter<T1, T2>(first, second);
        }

        public static IValueConverter<Tuple<T1, T2, T3>> WithAny<T1, T2, T3>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third)
        {
            return new UnorderedOptionsConverter<T1, T2, T3>(first, second, third);
        }

        public static IValueConverter<Tuple<T1, T2, T3, T4>> WithAny<T1, T2, T3, T4>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth)
        {
            return new UnorderedOptionsConverter<T1, T2, T3, T4>(first, second, third, fourth);
        }

        public static IValueConverter<Tuple<T1, T2, T3, T4, T5>> WithAny<T1, T2, T3, T4, T5>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth)
        {
            return new UnorderedOptionsConverter<T1, T2, T3, T4, T5>(first, second, third, fourth, fifth);
        }

        public static IValueConverter<Tuple<T1, T2, T3, T4, T5, T6>> WithAny<T1, T2, T3, T4, T5, T6>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth, IValueConverter<T6> sixth)
        {
            return new UnorderedOptionsConverter<T1, T2, T3, T4, T5, T6>(first, second, third, fourth, fifth, sixth);
        }

        public static IValueConverter<Tuple<T1, T2, T3, T4, T5, T6, T7>> WithAny<T1, T2, T3, T4, T5, T6, T7>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth, IValueConverter<T6> sixth, IValueConverter<T7> seventh)
        {
            return new UnorderedOptionsConverter<T1, T2, T3, T4, T5, T6, T7>(first, second, third, fourth, fifth, sixth, seventh);
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

        #endregion
    }
}
