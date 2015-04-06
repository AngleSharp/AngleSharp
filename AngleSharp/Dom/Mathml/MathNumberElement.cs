namespace AngleSharp.Dom.Mathml
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The mn math element.
    /// </summary>
    sealed class MathNumberElement : MathElement
    {
        public MathNumberElement(Document owner, String prefix = null)
            : base(owner, Tags.Mn, prefix, NodeFlags.MathTip | NodeFlags.Special | NodeFlags.Scoped)
	    {
	    }
    }
}
