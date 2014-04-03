namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font
    /// </summary>
    sealed class CSSFontProperty : CSSProperty
    {
        #region Fields

        static readonly ValueConverter<FontSettingMode> _parts = new ValueConverter<FontSettingMode>();
        static readonly CustomFontSettingMode _default = new CustomFontSettingMode(null, null, null, null, new CSSFontSizeProperty(), null, new CSSFontFamilyProperty());
        FontSettingMode _mode;

        #endregion

        #region ctor

        static CSSFontProperty()
        {
            _parts.AddStatic("caption", new SystemFontSettingMode(SystemFontSettingMode.Setting.Caption));
            _parts.AddStatic("icon", new SystemFontSettingMode(SystemFontSettingMode.Setting.Icon));
            _parts.AddStatic("menu", new SystemFontSettingMode(SystemFontSettingMode.Setting.Menu));
            _parts.AddStatic("message-box", new SystemFontSettingMode(SystemFontSettingMode.Setting.MessageBox));
            _parts.AddStatic("small-caption", new SystemFontSettingMode(SystemFontSettingMode.Setting.SmallCaption));
            _parts.AddStatic("status-bar", new SystemFontSettingMode(SystemFontSettingMode.Setting.StatusBar));
            _parts.AddCompound<CustomFontSettingMode>();
        }

        public CSSFontProperty()
            : base(PropertyNames.FONT)
        {
            _mode = _default;
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

        #region Modes

        abstract class FontSettingMode
        {
            //TODO Add members that make sense
        }

        /// <summary>
        /// Instead of specifying individual longhand properties, a
        /// keyword can be used to represent a specific system font.
        /// </summary>
        sealed class SystemFontSettingMode : FontSettingMode
        {
            Setting _setting;

            public SystemFontSettingMode(Setting setting)
            {
                _setting = setting;
            }

            public enum Setting
            {
                /// <summary>
                /// The font used for captioned controls (e.g., buttons, drop-downs, etc.).
                /// </summary>
                Caption,
                /// <summary>
                /// The font used to label icons.
                /// </summary>
                Icon,
                /// <summary>
                /// The font used in menus (e.g., dropdown menus and menu lists).
                /// </summary>
                Menu,
                /// <summary>
                /// The font used in dialog boxes.
                /// </summary>
                MessageBox,
                /// <summary>
                /// The font used for labeling small controls.
                /// </summary>
                SmallCaption,
                /// <summary>
                /// The font used in window status bars.
                /// </summary>
                StatusBar
            }
        }

        /// <summary>
        /// The font CSS property is a shorthand property for setting font-style,
        /// font-variant, font-weight, font-size, line-height and font-family.
        /// </summary>
        sealed class CustomFontSettingMode : FontSettingMode
        {
            CSSFontStyleProperty _style;
            CSSFontVariantProperty _variant;
            CSSFontWeightProperty _weight;
            CSSFontStretchProperty _stretch;
            CSSFontSizeProperty _size;
            CSSFontFamilyProperty _family;
            CSSLineHeightProperty _height;


            //[ [ <‘font-style’> || <font-variant> || <‘font-weight’> || <‘font-stretch’> ]? <‘font-size’> [ / <‘line-height’> ]? <‘font-family’> ]
            public CustomFontSettingMode(CSSFontStyleProperty style, CSSFontVariantProperty variant, CSSFontWeightProperty weight, CSSFontStretchProperty stretch, CSSFontSizeProperty size, CSSLineHeightProperty height, CSSFontFamilyProperty family)
            {
                _style = style ?? CSSFontStyleProperty.Default;
                _variant = variant ?? CSSFontVariantProperty.Default;
                _weight = weight ?? CSSFontWeightProperty.Default;
                _stretch = stretch ?? CSSFontStretchProperty.Default;
                _size = size;
                _family = family;
                _height = height ?? CSSLineHeightProperty.Default;
            }
        }

        #endregion
    }
}
