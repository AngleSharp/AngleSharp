namespace AngleSharp.Mathml.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The mtext math element.
    /// </summary>
    sealed class MathTextElement : MathElement
    {
        public MathTextElement(Document owner, String prefix = null)
            : base(owner, TagNames.Mtext, prefix, NodeFlags.MathTip | NodeFlags.Special | NodeFlags.Scoped)
	    {
	    }
    }
}
