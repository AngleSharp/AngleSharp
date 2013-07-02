using System;
using System.Collections.Generic;

namespace AngleSharp.Html
{
    /// <summary>
    /// Class for StartTagToken and EndTagToken.
    /// </summary>
    sealed class HtmlTagToken : HtmlToken
    {
        #region Members

        Boolean _selfClosing;
        List<KeyValuePair<String, String>> _attributes;
        String _name;
        
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
        /// Gets or sets the state of the self-closing flag.
        /// </summary>
        public Boolean IsSelfClosing
        {
            get { return _selfClosing; }
            set { _selfClosing = value; }
        }

        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        public String Name
        {
            get { return _name; }
            set { _name = value; }
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
        /// Finds out if the current token is a start tag token with the given name.
        /// </summary>
        /// <param name="name">The name of the tag.</param>
        /// <returns>True if the token is indeed a start tag token with the given name, otherwise false.</returns>
        public override Boolean IsStartTag(String name)
        {
            if (_type == HtmlTokenType.StartTag)
                return _name.Equals(name);

            return false;
        }

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
