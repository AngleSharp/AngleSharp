namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// Represents the possibility of creating rules.
    /// </summary>
    public interface ICssRuleCreator
    {
        /// <summary>
        /// Creates a new CSS rule and appends it to the current node.
        /// </summary>
        /// <param name="ruleType">The type of rule to create.</param>
        /// <returns>The created rule.</returns>
        ICssRule AddNewRule(CssRuleType ruleType);
    }
}
