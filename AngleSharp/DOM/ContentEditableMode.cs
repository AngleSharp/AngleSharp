using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// An enumeration with all contenteditable modes.
    /// </summary>
    public enum ContentEditableMode
    {
        /// <summary>
        /// Not contenteditable.
        /// </summary>
        False,
        /// <summary>
        /// The element is contenteditable.
        /// </summary>
        True,
        /// <summary>
        /// Inherited from the parent element.
        /// </summary>
        Inherited
    }
}
