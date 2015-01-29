namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Represents the CSS backface-visibility property.
    /// </summary>
    public interface ICssBackfaceVisibilityProperty : ICssProperty
    {
        /// <summary>
        /// Gets if the back face is visible, allowing the front
        /// face to be displayed mirrored.
        /// </summary>
        Boolean IsVisible { get; }
    }
}
