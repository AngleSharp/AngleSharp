namespace AngleSharp.Dom.Css
{
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

        static readonly IValueConverter ListConverter = Converters.AnimatableConverter.FromList().OrNone().OrDefault(Keywords.All);
        
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
            get { return ListConverter; }
        }

        #endregion
    }
}
