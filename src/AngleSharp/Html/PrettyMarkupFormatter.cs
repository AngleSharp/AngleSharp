namespace AngleSharp.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents the an HTML5 markup formatter with inserted indents.
    /// </summary>
    public class PrettyMarkupFormatter : IMarkupFormatter
    {
        #region Fields

        private String _indentString;
        private String _newLineString;
        private Int32 _indentCount;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new instance of the pretty markup formatter.
        /// </summary>
        public PrettyMarkupFormatter()
        {
            _indentCount = 0;
            _indentString = "\t";
            _newLineString = "\n";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the indentation string.
        /// </summary>
        public String Indentation
        {
            get { return _indentString; }
            set { _indentString = value; }
        }

        /// <summary>
        /// Gets or sets the newline string.
        /// </summary>
        public String NewLine
        {
            get { return _newLineString; }
            set { _newLineString = value; }
        }

        #endregion

        #region Methods

        String IMarkupFormatter.Comment(IComment comment)
        {
            var before = IndentBefore();
            return before + HtmlMarkupFormatter.Instance.Comment(comment);
        }

        String IMarkupFormatter.Doctype(IDocumentType doctype)
        {
            var before = String.Empty;

            if (doctype.ParentElement != null)
            {
                before = IndentBefore();
            }

            return before + HtmlMarkupFormatter.Instance.Doctype(doctype);
        }

        String IMarkupFormatter.Processing(IProcessingInstruction processing)
        {
            var before = IndentBefore();
            return before + HtmlMarkupFormatter.Instance.Processing(processing);
        }

        String IMarkupFormatter.Text(ICharacterData text)
        {
            var content = text.Data;
            var before = String.Empty;
            var singleLine = content.Replace(Symbols.LineFeed, Symbols.Space).TrimEnd();

            if (singleLine.Length > 0 && singleLine[0].IsSpaceCharacter())
            {
                singleLine = singleLine.TrimStart();
                before = IndentBefore();
            }

            return before + HtmlMarkupFormatter.EscapeText(singleLine);
        }

        String IMarkupFormatter.OpenTag(IElement element, Boolean selfClosing)
        {
            var before = String.Empty;
            var previousSibling = element.PreviousSibling as IText;

            if (element.ParentElement != null && (previousSibling == null || EndsWithSpace(previousSibling)))
            {
                before = IndentBefore();
            }            

            _indentCount++;
            return before + HtmlMarkupFormatter.Instance.OpenTag(element, selfClosing);
        }

        String IMarkupFormatter.CloseTag(IElement element, Boolean selfClosing)
        {
            _indentCount--;
            var before = String.Empty;
            var lastChild = element.LastChild as IText;

            if (element.HasChildNodes && (lastChild == null || EndsWithSpace(lastChild)))
            {
                before = IndentBefore();
            }
            
            return before + HtmlMarkupFormatter.Instance.CloseTag(element, selfClosing);
        }

        String IMarkupFormatter.Attribute(IAttr attribute)
        {
            return HtmlMarkupFormatter.Instance.Attribute(attribute);
        }

        #endregion

        #region Helpers

        private static Boolean EndsWithSpace(ICharacterData text)
        {
            var content = text.Data;
            return content.Length > 0 && content[content.Length - 1].IsSpaceCharacter();
        }

        private String IndentBefore()
        {
            return _newLineString + String.Join(String.Empty, Enumerable.Repeat(_indentString, _indentCount));
        }

        #endregion
    }
}
