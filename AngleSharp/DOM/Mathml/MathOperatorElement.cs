using System;

namespace AngleSharp.DOM.Mathml
{
    sealed class MathOperatorElement : MathElement, IScopeElement
    {
        internal MathOperatorElement()
	    {
            _name = Tags.Mo;
	    }

        /// <summary>
        /// Gets the status if the node is a MathML text integration point.
        /// </summary>
        protected internal override Boolean IsMathMLTIP
        {
            get { return true; }
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }
    }
}
