namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Collections;
    using System;

    /// <summary>
    /// Represents a generic node attribute.
    /// </summary>
    sealed class Attr : IAttr, IEquatable<IAttr>
    {
        #region Fields

        readonly AttrContainer _container;
        readonly String _name;
        readonly String _ns;
        String _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new NodeAttribute with empty value.
        /// </summary>
        /// <param name="container">The parent of the attribute.</param>
        /// <param name="name">The name of the attribute.</param>
        internal Attr(AttrContainer container, String name)
            : this(container, name, String.Empty, null)
        {
        }

        /// <summary>
        /// Creates a new NodeAttribute.
        /// </summary>
        /// <param name="container">The parent of the attribute.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        internal Attr(AttrContainer container, String name, String value)
            : this(container, name, value, null)
        {
        }

        /// <summary>
        /// Creates a new NodeAttribute.
        /// </summary>
        /// <param name="container">The parent of the attribute.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="ns">The namespace of the attribute.</param>
        internal Attr(AttrContainer container, String name, String value, String ns)
        {
            _container = container;
            _name = name;
            _value = value;
            _ns = ns ?? String.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the namespace prefix of the specified node, or null if no prefix is specified.
        /// </summary>
        public String Prefix
        {
            get
            {
                var index = _name.IndexOf(Specification.Colon);

                if (index == -1)
                    return String.Empty;

                return _name.Substring(0, index);
            }
        }

        /// <summary>
        /// Gets whether the attribute is an ID attribute.
        /// </summary>
        public Boolean IsId
        {
            get { return _name.Equals(AttributeNames.Id, StringComparison.OrdinalIgnoreCase); }
        }

        /// <summary>
        /// Gets if this attribute was explicitly given a value in the document.
        /// </summary>
        public Boolean Specified
        {
            get { return !String.IsNullOrEmpty(_value); }
        }

        /// <summary>
        /// Gets the name of the attribute.
        /// </summary>
        public String Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets or sets the value of the attribute.
        /// </summary>
        public String Value
        {
            get { return _value; }
            set { _value = value; _container.RaiseChanged(_name, _value); }
        }

        /// <summary>
        /// Gets the local name of the attribute.
        /// </summary>
        public String LocalName
        {
            get 
            {
                var index = _name.IndexOf(Specification.Colon); 
                
                if (index == -1)
                    return _name;

                return _name.Substring(index + 1);
            }
        }

        /// <summary>
        /// Gets the namespace URI of the attribute.
        /// </summary>
        public String NamespaceUri
        {
            get { return _ns; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compares the given attribute to the current one.
        /// </summary>
        /// <param name="other">The attibute to compare to.</param>
        /// <returns>True if both attributes are equal, otherwise false.</returns>
        public Boolean Equals(IAttr other)
        {
            return other == this || (_value == other.Value && _name == other.Name);
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns an HTML-code representation of the attribute.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToString()
        {
            var temp = Pool.NewStringBuilder();

            if (String.IsNullOrEmpty(_ns))
                temp.Append(LocalName);
            else if (_ns == Namespaces.XmlUri)
                temp.Append(Namespaces.XmlPrefix).Append(Specification.Colon).Append(LocalName);
            else if (_ns == Namespaces.XLinkUri)
                temp.Append(Namespaces.XLinkPrefix).Append(Specification.Colon).Append(LocalName);
            else if (_ns == Namespaces.XmlNsUri)
                temp.Append(XmlNamespaceLocalName());
            else
                temp.Append(_name);

            temp.Append(Specification.Equality).Append(Specification.DoubleQuote);

            for (int i = 0; i < _value.Length; i++)
            {
                switch (_value[i])
                {
                    case Specification.Ampersand: temp.Append("&amp;"); break;
                    case Specification.NoBreakSpace: temp.Append("&nbsp;"); break;
                    case Specification.DoubleQuote: temp.Append("&quot;"); break;
                    default: temp.Append(_value[i]); break;
                }
            }

            return temp.Append(Specification.DoubleQuote).ToPool();
        }

        String XmlNamespaceLocalName()
        {
            if (LocalName != Namespaces.XmlNsPrefix)
                return String.Concat(Namespaces.XmlNsPrefix, ":");

            return LocalName;
        }

        #endregion
    }
}
;