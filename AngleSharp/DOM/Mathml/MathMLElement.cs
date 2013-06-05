using System;

namespace AngleSharp.DOM.Mathml
{
    /// <summary>
    /// Represents an element of the MathML DOM.
    /// </summary>
    public class MathMLElement : Element
    {
        #region ctor

        /// <summary>
        /// Creates a new MathML element.
        /// </summary>
        public MathMLElement()
        {
            NamespaceURI = Namespaces.MathML;
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets the status if this node is the MathML namespace.
        /// </summary>
        internal protected override bool IsInMathML
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the status if the node is a MathML text integration point.
        /// </summary>
        protected internal override bool IsMathMLTIP
        {
            get
            {
                var name = NodeName;
                return name == "mo" || name == "mi" || name == "mn" || name == "ms" || name == "mtext";
            }
        }

        /// <summary>
        /// Gets the status if the node is an HTML text integration point.
        /// </summary>
        protected internal override bool IsHtmlTIP
        {
            get
            {
                var name = NodeName;

                if (name == "annotation-xml")
                {
                    var value = GetAttribute("encoding");

                    if (value != null)
                    {
                        value = value.ToLower();
                        return value == "text/html" || value == "application/xhtml+xml";
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get
            {
                var name = NodeName;
                return name == "mo" || name == "mi" || name == "mn" || name == "ms" || name == "mtext" || name == "annotation-xml";
            }
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
            var node = Factory(_name);
            CopyProperties(this, node, deep);
            return node;
        }

        /// <summary>
        /// Returns a specialized MathMLElement instance for the given tag name.
        /// </summary>
        /// <param name="tag">The given tag name.</param>
        /// <returns>The specialized MathMLElement instance.</returns>
        public static MathMLElement Factory(string tagName)
        {
            return new MathMLElement { _name = tagName };
        }

        #endregion
    }
}
