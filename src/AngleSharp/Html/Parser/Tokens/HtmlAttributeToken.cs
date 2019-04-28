namespace AngleSharp.Html.Parser.Tokens
{
    using AngleSharp.Text;
    using System;

    public struct HtmlAttributeToken
    {
        public HtmlAttributeToken(TextPosition position, String name, String value)
        {
            Position = position;
            Name = name;
            Value = value;
        }

        public String Name { get; }

        public String Value { get; }

        /// <summary>
        /// Gets the position of the token.
        /// </summary>
        public TextPosition Position { get; }
    }
}
