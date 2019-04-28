namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents an entity node.
    /// </summary>
    [DomName("Entity")]
    public sealed class Entity : Node
    {
        #region Fields

        private String _publicId;
        private String _systemId;
        private String _notationName;
        private String _inputEncoding;
        private String _xmlVersion;
        private String _xmlEncoding;
        private String _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new entity node.
        /// </summary>
        public Entity(Document owner)
            : this(owner, String.Empty)
        {
        }

        /// <summary>
        /// Creates a new entity node.
        /// </summary>
        public Entity(Document owner, String name)
            : base(owner, name, NodeType.Entity)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the public identiifer.
        /// </summary>
        [DomName("publicId")]
        public String PublicId => _publicId;

        /// <summary>
        /// Gets the system identifier.
        /// </summary>
        [DomName("systemId")]
        public String SystemId => _systemId;

        /// <summary>
        /// Gets or sets the notation name.
        /// </summary>
        [DomName("notationName")]
        public String NotationName
        {
            get => _notationName;
            set => _notationName = value;
        }

        /// <summary>
        /// Gets the used input encoding.
        /// </summary>
        [DomName("inputEncoding")]
        public String InputEncoding => _inputEncoding;

        /// <summary>
        /// Gets the used XML encoding.
        /// </summary>
        [DomName("xmlEncoding")]
        public String XmlEncoding => _xmlEncoding;

        /// <summary>
        /// Gets the used XML version.
        /// </summary>
        [DomName("xmlVersion")]
        public String XmlVersion => _xmlVersion;

        /// <summary>
        /// Gets or sets the entity's value.
        /// </summary>
        [DomName("textContent")]
        public override String TextContent
        {
            get => NodeValue;
            set => NodeValue = value;
        }

        /// <summary>
        /// Gets or sets the value of the entity.
        /// </summary>
        [DomName("nodeValue")]
        public override String NodeValue
        {
            get => _value;
            set => _value = value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        public override Node Clone(Document newOwner, Boolean deep)
        {
            var node = new Entity(newOwner, NodeName);
            CloneNode(node, newOwner, deep);
            node._xmlEncoding = this._xmlEncoding;
            node._xmlVersion = this._xmlVersion;
            node._systemId = this._systemId;
            node._publicId = this._publicId;
            node._inputEncoding = this._inputEncoding;
            node._notationName = this._notationName;
            return node;
        }

        #endregion
    }
}
