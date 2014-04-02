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
        static readonly CustomFontSettingMode _default = new CustomFontSettingMode();
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
        }

        public CSSFontProperty()
            : base(PropertyNames.FONT)
        {
            _mode = _default;
            _inherited = true;

            //[ [ <‘font-style’> || <font-variant> || <‘font-weight’> || <‘font-stretch’> ]? <‘font-size’> [ / <‘line-height’> ]? <‘font-family’> ]
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

        sealed class SystemFontSettingMode : FontSettingMode
        {
            Setting _setting;

            public SystemFontSettingMode(Setting setting)
            {
                _setting = setting;
            }

            public enum Setting
            {
                Caption,
                Icon,
                Menu,
                MessageBox,
                SmallCaption,
                StatusBar
            }
        }

        sealed class CustomFontSettingMode : FontSettingMode
        {
            CSSFontStyleProperty _style;
            CSSFontVariantProperty _variant;
            CSSFontWeightProperty _weight;
            CSSFontStretchProperty _stretch;
            CSSFontSizeProperty _size;
            CSSFontFamilyProperty _family;
            CSSLineHeightProperty _height;

            public CustomFontSettingMode()
            {
                //TODO: These will be only created IF they have been set.
                //_style = new Style();
                //_variant = new Variant();
                //_weight = new Weight();
                //_size = new Size();
                //_family = new Family();
                //_height = new LineHeight();
            }
        }

        #endregion
    }
}
