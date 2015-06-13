namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-size
    /// </summary>
    sealed class CssBackgroundSizeProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter SingleConverter = 
            Converters.AutoLengthOrPercentConverter.Or(
                Keywords.Cover, new BackgroundSize { IsCovered = true }).Or(
                Keywords.Contain, new BackgroundSize { IsContained = true }).Or(
                Converters.WithOrder(Converters.AutoLengthOrPercentConverter.Required(), Converters.AutoLengthOrPercentConverter.Required()));
        internal static readonly IValueConverter ListConverter = SingleConverter.FromList();

        #endregion

        #region ctor

        internal CssBackgroundSizeProperty()
            : base(PropertyNames.BackgroundSize, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Nothing (just use original size)
            get { return ListConverter; }
        }

        #endregion

        #region Structure

        internal struct BackgroundSize
        {
            public Boolean IsCovered;
            public Boolean IsContained;
            public Length Width;
            public Length Height;
        }

        #endregion
    }
}
