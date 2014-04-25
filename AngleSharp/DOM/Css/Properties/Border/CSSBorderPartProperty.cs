namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// The abstract base class for border properties.
    /// </summary>
    public abstract class CSSBorderPartProperty : CSSProperty
    {
        #region Fields

        Length _width;
        Color _color;
        LineStyle _style;

        #endregion

        #region ctor

        protected CSSBorderPartProperty(String name)
            : base(name)
        {
            _inherited = false;
            _width = Length.Medium;
            _color = Color.Transparent;
            _style = LineStyle.None;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width of the given border property.
        /// </summary>
        public Length Width
        {
            get { return _width; }
        }

        /// <summary>
        /// Gets the color of the given border property.
        /// </summary>
        public Color Color
        {
            get { return _color; }
        }

        /// <summary>
        /// Gets the style of the given border property.
        /// </summary>
        public LineStyle Style
        {
            get { return _style; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            return base.IsValid(value);
        }

        #endregion
    }
}
