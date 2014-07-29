namespace AngleSharp.DOM.Mathml
{
    /// <summary>
    /// The math string element.
    /// </summary>
    sealed class MathStringElement : MathElement, IScopeElement
    {
        internal MathStringElement()
            : base(Tags.Ms, NodeFlags.MathTip | NodeFlags.Special)
	    {
	    }
    }
}
