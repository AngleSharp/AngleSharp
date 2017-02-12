namespace AngleSharp.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents the an HTML5 markup formatter with inserted intends.
    /// </summary>
    public class PrettyMarkupFormatter : IMarkupFormatter
    {
        #region Fields

        private String _intendString;
        private String _newLineString;
        private Int32 _intendCount;
        private Boolean _trimAll;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new instance of the pretty markup formatter.
        /// </summary>
        public PrettyMarkupFormatter()
        {
            _intendCount = 0;
            _intendString = "\t";
            _newLineString = "\n";
            _trimAll = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the indentation string.
        /// </summary>
        public String Indentation
        {
            get { return _intendString; }
            set { _intendString = value; }
        }

        /// <summary>
        /// Gets or sets the newline string.
        /// </summary>
        public String NewLine
        {
            get { return _newLineString; }
            set { _newLineString = value; }
        }

        /// <summary>
        /// Gets or sets if all whitespace should be trimmed.
        /// Otherwise at least 1 space will remain.
        /// </summary>
        public Boolean TrimAllWhitespace
        {
            get { return _trimAll; }
            set { _trimAll = value; }
        }

        #endregion

        #region Methods

        String IMarkupFormatter.Comment(IComment comment)
        {
            var before = IntendBefore(comment.PreviousSibling);
            return before + HtmlMarkupFormatter.Instance.Comment(comment);
        }

        String IMarkupFormatter.Doctype(IDocumentType doctype)
        {
            var before = String.Empty;

            if (doctype.ParentElement != null)
            {
                before = IntendBefore(doctype);
            }

            return before + HtmlMarkupFormatter.Instance.Doctype(doctype);
        }

        String IMarkupFormatter.Processing(IProcessingInstruction processing)
        {
            var before = IntendBefore(processing);
            return before + HtmlMarkupFormatter.Instance.Processing(processing);
        }

        String IMarkupFormatter.Text(ICharacterData text)
        {
            var content = text.Data;
            var singleLine = content.Replace(Symbols.LineFeed, Symbols.Space).Trim();

            if (!_trimAll && singleLine.Length == 0 && content.Length > 0)
            {
                singleLine = " ";
            }

            return HtmlMarkupFormatter.EscapeText(singleLine);
        }

        String IMarkupFormatter.OpenTag(IElement element, Boolean selfClosing)
        {
            var before = String.Empty;

            if (element.ParentElement != null)
            {
                before = IntendBefore(element);
            }            

            _intendCount++;
            return before + HtmlMarkupFormatter.Instance.OpenTag(element, selfClosing);
        }

        String IMarkupFormatter.CloseTag(IElement element, Boolean selfClosing)
        {
            _intendCount--;
            var before = String.Empty;

            if (element.HasChildNodes && (element.ChildNodes.Length != 1 || element.ChildNodes[0].NodeType != NodeType.Text))
            {
                before = IntendBefore(element);
            }
            
            return before + HtmlMarkupFormatter.Instance.CloseTag(element, selfClosing);
        }

        String IMarkupFormatter.Attribute(IAttr attribute)
        {
            return HtmlMarkupFormatter.Instance.Attribute(attribute);
        }

        #endregion

        #region Helpers

        private String IntendBefore(INode node)
        {
            return _newLineString + String.Join(String.Empty, Enumerable.Repeat(_intendString, _intendCount));
        }

        #endregion
    }
}
