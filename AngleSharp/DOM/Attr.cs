namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// Represents a generic node attribute.
    /// </summary>
    sealed class Attr : IAttr, IEquatable<IAttr>
    {
        #region Fields

        readonly String _name;
        readonly String _ns;
        readonly Action<String> _changed;
        String _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new NodeAttribute with empty value.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="changed">The handler to call on changed.</param>
        internal Attr(String name, Action<String> changed)
            : this(name, String.Empty, null, changed)
        {
        }

        /// <summary>
        /// Creates a new NodeAttribute.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="changed">The handler to call on changed.</param>
        internal Attr(String name, String value, Action<String> changed)
            : this(name, value, null, changed)
        {
        }

        /// <summary>
        /// Creates a new NodeAttribute.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="ns">The namespace of the attribute.</param>
        /// <param name="changed">The handler to call on changed.</param>
        internal Attr(String name, String value, String ns, Action<String> changed)
        {
            _name = name;
            _value = value;
            _ns = ns ?? String.Empty;
            _changed = changed ?? (_ => { });
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
                var index = _name.IndexOf(':');

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
            set { _value = value; _changed(_name); }
        }

        /// <summary>
        /// Gets the local name of the attribute.
        /// </summary>
        public String LocalName
        {
            get 
            {
                var index = _name.IndexOf(':'); 
                
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
            if (other == this)
                return true;

            return _value == other.Value && _name == other.Name;
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns an HTML-code representation of the attribute.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToString()
        {
            if (_value.IndexOf(Specification.DoubleQuote) >= 0)
                _value = _value.Replace(Specification.DoubleQuote.ToString(), "&quot;");

            return String.Format("{0}={2}{1}{2}", _name, _value, Specification.DoubleQuote);
        }

        #endregion
    }
}
