namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// An enumeration over all possible fill-mode values.
    /// </summary>
    public enum AnimationFillStyle : ushort
    {
        /// <summary>
        /// The animation will not apply any styles to the
        /// target before or after it is executing.
        /// </summary>
        None,
        /// <summary>
        /// The target will retain the computed values set by
        /// the last keyframe encountered during execution. The
        /// last keyframe encountered depends on the direction
        /// and the number of iterations.
        /// </summary>
        Forwards,
        /// <summary>
        /// The animation will apply the values defined in the
        /// first relevant keyframe as soon as it is applied to
        /// the target, and retain this during the delayed period.
        /// The first relevant keyframe depends of the value of
        /// the direction.
        /// </summary>
        Backwards,
        /// <summary>
        /// The animation will follow the rules for both forwards
        /// and backwards, thus extending the animation properties
        /// in both directions.
        /// </summary>
        Both
    }
}
