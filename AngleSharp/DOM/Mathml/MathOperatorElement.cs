namespace AngleSharp.DOM.Mathml
{
    /// <summary>
    /// The mo math element.
    /// </summary>
    sealed class MathOperatorElement : MathElement
    {
        internal MathOperatorElement()
            : base(Tags.Mo, NodeFlags.Special | NodeFlags.MathTip | NodeFlags.Scoped)
	    {
	    }
    }
}
