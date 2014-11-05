namespace AngleSharp.DOM.Mathml
{
    using AngleSharp.Html;

    /// <summary>
    /// The mi math element.
    /// </summary>
    sealed class MathIdentifierElement : MathElement
    {
        internal MathIdentifierElement ()
            : base(Tags.Mi, NodeFlags.Special | NodeFlags.MathTip | NodeFlags.Scoped)
	    {
	    }
    }
}
