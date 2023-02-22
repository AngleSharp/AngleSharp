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

        private String? _value;

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
        public String? PublicId { get; private set; }

        /// <summary>
        /// Gets the system identifier.
        /// </summary>
        [DomName("systemId")]
        public String? SystemId { get; private set; }

        /// <summary>
        /// Gets or sets the notation name.
        /// </summary>
        [DomName("notationName")]
        public String? NotationName { get; set; }

        /// <summary>
        /// Gets the used input encoding.
        /// </summary>
        [DomName("inputEncoding")]
        public String? InputEncoding { get; private set; }

        /// <summary>
        /// Gets the used XML encoding.
        /// </summary>
        [DomName("xmlEncoding")]
        public String? XmlEncoding { get; private set; }

        /// <summary>
        /// Gets the used XML version.
        /// </summary>
        [DomName("xmlVersion")]
        public String? XmlVersion { get; private set; }

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
            get => _value!;
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
            node.XmlEncoding = this.XmlEncoding;
            node.XmlVersion = this.XmlVersion;
            node.SystemId = this.SystemId;
            node.PublicId = this.PublicId;
            node.InputEncoding = this.InputEncoding;
            node.NotationName = this.NotationName;
            return node;
        }

        #endregion
    }
}
