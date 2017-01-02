﻿namespace AngleSharp.Xml.Parser.Tokens
{
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class for StartTagToken and EndTagToken.
    /// </summary>
    sealed class XmlTagToken : XmlToken
    {
        #region Fields
        
        private readonly List<KeyValuePair<String, String>> _attributes;
        private String _name;
        private Boolean _selfClosing;

        #endregion

        #region ctor

        /// <summary>
        /// Sets the default values.
        /// </summary>
        public XmlTagToken(XmlTokenType type, TextPosition position)
            : base(type, position)
        {
            _name = String.Empty;
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
                if (_attributes[i].Key.Is(name))
                {
                    return _attributes[i].Value;
                }
            }

            return String.Empty;
        }

        #endregion
    }
}
