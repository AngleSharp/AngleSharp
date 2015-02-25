namespace AngleSharp.Dom.Mathml
{
    using AngleSharp.Html;

    /// <summary>
    /// The mi math element.
    /// </summary>
    sealed class MathIdentifierElement : MathElement
    {
        public MathIdentifierElement(Document owner)
            : base(owner, Tags.Mi, NodeFlags.Special | NodeFlags.MathTip | NodeFlags.Scoped)
	    {
	    }
    }
}
