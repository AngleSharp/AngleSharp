namespace AngleSharp.Html.Parser.Tokens
{
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// The token representation of an HTML tag attribute.
    /// </summary>
    public struct HtmlAttributeToken
    {
        /// <summary>
        /// Creates a new attribute token using the provided information.
        /// </summary>
        /// <param name="position">The start position of the attribute's name.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        public HtmlAttributeToken(TextPosition position, String name, String value)
        {
            Position = position;
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Gets the attribute's name.
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Gets the attribute's value.
        /// </summary>
        public String Value { get; }

        /// <summary>
        /// Gets the position of the token.
        /// </summary>
        public TextPosition Position { get; }
    }
}
