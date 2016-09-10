namespace AngleSharp.Dom
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents a generic node attribute.
    /// </summary>
    sealed class Attr : IAttr
    {
        #region Fields

        private readonly String _localName;
        private readonly String _prefix;
        private readonly String _namespace;
        private String _value;

        #endregion

        #region ctor

        internal Attr(String localName)
            : this(localName, String.Empty)
        {
        }

        internal Attr(String localName, String value)
        {
            _localName = localName;
            _value = value;
        }

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
            get;
            set;
        }

        #endregion

        #region Properties

        public String Prefix
        {
            get { return _prefix; }
        }

        public Boolean IsId
        {
            get { return _prefix == null && _localName.Isi(AttributeNames.Id); }
        }

        public Boolean Specified
        {
            get { return !String.IsNullOrEmpty(_value); }
        }

        public String Name
        {
            get { return _prefix == null ? _localName : String.Concat(_prefix, ":", _localName); }
        }

        public String Value
        {
            get { return _value; }
            set 
            { 
                var oldValue = _value;
                _value = value;
                Container?.RaiseChangedEvent(this, value, oldValue);
            }
        }

        public String LocalName
        {
            get { return _localName; }
        }

        public String NamespaceUri
        {
            get { return _namespace; }
        }

        #endregion

        #region Methods

        public Boolean Equals(IAttr other)
        {
            return Prefix.Is(other.Prefix) && NamespaceUri.Is(other.NamespaceUri) && Value.Is(other.Value);
        }

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