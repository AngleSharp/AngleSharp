namespace AngleSharp.Html.LinkRels
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Network.RequestProcessors;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    class ImportLinkRelation : BaseLinkRelation
    {
        #region Fields

        static readonly ConditionalWeakTable<IDocument, ImportList> ImportLists = new ConditionalWeakTable<IDocument, ImportList>();
        Boolean _isasync;

        #endregion

        #region ctor

        public ImportLinkRelation(HtmlLinkElement link)
            : base(link, DocumentRequestProcessor.Create(link))
        {
        }

        #endregion

        #region Properties

        public IDocument Import
        {
            get 
            {
                var processor = Processor as DocumentRequestProcessor;
                return processor?.ChildDocument;
            }
        }

        public Boolean IsAsync
        {
            get { return _isasync; }
        }

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
                _isasync = link.HasAttribute(AttributeNames.Async);
                return processor?.ProcessAsync(request);
            }

            return null;
        }

        #endregion

        #region Helpers

        static Boolean CheckCycle(IDocument document, Url location)
        {
            var ancestor = document.ImportAncestor;
            var list = default(ImportList);

            while (ancestor != null && ImportLists.TryGetValue(ancestor, out list))
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

        sealed class ImportList
        {
            readonly List<ImportEntry> _list;

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

            public void Add(ImportEntry item)
            {
                _list.Add(item);
            }

            public void Remove(ImportEntry item)
            {
                _list.Remove(item);
            }
        }

        struct ImportEntry
        {
            public ImportLinkRelation Relation;
            public Boolean IsCycle;
        }

        #endregion
    }
}
