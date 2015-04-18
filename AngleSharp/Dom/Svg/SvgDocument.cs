namespace AngleSharp.Dom.Svg
{
    using System;
    using AngleSharp.Extensions;
    using AngleSharp.Network;

    /// <summary>
    /// Represents a document node that contains only SVG nodes.
    /// </summary>
    sealed class SvgDocument : Document, ISvgDocument
    {
        internal SvgDocument(IBrowsingContext context, TextSource source)
            : base(context, source)
        {
            ContentType = MimeTypes.Svg;
        }

        internal SvgDocument(IBrowsingContext context = null)
            : this(context, new TextSource(String.Empty))
        {
        }

        public ISvgSvgElement RootElement
        {
            get { return this.FindChild<ISvgSvgElement>(); }
        }

        public override String Title
        {
            get
            {
                var title = RootElement.FindChild<ISvgTitleElement>();

                if (title != null)
                    return title.TextContent.CollapseAndStrip();

                return String.Empty;
            }
            set
            {
                var title = RootElement.FindChild<ISvgTitleElement>();

                if (title == null)
                {
                    title = new SvgTitleElement(this);
                    RootElement.AppendChild(title);
                }

                title.TextContent = value;
            }
        }

        public override INode Clone(Boolean deep = true)
        {
            var node = new SvgDocument(Context, new TextSource(Source.Text));
            CopyProperties(this, node, deep);
            CopyDocumentProperties(this, node, deep);
            return node;
        }
    }
}
