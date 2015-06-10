namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule
    /// </summary>
    sealed class CssColumnRuleProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter<Tuple<CssValue, CssValue, CssValue>> StyleConverter = 
            Converters.WithAny(
                Converters.ColorConverter.Val().Option(),
                Converters.LineWidthConverter.Val().Option(),
                Converters.LineStyleConverter.Val().Option());

        #endregion

        #region ctor

        internal CssColumnRuleProperty()
            : base(PropertyNames.ColumnRule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CssValue value)
        {
            return StyleConverter.Validate(value);
            //TODO Convert instead of validate
            /*, m =>
            {
                Get<CssColumnRuleColorProperty>().TrySetValue(m.Item1);
                Get<CssColumnRuleWidthProperty>().TrySetValue(m.Item2);
                Get<CssColumnRuleStyleProperty>().TrySetValue(m.Item3);
            });*/
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
