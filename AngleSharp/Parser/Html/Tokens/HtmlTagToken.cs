namespace AngleSharp.Parser.Html
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Class for StartTagToken and EndTagToken.
    /// </summary>
    sealed class HtmlTagToken : HtmlToken
    {
        #region Fields

        readonly List<KeyValuePair<String, String>> _attributes;

        Boolean _selfClosing;

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
            _attributes = new List<KeyValuePair<String, String>>();
        }

        #endregion

        #region Creators

        /// <summary>
        /// Creates a new opening HtmlTagToken for the given name.
        /// </summary>
        /// <param name="name">The name of the tag.</param>
        /// <returns>The new HTML tag token.</returns>
        [DebuggerStepThrough]
        public static HtmlTagToken Open(String name)
        {
            return new HtmlTagToken(HtmlTokenType.StartTag, TextPosition.Empty, name);
        }

        /// <summary>
        /// Creates a new closing HtmlTagToken for the given name.
        /// </summary>
        /// <param name="name">The name of the tag.</param>
        /// <returns>The new HTML tag token.</returns>
        [DebuggerStepThrough]
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
            get { return _selfClosing; }
            set { _selfClosing = value; }
        }

        /// <summary>
        /// Gets the list of attributes.
        /// </summary>
        public List<KeyValuePair<String, String>> Attributes
        {
            get { return _attributes; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a new attribute to the list of attributes. The value will
        /// be set to an empty string.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        public void AddAttribute(String name)
        {
            _attributes.Add(new KeyValuePair<String, String>(name, String.Empty));
        }

        /// <summary>
        /// Adds a new attribute to the list of attributes.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        public void AddAttribute(String name, String value)
        {
            _attributes.Add(new KeyValuePair<String, String>(name, value));
        }

        /// <summary>
        /// Sets the value of the last added attribute.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetAttributeValue(String value)
        {
            _attributes[_attributes.Count - 1] = new KeyValuePair<String, String>(_attributes[_attributes.Count - 1].Key, value);
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
                if (_attributes[i].Key == name)
                    return _attributes[i].Value;
            }

            return String.Empty;
        }

        #endregion
    }
}
