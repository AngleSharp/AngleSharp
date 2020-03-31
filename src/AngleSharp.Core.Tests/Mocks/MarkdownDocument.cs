namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using AngleSharp.Text;
    using System;

    sealed class MarkdownDocument : Document
    {
        public MarkdownDocument(IBrowsingContext context, TextSource source)
            : base(context, source)
        {
        }

        public override IElement DocumentElement => null;

        public override IEntityProvider Entities => HtmlEntityProvider.Resolver;

        public override Element CreateElementFrom(String name, String prefix, NodeFlags flags) => new AnyElement(this, name, prefix, null, flags);

        public override Node Clone(Document owner, Boolean deep)
        {
            var document = new MarkdownDocument(Context, Source);
            CloneDocument(document, deep);
            return document;
        }

        protected override void SetTitle(String value)
        {
        }
    }
}
