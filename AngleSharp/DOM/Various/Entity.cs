using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents an entity node.
    /// </summary>
    [DOM("Entity")]
    public sealed class Entity : Node
    {
        #region Members

        String _publicId;
        String _systemId;
        String _notationName;
        String _inputEncoding;
        String _xmlVersion;
        String _xmlEncoding;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new entity node.
        /// </summary>
        internal Entity()
        {
            _type = NodeType.Entity;
        }

        /// <summary>
        /// Creates a new entity node.
        /// </summary>
        /// <param name="name">Name of the entity.</param>
        internal Entity(String name)
            : this()
        {
            _name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the public identiifer.
        /// </summary>
        [DOM("publicId")]
        public String PublicId
        {
            get { return _publicId; }
        }

        /// <summary>
        /// Gets the system identifier.
        /// </summary>
        [DOM("systemId")]
        public String SystemId
        {
            get { return _systemId; }
        }

        /// <summary>
        /// Gets the notation name.
        /// </summary>
        [DOM("notationName")]
        public String NotationName
        {
            get { return _notationName; }
        }

        /// <summary>
        /// Gets the used input encoding.
        /// </summary>
        [DOM("inputEncoding")]
        public String InputEncoding
        {
            get { return _inputEncoding; }
        }

        /// <summary>
        /// Gets the used XML encoding.
        /// </summary>
        [DOM("xmlEncoding")]
        public String XmlEncoding
        {
            get { return _xmlEncoding; }
        }

        /// <summary>
        /// Gets the used XML version.
        /// </summary>
        [DOM("xmlVersion")]
        public String XmlVersion
        {
            get { return _xmlVersion; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        [DOM("cloneNode")]
        public override Node CloneNode(Boolean deep = true)
        {
            var node = new Entity();
            CopyProperties(this, node, deep);
            node._xmlEncoding = this._xmlEncoding;
            node._xmlVersion = this._xmlVersion;
            node._systemId = this._systemId;
            node._publicId = this._publicId;
            node._inputEncoding = this._inputEncoding;
            node._notationName = this._notationName;
            return node;
        }

        /// <summary>
        /// Returns the prefix for a given namespaceURI if present, and null if not. When multiple prefixes are possible,
        /// the result is implementation-dependent.
        /// </summary>
        /// <param name="namespaceURI">The namespaceURI to lookup.</param>
        /// <returns>The prefix.</returns>
        [DOM("lookupPrefix")]
        public override String LookupPrefix(String namespaceURI)
        {
            return null;
        }

        /// <summary>
        /// Accepts a namespace URI as an argument and returns true if the namespace is the default namespace on the given node or false if not.
        /// </summary>
        /// <param name="namespaceURI">A string representing the namespace against which the element will be checked.</param>
        /// <returns>True if the given namespaceURI is the default namespace.</returns>
        [DOM("isDefaultNamespace")]
        public override Boolean IsDefaultNamespace(String namespaceURI)
        {
            return false;
        }

        /// <summary>
        /// Takes a prefix and returns the namespaceURI associated with it on the given node if found (and null if not).
        /// Supplying null for the prefix will return the default namespace.
        /// </summary>
        /// <param name="prefix">The prefix to look for.</param>
        /// <returns>The namespace URI.</returns>
        [DOM("lookupNamespaceURI")]
        public override String LookupNamespaceURI(String prefix)
        {
            return null;
        }

        #endregion
    }
}
