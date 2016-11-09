namespace AngleSharp.Mathml.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The annotation-xml math element.
    /// </summary>
    sealed class MathAnnotationXmlElement : MathElement
    {
        public MathAnnotationXmlElement(Document owner, String prefix = null)
            : base(owner, TagNames.AnnotationXml, prefix, NodeFlags.Special | NodeFlags.Scoped)
	    {
	    }
    }
}
