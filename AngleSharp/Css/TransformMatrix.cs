namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// Represents a transformation matrix value.
    /// http://dev.w3.org/csswg/css-transforms/#mathematical-description
    /// </summary>
    public sealed class TransformMatrix : IEquatable<TransformMatrix>
    {
        #region Fields

        /// <summary>
        /// Gets the zero matrix.
        /// </summary>
        public static readonly TransformMatrix Zero = new TransformMatrix();

        /// <summary>
        /// Gets the unity matrix.
        /// </summary>
        public static readonly TransformMatrix One = new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 0f, 0f);

        readonly Single[,] _matrix;

        #endregion

        #region ctor

        TransformMatrix()
        {
            _matrix = new Single[4, 4];
        }

        /// <summary>
        /// Creates a new transformation matrix from a 1D-array.
        /// </summary>
        /// <param name="values">The array with values.</param>
        public TransformMatrix(Single[] values)
            : this()
        {
            if (values == null)
                throw new ArgumentNullException("values");

            if (values.Length != 16)
                throw new ArgumentException("You need to provide 16 (4x4) values.", "values");

            for (int i = 0, k = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++, k++)
                    _matrix[j, i] = values[k];
            }
        }

        /// <summary>
        /// Creates a transformation matrix.
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
        /// <param name="px">The x-perspective entry.</param>
        /// <param name="py">The y-perspective entry.</param>
        /// <param name="pz">The z-perspective entry.</param>
        public TransformMatrix(
            Single m11, Single m12, Single m13, 
            Single m21, Single m22, Single m23, 
            Single m31, Single m32, Single m33, 
            Single tx, Single ty, Single tz, 
            Single px, Single py, Single pz)
            : this()
        {
            _matrix[0, 0] = m11;
            _matrix[0, 1] = m12;
            _matrix[0, 2] = m13;
            _matrix[1, 0] = m21;
            _matrix[1, 1] = m22;
            _matrix[1, 2] = m23;
            _matrix[2, 0] = m31;
            _matrix[2, 1] = m32;
            _matrix[2, 2] = m33;
            _matrix[0, 3] = tx;
            _matrix[1, 3] = ty;
            _matrix[2, 3] = tz;
            _matrix[3, 0] = px;
            _matrix[3, 1] = py;
            _matrix[3, 2] = pz;
            _matrix[3, 3] = 1f;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the element of the 1st row, 1st column.
        /// </summary>
        public Single M11
        {
            get { return _matrix[0, 0]; }
        }

        /// <summary>
        /// Gets the element of the 1st row, 2nd column.
        /// </summary>
        public Single M12
        {
            get { return _matrix[0, 1]; }
        }

        /// <summary>
        /// Gets the element of the 1st row, 3rd column.
        /// </summary>
        public Single M13
        {
            get { return _matrix[0, 2]; }
        }

        /// <summary>
        /// Gets the element of the 2nd row, 1st column.
        /// </summary>
        public Single M21
        {
            get { return _matrix[1, 0]; }
        }

        /// <summary>
        /// Gets the element of the 2nd row, 2nd column.
        /// </summary>
        public Single M22
        {
            get { return _matrix[1, 1]; }
        }

        /// <summary>
        /// Gets the element of the 2nd row, 3rd column.
        /// </summary>
        public Single M23
        {
            get { return _matrix[1, 2]; }
        }

        /// <summary>
        /// Gets the element of the 3rd row, 1st column.
        /// </summary>
        public Single M31
        {
            get { return _matrix[2, 0]; }
        }

        /// <summary>
        /// Gets the element of the 3rd row, 2nd column.
        /// </summary>
        public Single M32
        {
            get { return _matrix[2, 1]; }
        }

        /// <summary>
        /// Gets the element of the 3rd row, 3rd column.
        /// </summary>
        public Single M33
        {
            get { return _matrix[2, 2]; }
        }

        /// <summary>
        /// Gets the x-element of the translation vector.
        /// </summary>
        public Single Tx
        {
            get { return _matrix[0, 3]; }
        }

        /// <summary>
        /// Gets the y-element of the translation vector.
        /// </summary>
        public Single Ty
        {
            get { return _matrix[1, 3]; }
        }

        /// <summary>
        /// Gets the z-element of the translation vector.
        /// </summary>
        public Single Tz
        {
            get { return _matrix[2, 3]; }
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
            var A = this._matrix;
            var B = other._matrix;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (A[i, j] != B[i, j])
                        return false;
                }
            }

            return true;
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
            var sum = 0f;

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    sum += _matrix[i, j] * (4 * i + j);

            return (Int32)(sum);
        }

        #endregion
    }
}
