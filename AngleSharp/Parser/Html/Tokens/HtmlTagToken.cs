namespace AngleSharp.Parser.Html
{
    using System;
    using System.Collections.Generic;

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
        public HtmlTagToken()
        {
            _name = String.Empty;
            _attributes = new List<KeyValuePair<String, String>>();
        }

        /// <summary>
        /// Creates a new HTML TagToken with the defined name.
        /// </summary>
        /// <param name="name">The name of the tag.</param>
        public HtmlTagToken(String name)
        {
            _name = name;
            _attributes = new List<KeyValuePair<String, String>>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the token can be used with IsHtmlTIP properties.
        /// </summary>
        public override Boolean IsHtmlCompatible
        {
            get { return _type == HtmlTokenType.StartTag; }
        }

        /// <summary>
        /// Gets if the token can be used with IsHtmlTIP properties.
        /// </summary>
        public override Boolean IsMathCompatible
        {
            get { return !IsStartTag("mglyph") && !IsStartTag("malignmark"); }
        }

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
            Attributes.Add(new KeyValuePair<String, String>(name, String.Empty));
        }

        /// <summary>
        /// Adds a new attribute to the list of attributes.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        public void AddAttribute(String name, String value)
        {
            Attributes.Add(new KeyValuePair<String, String>(name, value));
        }

        /// <summary>
        /// Sets the value of the last added attribute.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetAttributeValue(String value)
        {
            Attributes[Attributes.Count - 1] = new KeyValuePair<String, String>(Attributes[Attributes.Count - 1].Key, value);
        }

        /// <summary>
        /// Gets the value of the attribute with the given name or an empty
        /// string if the attribute is not available.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>The value of the attribute.</returns>
        public String GetAttribute(String name)
        {
            for (var i = 0; i != Attributes.Count; i++)
                if (Attributes[i].Key == name)
                    return Attributes[i].Value;

            return String.Empty;
        }

        #endregion
    }
}
