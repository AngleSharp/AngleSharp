namespace AngleSharp.Mathml.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The mi math element.
    /// </summary>
    sealed class MathIdentifierElement : MathElement
    {
        public MathIdentifierElement(Document owner, String prefix = null)
            : base(owner, TagNames.Mi, prefix, NodeFlags.Special | NodeFlags.MathTip | NodeFlags.Scoped)
	    {
	    }
    }
}
