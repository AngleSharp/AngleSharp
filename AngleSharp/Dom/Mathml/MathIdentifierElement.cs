namespace AngleSharp.Dom.Mathml
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The mi math element.
    /// </summary>
    sealed class MathIdentifierElement : MathElement
    {
        public MathIdentifierElement(Document owner, String prefix = null)
            : base(owner, Tags.Mi, prefix, NodeFlags.Special | NodeFlags.MathTip | NodeFlags.Scoped)
	    {
	    }
    }
}
