namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule
    /// </summary>
    sealed class CssColumnRuleProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter<Tuple<ICssValue, ICssValue, ICssValue>> Converter = 
            Converters.WithAny(
                Converters.ColorConverter.Val().Option(),
                Converters.LineWidthConverter.Val().Option(),
                Converters.LineStyleConverter.Val().Option());

        #endregion

        #region ctor

        internal CssColumnRuleProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ColumnRule, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m =>
            {
                Get<CssColumnRuleColorProperty>().TrySetValue(m.Item1);
                Get<CssColumnRuleWidthProperty>().TrySetValue(m.Item2);
                Get<CssColumnRuleStyleProperty>().TrySetValue(m.Item3);
            });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var width = properties.OfType<CssColumnRuleWidthProperty>().FirstOrDefault();
            var style = properties.OfType<CssColumnRuleStyleProperty>().FirstOrDefault();
            var color = properties.OfType<CssColumnRuleColorProperty>().FirstOrDefault();

            if (width == null || style == null || color == null)
                return String.Empty;

            return String.Format("{0} {1} {2}", width.SerializeValue(), style.SerializeValue(), color.SerializeValue());
        }

        #endregion
    }
}
