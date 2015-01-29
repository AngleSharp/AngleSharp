namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// Represents the CSS transform-origin property.
    /// </summary>
    public interface ICssTransformOriginProperty : ICssProperty
    {
        /// <summary>
        /// Gets how far from the left edge of the box the origin of the transform is set.
        /// </summary>
        Length X { get; }

        /// <summary>
        /// Gets how far from the top edge of the box the origin of the transform is set.
        /// </summary>
        Length Y { get; }

        /// <summary>
        /// Gets how far from the user eye the z = 0 origin is set.
        /// </summary>
        Length Z { get; }
    }
}
