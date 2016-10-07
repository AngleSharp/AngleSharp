namespace AngleSharp.Services
{
    using AngleSharp.Dom.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the factory the create CSS properties.
    /// </summary>
    public interface ICssPropertyFactory
    {
        /// <summary>
        /// Creates a new general property with the given name.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The new property, if any.</returns>
        ICssProperty Create(String name);

        /// <summary>
        /// Creates a new @font-face property with the given name.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The new property, if any.</returns>
        ICssProperty CreateFont(String name);

        /// <summary>
        /// Creates a new longhand property with the given name.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The new property, if any.</returns>
        ICssProperty CreateLonghand(String name);

        /// <summary>
        /// Creates the longhand properties for the given name.
        /// </summary>
        /// <param name="name">The name of the shorthand property.</param>
        /// <returns>The longhand properties.</returns>
        ICssProperty[] CreateLonghandsFor(String name);
        
        /// <summary>
        /// Creates a new shorthand property with the given name.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The new property, if any.</returns>
        ICssProperty CreateShorthand(String name);
        
        /// <summary>
        /// Creates a new @viewport property with the given name.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The new property, if any.</returns>
        ICssProperty CreateViewport(String name);

        /// <summary>
        /// Gets the longhand names of the given shorthand.
        /// </summary>
        /// <param name="name">The name of the shorthand.</param>
        /// <returns>The list of longhand names.</returns>
        IEnumerable<String> GetLonghands(String name);

        /// <summary>
        /// Gets the shorthand names for the given longhand.
        /// </summary>
        /// <param name="name">The name of the longhand.</param>
        /// <returns>The list of shorthand names.</returns>
        IEnumerable<String> GetShorthands(String name);

        /// <summary>
        /// Checks if the property with the given name is animatable.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if it is animatable, otherwise false.</returns>
        Boolean IsAnimatable(String name);

        /// <summary>
        /// Checks if the property with the given name is a longhand.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if it is a longhand, otherwise false.</returns>
        Boolean IsLonghand(String name);

        /// <summary>
        /// Checks if the property with the given name is a shorthand.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if it is a shorthand, otherwise false.</returns>
        Boolean IsShorthand(String name);
    }
}
