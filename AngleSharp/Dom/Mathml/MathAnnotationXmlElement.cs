namespace AngleSharp.Dom.Mathml
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The annotation-xml math element.
    /// </summary>
    sealed class MathAnnotationXmlElement : MathElement
    {
        public MathAnnotationXmlElement(Document owner, String prefix = null)
            : base(owner, Tags.AnnotationXml, prefix, NodeFlags.Special | NodeFlags.Scoped)
	    {
	    }
    }
}
