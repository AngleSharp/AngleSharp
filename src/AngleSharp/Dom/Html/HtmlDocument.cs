namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Events;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
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

        #endregion

        #region Methods

        public override INode Clone(Boolean deep = true)
        {
            var source = new TextSource(Source.Text);
            var node = new HtmlDocument(Context, source);
            CloneDocument(node, deep);
            return node;
        }

        internal async static Task<IDocument> LoadAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancelToken)
        {
            var scripting = context.Configuration.IsScripting();
            var parserOptions = new HtmlParserOptions { IsScripting = scripting };
            var document = new HtmlDocument(context, options.Source);
            var parser = new HtmlDomBuilder(document);
            document.Setup(options);
            context.NavigateTo(document);
            context.Fire(new HtmlParseEvent(document, completed: false));
            await parser.ParseAsync(parserOptions, cancelToken).ConfigureAwait(false);
            context.Fire(new HtmlParseEvent(document, completed: true));
            return document;
        }

        internal async static Task<IDocument> LoadTextAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancelToken)
        {
            var scripting = context.Configuration.IsScripting();
            var parserOptions = new HtmlParserOptions { IsScripting = scripting };
            var document = new HtmlDocument(context, options.Source);
            document.Setup(options);
            context.NavigateTo(document);
            var root = document.CreateElement(TagNames.Html);
            var head = document.CreateElement(TagNames.Head);
            var body = document.CreateElement(TagNames.Body);
            var pre = document.CreateElement(TagNames.Pre);
            document.AppendChild(root);
            root.AppendChild(head);
            root.AppendChild(body);
            body.AppendChild(pre);
            pre.SetAttribute(AttributeNames.Style, "word-wrap: break-word; white-space: pre-wrap;");
            await options.Source.PrefetchAllAsync(cancelToken).ConfigureAwait(false);
            pre.TextContent = options.Source.Text;
            return document;
        }

        #endregion

        #region Helpers

        protected override String GetTitle()
        {
            var title = DocumentElement.FindDescendant<IHtmlTitleElement>();
            return title?.TextContent.CollapseAndStrip() ?? base.GetTitle();
        }

        protected override void SetTitle(String value)
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

        #endregion
    }
}
