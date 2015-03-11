namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-size
    /// </summary>
    sealed class CssBackgroundSizeProperty : CssProperty
    {
        #region Fields

        internal static readonly BackgroundSize Default = 
            new BackgroundSize();
        internal static readonly IValueConverter<BackgroundSize> SingleConverter = 
            Converters.AutoLengthOrPercentConverter.To(m => new BackgroundSize { Width = m ?? Length.Full, Height = Length.Full }).Or(
                Keywords.Cover, new BackgroundSize { IsCovered = true }).Or(
                Keywords.Contain, new BackgroundSize { IsContained = true }).Or(
                Converters.WithOrder(Converters.AutoLengthOrPercentConverter.Required(), Converters.AutoLengthOrPercentConverter.Required()).To(
                    pt => new BackgroundSize { Width = pt.Item1 ?? Length.Full, Height = pt.Item2 ?? Length.Full }));
        internal static readonly IValueConverter<BackgroundSize[]> Converter = 
            SingleConverter.FromList();

        #endregion

        #region ctor

        internal CssBackgroundSizeProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BackgroundSize, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Default;
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
