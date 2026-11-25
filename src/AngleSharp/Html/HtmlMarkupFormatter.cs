namespace AngleSharp.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
#if NET8_0_OR_GREATER
    using System.IO;
#endif
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
            var temp = StringBuilderPool.Obtain();
            temp.Append(Symbols.LessThan);

            if (!String.IsNullOrEmpty(element.Prefix))
            {
                temp.Append(element.Prefix).Append(Symbols.Colon);
            }

            temp.Append(element.LocalName);

            // the name of the custom element it was created as is said by this when it comes back
            if (element is Element custom && CustomElementRegistry.GetIsValue(custom) is { } isValue && !element.HasAttribute("is"))
            {
                temp.Append(" is=\"").Append(isValue).Append('"');
            }

            foreach (var attribute in element.Attributes)
            {
                temp.Append(' ').Append(Attribute(attribute));
            }

            temp.Append(Symbols.GreaterThan);
            return temp.ToPool();
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
            var temp = StringBuilderPool.Obtain();

            WriteAttributeName(attr, temp);

            if (attr.Value != null)
            {
                temp.Append(Symbols.Equality).Append(Symbols.DoubleQuote);
                WriteAttributeValue(attr, temp);
                return temp.Append(Symbols.DoubleQuote).ToPool();
            }

            return temp.ToPool();
        }

        internal static void WriteAttributeName(IAttr attr, StringBuilder stringBuilder)
        {
            var namespaceUri = attr.NamespaceUri;
            var localName = attr.LocalName;

            if (String.IsNullOrEmpty(namespaceUri))
            {
                stringBuilder.Append(localName);
            }
            else if (namespaceUri.Is(NamespaceNames.XmlUri))
            {
                stringBuilder.Append(NamespaceNames.XmlPrefix).Append(Symbols.Colon).Append(localName);
            }
            else if (namespaceUri.Is(NamespaceNames.XLinkUri))
            {
                stringBuilder.Append(NamespaceNames.XLinkPrefix).Append(Symbols.Colon).Append(localName);
            }
            else if (namespaceUri.Is(NamespaceNames.XmlNsUri))
            {
                stringBuilder.Append(XmlNamespaceLocalName(localName));
            }
            else
            {
                stringBuilder.Append(attr.Name);
            }
        }

        internal static void WriteAttributeValue(IAttr attr, StringBuilder stringBuilder)
        {
            var value = attr.Value ?? String.Empty;

            for (var i = 0; i < value.Length; i++)
            {
                switch (value[i])
                {
                    case Symbols.Ampersand: stringBuilder.Append("&amp;"); break;
                    case Symbols.NoBreakSpace: stringBuilder.Append("&nbsp;"); break;
                    case Symbols.DoubleQuote: stringBuilder.Append("&quot;"); break;
                    // Escaped since the spec change of May 2025: a raw "<" or ">" in an attribute
                    // becomes markup when the output is reparsed with different rules, e.g. a
                    // "</noscript>" in a title once scripting is enabled (mutation XSS)
                    case Symbols.LessThan: stringBuilder.Append("&lt;"); break;
                    case Symbols.GreaterThan: stringBuilder.Append("&gt;"); break;
                    default: stringBuilder.Append(value[i]); break;
                }
            }
        }

