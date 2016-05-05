namespace AngleSharp
{
    using System;
    using System.Collections.Generic;

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
        String Sheet(IEnumerable<IStyleFormattable> rules);

        /// <summary>
        /// Concats the given rules to create a block serialization.
        /// </summary>
        /// <param name="rules">The rules to aggregate.</param>
        /// <returns>The serialization of the CSS rule block.</returns>
        String Block(IEnumerable<IStyleFormattable> rules);

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
        /// Creates the serialization of the declarations with the provided
        /// string representations.
        /// </summary>
        /// <param name="declarations">The declarations to aggregate.</param>
        /// <returns>The serialization of the declarations.</returns>
        String Declarations(IEnumerable<String> declarations);

        /// <summary>
        /// Serializes a CSS medium with the provided properties.
        /// </summary>
        /// <param name="exclusive">Is the medium exclusive (only)?</param>
        /// <param name="inverse">Is the medium inverse (not)?</param>
        /// <param name="type">The type of the medium.</param>
        /// <param name="constraints">The constraints to use.</param>
        /// <returns>The serialization of the medium.</returns>
        String Medium(Boolean exclusive, Boolean inverse, String type, IEnumerable<IStyleFormattable> constraints);

        /// <summary>
        /// Creates the serialization of the constraint with the provided name
        /// and value, if any.
        /// </summary>
        /// <param name="name">The name of the constraint.</param>
        /// <param name="value">The optional value of the constraint.</param>
        /// <returns>The serialization of the constraint.</returns>
        String Constraint(String name, String value);

        /// <summary>
        /// Converts the name and value of the provided rule to a simple rule.
        /// </summary>
        /// <param name="name">The name of the simple rule.</param>
        /// <param name="value">The value of the simple rule.</param>
        /// <returns>The serialization of the simple rule.</returns>
        String Rule(String name, String value);

        /// <summary>
        /// Converts the name, prelude and rules of the provided rule to a
        /// composed rule.
        /// </summary>
        /// <param name="name">The name of the nested rule.</param>
        /// <param name="prelude">The optional prelude.</param>
        /// <param name="rules">The serialization of the nested rules.</param>
        /// <returns>The serialization of the nested rule.</returns>
        String Rule(String name, String prelude, String rules);

        /// <summary>
        /// Creates the serialized form of a style rule for the given selector
        /// with the provided rules.
        /// </summary>
        /// <param name="selector">The selector to use.</param>
        /// <param name="rules">The plain rules contained in the style.</param>
        /// <returns>The serialization of the style rule.</returns>
        String Style(String selector, IStyleFormattable rules);

        /// <summary>
        /// Creates a serialization of a comment with the provided data.
        /// </summary>
        /// <param name="data">The data of the comment.</param>
        /// <returns>The serialization of the comment.</returns>
        String Comment(String data);
    }
}
