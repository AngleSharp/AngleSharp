namespace AngleSharp.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents the standard HTML5 markup formatter.
    /// </summary>
    public sealed class HtmlMarkupFormatter : IMarkupFormatter
    {
        #region Instance

        /// <summary>
        /// An instance of the HtmlMarkupFormatter.
        /// </summary>
        public static readonly IMarkupFormatter Instance = new HtmlMarkupFormatter();

        #endregion

        #region Methods

        String IMarkupFormatter.Comment(IComment comment)
        {
            return String.Concat("<!--", comment.Data, "-->");
        }

        String IMarkupFormatter.Doctype(IDocumentType doctype)
        {
            var ids = GetIds(doctype.PublicIdentifier, doctype.SystemIdentifier);
            return String.Concat("<!DOCTYPE ", doctype.Name, ids, ">");
        }

        String IMarkupFormatter.Processing(IProcessingInstruction processing)
        {
            var value = String.Concat(processing.Target, " ", processing.Data);
            return String.Concat("<?", value, ">");
        }

        String IMarkupFormatter.Text(String text)
        {
            var temp = Pool.NewStringBuilder();

            for (var i = 0; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case Symbols.Ampersand: temp.Append("&amp;"); break;
                    case Symbols.NoBreakSpace: temp.Append("&nbsp;"); break;
                    case Symbols.GreaterThan: temp.Append("&gt;"); break;
                    case Symbols.LessThan: temp.Append("&lt;"); break;
                    default: temp.Append(text[i]); break;
                }
            }

            return temp.ToPool();
        }

        String IMarkupFormatter.OpenTag(IElement element, Boolean selfClosing)
        {
            var temp = Pool.NewStringBuilder();
            temp.Append(Symbols.LessThan);

            if (!String.IsNullOrEmpty(element.Prefix))
            {
                temp.Append(element.Prefix).Append(Symbols.Colon);
            }

            temp.Append(element.LocalName);

            foreach (var attribute in element.Attributes)
            {
                temp.Append(" ").Append(Instance.Attribute(attribute));
            }

            temp.Append(Symbols.GreaterThan);
            return temp.ToPool();
        }

        String IMarkupFormatter.CloseTag(IElement element, Boolean selfClosing)
        {
            var prefix = element.Prefix;
            var name = element.LocalName;
            var tag = !String.IsNullOrEmpty(prefix) ? prefix + ":" + name : name;
            return selfClosing ? String.Empty : String.Concat("</", tag, ">");
        }

        String IMarkupFormatter.Attribute(IAttr attr)
        {
            var namespaceUri = attr.NamespaceUri;
            var localName = attr.LocalName;
            var value = attr.Value;
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
                temp.Append(attr.Name);
            }

            temp.Append(Symbols.Equality).Append(Symbols.DoubleQuote);

            for (var i = 0; i < value.Length; i++)
            {
                switch (value[i])
                {
                    case Symbols.Ampersand: temp.Append("&amp;"); break;
                    case Symbols.NoBreakSpace: temp.Append("&nbsp;"); break;
                    case Symbols.DoubleQuote: temp.Append("&quot;"); break;
                    default: temp.Append(value[i]); break;
                }
            }

            return temp.Append(Symbols.DoubleQuote).ToPool();
        }

        #endregion

        #region Helpers

        static String GetIds(String publicId, String systemId)
        {
            if (String.IsNullOrEmpty(publicId) && String.IsNullOrEmpty(systemId))
            {
                return String.Empty;
            }
            else if (String.IsNullOrEmpty(systemId))
            {
                return $" PUBLIC \"{publicId}\"";
            }
            else if (String.IsNullOrEmpty(publicId))
            {
                return $" SYSTEM \"{systemId}\"";
            }

            return $" PUBLIC \"{publicId}\" \"{systemId}\"";
        }

        static String XmlNamespaceLocalName(String name)
        {
            return name != NamespaceNames.XmlNsPrefix ? String.Concat(NamespaceNames.XmlNsPrefix, ":") : name;
        }

        #endregion
    }
}
