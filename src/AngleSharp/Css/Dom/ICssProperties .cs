namespace AngleSharp.Css.Dom
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a set of CSS properties.
    /// </summary>
    [DomNoInterfaceObject]
    public interface ICssProperties : IEnumerable<ICssProperty>
    {
        /// <summary>
        /// Gets the value of the property with the given name.
        /// </summary>
        /// <param name="propertyName">The name of the property to get.</param>
        /// <returns>The value of the property.</returns>
        String this[String propertyName] { get; }

        /// <summary>
        /// Gets the number of properties in the declaration.
        /// </summary>
        [DomName("length")]
        Int32 Length { get; }

        /// <summary>
        /// Gets the value of a property with the given name.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property to get the value of.
        /// </param>
        /// <returns>A string or null if nothing has been set.</returns>
        [DomName("getPropertyValue")]
        String GetPropertyValue(String propertyName);

        /// <summary>
        /// Returns the optional priority, "important" or null, if no priority
        /// has been set.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property to get the priority of.
        /// </param>
        /// <returns>A priority or null.</returns>
        [DomName("getPropertyPriority")]
        String GetPropertyPriority(String propertyName);

        /// <summary>
        /// Sets a property with the given name and value. Optionally the
        /// priority can be set to "important" or left empty.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        /// <param name="propertyValue">The value of the property.</param>
        /// <param name="priority">The optional priority.</param>
        [DomName("setProperty")]
        void SetProperty(String propertyName, String propertyValue, String priority = null);

        /// <summary>
        /// Removes the property with the given name and returns its value.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property to be removed.
        /// </param>
        /// <returns>The value of the deleted property.</returns>
        [DomName("removeProperty")]
        String RemoveProperty(String propertyName);
    }
}
