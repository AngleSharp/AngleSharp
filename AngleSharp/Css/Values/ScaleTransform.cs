namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// Represents the scale3d transformation.
    /// </summary>
    sealed class ScaleTransform : ITransform
    {
        #region Fields

        readonly Single _sx;
        readonly Single _sy;
        readonly Single _sz;

        #endregion

        #region ctor

        internal ScaleTransform(Single sx, Single sy, Single sz)
        {
            _sx = sx;
            _sy = sy;
            _sz = sz;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            return new TransformMatrix(_sx, 0f, 0f, 0f, _sy, 0f, 0f, 0f, _sz, 0f, 0f, 0f, 0f, 0f, 0f);
        }

        #endregion
    }
}
