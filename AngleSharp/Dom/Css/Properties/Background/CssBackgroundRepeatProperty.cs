namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

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
        static readonly IValueConverter<Repeat[]> ListConverter = 
            SingleConverter.FromList();

        #endregion

        #region ctor

        internal CssBackgroundRepeatProperty()
            : base(PropertyNames.BackgroundRepeat)
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
            return Default;
        }

        protected override Boolean IsValid(CssValue value)
        {
            return ListConverter.Validate(value);
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
