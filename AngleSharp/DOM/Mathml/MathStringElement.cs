namespace AngleSharp.DOM.Mathml
{
    /// <summary>
    /// The math string element.
    /// </summary>
    sealed class MathStringElement : MathElement
    {
        internal MathStringElement()
            : base(Tags.Ms, NodeFlags.MathTip | NodeFlags.Special | NodeFlags.Scoped)
	    {
	    }
    }
}
