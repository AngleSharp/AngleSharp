namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a transformation in CSS.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform#CSS_transform_functions
    /// </summary>
    abstract class CSSTransformValue : CSSPrimitiveValue
    {
        #region Properties

        public abstract TransformMatrix ComputeMatrix();

        #endregion

        #region Classes

        public sealed class Matrix : CSSTransformValue
        {
            TransformMatrix matrix;

            public Matrix(Single m11, Single m12, Single m21, Single m22, Single tx, Single ty)
            {
                matrix = new TransformMatrix(m11, m12, 0f, m21, m22, 0f, 0f, 0f, 1f, tx, ty, 0f);
            }

            public override TransformMatrix ComputeMatrix()
            {
                return matrix;
            }
        }

        public sealed class Matrix3D : CSSTransformValue
        {
            TransformMatrix matrix;

            public Matrix3D(Single m11, Single m12, Single m13, Single m21, Single m22, Single m23, Single m31, Single m32, Single m33, Single tx, Single ty, Single tz)
            {
                matrix = new TransformMatrix(m11, m12, m13, m21, m22, m23, m31, m32, m33, tx, ty, tz);
            }

            public override TransformMatrix ComputeMatrix()
            {
                return matrix;
            }
        }

        public sealed class Translate : CSSTransformValue
        {
            CSSCalcValue x;
            CSSCalcValue y;

            public Translate(CSSCalcValue x, CSSCalcValue y)
            {
                this.x = x;
                this.y = y;
            }

            public static CSSTransformValue TranslateX(CSSCalcValue dx)
            {
                return new Translate(dx, CSSCalcValue.FromLength(Length.Zero));
            }

            public static CSSTransformValue TranslateY(CSSCalcValue dy)
            {
                return new Translate(CSSCalcValue.FromLength(Length.Zero), dy);
            }

            public override TransformMatrix ComputeMatrix()
            {
                var dx = x.ToPixel();
                var dy = y.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, dx, dy, 0f);
            }
        }

        public sealed class Translate3D : CSSTransformValue
        {
            CSSCalcValue x;
            CSSCalcValue y;
            CSSCalcValue z;

            public Translate3D(CSSCalcValue x, CSSCalcValue y, CSSCalcValue z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public static CSSTransformValue TranslateX(CSSCalcValue dx)
            {
                return new Translate3D(dx, CSSCalcValue.FromLength(Length.Zero), CSSCalcValue.FromLength(Length.Zero));
            }

            public static CSSTransformValue TranslateY(CSSCalcValue dy)
            {
                return new Translate3D(CSSCalcValue.FromLength(Length.Zero), dy, CSSCalcValue.FromLength(Length.Zero));
            }

            public static CSSTransformValue TranslateZ(CSSCalcValue dz)
            {
                return new Translate3D(CSSCalcValue.FromLength(Length.Zero), CSSCalcValue.FromLength(Length.Zero), dz);
            }

            public override TransformMatrix ComputeMatrix()
            {
                var dx = x.ToPixel();
                var dy = y.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, dx, dy, 0f);
            }
        }

        public sealed class Rotate : CSSTransformValue
        {
            Single sina;
            Single cosa;

            public Rotate(Angle angle)
            {
                sina = angle.Sin();
                cosa = angle.Cos();
            }

            public override TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(cosa, sina, 0f, -sina, cosa, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }
        }

        public sealed class Rotate3D : CSSTransformValue
        {
            Single sina;
            Single cosa;
            Single l;
            Single m;
            Single n;

            public Rotate3D(Single x, Single y, Single z, Angle angle)
            {
                var norm = 1f / (Single)Math.Sqrt(x * x + y * y + z * z);
                sina = angle.Sin();
                cosa = angle.Cos();
                l = x * norm;
                m = y * norm;
                n = z * norm;
            }

            public static Rotate3D RotateX(Angle angle)
            {
                return new Rotate3D(1f, 0f, 0f, angle);
            }

            public static Rotate3D RotateY(Angle angle)
            {
                return new Rotate3D(0f, 1f, 0f, angle);
            }

            public static Rotate3D RotateZ(Angle angle)
            {
                return new Rotate3D(0f, 0f, 1f, angle);
            }

            public override TransformMatrix ComputeMatrix()
            {
                var omc = (1f - cosa);
                return new TransformMatrix(
                    l * l * omc + cosa, m * l * omc - n * sina, n * l * omc + m * sina,
                    l * m * omc + n * sina, m * m * omc + cosa, n * m * omc - l * sina,
                    l * n * omc - m * sina, m * n * omc + l * sina, n * n * omc + cosa,
                    0f, 0f, 0f);
            }
        }

        public sealed class Scale : CSSTransformValue
        {
            Single sx;
            Single sy;

            public Scale(Single sx, Single sy)
            {
                this.sx = sx;
                this.sy = sy;
            }

            public override TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(sx, 0f, 0f, 0f, sy, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            public static Scale ScaleX(Single sx)
            {
                return new Scale(sx, 0f);
            }

            public static Scale ScaleY(Single sy)
            {
                return new Scale(0f, sy);
            }
        }

        public sealed class Scale3D : CSSTransformValue
        {
            Single sx;
            Single sy;
            Single sz;

            public Scale3D(Single sx, Single sy, Single sz)
            {
                this.sx = sx;
                this.sy = sy;
                this.sz = sz;
            }

            public static Scale3D ScaleX(Single sx)
            {
                return new Scale3D(sx, 0f, 0f);
            }

            public static Scale3D ScaleY(Single sy)
            {
                return new Scale3D(0f, sy, 0f);
            }

            public static Scale3D ScaleZ(Single sz)
            {
                return new Scale3D(0f, 0f, sz);
            }

            public override TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(sx, 0f, 0f, 0f, sy, 0f, 0f, 0f, sz, 0f, 0f, 0f);
            }
        }

        public sealed class Skew : CSSTransformValue
        {
            Single a;
            Single b;

            public Skew(Angle alpha, Angle beta)
            {
                a = alpha.Tan();
                b = beta.Tan();
            }

            public static Skew SkewX(Angle angle)
            {
                return new Skew(angle, Angle.Zero);
            }

            public static Skew SkewY(Angle angle)
            {
                return new Skew(Angle.Zero, angle);
            }

            public override TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(1f, a, 0f, b, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }
        }

        #endregion
    }
}
