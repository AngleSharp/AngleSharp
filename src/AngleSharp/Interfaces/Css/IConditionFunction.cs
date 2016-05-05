namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Represents a function of the @supports rule.
    /// </summary>
    public interface IConditionFunction : ICssNode
    {
        /// <summary>
        /// Evaluates the condition and returns the result.
        /// </summary>
        /// <returns>True if the condition is supported, else false.</returns>
        Boolean Check();
    }
}
