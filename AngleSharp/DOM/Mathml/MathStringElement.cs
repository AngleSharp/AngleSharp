namespace AngleSharp.DOM.Mathml
{
    using AngleSharp.Html;

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
