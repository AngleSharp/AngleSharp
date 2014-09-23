namespace AngleSharp.DOM.Css
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS background-position property.
    /// </summary>
    public interface ICssBackgroundPositionProperty : ICssProperty
    {
        /// <summary>
        /// Gets the value of the background position property.
        /// </summary>
        IEnumerable<Point> Positions { get; }
    }

    /// <summary>
    /// Represents the CSS background-origin property.
    /// </summary>
    public interface ICssBackgroundOriginProperty : ICssProperty
    {
        /// <summary>
        /// Gets an enumeration with the desired origin settings.
        /// </summary>
        IEnumerable<BoxModel> Origins { get; }
    }

    /// <summary>
    /// Represents the CSS background-image property.
    /// </summary>
    public interface ICssBackgroundImageProperty : ICssProperty
    {
        /// <summary>
        /// Gets the enumeration of all images.
        /// </summary>
        IEnumerable<IBitmap> Images { get; }
    }

    /// <summary>
    /// Represents the CSS background-color property.
    /// </summary>
    public interface ICssBackgroundColorProperty : ICssProperty
    {
        /// <summary>
        /// Gets the color of the background.
        /// </summary>
        /// <returns></returns>
        Color Color { get; }
    }

    /// <summary>
    /// Represents the CSS background-clip property.
    /// </summary>
    public interface ICssBackgroundClipProperty : ICssProperty
    {
        /// <summary>
        /// Gets an enumeration with the desired clip settings.
        /// </summary>
        IEnumerable<BoxModel> Clips { get; }
    }

    /// <summary>
    /// Represents the CSS background-attachment property.
    /// </summary>
    public interface ICssBackgroundAttachmentProperty : ICssProperty
    {
        /// <summary>
        /// Gets an enumeration with the desired attachment settings.
        /// </summary>
        IEnumerable<BackgroundAttachment> Attachments { get; }
    }

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

    /// <summary>
    /// Represents the CSS background-size property.
    /// </summary>
    public interface ICssBackgroundSizeProperty : ICssProperty
    {
    }

    /// <summary>
    /// Represents the CSS background shorthand property.
    /// </summary>
    public interface ICssBackgroundProperty : ICssProperty, ICssBackgroundAttachmentProperty, ICssBackgroundClipProperty, ICssBackgroundColorProperty, ICssBackgroundImageProperty, ICssBackgroundOriginProperty, ICssBackgroundPositionProperty, ICssBackgroundRepeatProperty, ICssBackgroundSizeProperty
    {
    }
}
