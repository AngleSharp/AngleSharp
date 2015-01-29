namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// Represents the CSS position property.
    /// </summary>
    public interface ICssPositionProperty : ICssProperty
    {
        /// <summary>
        /// Gets the currently selected position mode.
        /// </summary>
        PositionMode State { get; }
    }
}
