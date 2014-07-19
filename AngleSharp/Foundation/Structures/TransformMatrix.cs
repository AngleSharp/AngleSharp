namespace AngleSharp.DOM
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a transformation matrix value.
    /// </summary>
    public struct TransformMatrix : IEquatable<TransformMatrix>, ICssObject
    {
        #region Fields

        /// <summary>
        /// Gets the zero matrix.
        /// </summary>
        public static readonly TransformMatrix Zero = new TransformMatrix();

        /// <summary>
        /// Gets the unity matrix.
        /// </summary>
        public static readonly TransformMatrix One = new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f);

        Single _m11;
        Single _m12;
        Single _m13;
        Single _m21;
        Single _m22;
        Single _m23;
        Single _m31;
        Single _m32;
        Single _m33;
        Single _tx;
        Single _ty;
        Single _tz;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a (3D) transformation matrix.
        /// </summary>
        /// <param name="m11">The (1, 1) entry.</param>
        /// <param name="m12">The (1, 2) entry.</param>
        /// <param name="m13">The (1, 3) entry.</param>
        /// <param name="m21">The (2, 1) entry.</param>
        /// <param name="m22">The (2, 2) entry.</param>
        /// <param name="m23">The (2, 3) entry.</param>
        /// <param name="m31">The (3, 1) entry.</param>
        /// <param name="m32">The (3, 2) entry.</param>
        /// <param name="m33">The (3, 3) entry.</param>
        /// <param name="tx">The x-translation entry.</param>
        /// <param name="ty">The y-translation entry.</param>
        /// <param name="tz">The z-translation entry.</param>
        public TransformMatrix(Single m11, Single m12, Single m13, Single m21, Single m22, Single m23, Single m31, Single m32, Single m33, Single tx, Single ty, Single tz)
        {
            _m11 = m11;
            _m12 = m12;
            _m13 = m13;
            _m21 = m21;
            _m22 = m22;
            _m23 = m23;
            _m31 = m31;
            _m32 = m32;
            _m33 = m33;
            _tx = tx;
            _ty = ty;
            _tz = tz;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the element of the 1st row, 1st column.
        /// </summary>
        public Single M11
        {
            get { return _m11; }
        }

        /// <summary>
        /// Gets the element of the 1st row, 2nd column.
        /// </summary>
        public Single M12
        {
            get { return _m12; }
        }

        /// <summary>
        /// Gets the element of the 1st row, 3rd column.
        /// </summary>
        public Single M13
        {
            get { return _m13; }
        }

        /// <summary>
        /// Gets the element of the 2nd row, 1st column.
        /// </summary>
        public Single M21
        {
            get { return _m21; }
        }

        /// <summary>
        /// Gets the element of the 2nd row, 2nd column.
        /// </summary>
        public Single M22
        {
            get { return _m22; }
        }

        /// <summary>
        /// Gets the element of the 2nd row, 3rd column.
        /// </summary>
        public Single M23
        {
            get { return _m23; }
        }

        /// <summary>
        /// Gets the element of the 3rd row, 1st column.
        /// </summary>
        public Single M31
        {
            get { return _m31; }
        }

        /// <summary>
        /// Gets the element of the 3rd row, 2nd column.
        /// </summary>
        public Single M32
        {
            get { return _m32; }
        }

        /// <summary>
        /// Gets the element of the 3rd row, 3rd column.
        /// </summary>
        public Single M33
        {
            get { return _m33; }
        }

        /// <summary>
        /// Gets the x-element of the translation vector.
        /// </summary>
        public Single Tx
        {
            get { return _tx; }
        }

        /// <summary>
        /// Gets the y-element of the translation vector.
        /// </summary>
        public Single Ty
        {
            get { return _ty; }
        }

        /// <summary>
        /// Gets the z-element of the translation vector.
        /// </summary>
        public Single Tz
        {
            get { return _tz; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks for equality with the given other transformation matrix.
        /// </summary>
        /// <param name="other">The other transformation matrix.</param>
        /// <returns>True if all elements are equal, otherwise false.</returns>
        public Boolean Equals(TransformMatrix other)
        {
            return _m11 == other._m11 && _m12 == other._m12 && _m13 == other._m13 &&
                _m21 == other._m21 && _m22 == other._m22 && _m32 == other._m32 &&
                _m31 == other._m31 && _m23 == other._m23 && _m33 == other._m33 &&
                _tx == other._tx && _ty == other._ty && _tz == other._tz;
        }

        #endregion

        #region Operator

        /// <summary>
        /// Computes the combination of two transform matrices.
        /// </summary>
        /// <param name="a">The first matrix (applied later).</param>
        /// <param name="b">The original transform matrix (has been applied).</param>
        /// <returns>The combination of both matrices.</returns>
        public static TransformMatrix operator *(TransformMatrix a, TransformMatrix b)
        {
            return new TransformMatrix(
                a._m11 * b._m11 + a._m12 * b._m21 + a._m13 * b._m31,
                a._m11 * b._m12 + a._m12 * b._m22 + a._m13 * b._m32,
                a._m11 * b._m13 + a._m12 * b._m23 + a._m13 * b._m33,
                a._m21 * b._m11 + a._m22 * b._m21 + a._m23 * b._m31,
                a._m21 * b._m12 + a._m22 * b._m22 + a._m23 * b._m32,
                a._m21 * b._m13 + a._m22 * b._m23 + a._m23 * b._m33,
                a._m31 * b._m11 + a._m32 * b._m21 + a._m33 * b._m31,
                a._m31 * b._m12 + a._m32 * b._m22 + a._m33 * b._m32,
                a._m31 * b._m13 + a._m32 * b._m23 + a._m33 * b._m33,
                a._m11 * b._tx + a._m12 * b._ty + a._m13 * b._tz + a._tx,
                a._m21 * b._tx + a._m22 * b._ty + a._m23 * b._tz + a._ty,
                a._m31 * b._tx + a._m32 * b._ty + a._m33 * b._tz + a._tz
            );
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            if (obj is TransformMatrix)
                return this.Equals((TransformMatrix)obj);

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current length.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode()
        {
            return (Int32)(_m11 + _m12 + _m13 + _m21 + _m22 + _m23 + _m31 + _m32 + _m33 + _tx + _ty + _tz);
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns the CSS representation of the transformation matrix.
        /// </summary>
        /// <returns>The string containing the CSS function.</returns>
        public String ToCss()
        {
            return FunctionNames.Build(FunctionNames.Matrix3d, _m11.ToString(CultureInfo.InvariantCulture), _m12.ToString(CultureInfo.InvariantCulture), _m13.ToString(CultureInfo.InvariantCulture), _m21.ToString(CultureInfo.InvariantCulture), _m22.ToString(CultureInfo.InvariantCulture), _m23.ToString(CultureInfo.InvariantCulture), _m31.ToString(CultureInfo.InvariantCulture), _m32.ToString(CultureInfo.InvariantCulture), _m33.ToString(CultureInfo.InvariantCulture), _tx.ToString(CultureInfo.InvariantCulture), _ty.ToString(CultureInfo.InvariantCulture), _tz.ToString(CultureInfo.InvariantCulture));
        }

        #endregion
    }
}
