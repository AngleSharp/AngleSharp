namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Represents the CSS opacity property.
    /// </summary>
    public interface ICssOpacityProperty : ICssProperty
    {
        /// <summary>
        /// Gets the value that should be used for the opacity.
        /// </summary>
        Single Opacity { get; }
    }
}