#if NET8_0_OR_GREATER
        /// <summary>
        /// Writes what <see cref="OpenTag"/> returns straight into the writer. Node.WriteHtml uses
        /// it, and the other Write methods, for this exact formatter on .NET 8 and later: a string per tag and per
        /// attribute is most of what serializing a tree allocates besides its result.
        /// </summary>
        internal static void WriteOpenTag(TextWriter writer, Element element)
        {
            writer.Write(Symbols.LessThan);
            var prefix = element.Prefix;

            if (!String.IsNullOrEmpty(prefix))
            {
                writer.Write(prefix);
                writer.Write(Symbols.Colon);
            }

            writer.Write(element.LocalName);

            if (CustomElementRegistry.GetIsValue(element) is { } isValue && !element.HasAttribute("is"))
            {
                writer.Write(" is=\"");
                writer.Write(isValue);
                writer.Write('"');
            }

            var attributes = element.Attributes;
            var n = attributes.Length;

            for (var i = 0; i < n; i++)
            {
                var attr = attributes[i]!;
                writer.Write(' ');
                WriteAttributeName(attr, writer);

                if (attr.Value != null)
                {
                    writer.Write(Symbols.Equality);
                    writer.Write(Symbols.DoubleQuote);
                    WriteEscaped(writer, attr.Value ?? String.Empty, _escapeAttributeCharacters);
                    writer.Write(Symbols.DoubleQuote);
                }
            }

            writer.Write(Symbols.GreaterThan);
        }

        /// <summary>
        /// Writes what <see cref="CloseTag"/> returns straight into the writer.
        /// </summary>
        internal static void WriteCloseTag(TextWriter writer, IElement element, Boolean selfClosing)
        {
            if (!selfClosing)
            {
                var prefix = element.Prefix;
                writer.Write("</");

                if (!String.IsNullOrEmpty(prefix))
                {
                    writer.Write(prefix);
                    writer.Write(':');
                }

                writer.Write(element.LocalName);
                writer.Write(Symbols.GreaterThan);
            }
        }

        /// <summary>
        /// Writes what <see cref="Text"/> returns straight into the writer.
        /// </summary>
        internal static void WriteText(TextWriter writer, String content) =>
            WriteEscaped(writer, content, _escapeTextCharacters);

        private static void WriteAttributeName(IAttr attr, TextWriter writer)
        {
            var namespaceUri = attr.NamespaceUri;
            var localName = attr.LocalName;

            if (String.IsNullOrEmpty(namespaceUri))
            {
                writer.Write(localName);
            }
            else if (namespaceUri.Is(NamespaceNames.XmlUri))
            {
                writer.Write(NamespaceNames.XmlPrefix);
                writer.Write(Symbols.Colon);
                writer.Write(localName);
            }
            else if (namespaceUri.Is(NamespaceNames.XLinkUri))
            {
                writer.Write(NamespaceNames.XLinkPrefix);
                writer.Write(Symbols.Colon);
                writer.Write(localName);
            }
            else if (namespaceUri.Is(NamespaceNames.XmlNsUri))
            {
                writer.Write(XmlNamespaceLocalName(localName));
            }
            else
            {
                writer.Write(attr.Name);
            }
        }

        /// <summary>
        /// Writes the content with the characters of the given set replaced by their entities,
        /// the runs between them in one call each.
        /// </summary>
        private static void WriteEscaped(TextWriter writer, String content, Char[] escaped)
        {
            var index = content.IndexOfAny(escaped);

            if (index < 0)
            {
                writer.Write(content);
                return;
            }

            var start = 0;

            while (index >= 0)
            {
                WriteRange(writer, content, start, index - start);

                switch (content[index])
                {
                    case Symbols.Ampersand: writer.Write("&amp;"); break;
                    case Symbols.NoBreakSpace: writer.Write("&nbsp;"); break;
                    case Symbols.DoubleQuote: writer.Write("&quot;"); break;
                    case Symbols.LessThan: writer.Write("&lt;"); break;
                    case Symbols.GreaterThan: writer.Write("&gt;"); break;
                }

                start = index + 1;
                index = start < content.Length ? content.IndexOfAny(escaped, start) : -1;
            }

            WriteRange(writer, content, start, content.Length - start);
        }

        private static void WriteRange(TextWriter writer, String content, Int32 start, Int32 count)
        {
            writer.Write(content.AsSpan(start, count));
        }
#endif

        #endregion

        #region Helpers

        private static readonly Char[] _escapeTextCharacters =
        {
            Symbols.Ampersand,
            Symbols.NoBreakSpace,
            Symbols.GreaterThan,
            Symbols.LessThan,
        };

#if NET8_0_OR_GREATER
        private static readonly Char[] _escapeAttributeCharacters =
        {
            Symbols.Ampersand,
            Symbols.NoBreakSpace,
            Symbols.DoubleQuote,
            Symbols.LessThan,
            Symbols.GreaterThan,
        };
#endif

        /// <summary>
        /// Escapes the given text by replacing special characters with their
        /// HTML entity (amp, nobsp, lt, and gt).
        /// </summary>
        /// <param name="content">The string to alter.</param>
        /// <returns>The altered string.</returns>
        public static String EscapeText(String content)
        {
            var index = content.IndexOfAny(_escapeTextCharacters);

            if (index < 0)
            {
                return content;
            }

            var temp = StringBuilderPool.Obtain();
            temp.Append(content, 0, index);

            for (var i = index; i < content.Length; i++)
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

            return temp.ToPool();
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
