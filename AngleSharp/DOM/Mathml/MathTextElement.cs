namespace AngleSharp.DOM.Mathml
{
    using AngleSharp.Html;

    /// <summary>
    /// The mtext math element.
    /// </summary>
    sealed class MathTextElement : MathElement
    {
        public MathTextElement(Document owner)
            : base(owner, Tags.Mtext, NodeFlags.MathTip | NodeFlags.Special | NodeFlags.Scoped)
	    {
	    }
    }
}
