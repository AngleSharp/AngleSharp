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

        internal static readonly IValueConverter<String[]> ListConverter = 
            Converters.Assign(Keywords.None, new String[0]).Or(Converters.IdentifierConverter.FromList());

        #endregion

        #region ctor

        internal CssAnimationNameProperty()
            : base(PropertyNames.AnimationName)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return ListConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return String.Empty;
        }

        protected override Boolean IsValid(CssValue value)
        {
            return ListConverter.Validate(value);
        }

        #endregion
    }
}
