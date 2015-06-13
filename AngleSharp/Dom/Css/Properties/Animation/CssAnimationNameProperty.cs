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

        internal static readonly IValueConverter ListConverter = Converters.IdentifierConverter.FromList().OrNone();

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
            // Default: Nothing
            get { return ListConverter; }
        }

        #endregion
    }
}
