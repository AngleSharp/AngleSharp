namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents the rotate3d transformation.
    /// </summary>
    sealed class RotateTransform : ITransform
    {
        #region Fields

        readonly Single _x;
        readonly Single _y;
        readonly Single _z;
        readonly Angle _angle;

        #endregion

        #region ctor

        internal RotateTransform(Single x, Single y, Single z, Angle angle)
        {
            _x = x;
            _y = y;
            _z = z;
            _angle = angle;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Constructs a rotate 3D transformation around the x-axis.
        /// </summary>
        /// <param name="angle">The angle to rotate.</param>
        /// <returns>The rotate 3D transformation.</returns>
        public static RotateTransform RotateX(Angle angle)
        {
            return new RotateTransform(1f, 0f, 0f, angle);
        }

        /// <summary>
        /// Constructs a rotate 3D transformation around the y-axis.
        /// </summary>
        /// <param name="angle">The angle to rotate.</param>
        /// <returns>The rotate 3D transformation.</returns>
        public static RotateTransform RotateY(Angle angle)
        {
            return new RotateTransform(0f, 1f, 0f, angle);
        }

        /// <summary>
        /// Constructs a rotate 3D transformation around the z-axis.
        /// </summary>
        /// <param name="angle">The angle to rotate.</param>
        /// <returns>The rotate 3D transformation.</returns>
        public static RotateTransform RotateZ(Angle angle)
        {
            return new RotateTransform(0f, 0f, 1f, angle);
        }

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            var norm = 1f / (Single)Math.Sqrt(_x * _x + _y * _y + _z * _z);
            var sina = _angle.Sin();
            var cosa = _angle.Cos();
            var l = _x * norm;
            var m = _y * norm;
            var n = _z * norm;
            var omc = (1f - cosa);
            return new TransformMatrix(
                l * l * omc + cosa, m * l * omc - n * sina, n * l * omc + m * sina,
                l * m * omc + n * sina, m * m * omc + cosa, n * m * omc - l * sina,
                l * n * omc - m * sina, m * n * omc + l * sina, n * n * omc + cosa,
                0f, 0f, 0f, 0f, 0f, 0f);
        }

        #endregion

        #region CSS Value

        CssValueType ICssValue.Type
        {
            get { return CssValueType.Primitive; }
        }

        String ICssValue.CssText
        {
            get
            {
                return FunctionNames.Build(FunctionNames.Rotate3d, 
                    _x.ToString(CultureInfo.InvariantCulture), 
                    _y.ToString(CultureInfo.InvariantCulture), 
                    _z.ToString(CultureInfo.InvariantCulture), 
                    ((ICssValue)_angle).CssText);
            }
        }

        #endregion
    }
}
