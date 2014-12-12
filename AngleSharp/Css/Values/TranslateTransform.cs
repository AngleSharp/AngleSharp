namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Represents the translate3d transformation.
    /// </summary>
    sealed class TranslateTransform : ITransform
    {
        #region Fields

        readonly IDistance _x;
        readonly IDistance _y;
        readonly IDistance _z;

        #endregion

        #region ctor

        internal TranslateTransform(IDistance x, IDistance y, IDistance z)
        {
            _x = x;
            _y = y;
            _z = z;
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
