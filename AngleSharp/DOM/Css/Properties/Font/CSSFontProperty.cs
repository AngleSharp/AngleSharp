namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font
    /// </summary>
    public sealed class CSSFontProperty : CSSProperty
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

        internal CSSFontProperty()
            : base(PropertyNames.Font)
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

        #region Properties

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

        ///// <summary>
        ///// Gets the value of the font weight property.
        ///// </summary>
        //public CSSFontWeightProperty Weight
        //{
        //    get { return _weight; }
        //}

        /// <summary>
        /// Gets the value of the font stretch property.
        /// </summary>
        public FontStretch Stretch
        {
            get { return _stretch.Stretch; }
        }

        ///// <summary>
        ///// Gets the value of the font size property.
        ///// </summary>
        //public CSSFontSizeProperty Size
        //{
        //    get { return _size; }
        //}

        ///// <summary>
        ///// Gets the value of the line height property.
        ///// </summary>
        //public CSSLineHeightProperty Height
        //{
        //    get { return _height; }
        //}

        /// <summary>
        /// Gets the value of the font family property.
        /// </summary>
        public IEnumerable<String> Family
        {
            get { return _family.Families; }
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
                var style = new CSSFontStyleProperty();
                var variant = new CSSFontVariantProperty();
                var weight = new CSSFontWeightProperty();
                var stretch = new CSSFontStretchProperty();
                var size = new CSSFontSizeProperty();
                var height = new CSSLineHeightProperty();
                var family = new CSSFontFamilyProperty();
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

                startGroup.Clear();

                if (!CheckSingleProperty(size, index, list) || ++index == list.Length)
                    return false;

                if (list[index] == CSSValue.Delimiter && (!CheckSingleProperty(height, ++index, list) || ++index == list.Length))
                    return false;

                if (!CheckLastProperty(family, index, list))
                    return false;

                _style = style;
                _variant = variant;
                _weight = weight;
                _stretch = stretch;
                _size = size;
                _height = height;
                _family = family;
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
