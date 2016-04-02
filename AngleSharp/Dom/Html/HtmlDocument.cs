namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Events;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using AngleSharp.Parser.Html;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a document node that contains only HTML nodes.
    /// </summary>
    sealed class HtmlDocument : Document, IHtmlDocument
    {
        #region ctor

        internal HtmlDocument(IBrowsingContext context, TextSource source)
            : base(context ?? BrowsingContext.New(), source)
        {
            ContentType = MimeTypeNames.Html;
        }

        internal HtmlDocument(IBrowsingContext context = null)
            : this(context, new TextSource(String.Empty))
        {
        }

        #endregion

        #region Properties

        public override IElement DocumentElement
        {
            get { return this.FindChild<HtmlHtmlElement>(); }
        }

        public override String Title
        {
            get
            {
                var title = DocumentElement.FindDescendant<IHtmlTitleElement>();

                if (title != null)
                {
                    return title.TextContent.CollapseAndStrip();
                }

                return String.Empty;
            }
            set
            {
                var title = DocumentElement.FindDescendant<IHtmlTitleElement>();

                if (title == null)
                {
                    var head = Head;

                    if (head == null)
                    {
                        return;
                    }

                    title = new HtmlTitleElement(this);
                    head.AppendChild(title);
                }

                title.TextContent = value;
            }
        }

        #endregion

        #region Methods

        public override INode Clone(Boolean deep = true)
        {
            var node = new HtmlDocument(Context, new TextSource(Source.Text));
            CloneDocument(node, deep);
            return node;
        }

        internal async static Task<IDocument> LoadAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancelToken)
        {
            var document = new HtmlDocument(context, options.Source);
            var parser = new HtmlDomBuilder(document);
            var parserOptions = new HtmlParserOptions { IsScripting = context.Configuration.IsScripting() };
            document.Setup(options);
            context.NavigateTo(document);
            context.Fire(new HtmlParseEvent(document, completed: false));
            await parser.ParseAsync(parserOptions, cancelToken).ConfigureAwait(false);
            context.Fire(new HtmlParseEvent(document, completed: true));
            return document;
        }

        #endregion
    }
}
