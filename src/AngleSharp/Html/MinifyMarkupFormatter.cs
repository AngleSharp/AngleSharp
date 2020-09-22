namespace AngleSharp.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the an HTML5 markup formatter with a normalization scheme.
    /// </summary>
    public class MinifyMarkupFormatter : HtmlMarkupFormatter
    {
        #region Fields

        private IEnumerable<String> _preservedTags = new[] { TagNames.Pre, TagNames.Textarea };

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the tags that should have preserved white-space.
        /// </summary>
        public IEnumerable<String> PreservedTags
        {
            get => _preservedTags ?? Enumerable.Empty<String>();
            set => _preservedTags = value;
        }

        /// <summary>
        /// Gets or sets if the automatically inserted standard elements
        /// (html, head, body) should be kept despite adding no value.
        /// </summary>
        public Boolean ShouldKeepStandardElements { get; set; }

        /// <summary>
        /// Gets or sets if comments should be preserved.
        /// </summary>
        public Boolean ShouldKeepComments { get; set; }

        /// <summary>
        /// Gets or sets if quotes of an attribute should be kept despite
        /// not needing them.
        /// </summary>
        public Boolean ShouldKeepAttributeQuotes { get; set; }

        /// <summary>
        /// Gets or sets if empty (zero-length) attributes should be kept.
        /// </summary>
        public Boolean ShouldKeepEmptyAttributes { get; set; }

        /// <summary>
        /// Gets or sets if implied end tags (e.g., "/li") should be preserved.
        /// </summary>
        public Boolean ShouldKeepImpliedEndTag { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override string Comment(IComment comment) => ShouldKeepComments ? base.Comment(comment) : String.Empty;

        /// <inheritdoc />
        public override String Text(ICharacterData text)
        {
            if (text.Parent is IHtmlHeadElement || text.Parent is IHtmlHtmlElement)
            {
                return String.Empty;
            }
            else
            {
                var data = base.Text(text);

                if (!PreservedTags.Contains(text.ParentElement?.LocalName))
                {
                    var sb = StringBuilderPool.Obtain();
                    var ws = false;
                    var onlyWs = true;

                    for (var i = 0; i < data.Length; i++)
                    {
                        var chr = data[i];

                        if (chr.IsWhiteSpaceCharacter())
                        {
                            if (!ws)
                            {
                                sb.Append(' ');
                                ws = true;
                            }
                        }
                        else
                        {
                            sb.Append(chr);
                            onlyWs = false;
                            ws = false;
                        }
                    }

                    if (!onlyWs || ShouldOutput(text))
                    {
                        return sb.ToPool();
                    }

                    return String.Empty;
                }

                return data;
            }
        }

        /// <inheritdoc />
        public override String OpenTag(IElement element, Boolean selfClosing)
        {
            if (!CanBeRemoved(element))
            {
                var temp = StringBuilderPool.Obtain();
                temp.Append(Symbols.LessThan);

                if (!String.IsNullOrEmpty(element.Prefix))
                {
                    temp.Append(element.Prefix).Append(Symbols.Colon);
                }

                temp.Append(element.LocalName);

                foreach (var attribute in element.Attributes)
                {
                    if (ShouldKeep(element, attribute))
                    {
                        if (!element.IsBooleanAttribute(attribute.Name))
                        {
                            var value = Attribute(attribute);

                            if (!String.IsNullOrEmpty(value))
                            {
                                temp.Append(' ').Append(value);
                            }
                        }
                        else
                        {
                            temp.Append(' ').Append(attribute.Name);
                        }
                    }
                }

                // The last attribute must not end with a '/' otherwise it is a self-closed tag
                if(temp[temp.Length -1] == '/')
                {
                    temp.Append(' ');
                }

                temp.Append(Symbols.GreaterThan);
                return temp.ToPool();
            }

            return String.Empty;
        }

        /// <inheritdoc />
        public override String CloseTag(IElement element, Boolean selfClosing)
        {
            if (!CanBeRemoved(element) && !CanBeSkipped(element))
            {
                return base.CloseTag(element, selfClosing);
            }

            return String.Empty;
        }

        /// <inheritdoc />
        protected override string Attribute(IAttr attr)
        {
            var value = attr.Value;

            if (ShouldKeepEmptyAttributes || !String.IsNullOrEmpty(value))
            {
                var temp = StringBuilderPool.Obtain();

                WriteAttributeName(attr, temp);

                if (value != null)
                {
                    temp.Append(Symbols.Equality);
                    var needQuotes = ShouldKeepAttributeQuotes || value.Any(MustBeQuotedAttributeValue);

                    if (needQuotes)
                    {
                        temp.Append(Symbols.DoubleQuote);
                    }

                    WriteAttributeValue(attr, temp);

                    if (needQuotes)
                    {
                        temp.Append(Symbols.DoubleQuote);
                    }
                }

                return temp.ToPool();
            }

            return String.Empty;
        }

        #endregion

        #region Helpers

        private Boolean CanBeRemoved(IElement element) =>
            !ShouldKeepStandardElements &&
            element.Attributes.Length == 0 &&
            element.LocalName.IsOneOf(TagNames.Html, TagNames.Head, TagNames.Body);

        private Boolean CanBeSkipped(IElement element) =>
            !ShouldKeepImpliedEndTag &&
            element.Flags.HasFlag(NodeFlags.ImpliedEnd) && (
                element.NextElementSibling == null ||
                element.NextElementSibling.LocalName == element.LocalName);

        private static Boolean ShouldOutput(ICharacterData text) =>
            text.Parent is HtmlBodyElement == false ||
            (text.NextSibling != null && text.PreviousSibling != null);

        private static Boolean ShouldKeep(IElement element, IAttr attribute) =>
            !IsStandardScript(element, attribute) &&
            !IsStandardStyle(element, attribute);

        private static Boolean IsStandardScript(IElement element, IAttr attr) =>
            element is HtmlScriptElement &&
            attr.Name.Is(AttributeNames.Type) &&
            attr.Value.Is(MimeTypeNames.DefaultJavaScript);

        private static Boolean IsStandardStyle(IElement element, IAttr attr) =>
            element is HtmlStyleElement &&
            attr.Name.Is(AttributeNames.Type) &&
            attr.Value.Is(MimeTypeNames.Css);

        private static bool MustBeQuotedAttributeValue(char c)
        {
            // https://w3c.github.io/html-reference/syntax.html#attr-value-unquoted
            return CharExtensions.IsWhiteSpaceCharacter(c) || c == '"' || c == '\'' || c == '=' || c == '>' || c == '<' || c == '`';
        }

        #endregion
    }
}
