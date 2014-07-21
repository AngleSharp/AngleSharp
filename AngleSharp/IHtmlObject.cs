namespace AngleSharp
{
    using System;

    /// <summary>
    /// Represents an object that can be represented in HTML markup.
    /// </summary>
    interface IHtmlObject
    {
        /// <summary>
        /// Returns the HTML markup representation of the object.
        /// </summary>
        /// <returns>The source code snippet.</returns>
        String ToHtml();
    }
}
