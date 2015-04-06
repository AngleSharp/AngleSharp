namespace AngleSharp.Dom.Mathml
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The mo math element.
    /// </summary>
    sealed class MathOperatorElement : MathElement
    {
        public MathOperatorElement(Document owner, String prefix = null)
            : base(owner, Tags.Mo, prefix, NodeFlags.Special | NodeFlags.MathTip | NodeFlags.Scoped)
	    {
	    }
    }
}
