namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents the matrix3d transformation.
    /// </summary>
    sealed class MatrixTransform : ITransform
    {
        readonly TransformMatrix _matrix;

        internal MatrixTransform(Single m11, Single m12, Single m13, Single m21, Single m22, Single m23, Single m31, Single m32, Single m33, Single tx, Single ty, Single tz)
        {
            _matrix = new TransformMatrix(m11, m12, m13, m21, m22, m23, m31, m32, m33, tx, ty, tz);
        }

        /// <summary>
        /// Returns the stored matrix.
        /// </summary>
        /// <returns>The current transformation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            return _matrix;
        }

        CssValueType ICssValue.Type
        {
            get { return CssValueType.Primitive; }
        }

        String ICssValue.CssText
        {
            get
            {
                return FunctionNames.Build(FunctionNames.Matrix3d,
                    _matrix.M11.ToString(CultureInfo.InvariantCulture), _matrix.M12.ToString(CultureInfo.InvariantCulture), _matrix.M13.ToString(CultureInfo.InvariantCulture),
                    _matrix.M21.ToString(CultureInfo.InvariantCulture), _matrix.M22.ToString(CultureInfo.InvariantCulture), _matrix.M23.ToString(CultureInfo.InvariantCulture),
                    _matrix.M31.ToString(CultureInfo.InvariantCulture), _matrix.M32.ToString(CultureInfo.InvariantCulture), _matrix.M33.ToString(CultureInfo.InvariantCulture),
                    _matrix.Tx.ToString(CultureInfo.InvariantCulture),  _matrix.Ty.ToString(CultureInfo.InvariantCulture),  _matrix.Tz.ToString(CultureInfo.InvariantCulture));
            }
        }
    }
}
