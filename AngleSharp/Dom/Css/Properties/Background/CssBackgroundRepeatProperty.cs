namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-repeat
    /// Gets an enumeration with the horizontal repeat modes.
    /// Gets an enumeration with the vertical repeat modes.
    /// </summary>
    sealed class CssBackgroundRepeatProperty : CssProperty
    {
        #region Fields

        static readonly Repeat Default = 
            new Repeat { Horizontal = BackgroundRepeat.Repeat, Vertical = BackgroundRepeat.Repeat };
        internal static readonly IValueConverter<Repeat> SingleConverter = 
            Map.BackgroundRepeats.ToConverter().To(m => new Repeat { Horizontal = m, Vertical = m }).Or(
                Keywords.RepeatX, new Repeat { Horizontal = BackgroundRepeat.Repeat, Vertical = BackgroundRepeat.NoRepeat }).Or(
                Keywords.RepeatY, new Repeat { Horizontal = BackgroundRepeat.NoRepeat, Vertical = BackgroundRepeat.Repeat }).Or(
                Converters.WithOrder(Map.BackgroundRepeats.ToConverter().Required(), Map.BackgroundRepeats.ToConverter().Required()).To(
                    m => new Repeat { Horizontal = m.Item1, Vertical = m.Item2 }));
        static readonly IValueConverter<Repeat[]> Converter = 
            SingleConverter.FromList();

        #endregion

        #region ctor

        internal CssBackgroundRepeatProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BackgroundRepeat, rule)
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

        internal struct Repeat
        {
            public BackgroundRepeat Horizontal;
            public BackgroundRepeat Vertical;
        }

        #endregion
    }
}
