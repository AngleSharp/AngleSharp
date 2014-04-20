namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a CSS shape.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/shape
    /// </summary>
    public sealed class CSSShapeValue : CSSValue
    {
        #region Fields

        Length _top;
        Length _right;
        Length _bottom;
        Length _left;

        #endregion

        #region ctor

        public CSSShapeValue(Length top, Length right, Length bottom, Length left)
        {
            _top = top;
            _right = right;
            _bottom = bottom;
            _left = left;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the top side.
        /// </summary>
        public Length Top
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the right side.
        /// </summary>
        public Length Right
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets the bottom side.
        /// </summary>
        public Length Bottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the left side.
        /// </summary>
        public Length Left
        {
            get { return _left; }
        }

        #endregion

        #region String Representation

        public override String ToCss()
        {
            return FunctionNames.Build(FunctionNames.Rect, _top.ToCss(), _right.ToCss(), _bottom.ToCss(), _left.ToCss());
        }

        #endregion
    }
}
