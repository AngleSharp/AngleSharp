namespace AngleSharp
{
    using System;

    /// <summary>
    /// Represents a general transform function.
    /// </summary>
    public abstract class TransformFunction : ICssObject
    {
        #region Methods

        /// <summary>
        /// Returns the CSS representation of the function.
        /// </summary>
        /// <returns>The string representing the CSS code.</returns>
        public abstract String ToCss();

        #endregion

        #region Pre-Made Transforms

        /// <summary>
        /// Gets the pre-defined ease function.
        /// </summary>
        public static readonly TransformCubicBezier Ease = new TransformCubicBezier(0.25f, 0.1f, 0.25f, 1f);
        /// <summary>
        /// Gets the pre-defined ease-in function.
        /// </summary>
        public static readonly TransformCubicBezier EaseIn = new TransformCubicBezier(0.42f, 0f, 1f, 1f);
        /// <summary>
        /// Gets the pre-defined ease-in-out function.
        /// </summary>
        public static readonly TransformCubicBezier EaseInOut = new TransformCubicBezier(0.42f, 0f, 0.58f, 1f);
        /// <summary>
        /// Gets the pre-defined ease-out function.
        /// </summary>
        public static readonly TransformCubicBezier EaseOut = new TransformCubicBezier(0f, 0f, 0.58f, 1f);
        /// <summary>
        /// Gets the pre-defined linear function.
        /// </summary>
        public static readonly TransformCubicBezier Linear = new TransformCubicBezier(0f, 0f, 1f, 1f);
        /// <summary>
        /// Gets the pre-defined step-start function.
        /// </summary>
        public static readonly TransformSteps StepStart = new TransformSteps(1, true);
        /// <summary>
        /// Gets the pre-defined step-end function.
        /// </summary>
        public static readonly TransformSteps StepEnd = new TransformSteps(1, false);

        #endregion
    }
}
