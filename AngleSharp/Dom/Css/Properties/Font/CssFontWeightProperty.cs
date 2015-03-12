namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight
    /// </summary>
    sealed class CssFontWeightProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<FontWeight> Converter = (new Dictionary<String, FontWeight>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Normal, new FontWeight { IsRelative = false, Value = 400 } },
            { Keywords.Bold, new FontWeight { IsRelative = false, Value = 700 } },
            { Keywords.Bolder, new FontWeight { IsRelative = true, Value = 100 } },
            { Keywords.Lighter, new FontWeight { IsRelative = true, Value = -100 } }
        }).ToConverter().Or(
            Converters.IntegerConverter.Constraint(m => m >= 100 && m <= 900).To(
            m => new FontWeight { IsRelative = false, Value = m }));

        #endregion

        #region ctor
        
        internal CssFontWeightProperty(CssStyleDeclaration rule)
            : base(PropertyNames.FontWeight, rule, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return new FontWeight { IsRelative = false, Value = 400 };
        }

        protected override Object Compute(IElement element)
        {
            return Converter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion

        #region Structure

        internal struct FontWeight
        {
            public Boolean IsRelative;
            public Int32 Value;
        }

        #endregion
    }
}
