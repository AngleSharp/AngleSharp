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

        static readonly Dictionary<String, SystemFont> _parts = new Dictionary<String, SystemFont>();
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
            _parts.Add("caption", SystemFont.Caption);
            _parts.Add("icon", SystemFont.Icon);
            _parts.Add("menu", SystemFont.Menu);
            _parts.Add("message-box", SystemFont.MessageBox);
            _parts.Add("small-caption", SystemFont.SmallCaption);
            _parts.Add("status-bar", SystemFont.StatusBar);
        }

        internal CSSFontProperty()
            : base(PropertyNames.Font, PropertyFlags.Inherited)
        {
            _style = new CSSFontStyleProperty();
            _variant = new CSSFontVariantProperty();
            _weight = new CSSFontWeightProperty();
            _stretch = new CSSFontStretchProperty();
            _size = new CSSFontSizeProperty();
            _family = new CSSFontFamilyProperty();
            _height = new CSSLineHeightProperty();
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

        /// <summary>
        /// Gets the mode of the font-size property.
        /// </summary>
        public FontSize SizeMode
        {
            get { return _size.Mode; }
        }

        /// <summary>
        /// Gets the custom set size of the font, if any.
        /// </summary>
        internal CSSCalcValue Size
        {
            get { return _size.Size; }
        }

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

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            SystemFont setting;

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
        void SetTo(SystemFont setting)
        {
            //TODO set properties to the setting given by the enumeration value
        }

        #endregion
    }
}
