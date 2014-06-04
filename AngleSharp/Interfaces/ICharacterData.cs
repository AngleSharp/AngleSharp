namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// The CharacterData abstract interface represents a Node object that contains characters. 
    /// </summary>
    [DOM("CharacterData")]
    public interface ICharacterData : INode
    {
        /// <summary>
        /// Gets or sets the contained text.
        /// </summary>
        [DOM("data")]
        String Data { get; set; }

        /// <summary>
        /// Gets the length of the contained text.
        /// </summary>
        [DOM("length")]
        Int32 Length { get; }

        /// <summary>
        /// Returns a string containing the part of Data of the specified
        /// length and starting at the specified offset.
        /// </summary>
        /// <param name="offset">The point where to start.</param>
        /// <param name="count">The numbers of characters to copy.</param>
        /// <returns>A string that contains a subset of the characters of Data.</returns>
        [DOM("substringData")]
        String Substring(Int32 offset, Int32 count);

        /// <summary>
        /// Appends the given value to the Data string.
        /// </summary>
        /// <param name="value">The text to append.</param>
        [DOM("appendData")]
        void Append(String value);

        /// <summary>
        /// Inserts the specified characters, at the specified offset,
        /// in the Data text.
        /// </summary>
        /// <param name="offset">The offset where to insert the text.</param>
        /// <param name="value">The text to insert.</param>
        [DOM("insertData")]
        void Insert(Int32 offset, String value);

        /// <summary>
        /// Removes the specified amount of characters, starting at
        /// the specified offset, from the Data.
        /// </summary>
        /// <param name="offset">The offset, where the removal should begin.</param>
        /// <param name="count">The number of characters to remove.</param>
        [DOM("deleteData")]
        void Delete(Int32 offset, Int32 count);

        /// <summary>
        /// Replaces the specified amount of characters, starting at the
        /// specified offset, with the specified value.
        /// </summary>
        /// <param name="offset">The offset, where the text should be inserted.</param>
        /// <param name="count">The amount of characters that should be overwritten.</param>
        /// <param name="value">The value to insert.</param>
        [DOM("replaceData")]
        void Replace(Int32 offset, Int32 count, String value);
    }
}