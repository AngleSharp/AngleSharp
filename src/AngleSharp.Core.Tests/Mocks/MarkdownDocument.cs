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

        public override IElement DocumentElement
        {
            get { return null; }
        }

        public override IEntityProvider Entities
        {
            get { return HtmlEntityProvider.Resolver; }
        }

        internal override Element CreateElementFrom(String name, String prefix)
        {
            return new Element(this, name, prefix, null);
        }

        internal override Node Clone(Document owner, Boolean deep)
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
