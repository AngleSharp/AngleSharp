namespace AngleSharp.DOM.Mathml
{
    /// <summary>
    /// The annotation-xml math element.
    /// </summary>
    sealed class MathAnnotationXmlElement : MathElement, IScopeElement
    {
        internal MathAnnotationXmlElement()
            : base(Tags.AnnotationXml, NodeFlags.Special)
	    {
	    }
    }
}
