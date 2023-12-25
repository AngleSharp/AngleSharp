namespace AngleSharp.Html.Parser.Tokens
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class for StartTagToken and EndTagToken.
    /// </summary>
    public sealed class HtmlTagToken : HtmlToken, ISourceReference
    {
        #region Fields

        private readonly List<HtmlAttributeToken> _attributes = new List<HtmlAttributeToken>();
        private Boolean _selfClosing;

        #endregion

        #region ctor

        /// <summary>
        /// Sets the default values.
        /// </summary>
        /// <param name="type">The type of the tag token.</param>
        /// <param name="position">The token's position.</param>
        public HtmlTagToken(HtmlTokenType type, TextPosition position)
            : this(type, position, String.Empty)
        {
        }

        /// <summary>
        /// Creates a new HTML TagToken with the defined name.
        /// </summary>
        /// <param name="type">The type of the tag token.</param>
        /// <param name="position">The token's position.</param>
        /// <param name="name">The name of the tag.</param>
        public HtmlTagToken(HtmlTokenType type, TextPosition position, String name)
            : base(type, position, name)
        {
        }

        #endregion

        #region Creators

        /// <summary>
        /// Creates a new opening HtmlTagToken for the given name.
        /// </summary>
        /// <param name="name">The name of the tag.</param>
        /// <returns>The new HTML tag token.</returns>
        public static HtmlTagToken Open(String name)
        {
            return new HtmlTagToken(HtmlTokenType.StartTag, TextPosition.Empty, name);
        }

        /// <summary>
        /// Creates a new closing HtmlTagToken for the given name.
        /// </summary>
        /// <param name="name">The name of the tag.</param>
        /// <returns>The new HTML tag token.</returns>
        public static HtmlTagToken Close(String name)
        {
            return new HtmlTagToken(HtmlTokenType.EndTag, TextPosition.Empty, name);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the state of the self-closing flag.
        /// </summary>
        public Boolean IsSelfClosing
        {
            get => _selfClosing;
            set => _selfClosing = value;
        }

        /// <summary>
        /// Gets the list of attributes.
        /// </summary>
        public List<HtmlAttributeToken> Attributes => _attributes;

        #endregion

        #region Methods

        /// <summary>
        /// Adds a new attribute to the list of attributes. The value will
        /// be set to an empty string.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="position">The starting position of the attribute.</param>
        public void AddAttribute(String name, TextPosition position)
        {
            _attributes.Add(new HtmlAttributeToken(position, name, String.Empty));
        }

        /// <summary>
        /// Adds a new attribute to the list of attributes.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        public void AddAttribute(String name, String value)
        {
            _attributes.Add(new HtmlAttributeToken(TextPosition.Empty, name, value));
        }

        /// <summary>
        /// Sets the value of the last added attribute.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetAttributeValue(String value)
        {
            var last = _attributes.Count - 1;
            var attr = _attributes[last];
            _attributes[last] = new HtmlAttributeToken(attr.Position, attr.Name, value);
        }

        /// <summary>
        /// Gets the value of the attribute with the given name or an empty
        /// string if the attribute is not available.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>The value of the attribute.</returns>
        public String GetAttribute(String name)
        {
            for (var i = 0; i != _attributes.Count; i++)
            {
                var attr = _attributes[i];

                if (attr.Name.Is(name))
                {
                    return attr.Value;
                }
            }

            return String.Empty;
        }

        #endregion
    }
}
