namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// Represents the matrix3d transformation.
    /// </summary>
    sealed class MatrixTransform : ITransform
    {
        #region Fields

        readonly Single[] _values;

        #endregion

        #region ctor

        internal MatrixTransform(Single[] values)
        {
            _values = values;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the stored matrix.
        /// </summary>
        /// <returns>The current transformation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            return new TransformMatrix(_values);
        }

        #endregion
    }
}
