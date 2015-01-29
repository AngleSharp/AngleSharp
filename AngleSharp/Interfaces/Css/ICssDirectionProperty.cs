namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// Represents the CSS direction property.
    /// </summary>
    public interface ICssDirectionProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected text direction.
        /// </summary>
        DirectionMode State { get; }
    }
}
