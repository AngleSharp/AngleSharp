namespace AngleSharp.Xhtml
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Text;

    /// <summary>
    /// Represents the standard XHTML markup formatter.
    /// </summary>
    public class XhtmlMarkupFormatter : IMarkupFormatter
    {
        #region Instance

        /// <summary>
        /// An instance of the XhtmlMarkupFormatter.
        /// </summary>
        public static readonly IMarkupFormatter Instance = new XhtmlMarkupFormatter();

        #endregion

        #region Methods

        /// <inheritdoc />
        public virtual String CloseTag(IElement element, Boolean selfClosing)
        {
            var prefix = element.Prefix;
            var name = element.LocalName;
            var tag = !String.IsNullOrEmpty(prefix) ? prefix + ":" + name : name;
            return (selfClosing || !element.HasChildNodes) ? String.Empty : String.Concat("</", tag, ">");
        }

        /// <inheritdoc />
        public virtual String Comment(IComment comment) =>
            String.Concat("<!--", comment.Data, "-->");

        /// <inheritdoc />
        public virtual String Doctype(IDocumentType doctype)
        {
            var publicId = doctype.PublicIdentifier;
            var systemId = doctype.SystemIdentifier;
            var noExternalId = String.IsNullOrEmpty(publicId) && String.IsNullOrEmpty(systemId);
            var externalId = noExternalId ? String.Empty : " " + (String.IsNullOrEmpty(publicId) ?
                String.Concat("SYSTEM \"", systemId, "\"") :
                String.Concat("PUBLIC \"", publicId, "\" \"", systemId, "\""));
            return String.Concat("<!DOCTYPE ", doctype.Name, externalId, ">");
        }

        /// <inheritdoc />
        public virtual String OpenTag(IElement element, Boolean selfClosing)
        {
            var prefix = element.Prefix;
            var sb = new ValueStringBuilder(100);
            sb.Append(Symbols.LessThan);

            if (!String.IsNullOrEmpty(prefix))
            {
                sb.Append(prefix);
                sb.Append(Symbols.Colon);
            }

            sb.Append(element.LocalName);

            foreach (var attribute in element.Attributes)
            {
                sb.Append(' ');
                sb.Append(Attribute(attribute));
            }

            if (selfClosing || !element.HasChildNodes)
            {
                sb.Append(" /");
            }

            sb.Append(Symbols.GreaterThan);
            return sb.ToString();
        }

        /// <inheritdoc />
        public virtual String Processing(IProcessingInstruction processing)
        {
            var value = String.Concat(processing.Target, " ", processing.Data);
            return String.Concat("<?", value, "?>");
        }

        /// <inheritdoc />
        public virtual String LiteralText(ICharacterData text) => text.Data;

        /// <inheritdoc />
        public virtual String Text(ICharacterData text) => EscapeText(text.Data);

        /// <summary>
        /// Creates the string representation of the attribute.
        /// </summary>
        /// <param name="attribute">The attribute to serialize.</param>
        /// <returns>The string representation.</returns>
        protected virtual String Attribute(IAttr attribute)
        {
            var namespaceUri = attribute.NamespaceUri;
            var localName = attribute.LocalName;
            var value = attribute.Value;
            var sb = new ValueStringBuilder(100);

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
                sb.Append(attribute.Name);
            }

            sb.Append(Symbols.Equality);
            sb.Append(Symbols.DoubleQuote);

            for (var i = 0; i < value.Length; i++)
            {
                switch (value[i])
                {
                    case Symbols.Ampersand: sb.Append("&amp;"); break;
                    case Symbols.NoBreakSpace: sb.Append("&#160;"); break;
                    case Symbols.LessThan: sb.Append("&lt;"); break;
                    case Symbols.DoubleQuote: sb.Append("&quot;"); break;
                    default: sb.Append(value[i]); break;
                }
            }

            sb.Append(Symbols.DoubleQuote);

            return sb.ToString();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Escapes the given text by replacing special characters with their
        /// XHTML entity (amp, nbsp as numeric value, lt, and gt).
        /// </summary>
        /// <param name="content">The string to alter.</param>
        /// <returns>The altered string.</returns>
        public static String EscapeText(String content)
        {
            var sb = new ValueStringBuilder(content.Length + 10);

            for (var i = 0; i < content.Length; i++)
            {
                switch (content[i])
                {
                    case Symbols.Ampersand: sb.Append("&amp;"); break;
                    case Symbols.NoBreakSpace: sb.Append("&#160;"); break;
                    case Symbols.GreaterThan: sb.Append("&gt;"); break;
                    case Symbols.LessThan: sb.Append("&lt;"); break;
                    default : sb.Append(content[i]); break;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets the local name using the XML namespace prefix if required.
        /// </summary>
        /// <param name="name">The name to be properly represented.</param>
        /// <returns>The string representation.</returns>
        public static String XmlNamespaceLocalName(String name) => !name.Is(NamespaceNames.XmlNsPrefix) ? String.Concat(NamespaceNames.XmlNsPrefix, ":") : name;

        #endregion
    }
}
