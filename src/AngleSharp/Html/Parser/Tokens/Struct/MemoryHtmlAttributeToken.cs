namespace AngleSharp.Html.Parser.Tokens.Struct;

using Common;
using Text;

/// <summary>
/// The token representation of an HTML tag attribute.
/// </summary>
public readonly struct MemoryHtmlAttributeToken
{
    /// <summary>
    /// Creates a new attribute token using the provided information.
    /// </summary>
    /// <param name="position">The start position of the attribute's name.</param>
    /// <param name="name">The name of the attribute.</param>
    /// <param name="value">The value of the attribute.</param>
    public MemoryHtmlAttributeToken(TextPosition position, StringOrMemory name, StringOrMemory value)
    {
        Position = position;
        Name = name;
        Value = value;
    }

    /// <summary>
    /// Gets the attribute's name.
    /// </summary>
    public StringOrMemory Name { get; }

    /// <summary>
    /// Gets the attribute's value.
    /// </summary>
    public StringOrMemory Value { get; }

    /// <summary>
    /// Gets the position of the token.
    /// </summary>
    public TextPosition Position { get; }
}