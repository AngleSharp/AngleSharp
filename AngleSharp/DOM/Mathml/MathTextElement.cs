namespace AngleSharp.DOM.Mathml
{
    using AngleSharp.Html;

    /// <summary>
    /// The mtext math element.
    /// </summary>
    sealed class MathTextElement : MathElement
    {
        internal MathTextElement()
            : base(Tags.Mtext, NodeFlags.MathTip | NodeFlags.Special | NodeFlags.Scoped)
	    {
	    }
    }
}
