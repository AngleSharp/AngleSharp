namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Represents the translate3d transformation.
    /// </summary>
    sealed class TranslateTransform : ITransform
    {
        readonly IDistance _x;
        readonly IDistance _y;
        readonly IDistance _z;

        internal TranslateTransform(IDistance x, IDistance y, IDistance z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            var dx = _x.ToPixel();
            var dy = _y.ToPixel();
            var dz = _z.ToPixel();
            return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, dx, dy, dz, 0f, 0f, 0f);
        }

        CssValueType ICssValue.Type
        {
            get { return CssValueType.Primitive; }
        }

        String ICssValue.CssText
        {
            get { return FunctionNames.Build(FunctionNames.Translate3d, _x.CssText, _y.CssText, _z.CssText); }
        }
    }
}
