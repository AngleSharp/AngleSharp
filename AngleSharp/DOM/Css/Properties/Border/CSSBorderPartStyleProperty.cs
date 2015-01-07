namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// The abstract base class for all border-style sub-properties.
    /// </summary>
    abstract class CSSBorderPartStyleProperty : CssProperty
    {
        #region Fields

        internal static readonly LineStyle Default = LineStyle.None;
        internal static readonly IValueConverter<LineStyle> Converter = Map.LineStyles.ToConverter();
        LineStyle _style;

        #endregion

        #region ctor

        internal CSSBorderPartStyleProperty(String name, CssStyleDeclaration rule)
            : base(name, rule)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected style for the border.
        /// </summary>
        public LineStyle Style
        {
            get { return _style; }
        }

        #endregion

        #region Methods

        void SetStyle(LineStyle style)
        {
            _style = style;
        }

        internal override void Reset()
        {
            _style = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetStyle);
        }

        #endregion
    }
}
