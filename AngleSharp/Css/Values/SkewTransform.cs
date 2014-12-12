namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Represents the skew transformation.
    /// </summary>
    sealed class SkewTransform : ITransform
    {
        #region Fields

        readonly Angle _alpha;
        readonly Angle _beta;

        #endregion

        #region ctor

        internal SkewTransform(Angle alpha, Angle beta)
        {
            _alpha = alpha;
            _beta = beta;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            var a = _alpha.Tan();
            var b = _beta.Tan();
            return new TransformMatrix(1f, a, 0f, b, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 0f, 0f);
        }

        #endregion
    }
}
