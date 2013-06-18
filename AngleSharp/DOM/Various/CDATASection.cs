using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents a CDATA section node.
    /// </summary>
    [DOM("CDATASection")]
    public sealed class CDATASection : CharacterData
    {
        #region ctor

        /// <summary>
        /// Creates a new CDATA Section node.
        /// </summary>
        internal CDATASection()
        {
            _name = "#cdata-section";
            _type = NodeType.CData;
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
            var node = new CDATASection();
            CopyProperties(this, node, deep);
            node.Data = this.Data;
            return node;
        }

        #endregion
    }
}
