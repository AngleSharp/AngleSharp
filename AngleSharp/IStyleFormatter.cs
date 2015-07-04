namespace AngleSharp
{
    using System;

    /// <summary>
    /// Basic interface for CSS node serialization.
    /// </summary>
    public interface IStyleFormatter
    {
        /// <summary>
        /// Concats the given rules to create the stylesheet serialization.
        /// </summary>
        /// <param name="rules">The rules to aggregate.</param>
        /// <returns>The serialization of the sheet.</returns>
        String Sheet(String[] rules);

        /// <summary>
        /// Creates the serialization of a declaration with the given name,
        /// value and important flag.
        /// </summary>
        /// <param name="name">The name of the declaration.</param>
        /// <param name="value">The value of the declaration.</param>
        /// <param name="important">The value of the important flag.</param>
        /// <returns>The serialization of the declaration.</returns>
        String Declaration(String name, String value, Boolean important);

        /// <summary>
        /// Serializes a CSS medium with the provided properties.
        /// </summary>
        /// <param name="exclusive">Is the medium exclusive (only)?</param>
        /// <param name="inverse">Is the medium inverse (not)?</param>
        /// <param name="type">The type of the medium.</param>
        /// <param name="constraints">The constraints to use.</param>
        /// <returns>The serialization of the medium.</returns>
        String Medium(Boolean exclusive, Boolean inverse, String type, String[] constraints);

        /// <summary>
        /// Creates the serialization of the constraint with the provided name
        /// and value, if any.
        /// </summary>
        /// <param name="name">The name of the constraint.</param>
        /// <param name="value">The optional value of the constraint.</param>
        /// <returns>The serialization of the constraint.</returns>
        String Constraint(String name, String value);
    }
}
