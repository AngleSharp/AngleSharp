namespace AngleSharp.Dom.Mathml
{
    using AngleSharp.Html;

    /// <summary>
    /// The math string element.
    /// </summary>
    sealed class MathStringElement : MathElement
    {
        public MathStringElement(Document owner)
            : base(owner, Tags.Ms, NodeFlags.MathTip | NodeFlags.Special | NodeFlags.Scoped)
	    {
	    }
    }
}
