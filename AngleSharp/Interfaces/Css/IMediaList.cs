namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A collection of media queries.
    /// </summary>
    [DomName("MediaList")]
    public interface IMediaList : IEnumerable<String>
    {
        /// <summary>
        /// Gets or sets the parsable textual representation of the media list.
        /// This is a comma-separated list of media.
        /// </summary>
        [DomName("mediaText")]
        String MediaText { get; set; }

        /// <summary>
        /// Gets the number of media in the list. 
        /// </summary>
        [DomName("length")]
        Int32 Length { get; }

        /// <summary>
        /// Gets the medium item at the specified index.
        /// </summary>
        /// <param name="index">Index into the collection.</param>
        /// <returns>
        /// The medium at the index-th position in the MediaList,
        /// or null if that is not a valid index.
        /// </returns>
        [DomAccessor(Accessors.Getter)]
        [DomName("item")]
        String this[Int32 index] { get; }

        /// <summary>
        /// Adds the medium to the end of the list. If the medium is already used,
        /// it is first removed.
        /// </summary>
        /// <param name="medium">The new medium to add.</param>
        [DomName("appendMedium")]
        void Add(String medium);

        /// <summary>
        /// Deletes the medium indicated from the list of media queries.
        /// </summary>
        /// <param name="medium">The medium to delete from the list.</param>
        [DomName("removeMedium")]
        void Remove(String medium);

        /// <summary>
        /// Validates the list of contained media against the rendering device.
        /// </summary>
        /// <param name="device">The rendering device.</param>
        /// <returns>True if the constraints are satisfied, otherwise false.</returns>
        Boolean Validate(RenderDevice device);
    }
}
