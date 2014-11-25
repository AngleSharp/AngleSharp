namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// Represents a general transform function.
    /// </summary>
    public abstract class TransitionFunction : ICssObject
    {
        #region Methods

        /// <summary>
        /// Returns the CSS representation of the function.
        /// </summary>
        /// <returns>The string representing the CSS code.</returns>
        public abstract String ToCss();

        #endregion

        #region Pre-Made Transitions

        /// <summary>
        /// Gets the pre-defined ease function.
        /// </summary>
        public static readonly TransitionFunction Ease = new CubicBezierTransitionFunction(0.25f, 0.1f, 0.25f, 1f);
        /// <summary>
        /// Gets the pre-defined ease-in function.
        /// </summary>
        public static readonly TransitionFunction EaseIn = new CubicBezierTransitionFunction(0.42f, 0f, 1f, 1f);
        /// <summary>
        /// Gets the pre-defined ease-in-out function.
        /// </summary>
        public static readonly TransitionFunction EaseInOut = new CubicBezierTransitionFunction(0.42f, 0f, 0.58f, 1f);
        /// <summary>
        /// Gets the pre-defined ease-out function.
        /// </summary>
        public static readonly TransitionFunction EaseOut = new CubicBezierTransitionFunction(0f, 0f, 0.58f, 1f);
        /// <summary>
        /// Gets the pre-defined linear function.
        /// </summary>
        public static readonly TransitionFunction Linear = new CubicBezierTransitionFunction(0f, 0f, 1f, 1f);
        /// <summary>
        /// Gets the pre-defined step-start function.
        /// </summary>
        public static readonly TransitionFunction StepStart = new StepsTransitionFunction(1, true);
        /// <summary>
        /// Gets the pre-defined step-end function.
        /// </summary>
        public static readonly TransitionFunction StepEnd = new StepsTransitionFunction(1, false);

        #endregion
    }
}
