namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Functionality for computing transformation.
    /// </summary>
    public interface ITransform : ICssValue
    {
        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        TransformMatrix ComputeMatrix();
    }
}
