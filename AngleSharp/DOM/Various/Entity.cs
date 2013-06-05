using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents an entity node.
    /// </summary>
    public sealed class Entity : Node
    {
        #region Members

        string _publicId;
        string _systemId;
        string _notationName;
        string _inputEncoding;
        string _xmlVersion;
        string _xmlEncoding;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new entity node.
        /// </summary>
        public Entity()
        {
            NodeType = NodeType.Entity;
        }

        /// <summary>
        /// Creates a new entity node.
        /// </summary>
        /// <param name="name">Name of the entity.</param>
        public Entity(string name)
            : this()
        {
            _name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the public identiifer.
        /// </summary>
        public string PublicId
        {
            get { return _publicId; }
        }

        /// <summary>
        /// Gets the system identifier.
        /// </summary>
        public string SystemId
        {
            get { return _systemId; }
        }

        /// <summary>
        /// Gets the notation name.
        /// </summary>
        public string NotationName
        {
            get { return _notationName; }
        }

        /// <summary>
        /// Gets the used input encoding.
        /// </summary>
        public string InputEncoding
        {
            get { return _inputEncoding; }
        }

        /// <summary>
        /// Gets the used XML encoding.
        /// </summary>
        public string XmlEncoding
        {
            get { return _xmlEncoding; }
        }

        /// <summary>
        /// Gets the used XML version.
        /// </summary>
        public string XmlVersion
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
        public override Node CloneNode(bool deep = true)
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
        public override string LookupPrefix(string namespaceURI)
        {
            return null;
        }

        /// <summary>
        /// Accepts a namespace URI as an argument and returns true if the namespace is the default namespace on the given node or false if not.
        /// </summary>
        /// <param name="namespaceURI">A string representing the namespace against which the element will be checked.</param>
        /// <returns>True if the given namespaceURI is the default namespace.</returns>
        public override bool IsDefaultNamespace(string namespaceURI)
        {
            return false;
        }

        /// <summary>
        /// Takes a prefix and returns the namespaceURI associated with it on the given node if found (and null if not).
        /// Supplying null for the prefix will return the default namespace.
        /// </summary>
        /// <param name="prefix">The prefix to look for.</param>
        /// <returns>The namespace URI.</returns>
        public override string LookupNamespaceURI(string prefix)
        {
            return null;
        }

        #endregion
    }
}
