using System;
using System.Collections.Generic;

namespace AngleSharp.Html
{
    /// <summary>
    /// Abstract base class for StartTagToken and EndTagToken.
    /// </summary>
    class HtmlTagToken : HtmlToken
    {
        #region Members

        bool selfClosing;
        List<KeyValuePair<string, string>> attributes;
        string _name;
        
        #endregion

        #region ctor

        /// <summary>
        /// Sets the default values.
        /// </summary>
        private HtmlTagToken()
        {
            attributes = new List<KeyValuePair<string, string>>();
        }

        #endregion

        #region Static Properties (Constructors)

        /// <summary>
        /// Gets a new HtmlTagToken with Type being a StartTag.
        /// </summary>
        public static HtmlTagToken Open
        {
            get 
            {
                return new HtmlTagToken() { _type = HtmlTokenType.StartTag };
            }
        }

        /// <summary>
        /// Gets a new HtmlTagToken with Type being an EndTag.
        /// </summary>
        public static HtmlTagToken Close
        {
            get
            {
                return new HtmlTagToken() { _type = HtmlTokenType.EndTag };
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the state of the self-closing flag.
        /// </summary>
        public bool IsSelfClosing
        {
            get { return selfClosing; }
            set { selfClosing = value; }
        }

        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets the list of attributes.
        /// </summary>
        public List<KeyValuePair<string, string>> Attributes
        {
            get { return attributes; }
        }

        #endregion

        #region Methods

        public HtmlTagToken SetName(string name)
        {
            _name = name;
            return this;
        }

        /// <summary>
        /// Adds a new attribute to the list of attributes. The value will
        /// be set to an empty string.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        public void AddAttribute(string name)
        {
            Attributes.Add(new KeyValuePair<string, string>(name, string.Empty));
        }

        /// <summary>
        /// Adds a new attribute to the list of attributes.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        public void AddAttribute(string name, string value)
        {
            Attributes.Add(new KeyValuePair<string, string>(name, value));
        }

        /// <summary>
        /// Sets the value of the last added attribute.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetAttributeValue(string value)
        {
            Attributes[Attributes.Count - 1] = new KeyValuePair<string,string>(Attributes[Attributes.Count - 1].Key, value);
        }

        /// <summary>
        /// Gets the value of the attribute with the given name or an empty
        /// string if the attribute is not available.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>The value of the attribute.</returns>
        public string GetAttribute(string name)
        {
            for (var i = 0; i != Attributes.Count; i++)
                if (Attributes[i].Key == name)
                    return Attributes[i].Value;

            return string.Empty;
        }

        #endregion
    }
}
