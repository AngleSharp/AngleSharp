using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents a node that contains a comment.
    /// </summary>
    [DOM("Comment")]
    public sealed class Comment : CharacterData
    {
        #region ctor

        /// <summary>
        /// Creates a new comment node.
        /// </summary>
        internal Comment()
        {
            _type = NodeType.Comment;
            _name = "#comment";
        }

        /// <summary>
        /// Creates a new comment node with the given data.
        /// </summary>
        /// <param name="data">The data to be initially set.</param>
        internal Comment(string data)
            : this()
        {
            AppendData(data);
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
            var node = new Comment(Data);
            CopyProperties(this, node, deep);
            return node;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns an HTML representation of the comment node.
        /// </summary>
        /// <returns>A string containing the HTML content.</returns>
        public override String ToHtml()
        {
            return "<!--" + Data + "-->";
        }

        /// <summary>
        /// Returns a string representing the comment.
        /// </summary>
        /// <returns>A string containing the content.</returns>
        public override String ToString()
        {
            return "//" + Data;
        }

        #endregion
    }
}
