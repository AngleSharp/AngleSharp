namespace AngleSharp.Dom.Svg
{
    using AngleSharp.Dom.Events;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using AngleSharp.Parser.Xml;
    using Services;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a document node that contains only SVG nodes.
    /// </summary>
    sealed class SvgDocument : Document, ISvgDocument
    {
        #region ctor

        internal SvgDocument(IBrowsingContext context, TextSource source)
            : base(context ?? BrowsingContext.New(), source)
        {
            ContentType = MimeTypeNames.Svg;
        }

        internal SvgDocument(IBrowsingContext context = null)
            : this(context, new TextSource(String.Empty))
        {
        }

        #endregion

        #region Properties

        public override IElement DocumentElement
        {
            get { return RootElement; }
        }

        public ISvgSvgElement RootElement
        {
            get { return this.FindChild<ISvgSvgElement>(); }
        }

        #endregion

        #region Methods

        public override INode Clone(Boolean deep = true)
        {
            var node = new SvgDocument(Context, new TextSource(Source.Text));
            CloneDocument(node, deep);
            return node;
        }

        internal async static Task<IDocument> LoadAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancelToken)
        {
            var parserOptions = new XmlParserOptions { };
            var document = new SvgDocument(context, options.Source);
            var factory = context.Configuration.GetFactory<IElementFactory<SvgElement>>();
            var parser = new XmlDomBuilder(document, factory.Create);
            document.Setup(options);
            context.NavigateTo(document);
            context.Fire(new HtmlParseEvent(document, completed: false));
            await parser.ParseAsync(parserOptions, cancelToken).ConfigureAwait(false);
            context.Fire(new HtmlParseEvent(document, completed: true));
            return document;
        }

        #endregion

        #region Helpers

        protected override String GetTitle()
        {
            var title = RootElement.FindChild<ISvgTitleElement>();
            return title?.TextContent.CollapseAndStrip() ?? base.GetTitle();
        }

        protected override void SetTitle(String value)
        {
            var title = RootElement.FindChild<ISvgTitleElement>();

            if (title == null)
            {
                title = new SvgTitleElement(this);
                RootElement.AppendChild(title);
            }

            title.TextContent = value;
        }

        #endregion
    }
}
