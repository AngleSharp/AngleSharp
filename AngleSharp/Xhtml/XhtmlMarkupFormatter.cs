namespace AngleSharp.XHtml
{
    using System;
    using AngleSharp.Dom;
    using AngleSharp.Extensions;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the standard XHTML markup formatter.
    /// </summary>
    public sealed class XhtmlMarkupFormatter : IMarkupFormatter
    {
        #region Instance

        /// <summary>
        /// An instance of the XhtmlMarkupFormatter.
        /// </summary>
        public static readonly IMarkupFormatter Instance = new XhtmlMarkupFormatter();

        #endregion

        #region Methods

        /// <summary>
        /// CloseTag method - Formatting the CloseTag(s).
        /// </summary>
        /// <param name="element"></param>
        /// <param name="selfClosing"></param>
        /// <returns></returns>
        String IMarkupFormatter.CloseTag(IElement element, Boolean selfClosing)
        {
            var prefix = element.Prefix;
            var name = element.LocalName;
            var tag = !String.IsNullOrEmpty(prefix) ? prefix + ":" + name : name;
            return (selfClosing || !element.HasChildNodes) ? String.Empty : String.Concat("</", tag, ">");
        }

        /// <summary>
        /// Comment method - Formatting the Comment(s).
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        String IMarkupFormatter.Comment(IComment comment)
        {
            return String.Concat("<!--", comment.Data, "-->");
        }

        /// <summary>
        /// DocType method - Formatting the DocType.
        /// </summary>
        /// <param name="doctype"></param>
        /// <returns></returns>
        String IMarkupFormatter.Doctype(IDocumentType doctype)
        {
            var publicId = doctype.PublicIdentifier;
            var systemId = doctype.SystemIdentifier;
            var noExternalId = String.IsNullOrEmpty(publicId) && String.IsNullOrEmpty(systemId);
            var externalId = noExternalId ? String.Empty : " " + (String.IsNullOrEmpty(publicId) ?
                String.Concat("SYSTEM \"", systemId, "\"") :
                String.Concat("PUBLIC \"", publicId, "\" \"", systemId, "\""));
            return String.Concat("<!DOCTYPE ", doctype.Name, externalId, ">");
        }

        /// <summary>
        /// OpenTag method - Formatting the Open Tags.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="selfClosing"></param>
        /// <returns></returns>
        String IMarkupFormatter.OpenTag(IElement element, Boolean selfClosing)
        {
            var prefix = element.Prefix;
            var temp = Pool.NewStringBuilder();
            temp.Append(Symbols.LessThan);

            if (!String.IsNullOrEmpty(prefix))
                temp.Append(prefix).Append(Symbols.Colon);

            temp.Append(element.LocalName);

            foreach (var attribute in element.Attributes)
                temp.Append(" ").Append(Instance.Attribute(attribute));

            if (selfClosing || !element.HasChildNodes )
                temp.Append(" /");

            temp.Append(Symbols.GreaterThan);
            return temp.ToPool();
        }

        /// <summary>
        /// Processing method - Formatting the Processing Instruction(s).
        /// </summary>
        /// <param name="processing"></param>
        /// <returns></returns>
        String IMarkupFormatter.Processing(IProcessingInstruction processing)
        {
            var value = String.Concat(processing.Target, " ", processing.Data);
            return String.Concat("<?", value, "?>");
        }

        /// <summary>
        /// Text method - Formatting the Text node(s).
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        String IMarkupFormatter.Text(String text)
        {
            var temp = Pool.NewStringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case Symbols.Ampersand: temp.Append("&amp;"); break;
                    case Symbols.NoBreakSpace: temp.Append("&#160;"); break;
                    case Symbols.GreaterThan: temp.Append("&gt;"); break;
                    case Symbols.LessThan: temp.Append("&lt;"); break;
                    default: temp.Append(text[i]); break;
                }
            }

            return temp.ToPool();
        }

        /// <summary>
        /// Attribute method - Formatting the Attribute(s).
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        String IMarkupFormatter.Attribute(IAttr attribute)
        {
            var namespaceUri = attribute.NamespaceUri;
            var localName = attribute.LocalName;
            var value = attribute.Value;
            var temp = Pool.NewStringBuilder();

            if (String.IsNullOrEmpty(namespaceUri))
            {
                temp.Append(localName);
            }
            else if (namespaceUri.Is(NamespaceNames.XmlUri))
            {
                temp.Append(NamespaceNames.XmlPrefix).Append(Symbols.Colon).Append(localName);
            }
            else if (namespaceUri.Is(NamespaceNames.XLinkUri))
            {
                temp.Append(NamespaceNames.XLinkPrefix).Append(Symbols.Colon).Append(localName);
            }
            else if (namespaceUri.Is(NamespaceNames.XmlNsUri))
            {
                temp.Append(XmlNamespaceLocalName(localName));
            }
            else
            {
                temp.Append(attribute.Name);
            }

            temp.Append(Symbols.Equality).Append(Symbols.DoubleQuote);

            for (int i = 0; i < value.Length; i++)
            {
                switch (value[i])
                {
                    case Symbols.Ampersand: temp.Append("&amp;"); break;
                    case Symbols.NoBreakSpace: temp.Append("&nbsp;"); break;
                    case Symbols.LessThan: temp.Append("&lt;"); break;
                    case Symbols.DoubleQuote: temp.Append("&quot;"); break;
                    default: temp.Append(value[i]); break;
                }
            }

            return temp.Append(Symbols.DoubleQuote).ToPool();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// GetIds method - Get the document type ids.
        /// </summary>
        /// <param name="publicId"></param>
        /// <param name="systemId"></param>
        /// <returns></returns>
        static String GetIds(String publicId, String systemId)
        {
            if (String.IsNullOrEmpty(publicId) && String.IsNullOrEmpty(systemId))
            {
                return String.Empty;
            }
            else if (String.IsNullOrEmpty(systemId))
            {
                return String.Format(" PUBLIC \"{0}\"", publicId);
            }
            else if (String.IsNullOrEmpty(publicId))
            {
                return String.Format(" SYSTEM \"{0}\"", systemId);
            }

            return String.Format(" PUBLIC \"{0}\" \"{1}\"", publicId, systemId);
        }

        /// <summary>
        /// XmlNamespaceLocalName method - Formatting Xml Namespaces.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static String XmlNamespaceLocalName(String name)
        {
            return name != NamespaceNames.XmlNsPrefix ? String.Concat(NamespaceNames.XmlNsPrefix, ":") : name;
        }

        #endregion
    }
}
