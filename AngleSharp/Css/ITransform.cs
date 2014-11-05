namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// Functionality for computing transformation.
    /// </summary>
    public interface ITransform
    {
        /// <summary>
        /// Computes the matrix for the given transformation.
        /// </summary>
        /// <returns>The transformation matrix representation.</returns>
        TransformMatrix ComputeMatrix();

        /// <summary>
        /// Returns the CSS representation of the object.
        /// </summary>
        /// <returns>The CSS value string.</returns>
        String ToCss();
    }
}
