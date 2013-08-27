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

        /// <summary>
        /// Gets the status if the node is an HTML text integration point.
        /// </summary>
        protected internal override Boolean IsHtmlTIP
        {
            get
            {
                if (NodeName.Equals(Specification.XML_ANNOTATION))
                {
                    var value = GetAttribute("encoding");

                    if (!String.IsNullOrEmpty(value))
                    {
                        value = value.ToLower();
                        return value.Equals(MimeTypes.Html) || value.Equals(MimeTypes.ApplicationXHtml);
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return _name == Specification.XML_ANNOTATION; }
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
