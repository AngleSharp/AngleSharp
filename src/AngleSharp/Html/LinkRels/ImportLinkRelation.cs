namespace AngleSharp.Html.LinkRels
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io.Processors;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    class ImportLinkRelation : BaseLinkRelation
    {
        #region Fields

        private  readonly ConditionalWeakTable<IDocument, HashSet<Uri>> ImportLists = new ();
        private Boolean _async;

        #endregion

        #region ctor

        public ImportLinkRelation(IHtmlLinkElement link)
            : base(link, new DocumentRequestProcessor(link?.Owner!.Context!))
        {
        }

        #endregion

        #region Properties

        public IDocument? Import => (Processor as DocumentRequestProcessor)?.ChildDocument;

        public Boolean IsAsync => _async;

        #endregion

        #region Methods

        /// <summary>
        /// See http://www.w3.org/TR/html-imports/#dfn-import-request.
        /// </summary>
        public override Task LoadAsync()
        {
            var link = Link;
            var document = link.Owner;
            //var list = ImportLists.GetOrCreateValue(document!);
            var location = Url;
            var processor = Processor;
            //var isCycle = location != null && CheckCycle(document!, location);

            if (document != null && location != null && document.AddImportUrl(location))
            {
                var request = link.CreateRequestFor(location);
                _async      = link.HasAttribute(AttributeNames.Async);
                return processor?.ProcessAsync(request)!;
            }

            return Task.CompletedTask;
        }

        #endregion

        #region Helpers

        private Boolean CheckCycle(IDocument document, Url location)
        {
            var ancestor = document;

            while (ancestor != null && ImportLists.TryGetValue(ancestor, out var list))
            {
                if (list.Contains(location))
                {
                    return true;
                }

                ancestor = ancestor.ImportAncestor;
            }

            return false;
        }

        #endregion
    }
}
