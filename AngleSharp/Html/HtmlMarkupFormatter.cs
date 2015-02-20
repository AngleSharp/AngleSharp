﻿namespace AngleSharp.Html
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;

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

        public virtual String Comment(IComment comment)
        {
            return String.Concat("<!--", comment.Data, "-->");
        }

        public virtual String Doctype(IDocumentType doctype)
        {
            var ids = GetIds(doctype.PublicIdentifier, doctype.SystemIdentifier);
            return String.Concat("<!DOCTYPE ", doctype.Name, ids, ">");
        }

        public virtual String Processing(IProcessingInstruction processing)
        {
            var value = String.Concat(processing.Target, " ", processing.Data);
            return String.Concat("<?", value, ">");
        }

        public virtual String Text(String text)
        {
            var temp = Pool.NewStringBuilder();

            for (int i = 0; i < text.Length; i++)
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

        public virtual String Attribute(IAttr attr)
        {
            var namespaceUri = attr.NamespaceUri;
            var localName = attr.LocalName;
            var value = attr.Value;
            var temp = Pool.NewStringBuilder();

            if (String.IsNullOrEmpty(namespaceUri))
                temp.Append(localName);
            else if (namespaceUri == Namespaces.XmlUri)
                temp.Append(Namespaces.XmlPrefix).Append(Symbols.Colon).Append(localName);
            else if (namespaceUri == Namespaces.XLinkUri)
                temp.Append(Namespaces.XLinkPrefix).Append(Symbols.Colon).Append(localName);
            else if (namespaceUri == Namespaces.XmlNsUri)
                temp.Append(XmlNamespaceLocalName(localName));
            else
                temp.Append(attr.Name);

            temp.Append(Symbols.Equality).Append(Symbols.DoubleQuote);

            for (int i = 0; i < value.Length; i++)
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

        public virtual String OpenTag(String tagName, IEnumerable<String> attributes, Boolean selfClosing)
        {
            var temp = Pool.NewStringBuilder();
            temp.Append(Symbols.LessThan).Append(tagName);

            foreach (var attribute in attributes)
                temp.Append(" ").Append(attribute);

            temp.Append(Symbols.GreaterThan);
            return temp.ToPool();
        }

        public virtual String CloseTag(String tagName, Boolean selfClosing)
        {
            return selfClosing ? String.Empty : String.Concat("</", tagName, ">");
        }

        #endregion

        #region Helpers

        static String GetIds(String publicId, String systemId)
        {
            if (String.IsNullOrEmpty(publicId) && String.IsNullOrEmpty(systemId))
                return String.Empty;
            else if (String.IsNullOrEmpty(systemId))
                return String.Format(" PUBLIC \"{0}\"", publicId);
            else if (String.IsNullOrEmpty(publicId))
                return String.Format(" SYSTEM \"{0}\"", systemId);

            return String.Format(" PUBLIC \"{0}\" \"{1}\"", publicId, systemId);
        }

        static String XmlNamespaceLocalName(String name)
        {
            return name != Namespaces.XmlNsPrefix ? String.Concat(Namespaces.XmlNsPrefix, ":") : name;
        }

        #endregion
    }
}
