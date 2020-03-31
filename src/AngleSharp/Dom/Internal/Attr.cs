namespace AngleSharp.Dom
{
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a generic node attribute.
    /// </summary>
    public sealed class Attr : IAttr
    {
        #region Fields

        private readonly String _localName;
        private readonly String _prefix;
        private readonly String _namespace;
        private String _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new attribute with the given local name.
        /// </summary>
        /// <param name="localName">The local name of the attribute.</param>
        public Attr(String localName)
            : this(localName, String.Empty)
        {
        }

        /// <summary>
        /// Creates a new attribute with the given local name and value.
        /// </summary>
        /// <param name="localName">The local name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        public Attr(String localName, String value)
        {
            _localName = localName;
            _value = value;
        }

        /// <summary>
        /// Creates a new attribute with the given properties.
        /// </summary>
        /// <param name="prefix">The prefix of the attribute.</param>
        /// <param name="localName">The local name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="namespaceUri">The namespace of the attribute.</param>
        public Attr(String prefix, String localName, String value, String namespaceUri)
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
            get;
            set;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the attribute's prefix.
        /// </summary>
        public String Prefix => _prefix;

        /// <summary>
        /// Gets if the attribute is an id attribute.
        /// </summary>
        public Boolean IsId => _prefix == null && _localName.Isi(AttributeNames.Id);

        /// <summary>
        /// Gets if the value is given or not.
        /// </summary>
        public Boolean Specified => !String.IsNullOrEmpty(_value);

        /// <summary>
        /// Gets the attribute's fully qualified name.
        /// </summary>
        public String Name => _prefix == null ? _localName : String.Concat(_prefix, ":", _localName);

        /// <summary>
        /// Gets the attribute's value.
        /// </summary>
        public String Value
        {
            get => _value;
            set
            {
                var oldValue = _value;
                _value = value;
                Container?.RaiseChangedEvent(this, value, oldValue);
            }
        }

        /// <summary>
        /// Gets the attribute's local name.
        /// </summary>
        public String LocalName => _localName;

        /// <summary>
        /// Gets the attribute's namespace.
        /// </summary>
        public String NamespaceUri => _namespace;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the attribute equals another attribute.
        /// </summary>
        /// <param name="other">The other attribute.</param>
        /// <returns>True if both are equivalent, otherwise false.</returns>
        public Boolean Equals(IAttr other)
        {
            return Prefix.Is(other.Prefix) && NamespaceUri.Is(other.NamespaceUri) && Value.Is(other.Value);
        }

        /// <summary>
        /// Computes the hash code of the attribute.
        /// </summary>
        /// <returns>The computed hash code.</returns>
        public override Int32 GetHashCode()
        {
            const int prime = 31;
            var result = 1;

            result = result * prime + _localName.GetHashCode();
            result = result * prime + (_value ?? String.Empty).GetHashCode();
            result = result * prime + (_namespace ?? String.Empty).GetHashCode();
            result = result * prime + (_prefix ?? String.Empty).GetHashCode();

            return result;
        }

        #endregion
    }
}