namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Represents the CSS column-rule-color property.
    /// </summary>
    public interface ICssColumnRuleColorProperty : ICssProperty
    {
        /// <summary>
        /// Gets the color for the vertical column rule.
        /// </summary>
        Color Color { get; }
    }

    /// <summary>
    /// Represents the CSS column-rule-style property.
    /// </summary>
    public interface ICssColumnRuleStyleProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected column-rule line style.
        /// </summary>
        LineStyle Style { get; }
    }

    /// <summary>
    /// Represents the CSS column-rule-width property.
    /// </summary>
    public interface ICssColumnRuleWidthProperty : ICssProperty
    {
        /// <summary>
        /// Gets the width of the column-rule.
        /// </summary>
        Length Width { get; }
    }

    /// <summary>
    /// Represents the CSS column-rule shorthand property.
    /// </summary>
    public interface ICssColumnRuleProperty : ICssProperty, ICssColumnRuleColorProperty, ICssColumnRuleStyleProperty, ICssColumnRuleWidthProperty
    {
    }
}
