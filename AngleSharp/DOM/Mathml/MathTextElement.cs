namespace AngleSharp.DOM.Mathml
{
    /// <summary>
    /// The mtext math element.
    /// </summary>
    sealed class MathTextElement : MathElement, IScopeElement
    {
        internal MathTextElement()
            : base(Tags.Mtext, NodeFlags.MathTip | NodeFlags.Special)
	    {
	    }
    }
}
