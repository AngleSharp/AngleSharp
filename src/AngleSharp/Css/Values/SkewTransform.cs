namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// Represents the skew transformation.
    /// </summary>
    sealed class SkewTransform : ITransform
    {
        #region Fields

        readonly Single _alpha;
        readonly Single _beta;

        #endregion

        #region ctor

        internal SkewTransform(Single alpha, Single beta)
        {
            _alpha = alpha;
            _beta = beta;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the first angle in radiants [0, 2pi].
        /// </summary>
        public Single Alpha
        {
            get { return _alpha; }
        }

        /// <summary>
        /// Gets the value of the second angle in radiants [0, 2pi].
        /// </summary>
        public Single Beta
        {
            get { return _beta; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            var a = (Single)Math.Tan(_alpha);
            var b = (Single)Math.Tan(_beta);
            return new TransformMatrix(1f, a, 0f, b, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 0f, 0f);
        }

        #endregion
    }
}
