namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font
    /// </summary>
    sealed class CSSFontProperty : CSSProperty
    {
        #region Fields

        Style _style;
        Variant _variant;
        Weight _weight;
        Size _size;
        Family _family;
        LineHeight _height;

        #endregion

        #region ctor

        public CSSFontProperty()
            : base(PropertyNames.FONT)
        {
            //TODO: These will be only created IF they have been set.
            //_style = new Style();
            //_variant = new Variant();
            //_weight = new Weight();
            //_size = new Size();
            //_family = new Family();
            //_height = new LineHeight();
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            //TODO
            return base.IsValid(value);
        }

        #endregion

        #region Nested

        /// <summary>
        /// Information:
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-style
        /// </summary>
        public sealed class Style : CSSProperty
        {
            static readonly Dictionary<String, FontStyle> _styles = new Dictionary<String, FontStyle>(StringComparer.OrdinalIgnoreCase);
            FontStyle _style;

            static Style()
            {
                _styles.Add("normal", FontStyle.Normal);
                _styles.Add("italic", FontStyle.Italic);
                _styles.Add("oblique", FontStyle.Oblique);
            }

            public Style()
                : base(PropertyNames.FONT_STYLE)
            {
                _inherited = true;
                _style = FontStyle.Normal;
            }

            protected override Boolean IsValid(CSSValue value)
            {
                FontStyle style;

                if (value is CSSIdentifierValue && _styles.TryGetValue(((CSSIdentifierValue)value).Value, out style))
                    _style = style;
                else if (value != CSSValue.Inherit)
                    return false;

                return true;
            }

            enum FontStyle
            {
                Normal,
                Italic,
                Oblique
            }
        }

        /// <summary>
        /// Information:
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-variant
        /// </summary>
        public sealed class Variant : CSSProperty
        {
            static readonly Dictionary<String, FontVariant> _styles = new Dictionary<String, FontVariant>(StringComparer.OrdinalIgnoreCase);
            FontVariant _style;

            static Variant()
            {
                _styles.Add("normal", FontVariant.Normal);
                _styles.Add("small-caps", FontVariant.SmallCaps);
            }

            public Variant()
                : base(PropertyNames.FONT_VARIANT)
            {
                _inherited = true;
                _style = FontVariant.Normal;
            }

            protected override Boolean IsValid(CSSValue value)
            {
                FontVariant style;
                
                if (value is CSSIdentifierValue && _styles.TryGetValue(((CSSIdentifierValue)value).Value, out style))
                    _style = style;
                else if (value != CSSValue.Inherit)
                    return false;

                return true;
            }

            enum FontVariant
            {
                Normal,
                SmallCaps
            }
        }

        /// <summary>
        /// Information:
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight
        /// </summary>
        public sealed class Weight : CSSProperty
        {
            static readonly ValueConverter<FontWeightMode> _weights = new ValueConverter<FontWeightMode>();
            static readonly NormalWeightMode _normal = new NormalWeightMode();
            FontWeightMode _weight;

            static Weight()
            {
                _weights.AddStatic("normal", _normal);
                _weights.AddStatic("bold", new BoldWeightMode());
                _weights.AddStatic("bolder", new BolderWeightMode());
                _weights.AddStatic("lighter", new LighterWeightMode());
                _weights.AddConstructed<NumberWeightMode>();
            }

            public Weight()
                : base(PropertyNames.FONT_WEIGHT)
            {
                _weight = _normal;
                _inherited = true;
            }

            protected override Boolean IsValid(CSSValue value)
            {
                FontWeightMode weight;
                
                if (_weights.TryCreate(value, out weight))
                    _weight = weight;
                else if (value != CSSValue.Inherit)
                    return false;

                return true;
            }

            abstract class FontWeightMode
            { }

            sealed class NormalWeightMode : FontWeightMode
            {
                // 400
            }

            sealed class BoldWeightMode : FontWeightMode
            {
                // 700
            }

            sealed class LighterWeightMode : FontWeightMode
            {
            }

            sealed class BolderWeightMode : FontWeightMode
            {
            }

            sealed class NumberWeightMode : FontWeightMode
            {
                public NumberWeightMode(Int32 weight)
                { }
            }
        }

        /// <summary>
        /// Information:
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-size
        /// </summary>
        public sealed class Size : CSSProperty
        {
            static readonly ValueConverter<FontSizeMode> _sizes = new ValueConverter<FontSizeMode>();
            static readonly AbsoluteFontSizeMode _medium = new AbsoluteFontSizeMode(AbsoluteSize.Medium);
            FontSizeMode _size;

            static Size()
            {
                _sizes.AddStatic("xx-small", new AbsoluteFontSizeMode(AbsoluteSize.Smallest));
                _sizes.AddStatic("x-small", new AbsoluteFontSizeMode(AbsoluteSize.Smaller));
                _sizes.AddStatic("small", new AbsoluteFontSizeMode(AbsoluteSize.Small));
                _sizes.AddStatic("medium", _medium);
                _sizes.AddStatic("large", new AbsoluteFontSizeMode(AbsoluteSize.Large));
                _sizes.AddStatic("x-large", new AbsoluteFontSizeMode(AbsoluteSize.Larger));
                _sizes.AddStatic("xx-large", new AbsoluteFontSizeMode(AbsoluteSize.Largest));
                _sizes.AddStatic("larger", new RelativeFontSizeMode(RelativeSize.Smaller));
                _sizes.AddStatic("smaller", new RelativeFontSizeMode(RelativeSize.Larger));
                _sizes.AddConstructed<PercentFontSizeMode>();
                _sizes.AddConstructed<LengthFontSizeMode>();
            }

            public Size()
                : base(PropertyNames.FONT_SIZE)
            {
                _size = _medium;
                _inherited = true;
            }

            protected override Boolean IsValid(CSSValue value)
            {
                FontSizeMode size;

                if (_sizes.TryCreate(value, out size))
                    _size = size;
                else if (value != CSSValue.Inherit)
                    return false;

                return true;
            }

            abstract class FontSizeMode
            {
                //TODO add members
            }

            sealed class AbsoluteFontSizeMode : FontSizeMode
            {
                public AbsoluteFontSizeMode(AbsoluteSize sizeMode)
                {
                    //TODO init some values
                }
            }

            sealed class RelativeFontSizeMode : FontSizeMode
            {
                public RelativeFontSizeMode(RelativeSize sizeMode)
                {
                    //TODO init some values
                }
            }

            sealed class PercentFontSizeMode : FontSizeMode
            {
                Single _scale;

                public PercentFontSizeMode(CSSPercentValue percent)
                {
                    _scale = percent.Value;
                }
            }

            sealed class LengthFontSizeMode : FontSizeMode
            {
                Length _length;

                public LengthFontSizeMode(Length length)
                {
                    _length = length;
                }
            }

            enum RelativeSize
            {
                Larger,
                Smaller
            }

            enum AbsoluteSize
            {
                Smallest,
                Smaller,
                Small,
                Medium,
                Large,
                Larger,
                Largest
            }
        }

        /// <summary>
        /// Information:
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-family
        /// </summary>
        public sealed class Family : CSSProperty
        {
            public Family()
                : base(PropertyNames.FONT_FAMILY)
            {
                //TODO
            }
        }

        /// <summary>
        /// Information:
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/line-height
        /// </summary>
        public sealed class LineHeight : CSSProperty
        {
            static readonly ValueConverter<LineHeightMode> _modes = new ValueConverter<LineHeightMode>();
            static readonly NormalLineHeightMode _normal = new NormalLineHeightMode();
            LineHeightMode _mode;

            static LineHeight()
            {
                _modes.AddStatic("normal", _normal);
                _modes.AddConstructed<RelativeLineHeightMode>();
                _modes.AddConstructed<AbsoluteLineHeightMode>();
                _modes.AddConstructed<MultipleLineHeightMode>();
            }

            public LineHeight()
                : base(PropertyNames.LINE_HEIGHT)
            {
                _inherited = true;
                _mode = _normal;
            }

            protected override Boolean IsValid(CSSValue value)
            {
                LineHeightMode mode;

                if (_modes.TryCreate(value, out mode))
                    _mode = mode;
                else if (value != CSSValue.Inherit)
                    return false;

                return true;
            }

            abstract class LineHeightMode
            { }

            sealed class NormalLineHeightMode : LineHeightMode
            { }

            sealed class RelativeLineHeightMode : LineHeightMode
            {
                Single _scale;

                public RelativeLineHeightMode(CSSPercentValue percent)
                {
                    _scale = percent.Value;
                }
            }

            sealed class AbsoluteLineHeightMode : LineHeightMode
            {
                Length _length;

                public AbsoluteLineHeightMode(Length length)
                {
                    _length = length;
                }
            }

            sealed class MultipleLineHeightMode : LineHeightMode
            {
                Single _factor;

                public MultipleLineHeightMode(Single factor)
                {
                    _factor = factor;
                }
            }
        }

        #endregion
    }
}
