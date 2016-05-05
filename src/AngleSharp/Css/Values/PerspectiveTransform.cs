namespace AngleSharp.Css.Values
{
    using AngleSharp.Dom.Css;

    /// <summary>
    /// Represents the distance transformation.
    /// </summary>
    sealed class PerspectiveTransform : ITransform
    {
        #region Fields

        readonly Length _distance;

        #endregion

        #region ctor

        internal PerspectiveTransform(Length distance)
        {
            _distance = distance;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 0f, -1f / _distance.ToPixel());
        }

        #endregion
    }
}
