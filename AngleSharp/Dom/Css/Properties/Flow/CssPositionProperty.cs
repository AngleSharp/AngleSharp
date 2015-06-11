namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/position
    /// Gets the currently selected position mode.
    /// </summary>
    sealed class CssPositionProperty : CssProperty
    {
        #region ctor

        internal CssPositionProperty()
            : base(PropertyNames.Position)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.PositionModeConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return PositionMode.Static;
        }

        #endregion
    }
}
