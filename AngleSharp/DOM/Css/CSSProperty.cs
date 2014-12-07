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

        Boolean _important;
        ICssValue _value;

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

        public static IValueConverter<Length> WithBorderWidth()
        {
            return Converters.LineWidthConverter;
        }

        public static IValueConverter<IDistance> WithBorderSlice()
        {
            return Converters.BorderSliceConverter;
        }

        /// <summary>
        /// Represents a length object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/length
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<Length> WithLength()
        {
            return Converters.LengthConverter;
        }

        /// <summary>
        /// Represents a time object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/time
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<Time> WithTime()
        {
            return Converters.TimeConverter;
        }

        public static IValueConverter<IDistance> WithDistance()
        {
            return Converters.DistanceConverter;
        }

        /// <summary>
        /// Represents an identifier object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/user-ident
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<String> WithIdentifier()
        {
            return Converters.IdentifierConverter;
        }

        public static IValueConverter<Tuple<Tuple<T1, T2, T3, T4>, Tuple<T5, T6, T7, T8>>> WithOptions<T1, T2, T3, T4, T5, T6, T7, T8>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth, IValueConverter<T6> sixth, IValueConverter<T7> seventh, IValueConverter<T8> eighth, Tuple<Tuple<T1, T2, T3, T4>, Tuple<T5, T6, T7, T8>> defaults)
        {
            return new OptionsValueConverter<T1, T2, T3, T4, T5, T6, T7, T8>(first, second, third, fourth, fifth, sixth, seventh, eighth, defaults);
        }

        /// <summary>
        /// Represents an integer object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/integer
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<Int32> WithInteger()
        {
            return Converters.IntegerConverter;
        }

        /// <summary>
        /// Represents a number object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/number
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<Single> WithNumber()
        {
            return Converters.NumberConverter;
        }

        /// <summary>
        /// Represents an image source object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/image
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<IImageSource> WithImageSource()
        {
            return Converters.ImageSourceConverter;
        }

        /// <summary>
        /// Represents a color object.
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/color_value
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<Color> WithColor()
        {
            return Converters.ColorConverter;
        }

        /// <summary>
        /// Represents a transform function.
        /// http://dev.w3.org/csswg/css-transforms/#two-d-transform-functions
        /// </summary>
        /// <returns>The value converter.</returns>
        public static IValueConverter<ITransform> WithTransform()
        {
            return Converters.TransformConverter;
        }

        public static IValueConverter<Tuple<T1, T2>> WithOrder<T1, T2>(IValueConverter<T1> first, IValueConverter<T2> second)
        {
            return new OrderedOptionsConverter<T1, T2>(first, second);
        }

        public static IValueConverter<Tuple<T1, T2, T3>> WithOrder<T1, T2, T3>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third)
        {
            return new OrderedOptionsConverter<T1, T2, T3>(first, second, third);
        }

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

        public static IValueConverter<Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>> WithAny<T1, T2, T3, T4, T5, T6, T7, T8>(IValueConverter<T1> first, IValueConverter<T2> second, IValueConverter<T3> third, IValueConverter<T4> fourth, IValueConverter<T5> fifth, IValueConverter<T6> sixth, IValueConverter<T7> seventh, IValueConverter<T8> eighth)
        {
            return new UnorderedOptionsConverter<T1, T2, T3, T4, T5, T6, T7, T8>(first, second, third, fourth, fifth, sixth, seventh, eighth);
        }

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
    }
}
