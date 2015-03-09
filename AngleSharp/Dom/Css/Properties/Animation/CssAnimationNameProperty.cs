namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-name
    /// Gets the names of the animations to trigger.
    /// </summary>
    sealed class CssAnimationNameProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<String[]> Converter = 
            Converters.Assign(Keywords.None, new String[0]).Or(Converters.IdentifierConverter.FromList());

        #endregion

        #region ctor

        internal CssAnimationNameProperty(CssStyleDeclaration rule)
            : base(PropertyNames.AnimationName, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return String.Empty;
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
    }
}
