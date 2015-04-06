namespace AngleSharp.Dom.Mathml
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The math string element.
    /// </summary>
    sealed class MathStringElement : MathElement
    {
        public MathStringElement(Document owner, String prefix = null)
            : base(owner, Tags.Ms, prefix, NodeFlags.MathTip | NodeFlags.Special | NodeFlags.Scoped)
	    {
	    }
    }
}
