namespace AngleSharp.Html.LinkRels
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    class ImportLinkRelation : BaseLinkRelation
    {
        #region Fields

        static readonly ConditionalWeakTable<IDocument, ImportList> ImportLists = new ConditionalWeakTable<IDocument, ImportList>();
        IDocument _import;
        Boolean _isasync;

        #endregion

        #region ctor

        public ImportLinkRelation(HtmlLinkElement link)
            : base(link)
        {
        }

        #endregion

        #region Properties

        public IDocument Import
        {
            get { return _import; }
        }

        /// <summary>
        /// Gets if scripts should be blocked (async = nope).
        /// </summary>
        public Boolean IsAsync
        {
            get { return _isasync; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// See http://www.w3.org/TR/html-imports/#dfn-import-request.
        /// </summary>
        public override async Task LoadAsync(IConfiguration configuration, IResourceLoader loader)
        {
            var link = Link;
            var document = link.Owner;
            var list = ImportLists.GetOrCreateValue(document);
            var location = Url;
            var request = link.CreateRequestFor(location);
            var item = new ImportEntry 
            { 
                Relation = this,
                IsCycle = CheckCycle(document, location)
            };
            _isasync = link.HasAttribute(AttributeNames.Async);
            list.Add(item);
            
            if (!item.IsCycle)
            {
                var nestedStatus = new TaskCompletionSource<Boolean>();
                var download = loader.DownloadAsync(request);
                SetDownload(download);

                await link.ProcessResponse(download, async response =>
                {
                    var context = new BrowsingContext(document.Context, Sandboxes.None);
                    var options = new CreateDocumentOptions(response, configuration)
                    {
                        ImportAncestor = document
                    };
                    _import = await context.OpenAsync(options, CancellationToken.None).ConfigureAwait(false);
                    nestedStatus.SetResult(true);
                }).ConfigureAwait(false);
                await nestedStatus.Task.ConfigureAwait(false);
            }
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
                    if (_list[i].Relation.Url.Equals(location))
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
