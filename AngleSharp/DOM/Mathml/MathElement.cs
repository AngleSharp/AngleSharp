using AngleSharp.DOM.Xml;
using System;

namespace AngleSharp.DOM.Mathml
{
    /// <summary>
    /// Represents an element of the MathML DOM.
    /// </summary>
    public class MathElement : Element
    {
        #region ctor

        /// <summary>
        /// Creates a new MathML element.
        /// </summary>
        internal MathElement()
        {
            _name = Tags.MATH;
            _ns = Namespaces.MathML;
        }

        #endregion

        #region Factory

        /// <summary>
        /// Returns a specialized MathMLElement instance for the given tag name.
        /// </summary>
        /// <param name="tagName">The given tag name.</param>
        /// <returns>The specialized MathMLElement instance.</returns>
        internal static MathElement Create(String tagName)
        {
            switch (tagName)
            {
                case Tags.MN:
                    return new MathNumberElement();

                case Tags.MO:
                    return new MathOperatorElement();

                case Tags.MI:
                    return new MathIdentifierElement();

                case Tags.MS:
                    return new MathStringElement();

                case Tags.MTEXT:
                    return new MathTextElement();

                case Tags.ANNOTATION_XML:
                    return new MathAnnotationXmlElement();

                default:
                    return new MathElement { _name = tagName };
            }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets the status if this node is the MathML namespace.
        /// </summary>
        internal protected override Boolean IsInMathML
        {
            get { return true; }
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
            var node = Create(_name);
            CopyProperties(this, node, deep);
            return node;
        }

        #endregion
    }
}
