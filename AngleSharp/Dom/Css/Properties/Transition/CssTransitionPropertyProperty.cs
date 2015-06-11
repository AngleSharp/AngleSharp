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

        static readonly IValueConverter<String[]> PropConverter =
            Converters.AnimatableConverter.FromList().Or(Keywords.None, new String[0]);
        
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
            // Default: All
            get { return PropConverter; }
        }

        #endregion
    }
}
