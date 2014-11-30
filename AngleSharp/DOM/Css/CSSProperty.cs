namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Fore more information about CSS properties see:
    /// http://www.w3.org/TR/CSS21/propidx.html.
    /// </summary>
    abstract class CSSProperty : ICssProperty, ICssObject
    {
        #region Fields

        readonly PropertyFlags _flags;
        readonly String _name;
        readonly CSSStyleDeclaration _rule;
        protected CSSValue _value;

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
        internal CSSValue Value
        {
            get { return _value ?? CSSValue.Initial; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the property is inherited.
        /// </summary>
        public Boolean IsInherited
        {
            get { return (_flags.HasFlag(PropertyFlags.Inherited) && IsInitial) || _value == CSSValue.Inherit; }
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
            get { return _value == null || _value == CSSValue.Initial; }
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

        #endregion

        #region Internal Methods

        /// <summary>
        /// Tries to set the given value and returns the status.
        /// </summary>
        /// <param name="value">The value that should be set.</param>
        /// <returns>True if the value is valid, otherwise false.</returns>
        internal Boolean TrySetValue(CSSValue value)
        {
            if (value == CSSValue.Inherit || value == CSSValue.Initial || value == null)
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

        internal Boolean CanTake(CSSValue value)
        {
            var current = _value;

            if (TrySetValue(value))
            {
                TrySetValue(current);
                return true;
            }

            return false;
        }

        internal Boolean CanStore(CSSValue value, ref CSSValue storagePosition)
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
        protected abstract Boolean IsValid(CSSValue value);

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the property.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public String ToCss()
        {
            return Serialize(_name, SerializeValue(), _important);
        }

        /// <summary>
        /// Serializes the current value of the CSS property.
        /// </summary>
        /// <returns></returns>
        internal virtual String SerializeValue()
        {
            return Value.ToCss();
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
            return WithOptions(TakeOne(Keywords.Left, -1).Or(TakeOne(Keywords.Right, 1)), TakeOne(Keywords.Top, -1).Or(TakeOne(Keywords.Bottom, 1)), Tuple.Create(0, 0)).
                   To(m => new Angle((Single)(Math.Atan2(m.Item1 - 0.5, 0.5 - m.Item2) * 180.0 / Math.PI), Angle.Unit.Deg));
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
        public static IValueConverter<Single> WithRatio()
        {
            return new StructValueConverter<Single>(ValueExtensions.ToAspectRatio);
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
            return new ClassValueConverter<String>(ValueExtensions.ToFontFamily);
        }

        public static IValueConverter<IDistance> WithDistance()
        {
            return new ClassValueConverter<IDistance>(ValueExtensions.ToDistance);
        }

        public static IValueConverter<Shadow> WithShadow()
        {
            return new ClassValueConverter<Shadow>(ValueExtensions.ToShadow);
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
            return new FunctionValueConverter<CssAttr>(FunctionNames.Attr, WithString().Or(WithIdentifier()).To(m => new CssAttr(m)));
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
            return new DictionaryValueConverter<TransitionFunction>(Map.TransitionFunctions).
                Or(new FunctionValueConverter<TransitionFunction>(FunctionNames.Steps,
                        WithInteger().To(m => (TransitionFunction)new StepsTransitionFunction(m)).Or(
                        WithArgs(WithInteger(), TakeOne(Keywords.Start, true).Or(TakeOne(Keywords.End, false)), m => (TransitionFunction)new StepsTransitionFunction(m.Item1, m.Item2))))).
                Or(new FunctionValueConverter<TransitionFunction>(FunctionNames.CubicBezier,
                        WithArgs(WithNumber(), WithNumber(), WithNumber(), WithNumber(), m => (TransitionFunction)new CubicBezierTransitionFunction(m.Item1, m.Item2, m.Item3, m.Item4))));
        }

        public static IValueConverter<Counter> WithCounter()
        {
            return new FunctionValueConverter<Counter>(FunctionNames.Counter,
                        WithIdentifier().To(m => new Counter(m, Keywords.Decimal, null)).Or(
                        WithArgs(WithIdentifier(), WithIdentifier(), m => new Counter(m.Item1, m.Item2, null)))).
                Or(new FunctionValueConverter<Counter>(FunctionNames.Counters,
                        WithArgs(WithIdentifier(), WithString(), m => new Counter(m.Item1, Keywords.Decimal, m.Item2)).Or(
                        WithArgs(WithIdentifier(), WithString(), WithIdentifier(), m => new Counter(m.Item1, m.Item3, m.Item2)))));
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
                       WithArgs(WithLength(), WithLength(), WithLength(), WithLength(), m => new Shape(m.Item1, m.Item2, m.Item3, m.Item4)));
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

        public static IValueConverter<GradientStop[]> WithGradientStops()
        {
            return new ClassValueConverter<GradientStop[]>(ValueExtensions.ToGradientStops);
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
            var angle = FirstArg(WithAngle().Or(side));
            var stops = RestArgs(WithGradientStops(), 1);
            var args = angle.And(stops).Or(WithGradientStops().To(m => Tuple.Create(Angle.Zero, m)));

            return new FunctionValueConverter<LinearGradient>(FunctionNames.LinearGradient,
                        args.To(m => new LinearGradient(m.Item1, m.Item2, false))).Or(
                   new FunctionValueConverter<LinearGradient>(FunctionNames.RepeatingLinearGradient,
                        args.To(m => new LinearGradient(m.Item1, m.Item2, true))));
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
            var args = FirstArg(WithAngle()).And(RestArgs(WithGradientStops(), 1)).Or(
                       WithGradientStops().To(m => Tuple.Create(Angle.Zero, m)));

            return new FunctionValueConverter<RadialGradient>(FunctionNames.RadialGradient,
                        args.To(m => new RadialGradient(Percent.Fifty, Percent.Fifty, Percent.Hundred, Percent.Hundred, m.Item2, false))).Or(
                   new FunctionValueConverter<RadialGradient>(FunctionNames.RepeatingRadialGradient,
                        args.To(m => new RadialGradient(Percent.Fifty, Percent.Fifty, Percent.Hundred, Percent.Hundred, m.Item2, true))));
        }

        /// <summary>
        /// Represents a gradient object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/gradient
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<IImageSource> WithGradient()
        {
            return WithLinearGradient().To(m => (IImageSource)m).Or(
                   WithRadialGradient().To(m => (IImageSource)m));
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
                        WithArgs(WithByte(), WithByte(), WithByte(), m => new Color(m.Item1, m.Item2, m.Item3)))).Or(
                   new FunctionValueConverter<Color>(FunctionNames.Rgba,
                        WithArgs(WithByte(), WithByte(), WithByte(), WithNumber(), m => new Color(m.Item1, m.Item2, m.Item3, m.Item4)))).Or(
                   new FunctionValueConverter<Color>(FunctionNames.Hsl,
                        WithArgs(WithNumber(), WithPercent(), WithPercent(), m => Color.FromHsl(hnorm * m.Item1, m.Item2.NormalizedValue, m.Item3.NormalizedValue)))).Or(
                   new FunctionValueConverter<Color>(FunctionNames.Hsla,
                        WithArgs(WithNumber(), WithPercent(), WithPercent(), WithNumber().Constraint(m => m >= 0f && m <= 1f), m => Color.FromHsla(hnorm * m.Item1, m.Item2.NormalizedValue, m.Item3.NormalizedValue, m.Item4))));
        }

        /// <summary>
        /// Represents a transform function.
        /// http://dev.w3.org/csswg/css-transforms/#two-d-transform-functions
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<ITransform> WithTransform()
        {
            return new FunctionValueConverter<ITransform>(FunctionNames.Matrix,
                        WithArgs(WithNumber(), 6, m => (ITransform)new MatrixTransform(m[0], m[1], m[2], m[3], m[4], m[5]))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.Matrix3d,
                        WithArgs(WithNumber(), 12, m => (ITransform)new Matrix3DTransform(m[0], m[1], m[2], m[3], m[4], m[5], m[6], m[7], m[8], m[9], m[10], m[11])))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.Translate,
                        WithDistance().To(m => (ITransform)new TranslateTransform(m, Length.Zero)).Or(
                        WithArgs(WithDistance(), 2, m => (ITransform)new TranslateTransform(m[0], m[1]))))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.Translate3d,
                        WithDistance().To(m => (ITransform)new Translate3DTransform(m, Length.Zero, Length.Zero)).Or(
                        WithArgs(WithDistance(), 2, m => (ITransform)new Translate3DTransform(m[0], m[1], Length.Zero))).Or(
                        WithArgs(WithDistance(), 3, m => (ITransform)new Translate3DTransform(m[0], m[1], m[2]))))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.TranslateX,
                        WithDistance().To(m => (ITransform)new TranslateXTransform(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.TranslateY,
                        WithDistance().To(m => (ITransform)new TranslateYTransform(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.TranslateZ,
                        WithDistance().To(m => (ITransform)new TranslateZTransform(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.Scale,
                        WithNumber().To(m => (ITransform)new ScaleTransform(m, m)).Or(
                        WithArgs(WithNumber(), 2, m => (ITransform)new ScaleTransform(m[0], m[1]))))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.Scale3d,
                        WithNumber().To(m => (ITransform)new Scale3DTransform(m, m, m)).Or(
                        WithArgs(WithNumber(), 3, m => (ITransform)new Scale3DTransform(m[0], m[1], m[2]))))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.ScaleX,
                        WithNumber().To(m => (ITransform)new ScaleXTransform(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.ScaleY,
                        WithNumber().To(m => (ITransform)new ScaleYTransform(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.ScaleZ,
                        WithNumber().To(m => (ITransform)new ScaleZTransform(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.Rotate,
                        WithAngle().To(m => (ITransform)new RotateTransform(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.Rotate3d,
                        WithArgs(WithNumber(), WithNumber(), WithNumber(), WithAngle(), m => (ITransform)new Rotate3DTransform(m.Item1, m.Item2, m.Item3, m.Item4)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.RotateX,
                        WithAngle().To(m => (ITransform)Rotate3DTransform.RotateX(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.RotateY,
                        WithAngle().To(m => (ITransform)Rotate3DTransform.RotateY(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.RotateZ,
                        WithAngle().To(m => (ITransform)Rotate3DTransform.RotateZ(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.Skew,
                        WithArgs(WithAngle(), 2, m => (ITransform)new SkewTransform(m[0], m[1])))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.SkewX,
                        WithAngle().To(m => (ITransform)new SkewXTransform(m)))).
                Or(new FunctionValueConverter<ITransform>(FunctionNames.SkewY,
                       WithAngle().To(m => (ITransform)new SkewYTransform(m))));
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
