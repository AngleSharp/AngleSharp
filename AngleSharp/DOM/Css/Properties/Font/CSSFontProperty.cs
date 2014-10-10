namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font
    /// </summary>
    sealed class CSSFontProperty : CSSProperty, ICssFontProperty
    {
        #region Fields

        static readonly Dictionary<String, SystemFont> _parts = new Dictionary<String, SystemFont>();
        FontStyle _style;
        FontVariant _variant;
        CSSFontWeightProperty _weight;
        FontStretch _stretch;
        FontSize _sizeMode;
        IDistance _size;
        IDistance _height;
        List<String> _families;

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

        internal CSSFontProperty()
            : base(PropertyNames.Font, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
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
            get { return _style; }
        }

        /// <summary>
        /// Gets the value of the font variant property.
        /// </summary>
        public FontVariant Variant
        {
            get { return _variant; }
        }

        /// <summary>
        /// Gets the value of the font stretch property.
        /// </summary>
        public FontStretch Stretch
        {
            get { return _stretch; }
        }

        /// <summary>
        /// Gets the mode of the font-size property.
        /// </summary>
        public FontSize SizingMode
        {
            get { return _sizeMode; }
        }

        /// <summary>
        /// Gets the custom set size of the font, if any.
        /// </summary>
        public IDistance Size
        {
            get { return _size; }
        }

        /// <summary>
        /// Gets the value of the line height property.
        /// </summary>
        public IDistance Height
        {
            get { return _height; }
        }

        /// <summary>
        /// Gets the value of the font family property.
        /// </summary>
        public IEnumerable<String> Families
        {
            get { return _families; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _style = FontStyle.Normal;
            _variant = FontVariant.Normal;
            _weight = new CSSFontWeightProperty();
            _stretch = FontStretch.Normal;
            _sizeMode = FontSize.Medium;
            _size = _sizeMode.ToDistance();

            if (_families == null)
                _families = new List<String>();
            else
                _families.Clear();

            _families.Add("Times New Roman");
            _height = new Percent(120f);
        }

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
                var weight = new CSSFontWeightProperty();
                var families = new List<String>();
                var allowDelim = false;
                FontStyle? style = null;
                FontVariant? variant = null;
                FontStretch? stretch = null;
                FontSize? sizeMode = null;
                IDistance size = null;
                IDistance height = null;

                for (var i = 0; i < entries.Length; i++)
                {
                    var entry = entries[i];

                    if (allowDelim)
                    {
                        if (entry == CSSValue.Delimiter)
                        {
                            if (++i == entries.Length)
                                return false;

                            height = entries[i].ToLineHeight();

                            if (height == null)
                                return false;
                            else if (++i == entries.Length)
                                return false;
                        }

                        var rest = entries.ToList(i);

                        foreach (var item in rest)
                        {
                            var family = (item.Length == 1 ? item[0] : item).ToFontFamily();

                            if (family == null)
                                return false;

                            families.Add(family);
                        }

                        if (families.Count == 0)
                            return false;

                        break;
                    }

                    if (style == null && (style = entry.ToFontStyle()).HasValue)
                        continue;

                    if (variant == null && (variant = entry.ToFontVariant()).HasValue)
                        continue;

                    if (weight.IsInitial && weight.TrySetValue(entry))
                        continue;

                    if (stretch == null && (stretch = entry.ToFontStretch()).HasValue)
                        continue;

                    if (sizeMode == null && (sizeMode = entry.ToFontSize()).HasValue)
                    {
                        size = sizeMode.Value.ToDistance();
                        allowDelim = true;
                        continue;
                    }

                    if (size == null && (size = entry.ToDistance()) != null)
                    {
                        sizeMode = FontSize.Custom;
                        allowDelim = true;
                        continue;
                    }
                }

                _stretch = stretch ?? FontStretch.Normal;
                _variant = variant ?? FontVariant.Normal;
                _sizeMode = sizeMode ?? FontSize.Medium;
                _size = size ?? _sizeMode.ToDistance();
                _height = height ?? new Percent(120f);
                _style = style ?? FontStyle.Normal;
                _weight = weight;
                _families = families;
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

        #endregion
    }
}
