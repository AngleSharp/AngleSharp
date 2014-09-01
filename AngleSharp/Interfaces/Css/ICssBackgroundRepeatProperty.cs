namespace AngleSharp.DOM.Css
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS background-repeat property.
    /// </summary>
    public interface ICssBackgroundRepeatProperty : ICssProperty
    {
        /// <summary>
        /// Gets an enumeration with the horizontal repeat modes.
        /// </summary>
        IEnumerable<BackgroundRepeat> HorizontalRepeats { get; }

        /// <summary>
        /// Gets an enumeration with the vertical repeat modes.
        /// </summary>
        IEnumerable<BackgroundRepeat> VerticalRepeats { get; }
    }
}
