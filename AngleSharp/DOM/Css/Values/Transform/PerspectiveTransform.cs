namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Represents the distance transformation.
    /// </summary>
    sealed class PerspectiveTransform : ITransform
    {
        readonly Length _distance;

        internal PerspectiveTransform(Length distance)
        {
            _distance = distance;
        }

        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        public TransformMatrix ComputeMatrix()
        {
            return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 0f, -1f / _distance.ToPixel());
        }

        CssValueType ICssValue.Type
        {
            get { return CssValueType.Primitive; }
        }

        String ICssValue.CssText
        {
            get { return FunctionNames.Build(FunctionNames.Perspective, ((ICssValue)_distance).CssText); }
        }
    }
}
