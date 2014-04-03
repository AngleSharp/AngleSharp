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

        static readonly Dictionary<String, SystemSetting> _parts = new Dictionary<String, SystemSetting>();
        CSSFontStyleProperty _style;
        CSSFontVariantProperty _variant;
        CSSFontWeightProperty _weight;
        CSSFontStretchProperty _stretch;
        CSSFontSizeProperty _size;
        CSSLineHeightProperty _height;
        CSSFontFamilyProperty _family;

        #endregion

        #region ctor

        static CSSFontProperty()
        {
            _parts.Add("caption", SystemSetting.Caption);
            _parts.Add("icon", SystemSetting.Icon);
            _parts.Add("menu", SystemSetting.Menu);
            _parts.Add("message-box", SystemSetting.MessageBox);
            _parts.Add("small-caption", SystemSetting.SmallCaption);
            _parts.Add("status-bar", SystemSetting.StatusBar);
        }

        public CSSFontProperty()
            : base(PropertyNames.FONT)
        {
            _style = new CSSFontStyleProperty();
            _variant = new CSSFontVariantProperty();
            _weight = new CSSFontWeightProperty();
            _stretch = new CSSFontStretchProperty();
            _size = new CSSFontSizeProperty();
            _family = new CSSFontFamilyProperty();
            _height = new CSSLineHeightProperty();
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            SystemSetting setting;

            if (value is CSSIdentifierValue && _parts.TryGetValue(((CSSIdentifierValue)value).Value, out setting))
                SetTo(setting);
            else if (value is CSSValueList)
            {
                var list = (CSSValueList)value;
                var index = 0;
                var startGroup = new List<CSSProperty>(4);
                var style = _style.Clone();
                var variant = _variant.Clone();
                var weight = _weight.Clone();
                var stretch = _stretch.Clone();
                var size = _size.Clone();
                var height = _height.Clone();
                var family = _family.Clone();
                startGroup.Add(style);
                startGroup.Add(variant);
                startGroup.Add(weight);
                startGroup.Add(stretch);

                while (true)
                {
                    var length = startGroup.Count;

                    for (int i = 0; i < length; i++)
                    {
                        if (CheckSingleProperty(startGroup[i], index, list))
                        {
                            startGroup.RemoveAt(i);
                            index++;
                            break;
                        }
                    }

                    if (length == startGroup.Count)
                        break;
                }

                if (!CheckSingleProperty(size, index, list) || ++index == list.Length)
                    return false;

                if (list[index] == CSSValue.Delimiter && (!CheckSingleProperty(height, ++index, list) || ++index == list.Length))
                    return false;

                if (!CheckLastProperty(family, index, list))
                    return false;

                _style = (CSSFontStyleProperty)style;
                _variant = (CSSFontVariantProperty)variant;
                _weight = (CSSFontWeightProperty)weight;
                _stretch = (CSSFontStretchProperty)stretch;
                _size = (CSSFontSizeProperty)size;
                _height = (CSSLineHeightProperty)height;
                _family = (CSSFontFamilyProperty)family;
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        /// <summary>
        /// Instead of specifying individual longhand properties, a
        /// keyword can be used to represent a specific system font.
        /// </summary>
        /// <param name="setting">The setting to apply.</param>
        void SetTo(SystemSetting setting)
        {
            //TODO set properties to the setting given by the enumeration value
        }

        #endregion

        #region Predefined settings

        enum SystemSetting
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

        #endregion
    }
}
