namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

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
            : base(PropertyNames.Font, rule, new CSSProperty[]{
                new CSSFontStyleProperty(rule),
                new CSSFontWeightProperty(rule),
                new CSSFontVariantProperty(rule),
                new CSSFontStretchProperty(rule),
                new CSSFontSizeProperty(rule),
                new CSSLineHeightProperty(rule),
                new CSSFontFamilyProperty(rule)
            }, PropertyFlags.Inherited | PropertyFlags.Animatable)
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
        /// Gets the mode of the font-size property.
        /// </summary>
        public FontSize SizingMode
        {
            get { return _size.SizingMode; }
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
        protected override Boolean IsValid(CSSValue value)
        {
            SystemFont setting;

            if (!_parts.TryGetValue(value, out setting))
            {
                var entries = value as CSSValueList ?? new CSSValueList(value);
                var allowDelim = false;
                CSSValue weight = null;
                CSSValue style = null;
                CSSValue variant = null;
                CSSValue stretch = null;
                CSSValue size = null;
                CSSValue height = null;

                for (var i = 0; i < entries.Length; i++)
                {
                    var entry = entries[i];

                    if (allowDelim)
                    {
                        if (entry == CSSValue.Delimiter)
                        {
                            if (++i == entries.Length)
                                return false;

                            height = entries[i];

                            if (!_height.CanTake(height) || ++i == entries.Length)
                                return false;
                        }

                        if (!_families.TrySetValue(entries.Subset(start: i)))
                            return false;

                        break;
                    }

                    if (style == null && _style.CanTake(entry))
                    {
                        style = entry;
                        continue;
                    }

                    if (variant == null && _variant.CanTake(entry))
                    {
                        variant = entry;
                        continue;
                    }

                    if (weight == null && _weight.CanTake(entry))
                    {
                        weight = entry;
                        continue;
                    }

                    if (stretch == null && _stretch.CanTake(entry))
                    {
                        stretch = entry;
                        continue;
                    }

                    if (size == null && _size.CanTake(entry))
                    {
                        size = entry;
                        allowDelim = true;
                        continue;
                    }

                    return false;
                }

                return _stretch.TrySetValue(stretch) &&
                    _variant.TrySetValue(variant) &&
                    _size.TrySetValue(size) && 
                    _height.TrySetValue(height) && 
                    _style.TrySetValue(style) && 
                    _weight.TrySetValue(weight);
            }
            else
                SetTo(setting);

            return true;
        }

        /// <summary>
        /// Instead of specifying individual longhand properties, a
        /// keyword can be used to represent a specific system font.
        /// </summary>
        /// <param name="setting">The setting to apply.</param>
        void SetTo(SystemFont setting)
        {
            //TODO set properties to the setting given by the enumeration value

            switch (setting)
            {
                case SystemFont.Caption:
                case SystemFont.Icon:
                case SystemFont.Menu:
                case SystemFont.MessageBox:
                case SystemFont.SmallCaption:
                case SystemFont.StatusBar:
                    return;
            }
        }

        internal static String Stringify(CSSStyleDeclaration style)
        {
            var height = style.GetPropertyCustomText(PropertyNames.LineHeight);
            var parts = new List<String>();
            parts.Add(style.GetPropertyCustomText(PropertyNames.FontStyle));
            parts.Add(style.GetPropertyCustomText(PropertyNames.FontVariant));
            parts.Add(style.GetPropertyCustomText(PropertyNames.FontWeight));
            parts.Add(style.GetPropertyCustomText(PropertyNames.FontStretch));
            parts.Add(style.GetPropertyCustomText(PropertyNames.FontSize));

            if (!String.IsNullOrEmpty(height))
            {
                parts.Add("/");
                parts.Add(height);
            }

            parts.Add(style.GetPropertyCustomText(PropertyNames.FontFamily));
            parts.RemoveAll(m => String.IsNullOrEmpty(m));

            return String.Join(" ", parts);
        }

        #endregion
    }
}
