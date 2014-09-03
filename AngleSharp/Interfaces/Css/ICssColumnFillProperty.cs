namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents the CSS column-fill property.
    /// </summary>
    public interface ICssColumnFillProperty : ICssProperty
    {
        /// <summary>
        /// Gets if the columns should be filled uniformly.
        /// </summary>
        Boolean IsBalanced { get; }
    }
}
