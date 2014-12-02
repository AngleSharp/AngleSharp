namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Fore more information about CSS properties see:
    /// http://www.w3.org/TR/CSS21/propidx.html.
    /// </summary>
    abstract class CSSProperty : ICssProperty
    {
        #region Fields

        readonly PropertyFlags _flags;
        readonly String _name;
        readonly CSSStyleDeclaration _rule;
        protected ICssValue _value;

        Boolean _important;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS property.
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <param name="rule">The parent style declaration.</param>
        /// <param name="flags">The property flags, if any.</param>
        internal CSSProperty(String name, CSSStyleDeclaration rule, PropertyFlags flags = PropertyFlags.None)
        {
            _rule = rule;
            _name = name;
            _flags = flags;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets or sets the assigned rule.
        /// </summary>
        internal CSSStyleDeclaration Rule
        {
            get { return _rule; }
        }

        /// <summary>
        /// Gets if the property has a value.
        /// </summary>
        internal Boolean HasValue
        {
            get { return _value != null; }
        }

        /// <summary>
        /// Gets if the property supports hashless colors in quirksmode.
        /// </summary>
        internal Boolean IsHashless
        {
            get { return _flags.HasFlag(PropertyFlags.Hashless); }
        }

        /// <summary>
        /// Gets if the property supports unitless lengths in quirksmode.
        /// </summary>
        internal Boolean IsUnitless
        {
            get { return _flags.HasFlag(PropertyFlags.Unitless); }
        }

        /// <summary>
        /// Gets if the property is actually only a shorthand.
        /// </summary>
        internal Boolean IsShorthand
        {
            get { return _flags.HasFlag(PropertyFlags.Shorthand); }
        }

        /// <summary>
        /// Gets the value of the property.
        /// </summary>
        internal ICssValue Value
        {
            get { return _value ?? CssValue.Initial; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the property is inherited.
        /// </summary>
        public Boolean IsInherited
        {
            get { return (_flags.HasFlag(PropertyFlags.Inherited) && IsInitial) || _value == CssValue.Inherit; }
        }

        /// <summary>
        /// Gets if the property can be animated, in general.
        /// </summary>
        public Boolean IsAnimatable
        {
            get { return _flags.HasFlag(PropertyFlags.Animatable); }
        }

        /// <summary>
        /// Gets if the property is currently in its initial state.
        /// </summary>
        public Boolean IsInitial
        {
            get { return _value == null || _value == CssValue.Initial; }
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public String Name
        {
            get { return _name; }
        }

        ICssValue ICssProperty.Value
        {
            get { return Value; }
        }

        /// <summary>
        /// Gets or sets if the !important flag has been set.
        /// </summary>
        public Boolean IsImportant
        {
            get { return _important; }
            set { _important = value; }
        }

        /// <summary>
        /// Gets a CSS code representation of the property.
        /// </summary>
        public String CssText
        {
            get { return Serialize(_name, SerializeValue(), _important); }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Tries to set the given value and returns the status.
        /// </summary>
        /// <param name="value">The value that should be set.</param>
        /// <returns>True if the value is valid, otherwise false.</returns>
        internal Boolean TrySetValue(ICssValue value)
        {
            if (value == CssValue.Inherit || value == CssValue.Initial || value == null)
            {
                Reset();
                _value = value;
                return true;
            }
            else if (IsValid(value))
            {
                _value = value;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Resets the property to its initial state.
        /// </summary>
        internal abstract void Reset();

        #endregion

        #region Methods

        internal Boolean CanTake(ICssValue value)
        {
            var current = _value;

            if (TrySetValue(value))
            {
                TrySetValue(current);
                return true;
            }

            return false;
        }

        internal Boolean CanStore(ICssValue value, ref ICssValue storagePosition)
        {
            if (storagePosition == null && CanTake(value))
            {
                storagePosition = value;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Notified once the value changed.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <returns>True if the value is valid, otherwise false.</returns>
        protected abstract Boolean IsValid(ICssValue value);

        #endregion

        #region String representation

        /// <summary>
        /// Serializes the current value of the CSS property.
        /// </summary>
        /// <returns></returns>
        internal virtual String SerializeValue()
        {
            return Value.CssText;
        }

        /// <summary>
        /// Serializes the full CSS declaration.
        /// </summary>
        /// <param name="name">The name of the declaration.</param>
        /// <param name="value">The value of the declaration.</param>
        /// <param name="important">True if the important flag is set.</param>
        /// <returns>The string representation of the declaration.</returns>
        internal static String Serialize(String name, String value, Boolean important)
        {
            return String.Concat(name, ": ", String.Concat(value, important ? " !important" : String.Empty, ";"));
        }

        #endregion

        #region Value Converters

        public static IValueConverter<T> From<T>(Dictionary<String, T> values)
        {
            return new DictionaryValueConverter<T>(values);
        }

        public static IValueConverter<Angle> WithSideOrCorner()
        {
            return WithOptions(TakeOne(Keywords.Left, -1.0).Or(TakeOne(Keywords.Right, 1.0)), TakeOne(Keywords.Top, 1.0).Or(TakeOne(Keywords.Bottom, -1.0)), Tuple.Create(0.0, 0.0)).
                   To(m => new Angle((Single)(Math.Atan2(m.Item1, m.Item2) * 180.0 / Math.PI), Angle.Unit.Deg));
        }

        public static IValueConverter<Length> WithBorderWidth()
        {
            return new StructValueConverter<Length>(ValueExtensions.ToBorderWidth);
        }

        public static IValueConverter<IDistance> WithLineHeight()
        {
            return new ClassValueConverter<IDistance>(ValueExtensions.ToLineHeight);
        }

        public static IValueConverter<IDistance> WithBorderSlice()
        {
            return new ClassValueConverter<IDistance>(ValueExtensions.ToBorderSlice);
        }

        public static IValueConverter<Point> WithPoint()
        {
            return new ClassValueConverter<Point>(ValueExtensions.ToPoint);
        }

        public static IValueConverter<IDistance> WithImageBorderWidth()
        {
            return new ClassValueConverter<IDistance>(ValueExtensions.ToImageBorderWidth);
        }

        /// <summary>
        /// Represents a ratio object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/ratio
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<Tuple<Int32, Int32>> WithRatio()
        {
            var condition = new StructValueConverter<Boolean>(m => m == CssValue.Delimiter ? (Boolean?)true : null);
            return new SplitValueConverter<Boolean, Int32>(condition, WithArg(WithInteger()), false).Constraint(m => m.Length == 2).To(m => Tuple.Create(m[0], m[1]));
        }

        /// <summary>
        /// Represents a length object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/length
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<Length> WithLength()
        {
            return new StructValueConverter<Length>(ValueExtensions.ToLength);
        }

        /// <summary>
        /// Represents a resolution object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/resolution
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<Resolution> WithResolution()
        {
            return new StructValueConverter<Resolution>(ValueExtensions.ToResolution);
        }

        /// <summary>
        /// Represents a frequency object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/frequency
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<Frequency> WithFrequency()
        {
            return new StructValueConverter<Frequency>(ValueExtensions.ToFrequency);
        }

        /// <summary>
        /// Represents a time object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/time
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<Time> WithTime()
        {
            return new StructValueConverter<Time>(ValueExtensions.ToTime);
        }

        public static IValueConverter<String> WithFontFamily()
        {
            return From(Map.DefaultFontFamilies).Or(WithString()).Or(TakeMany(WithIdentifier()).To(names => String.Join(" ", names)));
        }

        public static IValueConverter<IDistance> WithDistance()
        {
            return new ClassValueConverter<IDistance>(ValueExtensions.ToDistance);
        }

        public static IValueConverter<Shadow> WithShadow()
        {
            return WithArgs(WithLength().Required(), WithLength().Required(), WithColor().Option(Color.Black),
                        m => new Shadow(false, m.Item1, m.Item2, Length.Zero, Length.Zero, m.Item3)).Or(
                   WithArgs(WithLength().Required(), WithLength().Required(), WithLength().Required(), WithColor().Option(Color.Black), 
                        m => new Shadow(false, m.Item1, m.Item2, m.Item3, Length.Zero, m.Item4))).Or(
                   WithArgs(WithLength().Required(), WithLength().Required(), WithLength().Required(), WithLength().Required(), WithColor().Option(Color.Black), 
                        m => new Shadow(false, m.Item1, m.Item2, m.Item3, m.Item4, m.Item5))).Or(
                   WithArgs(TakeOne(Keywords.Inset, true).Required(), WithLength().Required(), WithLength().Required(), WithColor().Option(Color.Black), 
                        m => new Shadow(m.Item1, m.Item2, m.Item3, Length.Zero, Length.Zero, m.Item4))).Or(
                   WithArgs(TakeOne(Keywords.Inset, true).Required(), WithLength().Required(), WithLength().Required(), WithLength().Required(), WithColor().Option(Color.Black), 
                        m => new Shadow(m.Item1, m.Item2, m.Item3, m.Item4, Length.Zero, m.Item5))).Or(
                   WithArgs(TakeOne(Keywords.Inset, true).Required(), WithLength().Required(), WithLength().Required(), WithLength().Required(), WithLength().Required(), WithColor().Option(Color.Black), 
                        m => new Shadow(m.Item1, m.Item2, m.Item3, m.Item4, m.Item5, m.Item6)));
        }

        /// <summary>
        /// Represents an URL object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/uri
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<CssUrl> WithUrl()
        {
            return new ClassValueConverter<CssUrl>(ValueExtensions.ToUri);
        }

        /// <summary>
        /// Represents a string object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/string
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<String> WithString()
        {
            return new ClassValueConverter<String>(ValueExtensions.ToCssString);
        }

        /// <summary>
        /// Represents an identifier object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/user-ident
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<String> WithIdentifier()
        {
            return new ClassValueConverter<String>(ValueExtensions.ToIdentifier);
        }

        public static IValueConverter<String> WithAnimatableIdentifier()
        {
            return new ClassValueConverter<String>(ValueExtensions.ToAnimatableIdentifier);
        }

        public static IValueConverter<CssAttr> WithAttr()
        {
            return new FunctionValueConverter<CssAttr>(FunctionNames.Attr, WithString().Or(WithIdentifier()).To(m => new CssAttr(m)).Atomic());
        }

        public static IValueConverter<T> WithArg<T>(IValueConverter<T> converter)
        {
            return new ArgumentsValueConverter<T>(converter, 1).To(m => m[0]);
        }

        public static IValueConverter<T> WithArgs<T1, T>(IValueConverter<T1> first, Int32 arguments, Func<T1[], T> converter)
        {
            return new ArgumentsValueConverter<T1>(first, arguments).To(converter);
        }

        public static IValueConverter<T> WithArgs<T1, T2, T>(IValueConverter<T1> first, IValueConverter<T2> second, Func<Tuple<T1, T2>, T> converter)
        {
            return new ArgumentsValueConverter<T1, T2>(first, second).To(converter);
        }

        public static IValueConverter<T> WithArgs<T1, T2, T3, T>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, Func<Tuple<T1, T2, T3>, T> converter)
        {
            return new ArgumentsValueConverter<T1, T2, T3>(first, second, third).To(converter);
        }

        public static IValueConverter<T> WithArgs<T1, T2, T3, T4, T>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, Func<Tuple<T1, T2, T3, T4>, T> converter)
        {
            return new ArgumentsValueConverter<T1, T2, T3, T4>(first, second, third, fourth).To(converter);
        }

        public static IValueConverter<T> WithArgs<T1, T2, T3, T4, T5, T>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth, Func<Tuple<T1, T2, T3, T4, T5>, T> converter)
        {
            return new ArgumentsValueConverter<T1, T2, T3, T4, T5>(first, second, third, fourth, fifth).To(converter);
        }

        public static IValueConverter<T> WithArgs<T1, T2, T3, T4, T5, T6, T>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth, IValueConverter<T6> sixth, Func<Tuple<T1, T2, T3, T4, T5, T6>, T> converter)
        {
            return new ArgumentsValueConverter<T1, T2, T3, T4, T5, T6>(first, second, third, fourth, fifth, sixth).To(converter);
        }

        public static IValueConverter<Tuple<T1, T2>> WithOptions<T1, T2>(IValueConverter<T1> first, IValueConverter<T2> second, Tuple<T1, T2> defaults)
        {
            return new OptionsValueConverter<T1, T2>(first, second, defaults);
        }

        public static IValueConverter<Tuple<T1, T2, T3>> WithOptions<T1, T2, T3>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, Tuple<T1, T2, T3> defaults)
        {
            return new OptionsValueConverter<T1, T2, T3>(first, second, third, defaults);
        }

        public static IValueConverter<Tuple<T1, T2, T3, T4>> WithOptions<T1, T2, T3, T4>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, Tuple<T1, T2, T3, T4> defaults)
        {
            return new OptionsValueConverter<T1, T2, T3, T4>(first, second, third, fourth, defaults);
        }

        public static IValueConverter<Tuple<Tuple<T1, T2, T3, T4>, Tuple<T5, T6, T7, T8>>> WithOptions<T1, T2, T3, T4, T5, T6, T7, T8>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth, IValueConverter<T6> sixth, IValueConverter<T7> seventh, IValueConverter<T8> eighth, Tuple<Tuple<T1, T2, T3, T4>, Tuple<T5, T6, T7, T8>> defaults)
        {
            return new OptionsValueConverter<T1, T2, T3, T4, T5, T6, T7, T8>(first, second, third, fourth, fifth, sixth, seventh, eighth, defaults);
        }

        /// <summary>
        /// Represents a timing-function object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/timing-function
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<TransitionFunction> WithTransition()
        {
            return new DictionaryValueConverter<TransitionFunction>(Map.TransitionFunctions).Or(
                   new FunctionValueConverter<TransitionFunction>(FunctionNames.Steps,
                        WithArgs(WithInteger().Required(), TakeOne(Keywords.Start, true).Or(TakeOne(Keywords.End, false)).Option(false), 
                            m => (TransitionFunction)new StepsTransitionFunction(m.Item1, m.Item2)))).Or(
                   new FunctionValueConverter<TransitionFunction>(FunctionNames.CubicBezier,
                        WithArgs(WithNumber().Required(), WithNumber().Required(), WithNumber().Required(), WithNumber().Required(), 
                            m => (TransitionFunction)new CubicBezierTransitionFunction(m.Item1, m.Item2, m.Item3, m.Item4))));
        }

        public static IValueConverter<Counter> WithCounter()
        {
            return new FunctionValueConverter<Counter>(FunctionNames.Counter,
                        WithArgs(WithIdentifier().Required(), WithIdentifier().Option(Keywords.Decimal), 
                            m => new Counter(m.Item1, m.Item2, null))).Or(
                   new FunctionValueConverter<Counter>(FunctionNames.Counters,
                        WithArgs(WithIdentifier().Required(), WithString().Required(), WithIdentifier().Option(Keywords.Decimal), 
                            m => new Counter(m.Item1, m.Item3, m.Item2))));
        }

        /// <summary>
        /// Represents an integer object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/integer
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<Int32> WithInteger()
        {
            return new StructValueConverter<Int32>(ValueExtensions.ToInteger);
        }

        /// <summary>
        /// Represents a shape object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/shape
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<Shape> WithShape()
        {
            return new FunctionValueConverter<Shape>(FunctionNames.Rect,
                        WithArgs(WithLength().Required(), WithLength().Required(), WithLength().Required(), WithLength().Required(), 
                            m => new Shape(m.Item1, m.Item2, m.Item3, m.Item4)).Or(
                        WithArg(TakeMany(WithLength()).Constraint(m => m.Length == 4).To(
                            m => new Shape(m[0], m[1], m[2], m[3])))));
        }

        /// <summary>
        /// Represents an angle object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/angle
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<Angle> WithAngle()
        {
            return new StructValueConverter<Angle>(ValueExtensions.ToAngle);
        }

        /// <summary>
        /// Represents a number object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/number
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<Single> WithNumber()
        {
            return new StructValueConverter<Single>(ValueExtensions.ToSingle);
        }

        /// <summary>
        /// Represents a percentage object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/percentage
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<Percent> WithPercent()
        {
            return new StructValueConverter<Percent>(ValueExtensions.ToPercent);
        }

        /// <summary>
        /// Represents an integer object reduced to [0, 255].
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/integer
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<Byte> WithByte()
        {
            return new StructValueConverter<Byte>(ValueExtensions.ToByte);
        }

        public static IValueConverter<T> FirstArg<T>(IValueConverter<T> converter)
        {
            return new SubsetValueConverter<T>(converter, 0, 1);
        }

        public static IValueConverter<T> RestArgs<T>(IValueConverter<T> converter, Int32 start)
        {
            return new SubsetValueConverter<T>(converter, start, -1);
        }

        /// <summary>
        /// Represents a linear-gradient object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/linear-gradient
        /// </summary>
        /// <returns>The value converter.</returns>
        static IValueConverter<LinearGradient> WithLinearGradient()
        {
            var side = FirstArg(TakeOne(Keywords.To, true)).And(RestArgs(WithSideOrCorner(), 1)).To(m => m.Item2);
            var angle = WithAngle().Or(side);
            var gradient = new GradientConverter<Angle>(angle, new Angle(180f, Angle.Unit.Deg));

            return new FunctionValueConverter<LinearGradient>(FunctionNames.LinearGradient,
                        gradient.To(m => new LinearGradient(m.Item1, m.Item2, false))).Or(
                   new FunctionValueConverter<LinearGradient>(FunctionNames.RepeatingLinearGradient,
                        gradient.To(m => new LinearGradient(m.Item1, m.Item2, true))));
        }
        
        /// <summary>
        /// Represents a radial-gradient object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/radial-gradient
        /// </summary>
        /// <returns>The value converter.</returns>
        static IValueConverter<RadialGradient> WithRadialGradient()
        {
            //TODO
            //Determine first argument (if any):
            // [ <ending-shape> || <size> ]? [ at <position> ]?
            //where:
            // <size> = [ <predefined> | <length> | [ <length> | <percentage> ]{2} ]
            // <ending-shape> = [ ellipse | circle ]
            // <predefined> = [ closest-side | closest-corner | farthest-side | farthest-corner ]
            
            var center = new Point(Percent.Fifty, Percent.Fifty);
            var zeroSize = new Point(Percent.Zero, Percent.Zero);
            var defaults = Tuple.Create(zeroSize, center);
            var endingShape = Toggle(Keywords.Ellipse, Keywords.Circle).To(m => zeroSize);
            var predefinedSize = WithIdentifier().Constraint(m => m.IsOneOf(Keywords.ClosestSide, Keywords.ClosestCorner, Keywords.FarthestSide, Keywords.FarthestCorner)).To(m => new Point(Percent.Zero, Percent.Zero));
            var size = predefinedSize.Or(WithLength().To(m => new Point(m, m))).Or(WithArgs(WithDistance().Required(), WithDistance().Required(), m => new Point(m.Item1, m.Item2)));
            var position = FirstArg(TakeOne(Keywords.At, true)).And(RestArgs(WithPoint(), 1)).To(m => m.Item2);
            var dimensions = WithOptions(endingShape.Or(size), position, defaults);
            var gradient = new GradientConverter<Tuple<Point, Point>>(dimensions, defaults);

            return new FunctionValueConverter<RadialGradient>(FunctionNames.RadialGradient,
                        gradient.To(m => new RadialGradient(m.Item1.Item2, m.Item1.Item1, m.Item2, false))).Or(
                   new FunctionValueConverter<RadialGradient>(FunctionNames.RepeatingRadialGradient,
                        gradient.To(m => new RadialGradient(m.Item1.Item2, m.Item1.Item1, m.Item2, true))));
        }

        /// <summary>
        /// Represents a gradient object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/gradient
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<IImageSource> WithGradient()
        {
            return WithLinearGradient().To(m => (IImageSource)m).Or(WithRadialGradient().To(m => (IImageSource)m));
        }

        /// <summary>
        /// Represents an image source object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/image
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<IImageSource> WithImageSource()
        {
            return WithUrl().To(m => (IImageSource)new ImageUrl(m)).Or(WithGradient());
        }

        /// <summary>
        /// Represents a color object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color_value
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<Color> WithColor()
        {
            const Single hnorm = 1f / 360f;

            return new StructValueConverter<Color>(ValueExtensions.ToColor).Or(
                   new FunctionValueConverter<Color>(FunctionNames.Rgb,
                        WithArgs(WithByte().Required(), WithByte().Required(), WithByte().Required(), 
                            m => new Color(m.Item1, m.Item2, m.Item3)))).Or(
                   new FunctionValueConverter<Color>(FunctionNames.Rgba,
                        WithArgs(WithByte().Required(), WithByte().Required(), WithByte().Required(), WithNumber().Required(), 
                            m => new Color(m.Item1, m.Item2, m.Item3, m.Item4)))).Or(
                   new FunctionValueConverter<Color>(FunctionNames.Hsl,
                        WithArgs(WithNumber().Required(), WithPercent().Required(), WithPercent().Required(), 
                            m => Color.FromHsl(hnorm * m.Item1, m.Item2.NormalizedValue, m.Item3.NormalizedValue)))).Or(
                   new FunctionValueConverter<Color>(FunctionNames.Hsla,
                        WithArgs(WithNumber().Required(), WithPercent().Required(), WithPercent().Required(), WithNumber().Constraint(m => m >= 0f && m <= 1f).Required(), 
                            m => Color.FromHsla(hnorm * m.Item1, m.Item2.NormalizedValue, m.Item3.NormalizedValue, m.Item4))));
        }

        /// <summary>
        /// Represents a transform function.
        /// http://dev.w3.org/csswg/css-transforms/#two-d-transform-functions
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<ITransform> WithTransform()
        {
            return WithMatrixTransform().To(m => (ITransform)m).Or(
                   WithScaleTransform().To(m => (ITransform)m)).Or(
                   WithRotateTransform().To(m => (ITransform)m)).Or(
                   WithTranslateTransform().To(m => (ITransform)m)).Or(
                   WithSkewTransform().To(m => (ITransform)m));
        }

        public static IValueConverter<MatrixTransform> WithMatrixTransform()
        {
            return new FunctionValueConverter<MatrixTransform>(FunctionNames.Matrix,
                        WithArgs(WithNumber(), 6,
                            m => new MatrixTransform(m[0], m[1], 0f, m[2], m[3], 0f, 0f, 0f, 1f, m[4], m[5], 0f))).Or(
                   new FunctionValueConverter<MatrixTransform>(FunctionNames.Matrix3d,
                        WithArgs(WithNumber(), 12,
                            m => new MatrixTransform(m[0], m[1], m[2], m[3], m[4], m[5], m[6], m[7], m[8], m[9], m[10], m[11]))));
        }

        public static IValueConverter<TranslateTransform> WithTranslateTransform()
        {
            return new FunctionValueConverter<TranslateTransform>(FunctionNames.Translate,
                        WithArgs(WithDistance().Required(), WithDistance().Option(Length.Zero),
                            m => new TranslateTransform(m.Item1, m.Item2, Length.Zero))).Or(
                   new FunctionValueConverter<TranslateTransform>(FunctionNames.Translate3d,
                        WithArgs(WithDistance().Required(), WithDistance().Option(Length.Zero), WithDistance().Option(Length.Zero),
                            m => new TranslateTransform(m.Item1, m.Item2, m.Item3)))).Or(
                   new FunctionValueConverter<TranslateTransform>(FunctionNames.TranslateX,
                        WithArg(WithDistance().To(
                            m => new TranslateTransform(m, Length.Zero, Length.Zero))))).Or(
                   new FunctionValueConverter<TranslateTransform>(FunctionNames.TranslateY,
                        WithArg(WithDistance().To(
                            m => new TranslateTransform(Length.Zero, m, Length.Zero))))).Or(
                   new FunctionValueConverter<TranslateTransform>(FunctionNames.TranslateZ,
                        WithArg(WithDistance().To(
                            m => new TranslateTransform(Length.Zero, Length.Zero, m)))));
        }

        public static IValueConverter<ScaleTransform> WithScaleTransform()
        {
            return new FunctionValueConverter<ScaleTransform>(FunctionNames.Scale,
                        WithArgs(WithNumber().Required(), WithNumber().Option(Single.NaN),
                            m => new ScaleTransform(m.Item1, Single.IsNaN(m.Item2) ? m.Item1 : m.Item2, 1f))).Or(
                   new FunctionValueConverter<ScaleTransform>(FunctionNames.Scale3d,
                        WithArgs(WithNumber().Required(), WithNumber().Option(Single.NaN), WithNumber().Option(Single.NaN),
                            m => new ScaleTransform(m.Item1, Single.IsNaN(m.Item2) ? m.Item1 : m.Item2, Single.IsNaN(m.Item3) ? m.Item1 : m.Item3)))).Or(
                   new FunctionValueConverter<ScaleTransform>(FunctionNames.ScaleX,
                        WithArg(WithNumber().To(
                            m => new ScaleTransform(m, 1f, 1f))))).Or(
                   new FunctionValueConverter<ScaleTransform>(FunctionNames.ScaleY,
                        WithArg(WithNumber().To(
                            m => new ScaleTransform(1f, m, 1f))))).Or(
                   new FunctionValueConverter<ScaleTransform>(FunctionNames.ScaleZ,
                        WithArg(WithNumber().To(
                            m => new ScaleTransform(1f, 1f, m)))));
        }

        public static IValueConverter<RotateTransform> WithRotateTransform()
        {
            return new FunctionValueConverter<RotateTransform>(FunctionNames.Rotate,
                        WithArg(WithAngle().To(
                            m => RotateTransform.RotateZ(m)))).Or(
                   new FunctionValueConverter<RotateTransform>(FunctionNames.Rotate3d,
                        WithArgs(WithNumber().Required(), WithNumber().Required(), WithNumber().Required(), WithAngle().Required(), 
                            m => new RotateTransform(m.Item1, m.Item2, m.Item3, m.Item4)))).Or(
                   new FunctionValueConverter<RotateTransform>(FunctionNames.RotateX,
                        WithArg(WithAngle().To(
                            m => RotateTransform.RotateX(m))))).Or(
                   new FunctionValueConverter<RotateTransform>(FunctionNames.RotateY,
                        WithArg(WithAngle().To(
                            m => RotateTransform.RotateY(m))))).Or(
                   new FunctionValueConverter<RotateTransform>(FunctionNames.RotateZ,
                        WithArg(WithAngle().To(
                            m => RotateTransform.RotateZ(m)))));
        }

        public static IValueConverter<SkewTransform> WithSkewTransform()
        {
            return new FunctionValueConverter<SkewTransform>(FunctionNames.Skew,
                        WithArgs(WithAngle().Required(), WithAngle().Required(), 
                            m => new SkewTransform(m.Item1, m.Item2))).Or(
                   new FunctionValueConverter<SkewTransform>(FunctionNames.SkewX,
                        WithArg(WithAngle().To(
                            m => new SkewTransform(m, Angle.Zero))))).Or(
                   new FunctionValueConverter<SkewTransform>(FunctionNames.SkewY,
                       WithArg(WithAngle().To(
                            m => new SkewTransform(Angle.Zero, m)))));
        }

        public static IValueConverter<Boolean> Toggle(String on, String off)
        {
            return TakeOne(on, true).Or(TakeOne(off, false));
        }

        public static IValueConverter<T> TakeOne<T>(String identifier, T result)
        {
            return new IdentifierValueConverter<T>(identifier, result);
        }

        public static IValueConverter<T[]> TakeMany<T>(IValueConverter<T> converter)
        {
            return new OneOrMoreValueConverter<T>(converter);
        }

        public static IValueConverter<T[]> TakeList<T>(IValueConverter<T> converter)
        {
            return new ListValueConverter<T>(converter);
        }

        #endregion
    }
}
