namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS list-style-type property.
    /// </summary>
    public interface ICssListStyleTypeProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected style for the list.
        /// </summary>
        ListStyle Style { get; }
    }

    /// <summary>
    /// Represents the CSS list-style-position property.
    /// </summary>
    public interface ICssListStylePositionProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected position.
        /// </summary>
        ListPosition Position { get; }
    }

    /// <summary>
    /// Represents the CSS list-style-image property.
    /// </summary>
    public interface ICssListStyleImageProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected image for the list.
        /// </summary>
        IEnumerable<Url> Images { get; }
    }

    /// <summary>
    /// Represents the CSS list-style shorthand property.
    /// </summary>
    public interface ICssListStyleProperty : ICssProperty, ICssListStyleImageProperty, ICssListStylePositionProperty, ICssListStyleTypeProperty
    {
    }
}
