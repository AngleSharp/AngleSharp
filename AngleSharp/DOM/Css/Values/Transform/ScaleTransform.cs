namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents the scale3d transformation.
    /// </summary>
    sealed class ScaleTransform : ITransform
    {
        readonly Single _sx;
        readonly Single _sy;
        readonly Single _sz;

        internal ScaleTransform(Single sx, Single sy, Single sz)
        {
            _sx = sx;
            _sy = sy;
            _sz = sz;
        }

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            return new TransformMatrix(_sx, 0f, 0f, 0f, _sy, 0f, 0f, 0f, _sz, 0f, 0f, 0f);
        }

        CssValueType ICssValue.Type
        {
            get { return CssValueType.Primitive; }
        }

        String ICssValue.CssText
        {
            get { return FunctionNames.Build(FunctionNames.Scale3d, _sx.ToString(CultureInfo.InvariantCulture), _sy.ToString(CultureInfo.InvariantCulture), _sz.ToString(CultureInfo.InvariantCulture)); }
        }
    }
}
