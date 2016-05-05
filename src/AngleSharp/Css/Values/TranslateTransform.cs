namespace AngleSharp.Css.Values
{
    using AngleSharp.Dom.Css;

    /// <summary>
    /// Represents the translate3d transformation.
    /// </summary>
    sealed class TranslateTransform : ITransform
    {
        #region Fields

        readonly Length _x;
        readonly Length _y;
        readonly Length _z;

        #endregion

        #region ctor

        internal TranslateTransform(Length x, Length y, Length z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the shift in x-direction.
        /// </summary>
        public Length Dx
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets the shift in y-direction.
        /// </summary>
        public Length Dy
        {
            get { return _y; }
        }

        /// <summary>
        /// Gets the shift in z-direction.
        /// </summary>
        public Length Dz
        {
            get { return _z; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            var dx = _x.ToPixel();
            var dy = _y.ToPixel();
            var dz = _z.ToPixel();
            return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, dx, dy, dz, 0f, 0f, 0f);
        }

        #endregion
    }
}
