namespace AngleSharp.Html
{
    using System;
    using System.Linq;
    using AngleSharp.Dom;

    /// <summary>
    /// Represents the an HTML5 markup formatter with inserted intends.
    /// </summary>
    public class PrettyMarkupFormatter : IMarkupFormatter
    {
        #region Fields

        String _intendString;
        String _newLineString;
        Int32 _intendCount;

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

        #endregion

        #region Methods

        String IMarkupFormatter.Comment(IComment comment)
        {
            return String.Concat(
                IntendBefore(comment.PreviousSibling),
                HtmlMarkupFormatter.Instance.Comment(comment),
                NewLineAfter(comment.NextSibling));
        }

        String IMarkupFormatter.Doctype(IDocumentType doctype)
        {
            return String.Concat(
                IntendBefore(doctype.PreviousSibling),
                HtmlMarkupFormatter.Instance.Doctype(doctype),
                NewLineAfter(doctype.NextSibling));
        }

        String IMarkupFormatter.Processing(IProcessingInstruction processing)
        {
            return String.Concat(
                IntendBefore(processing.PreviousSibling),
                HtmlMarkupFormatter.Instance.Processing(processing),
                NewLineAfter(processing.NextSibling));
        }

        String IMarkupFormatter.Text(String text)
        {
            return HtmlMarkupFormatter.Instance.Text(text.Replace("\n", ""));
        }

        String IMarkupFormatter.OpenTag(IElement element, Boolean selfClosing)
        {
            var before = IntendBefore(element.PreviousSibling ?? element.Parent);
            _intendCount++;
            return String.Concat(
                before,
                HtmlMarkupFormatter.Instance.OpenTag(element, selfClosing),
                NewLineAfter(element.FirstChild ?? element.NextSibling));
        }

        String IMarkupFormatter.CloseTag(IElement element, Boolean selfClosing)
        {
            _intendCount--;
            var before = IntendBefore(element.LastChild ?? element.Parent);
            return String.Concat(
                before,
                HtmlMarkupFormatter.Instance.CloseTag(element, selfClosing),
                NewLineAfter(element.NextSibling ?? element.Parent));
        }

        String IMarkupFormatter.Attribute(IAttr attribute)
        {
            return HtmlMarkupFormatter.Instance.Attribute(attribute);
        }

        #endregion

        #region Helpers

        String NewLineAfter(INode node)
        {
            if (node != null && node.NodeType != NodeType.Text)
            {
                return _newLineString;
            }

            return String.Empty;
        }

        String IntendBefore(INode node)
        {
            if (node != null && node.NodeType != NodeType.Text)
            {
                return String.Join(String.Empty, Enumerable.Repeat(_intendString, _intendCount));
            }

            return String.Empty;
        }

        #endregion
    }
}
