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

        private static readonly ConditionalWeakTable<IDocument, ImportList> ImportLists = new ConditionalWeakTable<IDocument, ImportList>();
        private Boolean _async;

        #endregion

        #region ctor

        public ImportLinkRelation(IHtmlLinkElement link)
            : base(link, new DocumentRequestProcessor(link?.Owner.Context))
        {
        }

        #endregion

        #region Properties

        public IDocument Import => (Processor as DocumentRequestProcessor)?.ChildDocument;

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
            var list = ImportLists.GetOrCreateValue(document);
            var location = Url;
            var processor = Processor;
            var item = new ImportEntry
            {
                Relation = this,
                IsCycle = location != null && CheckCycle(document, location)
            };
            list.Add(item);

            if (location != null && !item.IsCycle)
            {
                var request = link.CreateRequestFor(location);
                _async = link.HasAttribute(AttributeNames.Async);
                return processor?.ProcessAsync(request);
            }

            return Task.CompletedTask;
        }

        #endregion

        #region Helpers

        private static Boolean CheckCycle(IDocument document, Url location)
        {
            var ancestor = document.ImportAncestor;

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

        #region Import List

        private sealed class ImportList
        {
            private readonly List<ImportEntry> _list;

            public ImportList()
            {
                _list = new List<ImportEntry>();
            }

            public Boolean Contains(Url location)
            {
                for (var i = 0; i < _list.Count; i++)
                {
                    Url relationUrl = _list[i].Relation.Url;
                    if (relationUrl != null && relationUrl.Equals(location))
                    {
                        return true;
                    }
                }

                return false;
            }

            public void Add(ImportEntry item) => _list.Add(item);

            public void Remove(ImportEntry item) => _list.Remove(item);
        }

        private struct ImportEntry
        {
            public ImportLinkRelation Relation;
            public Boolean IsCycle;
        }

        #endregion
    }
}
