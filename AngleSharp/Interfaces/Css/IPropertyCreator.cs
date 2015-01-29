namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Defines a special property creator.
    /// </summary>
    interface IPropertyCreator
    {
        /// <summary>
        /// Tries to create a declaration with the provided name.
        /// </summary>
        /// <param name="name">The name of the declaration.</param>
        /// <param name="style">The parent declarations.</param>
        /// <returns>The created property, if any.</returns>
        CssProperty Create(String name, CssStyleDeclaration style);
    }
}
