namespace AngleSharp.DOM.Css
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS animation-play-state property.
    /// </summary>
    public interface ICssAnimationPlayStateProperty : ICssProperty
    {
        /// <summary>
        /// Gets an enumerable over the defined play states.
        /// </summary>
        IEnumerable<PlayState> States { get; }
    }
}
