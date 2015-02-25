namespace AngleSharp.Dom.Mathml
{
    using AngleSharp.Html;

    /// <summary>
    /// The mn math element.
    /// </summary>
    sealed class MathNumberElement : MathElement
    {
        public MathNumberElement(Document owner)
            : base(owner, Tags.Mn, NodeFlags.MathTip | NodeFlags.Special | NodeFlags.Scoped)
	    {
	    }
    }
}
