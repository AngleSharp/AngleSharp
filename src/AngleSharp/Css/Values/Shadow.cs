namespace AngleSharp.Css.Values
{
    using AngleSharp.Dom.Css;
    using System;

    /// <summary>
    /// The shadow class for holding information about
    /// a box or text-shadow.
    /// </summary>
    public sealed class Shadow
    {
        #region Fields

        readonly Boolean _inset;
        readonly Length _offsetX;
        readonly Length _offsetY;
        readonly Length _blurRadius;
        readonly Length _spreadRadius;
        readonly Color _color;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS shadow.
        /// </summary>
        /// <param name="inset">If the shadow is an inset.</param>
        /// <param name="offsetX">The x-coordinate offset.</param>
        /// <param name="offsetY">The y-coordinate offset.</param>
        /// <param name="blurRadius">The blur radius of the shadow.</param>
        /// <param name="spreadRadius">The spread radius of the shadow.</param>
        /// <param name="color">The color of the shadow.</param>
        public Shadow(Boolean inset, Length offsetX, Length offsetY, Length blurRadius, Length spreadRadius, Color color)
        {
            _inset = inset;
            _offsetX = offsetX;
            _offsetY = offsetY;
            _blurRadius = blurRadius;
            _spreadRadius = spreadRadius;
            _color = color;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the color of the shadow.
        /// </summary>
        public Color Color
        {
            get { return _color; }
        }

        /// <summary>
        /// Gets the horizontal offset.
        /// </summary>
        public Length OffsetX
        {
            get { return _offsetX; }
        }

        /// <summary>
        /// Gets the vertical offset.
        /// </summary>
        public Length OffsetY
        {
            get { return _offsetY; }
        }

        /// <summary>
        /// Gets the blur radius.
        /// </summary>
        public Length BlurRadius
        {
            get { return _blurRadius; }
        }

        /// <summary>
        /// Gets the spread radius.
        /// </summary>
        public Length SpreadRadius
        {
            get { return _spreadRadius; }
        }

        /// <summary>
        /// Gets if the shadow is inset.
        /// </summary>
        public Boolean IsInset
        {
            get { return _inset; }
        }

        #endregion
    }
}
