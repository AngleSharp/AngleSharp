namespace AngleSharp.Dom.Mathml
{
    using AngleSharp.Html;

    /// <summary>
    /// The annotation-xml math element.
    /// </summary>
    sealed class MathAnnotationXmlElement : MathElement
    {
        public MathAnnotationXmlElement(Document owner)
            : base(owner, Tags.AnnotationXml, NodeFlags.Special | NodeFlags.Scoped)
	    {
	    }
    }
}
