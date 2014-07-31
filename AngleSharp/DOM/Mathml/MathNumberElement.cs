namespace AngleSharp.DOM.Mathml
{
    /// <summary>
    /// The mn math element.
    /// </summary>
    sealed class MathNumberElement : MathElement
    {
        internal MathNumberElement()
            : base(Tags.Mn, NodeFlags.MathTip | NodeFlags.Special | NodeFlags.Scoped)
	    {
	    }
    }
}
