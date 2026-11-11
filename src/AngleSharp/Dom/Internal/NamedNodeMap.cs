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
        /// The attributes in order, in the first <see cref="_count"/> slots. Every element owns a
        /// map, so the storage is the array itself rather than a list wrapping one: the shared
        /// empty array until there is an attribute, and exactly as long as the tag's attribute
        /// list when the tree builder fills it (see <see cref="Reserve"/>).
        /// </summary>
        private Attr[] _items;

        /// <summary>
        /// Maps each qualified name to its first attribute, once a name lookup found more than
        /// <see cref="IndexThreshold"/> of them; null until then, and whenever it has to be
        /// rebuilt. A name lookup is a scan otherwise, and the tree builder does one for every
        /// attribute it merges onto html or body - repeated start tags piling attributes up
        /// there made that quadratic.
        /// </summary>
        private Dictionary<StringOrMemory, Attr>? _index;

        // A plain reference: the element owns the map, so the two are collected together anyway,
        // and a weak one costs a GC handle and a finalizable object for every element of a tree.
        private readonly Element _owner;
        private Int32 _count;

        /// <summary>
        /// Set once two attributes share a qualified name, which the namespaced setter can
        /// produce.
        /// </summary>
        private Boolean _hasDuplicateNames;

        private const Int32 IndexThreshold = 16;

        #endregion

        #region ctor

        /// <inheritdoc />
        public NamedNodeMap(Element owner)
        {
            _items = [];
            _owner = owner;
        }

        #endregion

        #region Index

        /// <inheritdoc />
        public IAttr? this[String name] => GetNamedItem(name);

        /// <inheritdoc />
        public IAttr? this[Int32 index] => (UInt32)index < (UInt32)_count ? _items[index] : null;

        #endregion

        #region Properties

        /// <inheritdoc />
        public Int32 Length => _count;

        public Element? Owner => _owner;

        /// <summary>
        /// Gets the attributes in order, for the loops that must not allocate an enumerator.
        /// </summary>
        internal ReadOnlySpan<Attr> Items => new(_items, 0, _count);

        #endregion

        #region Internal Methods

        // Construction only: no duplicate check, no attribute change steps, no mutation record and
        // no advance of the document's mutation version - the tree builder reaches this for every
        // attribute of a parsed document. See Element.AddAttribute and SetAttributes; a mutation of
        // an existing element goes through SetNamedItem and NotifyChanged below instead.
        internal void FastAddItem(Attr attr) => Add(attr);

        /// <summary>
        /// Makes room for the given number of attributes ahead of adding them, so that a tag's
        /// attribute list is stored in an array of exactly its length.
        /// </summary>
        internal void Reserve(Int32 capacity)
        {
            if (capacity > _items.Length)
            {
                var items = new Attr[capacity];
                Array.Copy(_items, items, _count);
                _items = items;
            }
        }

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
            var element = _owner;
            element.Owner?.MarkMutated();

            if (!suppressMutationObservers)
            {
                element.AttributeChanged(attr.LocalName, attr.NamespaceUri, oldValue, newValue);
            }
        }

        internal IAttr? RemoveNamedItemOrDefault(String name, Boolean suppressMutationObservers)
        {
            var attr = Find(name);

            if (attr is not null)
            {
                RemoveAt(IndexOf(attr));
                attr.Container = null;
                NotifyChanged(attr, null, attr.Value, suppressMutationObservers);
            }

            return attr;
        }

        internal IAttr? RemoveNamedItemOrDefault(String name) => RemoveNamedItemOrDefault(name, false);

        internal IAttr? RemoveNamedItemOrDefault(String? namespaceUri, String localName, Boolean suppressMutationObservers)
        {
            for (var i = 0; i < _count; i++)
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
            for (var i = 0; i < _count; i++)
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
                    _items[IndexOf(attr)] = proposed;

                    if (_index is { } index)
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

                for (var i = 0; i < _count; i++)
                {
                    if (localName.Is(_items[i].LocalName) && namespaceUri.Is(_items[i].NamespaceUri))
                    {
                        var attr = _items[i];
                        _items[i] = proposed;

                        if (_index is { } index)
                        {
                            var name = attr.Name;

                            // The prefix, and with it the qualified name, may differ; and with a
                            // duplicate name the entry may belong to an earlier attribute.
                            if (!name.Is(proposed.Name))
                            {
                                _index = null;
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
        public IEnumerator<IAttr> GetEnumerator()
        {
            for (var i = 0; i < _count; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Helpers

        private Attr? Find(StringOrMemory name)
        {
            var count = _count;

            if (count > IndexThreshold)
            {
                return GetIndex().TryGetValue(name, out var attr) ? attr : null;
            }

            var items = _items;

            for (var i = 0; i < count; i++)
            {
                if (name.Is(items[i].Name))
                {
                    return items[i];
                }
            }

            return null;
        }

        // Built on a read, which a parsed document may see from several threads at once: each
        // racing reader builds its own and one is published.
        private Dictionary<StringOrMemory, Attr> GetIndex()
        {
            var index = _index;

            if (index is null)
            {
                var count = _count;
                var items = _items;
                index = new Dictionary<StringOrMemory, Attr>(count, AngleSharp.Html.OrdinalStringOrMemoryComparer.Instance);

                for (var i = 0; i < count; i++)
                {
                    var attr = items[i];
                    var name = attr.Name;

                    if (!index.ContainsKey(name))
                    {
                        index.Add(name, attr);
                    }
                    else
                    {
                        _hasDuplicateNames = true;
                    }
                }

                index = Interlocked.CompareExchange(ref _index, index, null) ?? index;
            }

            return index;
        }

        private Int32 IndexOf(Attr attr) => Array.IndexOf(_items, attr, 0, _count);

        private void Add(Attr attr)
        {
            var items = _items;
            var count = _count;

            if (count == items.Length)
            {
                items = new Attr[count == 0 ? 4 : count * 2];
                Array.Copy(_items, items, count);
                _items = items;
            }

            items[count] = attr;
            _count = count + 1;

            // The tree builder comes here for every attribute it parses, and nearly no map is
            // ever indexed.
            if (_index is { } index)
            {
                var name = attr.Name;

                if (!index.ContainsKey(name))
                {
                    index.Add(name, attr);
                }
                else
                {
                    _hasDuplicateNames = true;
                }
            }
        }

        private void RemoveAt(Int32 position)
        {
            var items = _items;
            var attr = items[position];
            var count = _count - 1;
            Array.Copy(items, position + 1, items, position, count - position);
            items[count] = null!;
            _count = count;

            if (_index is { } index)
            {
                // With a duplicate name the next attribute in order would have to take the entry
                // over; dropping the index, to be rebuilt by the next lookup, is simpler.
                if (_hasDuplicateNames)
                {
                    _index = null;
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

        #region Construction

        IConstructableAttr? IConstructableNamedNodeMap.this[StringOrMemory name] => Find(name);

        Boolean IConstructableNamedNodeMap.SameAs(IConstructableNamedNodeMap? attributes)
        {
            if (attributes is null || attributes.Length != Length)
            {
                return false;
            }

            for (var i = 0; i < _count; i++)
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
