namespace AngleSharp.Svg.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Text;
    using AngleSharp.Xml;
    using System;

    /// <summary>
    /// Represents a document node that contains only SVG nodes.
    /// </summary>
    sealed class SvgDocument : Document, ISvgDocument
    {
        #region Fields

        private readonly IElementFactory<Document, SvgElement> _factory;

        #endregion

        #region ctor

        internal SvgDocument(IBrowsingContext context, TextSource source)
            : base(context ?? BrowsingContext.New(), source)
        {
            ContentType = MimeTypeNames.Svg;
            _factory = Context.GetFactory<IElementFactory<Document, SvgElement>>();
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

        public override IEntityProvider Entities
        {
            get { return Context.GetProvider<IEntityProvider>() ?? XmlEntityProvider.Resolver; }
        }

        #endregion

        #region Helpers

        internal override Element CreateElementFrom(String name, String prefix)
        {
            return _factory.Create(this, name, prefix);
        }

        internal override Node Clone(Document owner, Boolean deep)
        {
            var node = new SvgDocument(Context, new TextSource(Source.Text));
            CloneDocument(node, deep);
            return node;
        }

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
