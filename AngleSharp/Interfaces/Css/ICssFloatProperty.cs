namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS float property.
    /// </summary>
    public interface ICssFloatProperty : ICssProperty
    {
        /// <summary>
        /// Gets the value of the floating property.
        /// </summary>
        Floating State { get; }
    }
}
