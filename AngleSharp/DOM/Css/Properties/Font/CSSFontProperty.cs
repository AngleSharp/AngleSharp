namespace AngleSharp.Dom.Css
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
    sealed class CssFontProperty : CssShorthandProperty, ICssFontProperty
    {
        #region Fields

        static readonly Dictionary<String, SystemFont> _parts = new Dictionary<String, SystemFont>();
        readonly CssFontStyleProperty _style;
        readonly CssFontVariantProperty _variant;
        readonly CssFontWeightProperty _weight;
        readonly CssFontStretchProperty _stretch;
        readonly CssFontSizeProperty _size;
        readonly CssLineHeightProperty _height;
        readonly CssFontFamilyProperty _families;

        internal static readonly IValueConverter<SystemFont> SystemFontConverter = _parts.ToConverter();

        internal static readonly IValueConverter<Tuple<Tuple<ICssValue, ICssValue, ICssValue, ICssValue>, Tuple<ICssValue, ICssValue>, ICssValue>> Converter = Converters.WithOrder(
            Converters.WithAny(
                CssFontStyleProperty.Converter.Val().Option(),
                CssFontVariantProperty.Converter.Val().Option(),
                CssFontWeightProperty.Converter.Val().Option(),
                CssFontStretchProperty.Converter.Val().Option()
            ),
            Converters.WithOrder(
                CssFontSizeProperty.Converter.Val().Required(),
                CssLineHeightProperty.Converter.Val().StartsWithDelimiter().Option()
            ),
            CssFontFamilyProperty.Converter.Val().Required()
        );

        #endregion

        #region ctor

        static CssFontProperty()
        {
            _parts.Add(Keywords.Caption, SystemFont.Caption);
            _parts.Add(Keywords.Icon, SystemFont.Icon);
            _parts.Add(Keywords.Menu, SystemFont.Menu);
            _parts.Add(Keywords.MessageBox, SystemFont.MessageBox);
            _parts.Add(Keywords.SmallCaption, SystemFont.SmallCaption);
            _parts.Add(Keywords.StatusBar, SystemFont.StatusBar);
        }

        internal CssFontProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Font, rule, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
            _style = Get<CssFontStyleProperty>();
            _variant = Get<CssFontVariantProperty>();
            _weight = Get<CssFontWeightProperty>();
            _stretch = Get<CssFontStretchProperty>();
            _size = Get<CssFontSizeProperty>();
            _height = Get<CssLineHeightProperty>();
            _families = Get<CssFontFamilyProperty>();
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
        public Length Size
        {
            get { return _size.Size; }
        }

        /// <summary>
        /// Gets the value of the line height property.
        /// </summary>
        public Length Height
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
        void SetSystemFont(SystemFont font)
        {
            switch (font)
            {
                case SystemFont.Caption:
                case SystemFont.Icon:
                case SystemFont.MessageBox:
                    SetFont("Arial", "16px");
                    break;
                case SystemFont.StatusBar:
                case SystemFont.Menu:
                    SetFont("Segoe UI", "12px");
                    break;
                case SystemFont.SmallCaption:
                    SetFont("Segoe UI", "15px");
                    break;
            }
        }

        void SetFont(String family, String size)
        {
            var value = String.Concat(size, " ", family.CssString());
            Rule.SetPropertyValue(Name, value);
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
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
