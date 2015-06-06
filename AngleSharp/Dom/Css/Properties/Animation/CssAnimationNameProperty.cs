namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

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

        internal CssAnimationNameProperty()
            : base(PropertyNames.AnimationName)
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

        protected override Boolean IsValid(CssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}
