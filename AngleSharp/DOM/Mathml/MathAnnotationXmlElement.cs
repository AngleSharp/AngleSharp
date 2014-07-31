namespace AngleSharp.DOM.Mathml
{
    /// <summary>
    /// The annotation-xml math element.
    /// </summary>
    sealed class MathAnnotationXmlElement : MathElement
    {
        internal MathAnnotationXmlElement()
            : base(Tags.AnnotationXml, NodeFlags.Special | NodeFlags.Scoped)
	    {
	    }
    }
}
