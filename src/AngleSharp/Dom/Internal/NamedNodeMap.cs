namespace AngleSharp.Dom
{
    using AngleSharp.Text;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using Common;
    using Html.Construction;

    /// <summary>
    /// NamedNodeNap is a key/value pair of nodes that can be accessed by
    /// numeric or string index.
    /// </summary>
    sealed class NamedNodeMap : INamedNodeMap, IConstructableNamedNodeMap
    {
        #region Fields

        /// <summary>
        /// The attributes in order; an <see cref="IndexedItems"/> once a name lookup found more
        /// than <see cref="IndexThreshold"/> of them. The index lives in the list rather than in
        /// a field here, so that a map of a few attributes - nearly every element - costs not a
        /// byte more for it.
        /// </summary>
        private List<Attr> _items;
        private readonly WeakReference<Element> _owner;

        private const Int32 IndexThreshold = 16;

        #endregion

        #region ctor

        /// <inheritdoc />
        public NamedNodeMap(Element owner)
        {
            _items = [];
            _owner = new WeakReference<Element>(owner);
        }

        #endregion

        #region Index

        /// <inheritdoc />
        public IAttr? this[String name] => GetNamedItem(name);

        /// <inheritdoc />
        public IAttr? this[Int32 index] => index >= 0 && index < _items.Count ? _items[index] : null;

        #endregion

        #region Properties

        /// <inheritdoc />
        public Int32 Length => _items.Count;

        public Element? Owner => _owner.TryGetTarget(out var element) ? element : null;

        #endregion

        #region Internal Methods

        // Construction only: no duplicate check, no attribute change steps, no mutation record and
        // no advance of the document's mutation version - the tree builder reaches this for every
        // attribute of a parsed document. See Element.AddAttribute and SetAttributes; a mutation of
        // an existing element goes through SetNamedItem and NotifyChanged below instead.
        internal void FastAddItem(Attr attr) => Add(attr);

        internal void RaiseChangedEvent(Attr attr, String? newValue, String? oldValue) =>
            NotifyChanged(attr, newValue, oldValue, suppressMutationObservers: false);

        /// <summary>
        /// Advances the owning document's mutation version and, unless the caller does its own
        /// bookkeeping, runs the attribute change steps. This is already the level that decided a
        /// mutation happened - only a set or a remove through the map reaches it, never the tree
        /// builder - so the version is advanced either way: a suppressed write still changes the
        /// attribute, exactly as a suppressed child list step still changes the tree.
        /// </summary>
        private void NotifyChanged(Attr attr, String? newValue, String? oldValue, Boolean suppressMutationObservers)
        {
            if (_owner.TryGetTarget(out var element))
            {
                element.Owner?.MarkMutated();

                if (!suppressMutationObservers)
                {
                    element.AttributeChanged(attr.LocalName, attr.NamespaceUri, oldValue, newValue);
                }
            }
        }

        internal IAttr? RemoveNamedItemOrDefault(String name, Boolean suppressMutationObservers)
        {
            var attr = Find(name);

            if (attr is not null)
            {
                RemoveAt(_items.IndexOf(attr));
                attr.Container = null;
                NotifyChanged(attr, null, attr.Value, suppressMutationObservers);
            }

            return attr;
        }

        internal IAttr? RemoveNamedItemOrDefault(String name) => RemoveNamedItemOrDefault(name, false);

        internal IAttr? RemoveNamedItemOrDefault(String? namespaceUri, String localName, Boolean suppressMutationObservers)
        {
            for (var i = 0; i < _items.Count; i++)
            {
                if (localName.Is(_items[i].LocalName) && namespaceUri.Is(_items[i].NamespaceUri))
                {
                    var attr = _items[i];
                    RemoveAt(i);
                    attr.Container = null;
                    NotifyChanged(attr, null, attr.Value, suppressMutationObservers);
                    return attr;
                }
            }

            return null;
        }

        internal IAttr? RemoveNamedItemOrDefault(String? namespaceUri, String localName) => RemoveNamedItemOrDefault(namespaceUri, localName, false);

        #endregion

        #region Methods

        /// <inheritdoc />
        public IAttr? GetNamedItem(String name) => Find(name);

        /// <inheritdoc />
        public IAttr? GetNamedItem(StringOrMemory name) => Find(name);

        /// <inheritdoc />
        public IAttr? GetNamedItem(String? namespaceUri, String localName)
        {
            for (var i = 0; i < _items.Count; i++)
            {
                if (localName.Is(_items[i].LocalName) && namespaceUri.Is(_items[i].NamespaceUri))
                {
                    return _items[i];
                }
            }

            return null;
        }

        /// <inheritdoc />
        public IAttr? SetNamedItem(IAttr item)
        {
            var proposed = Prepare(item);

            if (proposed != null)
            {
                var attr = Find(item.Name);

                if (attr is not null)
                {
                    // Same qualified name, so the index entry simply moves to the replacement.
                    _items[_items.IndexOf(attr)] = proposed;

                    if (Index is { } index)
                    {
                        index[proposed.Name] = proposed;
                    }

                    RaiseChangedEvent(proposed, proposed.Value, attr.Value);
                    return attr;
                }

                Add(proposed);
                RaiseChangedEvent(proposed, proposed.Value, null);
            }

            return null;
        }

        /// <inheritdoc />
        public IAttr? SetNamedItemWithNamespaceUri(IAttr item, Boolean suppressMutationObservers)
        {
            var proposed = Prepare(item);

            if (proposed != null)
            {
                var localName = item.LocalName;
                var namespaceUri = item.NamespaceUri;

                for (var i = 0; i < _items.Count; i++)
                {
                    if (localName.Is(_items[i].LocalName) && namespaceUri.Is(_items[i].NamespaceUri))
                    {
                        var attr = _items[i];
                        _items[i] = proposed;

                        if (Index is { } index)
                        {
                            var name = attr.Name;

                            // The prefix, and with it the qualified name, may differ; and with a
                            // duplicate name the entry may belong to an earlier attribute.
                            if (!name.Is(proposed.Name))
                            {
                                ((IndexedItems)_items).Index = null;
                            }
                            else if (index.TryGetValue(name, out var first) && Object.ReferenceEquals(first, attr))
                            {
                                index[name] = proposed;
                            }
                        }

                        NotifyChanged(proposed, proposed.Value, attr.Value, suppressMutationObservers);
                        return attr;
                    }
                }

                Add(proposed);
                NotifyChanged(proposed, proposed.Value, null, suppressMutationObservers);
            }

            return null;
        }

        /// <inheritdoc />
        public IAttr? SetNamedItemWithNamespaceUri(IAttr item) => SetNamedItemWithNamespaceUri(item, false);

        /// <inheritdoc />
        public IAttr RemoveNamedItem(String name)
        {
            var result = RemoveNamedItemOrDefault(name);

            if (result is null)
            {
                throw new DomException(DomError.NotFound);
            }

            return result;
        }

        /// <inheritdoc />
        public IAttr RemoveNamedItem(String? namespaceUri, String localName)
        {
            var result = RemoveNamedItemOrDefault(namespaceUri, localName);

            if (result is null)
            {
                throw new DomException(DomError.NotFound);
            }

            return result;
        }


        /// <inheritdoc />
        public IEnumerator<IAttr> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        #endregion

        #region Helpers

        /// <summary>
        /// Gets the index if there is one to keep up to date.
        /// </summary>
        private Dictionary<StringOrMemory, Attr>? Index => (_items as IndexedItems)?.Index;

        private Attr? Find(StringOrMemory name)
        {
            var items = _items;

            if (items.Count > IndexThreshold)
            {
                var indexed = items as IndexedItems ?? Promote(items);
                return indexed.GetIndex().TryGetValue(name, out var attr) ? attr : null;
            }

            for (var i = 0; i < items.Count; i++)
            {
                if (name.Is(items[i].Name))
                {
                    return items[i];
                }
            }

            return null;
        }

        // Swapped in on a read, which a parsed document may see from several threads at once:
        // the old list stays intact for anyone still holding it, and only one copy is published.
        private IndexedItems Promote(List<Attr> items)
        {
            var indexed = new IndexedItems(items);
            Interlocked.CompareExchange(ref _items, indexed, items);
            return _items as IndexedItems ?? indexed;
        }

        private void Add(Attr attr)
        {
            var items = _items;
            items.Add(attr);

            // The tree builder comes here for every attribute it parses: a short list is never
            // indexed, so the count settles it before the type test.
            if (items.Count > IndexThreshold && items is IndexedItems { Index: { } index } indexed)
            {
                var name = attr.Name;

                if (!index.ContainsKey(name))
                {
                    index.Add(name, attr);
                }
                else
                {
                    indexed.HasDuplicateNames = true;
                }
            }
        }

        private void RemoveAt(Int32 position)
        {
            var attr = _items[position];
            _items.RemoveAt(position);

            if (_items is IndexedItems { Index: { } index } indexed)
            {
                // With a duplicate name the next attribute in order would have to take the entry
                // over; dropping the index, to be rebuilt by the next lookup, is simpler.
                if (indexed.HasDuplicateNames)
                {
                    indexed.Index = null;
                }
                else
                {
                    index.Remove(attr.Name);
                }
            }
        }

        private Attr? Prepare(IAttr item)
        {
            var attr = item as Attr;

            if (attr != null)
            {
                if (Object.ReferenceEquals(attr.Container, this))
                {
                    return null;
                }

                if (attr.Container != null)
                {
                    throw new DomException(DomError.InUse);
                }

                attr.Container = this;
            }

            return attr;
        }

        #endregion

        #region Indexed Items

        /// <summary>
        /// The attribute list once it is long enough to index: maps each qualified name to its
        /// first attribute. A name lookup is a scan otherwise, and the tree builder does one for
        /// every attribute it merges onto html or body - repeated start tags piling attributes
        /// up there made that quadratic.
        /// </summary>
        private sealed class IndexedItems : List<Attr>
        {
            public Dictionary<StringOrMemory, Attr>? Index;

            /// <summary>
            /// Set once two attributes share a qualified name, which the namespaced setter can
            /// produce.
            /// </summary>
            public Boolean HasDuplicateNames;

            public IndexedItems(List<Attr> items)
                : base(items)
            {
            }

            // Built on a read as well: each racing reader builds its own and one is published.
            public Dictionary<StringOrMemory, Attr> GetIndex()
            {
                var index = Index;

                if (index is null)
                {
                    index = new Dictionary<StringOrMemory, Attr>(Count, AngleSharp.Html.OrdinalStringOrMemoryComparer.Instance);

                    for (var i = 0; i < Count; i++)
                    {
                        var attr = this[i];
                        var name = attr.Name;

                        if (!index.ContainsKey(name))
                        {
                            index.Add(name, attr);
                        }
                        else
                        {
                            HasDuplicateNames = true;
                        }
                    }

                    index = Interlocked.CompareExchange(ref Index, index, null) ?? index;
                }

                return index;
            }
        }

        #endregion

        #region Construction

        IConstructableAttr? IConstructableNamedNodeMap.this[StringOrMemory name] => Find(name);

        Boolean IConstructableNamedNodeMap.SameAs(IConstructableNamedNodeMap? attributes)
        {
            if (attributes is null || attributes.Length != Length)
            {
                return false;
            }

            for (var i = 0; i < Length; i++)
            {
                var attr = _items[i];
                var other = attributes[attr.Name];

                if (other is null || !attr.Value.Is(other.Value))
                {
                    return false;
                }
            }

            return true;
        }

        // IEnumerator<IConstructableAttr> IEnumerable<IConstructableAttr>.GetEnumerator() =>
        //     _items.GetEnumerator();

        #endregion
    }
}
