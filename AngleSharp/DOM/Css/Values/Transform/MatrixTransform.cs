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
        readonly Single[] _values;

        internal MatrixTransform(Single[] values)
        {
            _values = values;
        }

        /// <summary>
        /// Returns the stored matrix.
        /// </summary>
        /// <returns>The current transformation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            return new TransformMatrix(_values);
        }

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
    }
}
