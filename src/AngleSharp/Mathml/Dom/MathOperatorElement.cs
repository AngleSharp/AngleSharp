namespace AngleSharp.Mathml.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The mo math element.
    /// </summary>
    sealed class MathOperatorElement : MathElement
    {
        public MathOperatorElement(Document owner, String prefix = null)
            : base(owner, TagNames.Mo, prefix, NodeFlags.Special | NodeFlags.MathTip | NodeFlags.Scoped)
	    {
	    }
    }
}
