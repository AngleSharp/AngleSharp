using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Xml;
using AngleSharp.XHtml;
using AngleSharp.Html;

namespace AngleSharp
{
    /// <summary>
    /// AutoSelectedMarkupFormatter class responsible for selecting the proper MarkupFormatter implementation depending on the DocumentType.
    /// </summary>
    public sealed class AutoSelectedMarkupFormatter : IMarkupFormatter
    {
        private IMarkupFormatter childFormatter = null;

        /// <summary>
        /// AutoSelectedMarkupFormatter constructor optional injecting the DocumentType.
        /// </summary>
        /// <param name="docType">Optional DocumentType which tells which implementation to use.</param>
        public AutoSelectedMarkupFormatter(IDocumentType docType = null)
        {
            DocType = docType;
        }


        /// <summary>
        /// ChildFormatter Property to lazy load the proper MarkupFormatter implementation.
        /// </summary>
        public IMarkupFormatter ChildFormatter
        {
            get
            {
                if (childFormatter == null )
                {
                    if ( DocType != null )
                    {
                        if ( DocType.PublicIdentifier.Contains("XML") )
                        {
                            childFormatter = XmlMarkupFormatter.Instance;
                        }
                        else if ( DocType.PublicIdentifier.Contains("XHTML") )
                        {
                            childFormatter = XhtmlMarkupFormatter.Instance;
                        }
                    }

                    if ( childFormatter == null )
                    {
                        childFormatter = HtmlMarkupFormatter.Instance;
                    }
                }
                return childFormatter;
            }
            set
            {
                childFormatter = value;
            }
        }

        /// <summary>
        /// DocType Property - The documentation type.
        /// </summary>
        public IDocumentType DocType
        {
            get;
            set;
        }

        /// <summary>
        /// Attribute method formatting all attributes in the document.
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public string Attribute(IAttr attribute)
        {
            return ChildFormatter.Attribute(attribute);
        }

        /// <summary>
        /// CloseTag method formatting the closetags.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="selfClosing"></param>
        /// <returns></returns>
        public string CloseTag(IElement element, bool selfClosing)
        {
            if (DocType == null)
            {
                DocType = element.Owner.Doctype;
            }

            return ChildFormatter.CloseTag(element, selfClosing);
        }


        /// <summary>
        /// Comment method - formatting the (X)Html comment(s).
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public string Comment(IComment comment)
        {
            if (DocType == null)
            {
                DocType = comment.Owner.Doctype;
            }

            return ChildFormatter.Comment(comment);
        }

        /// <summary>
        /// DocType method - Formatting the DocType.
        /// </summary>
        /// <param name="docType"></param>
        /// <returns></returns>
        public string Doctype(IDocumentType docType)
        {
            if ( DocType==null )
            {
                DocType = docType;
            }

            return ChildFormatter.Doctype(docType);
        }

        /// <summary>
        /// OpenTag method - Formatting the Open tag.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="selfClosing"></param>
        /// <returns></returns>
        public string OpenTag(IElement element, bool selfClosing)
        {
            if ( DocType==null )
            {
                DocType = element.Owner.Doctype;
            }

            return ChildFormatter.OpenTag(element, selfClosing);
        }

        /// <summary>
        /// Processing method - Formatting Processing instruction(s).
        /// </summary>
        /// <param name="processing"></param>
        /// <returns></returns>
        public string Processing(IProcessingInstruction processing)
        {
            if (DocType == null)
            {
                DocType = processing.Owner.Doctype;
            }

            return ChildFormatter.Processing(processing);
        }

        /// <summary>
        /// Text method - Formatting the Text node(s).
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Text(string text)
        {
            return ChildFormatter.Text(text);
        }
    }
}
