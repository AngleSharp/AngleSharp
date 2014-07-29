namespace AngleSharp.DOM.Mathml
{
    /// <summary>
    /// The mi math element.
    /// </summary>
    sealed class MathIdentifierElement : MathElement, IScopeElement
    {
        internal MathIdentifierElement ()
            : base(Tags.Mi, NodeFlags.Special | NodeFlags.MathTip)
	    {
	    }
    }
}
