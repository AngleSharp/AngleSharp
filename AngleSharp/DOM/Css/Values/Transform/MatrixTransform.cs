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

        #region CSS Value

        CssValueType ICssValue.Type
        {
            get { return CssValueType.Primitive; }
        }

        String ICssValue.CssText
        {
            get
            {
                var values = new String[_values.Length];

                for (int i = 0; i < values.Length; i++)
                    values[i] = _values[i].ToString(CultureInfo.InvariantCulture);

                return FunctionNames.Build(FunctionNames.Matrix3d, values);
            }
        }

        #endregion
    }
}
