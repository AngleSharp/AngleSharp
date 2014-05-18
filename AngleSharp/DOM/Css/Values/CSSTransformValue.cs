namespace AngleSharp.DOM.Css
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a transformation in CSS.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform#CSS_transform_functions
    /// </summary>
    public abstract class CSSTransformValue : CSSValue
    {
        #region Properties

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public abstract TransformMatrix ComputeMatrix();

        #endregion

        #region Classes

        /// <summary>
        /// Represents the matrix transformation.
        /// </summary>
        public sealed class Matrix : CSSTransformValue
        {
            TransformMatrix _matrix;

            internal Matrix(Single m11, Single m12, Single m21, Single m22, Single tx, Single ty)
            {
                _matrix = new TransformMatrix(m11, m12, 0f, m21, m22, 0f, 0f, 0f, 1f, tx, ty, 0f);
            }

            /// <summary>
            /// Returns the matrix transformation.
            /// </summary>
            /// <returns>The stored matrix.</returns>
            public override TransformMatrix ComputeMatrix()
            {
                return _matrix;
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Matrix, _matrix.M11.ToString(CultureInfo.InvariantCulture), _matrix.M12.ToString(CultureInfo.InvariantCulture), _matrix.M21.ToString(CultureInfo.InvariantCulture), _matrix.M22.ToString(CultureInfo.InvariantCulture), _matrix.Tx.ToString(CultureInfo.InvariantCulture), _matrix.Ty.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Represents the matrix3d transformation.
        /// </summary>
        public sealed class Matrix3D : CSSTransformValue
        {
            TransformMatrix _matrix;

            internal Matrix3D(Single m11, Single m12, Single m13, Single m21, Single m22, Single m23, Single m31, Single m32, Single m33, Single tx, Single ty, Single tz)
            {
                _matrix = new TransformMatrix(m11, m12, m13, m21, m22, m23, m31, m32, m33, tx, ty, tz);
            }

            /// <summary>
            /// Returns the stored matrix.
            /// </summary>
            /// <returns>The current transformation.</returns>
            public override TransformMatrix ComputeMatrix()
            {
                return _matrix;
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public override String ToCss()
            {
                return _matrix.ToCss();
            }
        }

        /// <summary>
        /// Represents the translate transformation.
        /// </summary>
        public sealed class Translate : CSSTransformValue
        {
            CSSCalcValue _x;
            CSSCalcValue _y;

            internal Translate(CSSCalcValue x, CSSCalcValue y)
            {
                _x = x;
                _y = y;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public override TransformMatrix ComputeMatrix()
            {
                var dx = _x.ToPixel();
                var dy = _y.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, dx, dy, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Translate, _x.CssText, _y.CssText);
            }
        }

        /// <summary>
        /// Represents the translate-x transformation.
        /// </summary>
        public sealed class TranslateX : CSSTransformValue
        {
            CSSCalcValue _x;

            internal TranslateX(CSSCalcValue x)
            {
                _x = x;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public override TransformMatrix ComputeMatrix()
            {
                var dx = _x.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, dx, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.TranslateX, _x.CssText);
            }
        }

        /// <summary>
        /// Represents the translate-y transformation.
        /// </summary>
        public sealed class TranslateY : CSSTransformValue
        {
            CSSCalcValue _y;

            internal TranslateY(CSSCalcValue y)
            {
                _y = y;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public override TransformMatrix ComputeMatrix()
            {
                var dy = _y.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, dy, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.TranslateY, _y.CssText);
            }
        }

        /// <summary>
        /// Represents the translate-z transformation.
        /// </summary>
        public sealed class TranslateZ : CSSTransformValue
        {
            CSSCalcValue _z;

            internal TranslateZ(CSSCalcValue z)
            {
                _z = z;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public override TransformMatrix ComputeMatrix()
            {
                var dz = _z.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, dz);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.TranslateZ, _z.CssText);
            }
        }

        /// <summary>
        /// Represents the translate3d transformation.
        /// </summary>
        public sealed class Translate3D : CSSTransformValue
        {
            CSSCalcValue _x;
            CSSCalcValue _y;
            CSSCalcValue _z;

            internal Translate3D(CSSCalcValue x, CSSCalcValue y, CSSCalcValue z)
            {
                _x = x;
                _y = y;
                _z = z;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public override TransformMatrix ComputeMatrix()
            {
                var dx = _x.ToPixel();
                var dy = _y.ToPixel();
                var dz = _z.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, dx, dy, dz);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Translate3d, _x.CssText, _y.CssText, _z.CssText);
            }
        }

        /// <summary>
        /// Represents the rotate transformation.
        /// </summary>
        public sealed class Rotate : CSSTransformValue
        {
            Angle _angle;

            internal Rotate(Angle angle)
            {
                _angle = angle;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public override TransformMatrix ComputeMatrix()
            {
                var sina = _angle.Sin();
                var cosa = _angle.Cos();
                return new TransformMatrix(cosa, sina, 0f, -sina, cosa, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Rotate, _angle.ToCss());
            }
        }

        /// <summary>
        /// Represents the rotate3d transformation.
        /// </summary>
        public sealed class Rotate3D : CSSTransformValue
        {
            Single _x;
            Single _y;
            Single _z;
            Angle _angle;

            internal Rotate3D(Single x, Single y, Single z, Angle angle)
            {
                _x = x;
                _y = y;
                _z = z;
                _angle = angle;
            }

            /// <summary>
            /// Constructs a rotate 3D transformation around the x-axis.
            /// </summary>
            /// <param name="angle">The angle to rotate.</param>
            /// <returns>The rotate 3D transformation.</returns>
            public static Rotate3D RotateX(Angle angle)
            {
                return new Rotate3D(1f, 0f, 0f, angle);
            }

            /// <summary>
            /// Constructs a rotate 3D transformation around the y-axis.
            /// </summary>
            /// <param name="angle">The angle to rotate.</param>
            /// <returns>The rotate 3D transformation.</returns>
            public static Rotate3D RotateY(Angle angle)
            {
                return new Rotate3D(0f, 1f, 0f, angle);
            }

            /// <summary>
            /// Constructs a rotate 3D transformation around the z-axis.
            /// </summary>
            /// <param name="angle">The angle to rotate.</param>
            /// <returns>The rotate 3D transformation.</returns>
            public static Rotate3D RotateZ(Angle angle)
            {
                return new Rotate3D(0f, 0f, 1f, angle);
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public override TransformMatrix ComputeMatrix()
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
                    0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Rotate3d, _x.ToString(CultureInfo.InvariantCulture), _y.ToString(CultureInfo.InvariantCulture), _z.ToString(CultureInfo.InvariantCulture), _angle.ToCss());
            }
        }

        /// <summary>
        /// Represents the scale transformation.
        /// </summary>
        public sealed class Scale : CSSTransformValue
        {
            Single _sx;
            Single _sy;

            internal Scale(Single scale)
            {
                _sx = scale;
                _sy = scale;
            }

            internal Scale(Single sx, Single sy)
            {
                _sx = sx;
                _sy = sy;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public override TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(_sx, 0f, 0f, 0f, _sy, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public override String ToCss()
            {
                if (_sx == _sy)
                    return FunctionNames.Build(FunctionNames.Scale, _sx.ToString(CultureInfo.InvariantCulture));

                return FunctionNames.Build(FunctionNames.Scale, _sx.ToString(CultureInfo.InvariantCulture), _sy.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Represents the scale-x transformation.
        /// </summary>
        public sealed class ScaleX : CSSTransformValue
        {
            Single _scale;

            internal ScaleX(Single scale)
            {
                _scale = scale;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public override TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(_scale, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.ScaleX, _scale.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Represents the scale-y transformation.
        /// </summary>
        public sealed class ScaleY : CSSTransformValue
        {
            Single _scale;

            internal ScaleY(Single scale)
            {
                _scale = scale;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public override TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(1f, 0f, 0f, 0f, _scale, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.ScaleY, _scale.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Represents the scale-z transformation.
        /// </summary>
        public sealed class ScaleZ : CSSTransformValue
        {
            Single _scale;

            internal ScaleZ(Single scale)
            {
                _scale = scale;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public override TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, _scale, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.ScaleZ, _scale.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Represents the scale3d transformation.
        /// </summary>
        public sealed class Scale3D : CSSTransformValue
        {
            Single _sx;
            Single _sy;
            Single _sz;

            internal Scale3D(Single sx, Single sy, Single sz)
            {
                _sx = sx;
                _sy = sy;
                _sz = sz;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public override TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(_sx, 0f, 0f, 0f, _sy, 0f, 0f, 0f, _sz, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Scale3d, _sx.ToString(CultureInfo.InvariantCulture), _sy.ToString(CultureInfo.InvariantCulture), _sz.ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Represents the skew transformation.
        /// </summary>
        public sealed class Skew : CSSTransformValue
        {
            Angle _alpha;
            Angle _beta;

            internal Skew(Angle alpha, Angle beta)
            {
                _alpha = alpha;
                _beta = beta;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public override TransformMatrix ComputeMatrix()
            {
                var a = _alpha.Tan();
                var b = _beta.Tan();
                return new TransformMatrix(1f, a, 0f, b, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Skew, _alpha.ToCss(), _beta.ToCss());
            }
        }

        /// <summary>
        /// Represents the skew-x transformation.
        /// </summary>
        public sealed class SkewX : CSSTransformValue
        {
            Angle _angle;

            internal SkewX(Angle alpha)
            {
                _angle = alpha;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public override TransformMatrix ComputeMatrix()
            {
                var a = _angle.Tan();
                return new TransformMatrix(1f, a, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.SkewX, _angle.ToCss());
            }
        }

        /// <summary>
        /// Represents the skew-y transformation.
        /// </summary>
        public sealed class SkewY : CSSTransformValue
        {
            Angle _angle;

            internal SkewY(Angle beta)
            {
                _angle = beta;
            }

            /// <summary>
            /// Computes the matrix for the given transformation.
            /// </summary>
            /// <returns>The transformation matrix representation.</returns>
            public override TransformMatrix ComputeMatrix()
            {
                var b = _angle.Tan();
                return new TransformMatrix(1f, 0f, 0f, b, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            /// <summary>
            /// Returns a CSS representation of the transformation.
            /// </summary>
            /// <returns>The CSS value string.</returns>
            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.SkewY, _angle.ToCss());
            }
        }

        #endregion
    }
}
