namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Dom;
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

        public override INode Clone(Boolean deep = true)
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
