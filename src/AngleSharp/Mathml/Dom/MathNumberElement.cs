namespace AngleSharp.Mathml.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The mn math element.
    /// </summary>
    sealed class MathNumberElement : MathElement
    {
        public MathNumberElement(Document owner, String prefix = null)
            : base(owner, TagNames.Mn, prefix, NodeFlags.MathTip | NodeFlags.Special | NodeFlags.Scoped)
	    {
	    }
    }
}
