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
    sealed class CssFontProperty : CssShorthandProperty
    {
        #region Fields

        internal static readonly IValueConverter<SystemFont> SystemFontConverter = (new Dictionary<String, SystemFont>()
        {
            { Keywords.Caption, SystemFont.Caption },
            { Keywords.Icon, SystemFont.Icon },
            { Keywords.Menu, SystemFont.Menu },
            { Keywords.MessageBox, SystemFont.MessageBox },
            { Keywords.SmallCaption, SystemFont.SmallCaption },
            { Keywords.StatusBar, SystemFont.StatusBar }
        }).ToConverter();

        internal static readonly IValueConverter<Tuple<Tuple<ICssValue, ICssValue, ICssValue, ICssValue>, Tuple<ICssValue, ICssValue>, ICssValue>> Converter = 
            Converters.WithOrder(
                Converters.WithAny(
                    Converters.FontStyleConverter.Val().Option(),
                    Converters.FontVariantConverter.Val().Option(),
                    CssFontWeightProperty.Converter.Val().Option(),
                    Converters.FontStretchConverter.Val().Option()),
                Converters.WithOrder(
                    Converters.FontSizeConverter.Val().Required(),
                    Converters.LineHeightConverter.Val().StartsWithDelimiter().Option()),
                CssFontFamilyProperty.Converter.Val().Required());

        #endregion

        #region ctor

        internal CssFontProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Font, rule, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(ICssValue value)
        {
            //[ [ <‘font-style’> || <font-variant-css21> || <‘font-weight’> || <‘font-stretch’> ]? <‘font-size’> [ / <‘line-height’> ]? <‘font-family’> ] | caption | icon | menu | message-box | small-caption | status-bar

            return Converter.TryConvert(value, m =>
            {
                Get<CssFontStyleProperty>().TrySetValue(m.Item1.Item1);
                Get<CssFontVariantProperty>().TrySetValue(m.Item1.Item2);
                Get<CssFontWeightProperty>().TrySetValue(m.Item1.Item3);
                Get<CssFontStretchProperty>().TrySetValue(m.Item1.Item4);
                Get<CssFontSizeProperty>().TrySetValue(m.Item2.Item1);
                Get<CssLineHeightProperty>().TrySetValue(m.Item2.Item2);
                Get<CssFontFamilyProperty>().TrySetValue(m.Item3);
            }) || SystemFontConverter.TryConvert(value, SetSystemFont);
        }

        /// <summary>
        /// Instead of specifying individual longhand properties, a keyword
        /// can be used to represent a specific system font.
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
            var families = properties.OfType<CssFontFamilyProperty>().FirstOrDefault();
            var size = properties.OfType<CssFontSizeProperty>().FirstOrDefault();
            var style = properties.OfType<CssFontStyleProperty>().FirstOrDefault();
            var variant = properties.OfType<CssFontVariantProperty>().FirstOrDefault();
            var weight = properties.OfType<CssFontWeightProperty>().FirstOrDefault();
            var stretch = properties.OfType<CssFontStretchProperty>().FirstOrDefault();
            var height = properties.OfType<CssLineHeightProperty>().FirstOrDefault();

            if (families == null || size == null)
                return String.Empty;

            var values = new List<String>();

            if (style != null && style.HasValue)
                values.Add(style.SerializeValue());

            if (variant != null && variant.HasValue)
                values.Add(variant.SerializeValue());

            if (weight != null && weight.HasValue)
                values.Add(weight.SerializeValue());

            if (stretch != null && stretch.HasValue)
                values.Add(stretch.SerializeValue());

            values.Add(size.SerializeValue());

            if (height != null && height.HasValue)
            {
                values.Add("/");
                values.Add(height.SerializeValue());
            }

            values.Add(families.SerializeValue());
            values.RemoveAll(m => String.IsNullOrEmpty(m));

            return String.Join(" ", values);
        }

        #endregion
    }
}
