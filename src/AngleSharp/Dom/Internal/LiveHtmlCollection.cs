namespace AngleSharp.Dom
{
    using AngleSharp.Common;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The collection of getElementsByTagName and the like, which always tells what the tree has
    /// now (DOM 4.2.10). The elements are only searched again after something has changed, which
    /// <see cref="Document.MutationVersion"/> tells, as script asks for the length and an item for
    /// every step of a loop. A parse doesn't advance the version, so nothing is kept while one is
    /// running, and <c>document.open</c> and <c>Clear</c> are seen by the root element they take
    /// away. Each search gives a new list, so that what goes over the collection may change the tree.
    /// </summary>
    sealed class LiveHtmlCollection : IHtmlCollection<IElement>
    {
        private readonly Node _root;
        private readonly Func<IEnumerable<IElement>> _search;
        private IElement[]? _elements;
        private Document? _owner;
        private IElement? _documentElement;
        private Int64 _version;

        public LiveHtmlCollection(Node root, Func<IEnumerable<IElement>> search)
        {
            _root = root;
            _search = search;
        }

        public IElement this[Int32 index] => Elements.GetItemByIndex(index);

        public IElement? this[String id] => Elements.GetElementById(id);

        public Int32 Length => Elements.Length;

        public IEnumerator<IElement> GetEnumerator() => ((IEnumerable<IElement>)Elements).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Elements.GetEnumerator();

        private IElement[] Elements
        {
            get
            {
                var owner = _root as Document ?? _root.Owner;

                if (owner is null || owner.IsLoading)
                {
                    _elements = null;
                    return _search.Invoke().ToArray();
                }

                var version = owner.MutationVersion;
                var documentElement = owner.DocumentElement;

                if (_elements is null || !ReferenceEquals(owner, _owner) || version != _version || !ReferenceEquals(documentElement, _documentElement))
                {
                    _elements = _search.Invoke().ToArray();
                    _owner = owner;
                    _version = version;
                    _documentElement = documentElement;
                }

                return _elements;
            }
        }
    }
}
