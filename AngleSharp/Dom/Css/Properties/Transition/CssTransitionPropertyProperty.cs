namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-property
    /// Gets the names of the selected properties.
    /// </summary>
    sealed class CssTransitionPropertyProperty : CssProperty
    {
        #region Fields

        // Default: All
        static readonly IValueConverter PropConverter = Converters.AnimatableConverter.FromList().OrNone();
        
        #endregion

        #region ctor

        internal CssTransitionPropertyProperty()
            : base(PropertyNames.TransitionProperty)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return PropConverter; }
        }

        #endregion
    }
}
