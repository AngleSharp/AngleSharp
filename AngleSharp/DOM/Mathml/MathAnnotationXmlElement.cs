using System;

namespace AngleSharp.DOM.Mathml
{
    class MathAnnotationXmlElement : MathElement
    {
        internal MathAnnotationXmlElement()
	    {
            _name = Tags.ANNOTATION_XML;
	    }

        /// <summary>
        /// Gets the status if the node is an HTML text integration point.
        /// </summary>
        protected internal override Boolean IsHtmlTIP
        {
            get
            {
                var value = GetAttribute("encoding");

                if (!String.IsNullOrEmpty(value))
                {
                    value = value.ToLower();
                    return value.Equals(MimeTypes.Html) || value.Equals(MimeTypes.ApplicationXHtml);
                }

                return false;
            }
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }
    }
}
