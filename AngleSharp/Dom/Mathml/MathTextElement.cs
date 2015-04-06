namespace AngleSharp.Dom.Mathml
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The mtext math element.
    /// </summary>
    sealed class MathTextElement : MathElement
    {
        public MathTextElement(Document owner, String prefix = null)
            : base(owner, Tags.Mtext, prefix, NodeFlags.MathTip | NodeFlags.Special | NodeFlags.Scoped)
	    {
	    }
    }
}
