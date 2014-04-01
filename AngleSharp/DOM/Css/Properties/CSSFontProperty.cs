namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font
    /// </summary>
    sealed class CSSFontProperty : CSSCompoundProperty
    {
        #region Fields

        CSSFontStyleProperty _style;
        CSSFontVariantProperty _variant;
        CSSFontWeightProperty _weight;
        CSSFontSizeProperty _size;
        CSSFontFamilyProperty _family;
        CSSLineHeightProperty _height;

        #endregion

        #region ctor

        public CSSFontProperty()
            : base(PropertyNames.FONT)
        {
            _style = new CSSFontStyleProperty();
            _variant = new CSSFontVariantProperty();
            _weight = new CSSFontWeightProperty();
            _size = new CSSFontSizeProperty();
            _family = new CSSFontFamilyProperty();
            _height = new CSSLineHeightProperty();
            _inherited = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the style of the font.
        /// </summary>
        public CSSProperty FontStyle
        {
            get { return _style; }
        }

        /// <summary>
        /// Gets or sets the variant of the font.
        /// </summary>
        public CSSProperty FontVariant
        {
            get { return _variant; }
        }

        /// <summary>
        /// Gets or sets the weight of the font.
        /// </summary>
        public CSSProperty FontWeight
        {
            get { return _weight; }
        }

        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        public CSSProperty FontSize
        {
            get { return _size; }
        }

        /// <summary>
        /// Gets or sets the family of the font.
        /// </summary>
        public CSSProperty FontFamily
        {
            get { return _family; }
        }

        /// <summary>
        /// Gets or sets the height of the line.
        /// </summary>
        public CSSProperty LineHeight
        {
            get { return _height; }
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
        sealed class CSSFontStyleProperty : CSSProperty
        {
            static readonly Dictionary<String, FontStyle> _styles = new Dictionary<String, FontStyle>(StringComparer.OrdinalIgnoreCase);
            FontStyle _style;

            static CSSFontStyleProperty()
            {
                _styles.Add("normal", FontStyle.Normal);
                _styles.Add("italic", FontStyle.Italic);
                _styles.Add("oblique", FontStyle.Oblique);
            }

            public CSSFontStyleProperty()
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
        sealed class CSSFontVariantProperty : CSSProperty
        {
            static readonly Dictionary<String, FontVariant> _styles = new Dictionary<String, FontVariant>(StringComparer.OrdinalIgnoreCase);
            FontVariant _style;

            static CSSFontVariantProperty()
            {
                _styles.Add("normal", FontVariant.Normal);
                _styles.Add("small-caps", FontVariant.SmallCaps);
            }

            public CSSFontVariantProperty()
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
        sealed class CSSFontWeightProperty : CSSProperty
        {
            static readonly ValueConverter<FontWeightMode> _weights = new ValueConverter<FontWeightMode>();
            static readonly NormalWeightMode _normal = new NormalWeightMode();
            FontWeightMode _weight;

            static CSSFontWeightProperty()
            {
                _weights.AddStatic("normal", _normal);
                _weights.AddStatic("bold", new BoldWeightMode());
                _weights.AddStatic("bolder", new BolderWeightMode());
                _weights.AddStatic("lighter", new LighterWeightMode());
                _weights.AddConstructed<NumberWeightMode>();
            }

            public CSSFontWeightProperty()
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
        sealed class CSSFontSizeProperty : CSSProperty
        {
            static readonly ValueConverter<FontSizeMode> _sizes = new ValueConverter<FontSizeMode>();
            static readonly AbsoluteFontSizeMode _medium = new AbsoluteFontSizeMode(AbsoluteSize.Medium);
            FontSizeMode _size;

            static CSSFontSizeProperty()
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

            public CSSFontSizeProperty()
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
        sealed class CSSFontFamilyProperty : CSSProperty
        {
            public CSSFontFamilyProperty()
                : base(PropertyNames.FONT_FAMILY)
            {
                //TODO
            }
        }

        /// <summary>
        /// Information:
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/line-height
        /// </summary>
        sealed class CSSLineHeightProperty : CSSProperty
        {
            static readonly ValueConverter<LineHeightMode> _modes = new ValueConverter<LineHeightMode>();
            static readonly NormalLineHeightMode _normal = new NormalLineHeightMode();
            LineHeightMode _mode;

            static CSSLineHeightProperty()
            {
                _modes.AddStatic("normal", _normal);
                _modes.AddConstructed<RelativeLineHeightMode>();
                _modes.AddConstructed<AbsoluteLineHeightMode>();
                _modes.AddConstructed<MultipleLineHeightMode>();
            }

            public CSSLineHeightProperty()
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
