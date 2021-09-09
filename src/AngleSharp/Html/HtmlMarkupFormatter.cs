namespace AngleSharp.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Text;

    /// <summary>
    /// Represents the standard HTML5 markup formatter.
    /// </summary>
    public class HtmlMarkupFormatter : IMarkupFormatter
    {
        #region Instance

        /// <summary>
        /// An instance of the HtmlMarkupFormatter.
        /// </summary>
        public static readonly IMarkupFormatter Instance = new HtmlMarkupFormatter();

        #endregion

        #region Methods

        /// <inheritdoc />
        public virtual String Comment(IComment comment) => String.Concat("<!--", comment.Data, "-->");

        /// <inheritdoc />
        public virtual String Doctype(IDocumentType doctype)
        {
            var ids = GetIds(doctype.PublicIdentifier, doctype.SystemIdentifier);
            return String.Concat("<!DOCTYPE ", doctype.Name, ids, ">");
        }

        /// <inheritdoc />
        public virtual String Processing(IProcessingInstruction processing)
        {
            var value = String.Concat(processing.Target, " ", processing.Data);
            return String.Concat("<?", value, ">");
        }

        /// <inheritdoc />
        public virtual String LiteralText(ICharacterData text) => text.Data;

        /// <inheritdoc />
        public virtual String Text(ICharacterData text)
        {
            var content = text.Data;
            return EscapeText(content);
        }

        /// <inheritdoc />
        public virtual String OpenTag(IElement element, Boolean selfClosing)
        {
            var temp = new ValueStringBuilder(128);

            temp.Append(Symbols.LessThan);

            if (!String.IsNullOrEmpty(element.Prefix))
            {
                temp.Append(element.Prefix);
                temp.Append(Symbols.Colon);
            }

            temp.Append(element.LocalName);

            foreach (var attribute in element.Attributes)
            {
                temp.Append(' ');
                temp.Append(Attribute(attribute));
            }

            temp.Append(Symbols.GreaterThan);
            return temp.ToString();
        }

        /// <inheritdoc />
        public virtual String CloseTag(IElement element, Boolean selfClosing)
        {
            var prefix = element.Prefix;
            var name = element.LocalName;
            var tag = !String.IsNullOrEmpty(prefix) ? String.Concat(prefix, ":", name) : name;
            return selfClosing ? String.Empty : String.Concat("</", tag, ">");
        }

        /// <summary>
        /// Creates the string representation of the attribute.
        /// </summary>
        /// <param name="attr">The attribute to serialize.</param>
        /// <returns>The string representation.</returns>
        protected virtual String Attribute(IAttr attr)
        {
            var sb = new ValueStringBuilder(128);

            WriteAttributeName(attr, ref sb);

            if (attr.Value != null)
            {
                sb.Append(Symbols.Equality);
                sb.Append(Symbols.DoubleQuote);
                WriteAttributeValue(attr, ref sb);
                sb.Append(Symbols.DoubleQuote);
            }

            return sb.ToString();
        }

        internal static void WriteAttributeName(IAttr attr, ref ValueStringBuilder sb)
        {
            var namespaceUri = attr.NamespaceUri;
            var localName = attr.LocalName;

            if (String.IsNullOrEmpty(namespaceUri))
            {
                sb.Append(localName);
            }
            else if (namespaceUri.Is(NamespaceNames.XmlUri))
            {
                sb.Append(NamespaceNames.XmlPrefix);
                sb.Append(Symbols.Colon);
                sb.Append(localName);
            }
            else if (namespaceUri.Is(NamespaceNames.XLinkUri))
            {
                sb.Append(NamespaceNames.XLinkPrefix);
                sb.Append(Symbols.Colon);
                sb.Append(localName);
            }
            else if (namespaceUri.Is(NamespaceNames.XmlNsUri))
            {
                sb.Append(XmlNamespaceLocalName(localName));
            }
            else
            {
                sb.Append(attr.Name);
            }
        }

        internal static void WriteAttributeValue(IAttr attr, ref ValueStringBuilder sb)
        {
            var value = attr.Value ?? String.Empty;

            for (var i = 0; i < value.Length; i++)
            {
                switch (value[i])
                {
                    case Symbols.Ampersand: sb.Append("&amp;"); break;
                    case Symbols.NoBreakSpace: sb.Append("&nbsp;"); break;
                    case Symbols.DoubleQuote: sb.Append("&quot;"); break;
                    default: sb.Append(value[i]); break;
                }
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Escapes the given text by replacing special characters with their
        /// HTML entity (amp, nobsp, lt, and gt).
        /// </summary>
        /// <param name="content">The string to alter.</param>
        /// <returns>The altered string.</returns>
        public static String EscapeText(String content)
        {
            var temp = new ValueStringBuilder(content.Length + 20);

            for (var i = 0; i < content.Length; i++)
            {
                switch (content[i])
                {
                    case Symbols.Ampersand: temp.Append("&amp;"); break;
                    case Symbols.NoBreakSpace: temp.Append("&nbsp;"); break;
                    case Symbols.GreaterThan: temp.Append("&gt;"); break;
                    case Symbols.LessThan: temp.Append("&lt;"); break;
                    default: temp.Append(content[i]); break;
                }
            }

            return temp.ToString();
        }

        /// <summary>
        /// Gets the doctype identifiers from the given public and system identifier.
        /// </summary>
        /// <param name="publicId">The public identifier.</param>
        /// <param name="systemId">The system identifier.</param>
        /// <returns>The combined string representation.</returns>
        public static String GetIds(String publicId, String systemId)
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

        /// <summary>
        /// Gets the local name using the XML namespace prefix if required.
        /// </summary>
        /// <param name="name">The name to be properly represented.</param>
        /// <returns>The string representation.</returns>
        public static String XmlNamespaceLocalName(String name) => name != NamespaceNames.XmlNsPrefix ? String.Concat(NamespaceNames.XmlNsPrefix, ":", name) : name;

        #endregion
    }
}
