namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font
    /// </summary>
    sealed class CSSFontProperty : CSSShorthandProperty, ICssFontProperty
    {
        #region Fields

        static readonly Dictionary<String, SystemFont> _parts = new Dictionary<String, SystemFont>();
        readonly CSSFontStyleProperty _style;
        readonly CSSFontVariantProperty _variant;
        readonly CSSFontWeightProperty _weight;
        readonly CSSFontStretchProperty _stretch;
        readonly CSSFontSizeProperty _size;
        readonly CSSLineHeightProperty _height;
        readonly CSSFontFamilyProperty _families;

        internal static readonly IValueConverter<SystemFont> SystemFontConverter = _parts.ToConverter();

        internal static readonly IValueConverter<Tuple<Tuple<ICssValue, ICssValue, ICssValue, ICssValue>, Tuple<ICssValue, ICssValue>, ICssValue>> Converter = WithOrder(
            Converters.WithAny(
                CSSFontStyleProperty.Converter.Val().Option(),
                CSSFontVariantProperty.Converter.Val().Option(),
                CSSFontWeightProperty.Converter.Val().Option(),
                CSSFontStretchProperty.Converter.Val().Option()
            ),
            Converters.WithOrder(
                CSSFontSizeProperty.Converter.Val().Required(),
                CSSLineHeightProperty.Converter.Val().StartsWithDelimiter().Option()
            ),
            CSSFontFamilyProperty.Converter.Val().Required()
        );

        #endregion

        #region ctor

        static CSSFontProperty()
        {
            _parts.Add(Keywords.Caption, SystemFont.Caption);
            _parts.Add(Keywords.Icon, SystemFont.Icon);
            _parts.Add(Keywords.Menu, SystemFont.Menu);
            _parts.Add(Keywords.MessageBox, SystemFont.MessageBox);
            _parts.Add(Keywords.SmallCaption, SystemFont.SmallCaption);
            _parts.Add(Keywords.StatusBar, SystemFont.StatusBar);
        }

        internal CSSFontProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Font, rule, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
            _style = Get<CSSFontStyleProperty>();
            _variant = Get<CSSFontVariantProperty>();
            _weight = Get<CSSFontWeightProperty>();
            _stretch = Get<CSSFontStretchProperty>();
            _size = Get<CSSFontSizeProperty>();
            _height = Get<CSSLineHeightProperty>();
            _families = Get<CSSFontFamilyProperty>();
        }

        #endregion

        #region Properties

        public Int32 Weight
        {
            get { return _weight.Weight; }
        }

        public Boolean IsRelative
        {
            get { return _weight.IsRelative; }
        }

        /// <summary>
        /// Gets the value of the font style property.
        /// </summary>
        public FontStyle Style
        {
            get { return _style.Style; }
        }

        /// <summary>
        /// Gets the value of the font variant property.
        /// </summary>
        public FontVariant Variant
        {
            get { return _variant.Variant; }
        }

        /// <summary>
        /// Gets the value of the font stretch property.
        /// </summary>
        public FontStretch Stretch
        {
            get { return _stretch.Stretch; }
        }

        /// <summary>
        /// Gets the custom set size of the font, if any.
        /// </summary>
        public IDistance Size
        {
            get { return _size.Size; }
        }

        /// <summary>
        /// Gets the value of the line height property.
        /// </summary>
        public IDistance Height
        {
            get { return _height.Height; }
        }

        /// <summary>
        /// Gets the value of the font family property.
        /// </summary>
        public IEnumerable<String> Families
        {
            get { return _families.Families; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            //[ [ <‘font-style’> || <font-variant-css21> || <‘font-weight’> || <‘font-stretch’> ]? <‘font-size’> [ / <‘line-height’> ]? <‘font-family’> ] | caption | icon | menu | message-box | small-caption | status-bar

            return Converter.TryConvert(value, m =>
            {
                _style.TrySetValue(m.Item1.Item1);
                _variant.TrySetValue(m.Item1.Item2);
                _weight.TrySetValue(m.Item1.Item3);
                _stretch.TrySetValue(m.Item1.Item4);
                _size.TrySetValue(m.Item2.Item1);
                _height.TrySetValue(m.Item2.Item2);
                _families.TrySetValue(m.Item3);
            }) || SystemFontConverter.TryConvert(value, SetSystemFont);
        }

        /// <summary>
        /// Instead of specifying individual longhand properties, a
        /// keyword can be used to represent a specific system font.
        /// </summary>
        /// <param name="font">The font to select.</param>
        static void SetSystemFont(SystemFont font)
        {
            //TODO set properties to the setting given by the enumeration value

            switch (font)
            {
                case SystemFont.Caption:
                case SystemFont.Icon:
                case SystemFont.Menu:
                case SystemFont.MessageBox:
                case SystemFont.SmallCaption:
                case SystemFont.StatusBar:
                    break;
            }
        }

        internal override String SerializeValue(IEnumerable<CSSProperty> properties)
        {
            if (!properties.Contains(_families) || !properties.Contains(_size))
                return String.Empty;

            var values = new List<String>();

            if (_style.HasValue && properties.Contains(_style))
                values.Add(_style.SerializeValue());

            if (_variant.HasValue && properties.Contains(_variant))
                values.Add(_variant.SerializeValue());

            if (_weight.HasValue && properties.Contains(_weight))
                values.Add(_weight.SerializeValue());

            if (_stretch.HasValue && properties.Contains(_stretch))
                values.Add(_stretch.SerializeValue());

            values.Add(_size.SerializeValue());

            if (_height.HasValue && properties.Contains(_height))
            {
                values.Add("/");
                values.Add(_height.SerializeValue());
            }

            values.Add(_families.SerializeValue());
            values.RemoveAll(m => String.IsNullOrEmpty(m));

            return String.Join(" ", values);
        }

        #endregion
    }
}
