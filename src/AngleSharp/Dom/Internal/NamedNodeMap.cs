namespace AngleSharp.Dom
{
    using AngleSharp.Text;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Common;
    using Html.Construction;

    /// <summary>
    /// NamedNodeNap is a key/value pair of nodes that can be accessed by
    /// numeric or string index.
    /// </summary>
    sealed class NamedNodeMap : INamedNodeMap, IConstructableNamedNodeMap
    {
        #region Fields

        private readonly List<Attr> _items;
        private readonly WeakReference<Element> _owner;

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
        internal void FastAddItem(Attr attr) => _items.Add(attr);

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
            for (var i = 0; i < _items.Count; i++)
            {
                if (name.Is(_items[i].Name))
                {
                    var attr = _items[i];
                    _items.RemoveAt(i);
                    attr.Container = null;
                    NotifyChanged(attr, null, attr.Value, suppressMutationObservers);
                    return attr;
                }
            }

            return null;
        }

        internal IAttr? RemoveNamedItemOrDefault(String name) => RemoveNamedItemOrDefault(name, false);

        internal IAttr? RemoveNamedItemOrDefault(String? namespaceUri, String localName, Boolean suppressMutationObservers)
        {
            for (var i = 0; i < _items.Count; i++)
            {
                if (localName.Is(_items[i].LocalName) && namespaceUri.Is(_items[i].NamespaceUri))
                {
                    var attr = _items[i];
                    _items.RemoveAt(i);
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
        public IAttr? GetNamedItem(String name)
        {
            for (var i = 0; i < _items.Count; i++)
            {
                if (name.Is(_items[i].Name))
                {
                    return _items[i];
                }
            }

            return null;
        }

        /// <inheritdoc />
        public IAttr? GetNamedItem(StringOrMemory name)
        {
            for (var i = 0; i < _items.Count; i++)
            {
                if (name.Is(_items[i].Name))
                {
                    return _items[i];
                }
            }

            return null;
        }

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
                var name = item.Name;

                for (var i = 0; i < _items.Count; i++)
                {
                    if (name.Is(_items[i].Name))
                    {
                        var attr = _items[i];
                        _items[i] = proposed;
                        RaiseChangedEvent(proposed, proposed.Value, attr.Value);
                        return attr;
                    }
                }

                _items.Add(proposed);
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
                        NotifyChanged(proposed, proposed.Value, attr.Value, suppressMutationObservers);
                        return attr;
                    }
                }

                _items.Add(proposed);
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

        IConstructableAttr? IConstructableNamedNodeMap.this[StringOrMemory name]
        {
            get
            {
                for (var i = 0; i < _items.Count; i++)
                {
                    if (name.Is(_items[i].Name))
                    {
                        return _items[i];
                    }
                }

                return null;
            }
        }

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
