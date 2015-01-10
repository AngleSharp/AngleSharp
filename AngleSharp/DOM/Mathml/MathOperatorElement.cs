namespace AngleSharp.DOM.Mathml
{
    using AngleSharp.Html;

    /// <summary>
    /// The mo math element.
    /// </summary>
    sealed class MathOperatorElement : MathElement
    {
        public MathOperatorElement(Document owner)
            : base(owner, Tags.Mo, NodeFlags.Special | NodeFlags.MathTip | NodeFlags.Scoped)
	    {
	    }
    }
}
