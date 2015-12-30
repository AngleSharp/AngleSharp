namespace AngleSharp.Dom
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a generic node attribute.
    /// </summary>
    [DebuggerStepThrough]
    sealed class Attr : IAttr
    {
        #region Fields

        readonly String _localName;
        readonly String _prefix;
        readonly String _namespace;
        String _value;
        NamedNodeMap _container;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new NodeAttribute with empty value.
        /// </summary>
        /// <param name="localName">The name of the attribute.</param>
        internal Attr(String localName)
            : this(localName, String.Empty)
        {
        }

        /// <summary>
        /// Creates a new NodeAttribute.
        /// </summary>
        /// <param name="localName">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        internal Attr(String localName, String value)
        {
            _localName = localName;
            _value = value;
        }

        /// <summary>
        /// Creates a new NodeAttribute.
        /// </summary>
        /// <param name="prefix">The prefix of the attribute.</param>
        /// <param name="localName">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="namespaceUri">The namespace of the attribute.</param>
        internal Attr(String prefix, String localName, String value, String namespaceUri)
        {
            _prefix = prefix;
            _localName = localName;
            _value = value;
            _namespace = namespaceUri;
        }

        #endregion

        #region Internal Properties

        internal NamedNodeMap Container
        {
            get { return _container; }
            set { _container = value; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the namespace prefix of the specified node, or null if no
        /// prefix is specified.
        /// </summary>
        public String Prefix
        {
            get { return _prefix; }
        }

        /// <summary>
        /// Gets whether the attribute is an ID attribute.
        /// </summary>
        public Boolean IsId
        {
            get { return _prefix == null && String.Equals(_localName, AttributeNames.Id, StringComparison.OrdinalIgnoreCase); }
        }

        /// <summary>
        /// Gets if this attribute was explicitly given a value in the
        /// document.
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
            get { return _prefix == null ? _localName : String.Concat(_prefix, ":", _localName); }
        }

        /// <summary>
        /// Gets or sets the value of the attribute.
        /// </summary>
        public String Value
        {
            get { return _value; }
            set 
            { 
                var oldValue = _value;
                _value = value;

                if (_container != null)
                    _container.RaiseChangedEvent(this, value, oldValue);
            }
        }

        /// <summary>
        /// Gets the local name of the attribute.
        /// </summary>
        public String LocalName
        {
            get { return _localName; }
        }

        /// <summary>
        /// Gets the namespace URI of the attribute.
        /// </summary>
        public String NamespaceUri
        {
            get { return _namespace; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compares the given attribute to the current one.
        /// </summary>
        /// <param name="other">The attibute to compare to.</param>
        /// <returns>
        /// True if both attributes are equal, otherwise false.
        /// </returns>
        public Boolean Equals(IAttr other)
        {
            return Prefix.Is(other.Prefix) && NamespaceUri.Is(other.NamespaceUri) && Value.Is(other.Value);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>The hash code for the current attribute.</returns>
        public override Int32 GetHashCode()
        {
            const int prime = 31;
            var result = 1;

            result = result * prime + _localName.GetHashCode();
            result = result * prime + _value.GetHashCode();
            result = result * prime + (_namespace ?? "").GetHashCode();
            result = result * prime + (_prefix ?? "").GetHashCode();

            return result;
        }

        #endregion
    }
}