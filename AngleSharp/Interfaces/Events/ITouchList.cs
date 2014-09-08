namespace AngleSharp.DOM.Events
{
    using System;

    /// <summary>
    /// Represents a list with touch points.
    /// </summary>
    [DomName("TouchList")]
    public interface ITouchList
    {
        /// <summary>
        /// Gets the number of contained touch points.
        /// </summary>
        [DomName("length")]
        Int32 Length { get; }

        /// <summary>
        /// Gets the data of the touch point at the given index.
        /// </summary>
        /// <param name="index">The index of the touch point.</param>
        /// <returns>The touch point at the index.</returns>
        [DomAccessor(Accessors.Getter)]
        [DomName("item")]
        ITouch this[Int32 index] { get; }
    }
}
