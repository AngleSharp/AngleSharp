namespace AngleSharp.Dom.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// NamedNodeNap is a key/value pair of nodes that can be accessed by
    /// numeric or string index.
    /// </summary>
    sealed class NamedNodeMap : INamedNodeMap
    {
        #region Fields
        
        readonly List<IAttr> _items;

        #endregion

        #region ctor

        public NamedNodeMap()
        {
            _items = new List<IAttr>();
        }

        #endregion

        #region Index

        public IAttr this[String name]
        {
            get { return GetNamedItem(name); }
        }

        public IAttr this[Int32 index]
        {
            get { return index >= 0 && index < _items.Count ? _items[index] : null; }
        }

        #endregion

        #region Properties

        public Int32 Length
        {
            get { return _items.Count; }
        }

        #endregion

        #region Methods

        public IAttr GetNamedItem(String name)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Name.Equals(name, StringComparison.Ordinal))
                    return _items[i];
            }

            return null;
        }

        public IAttr GetNamedItem(String namespaceUri, String localName)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (String.Equals(_items[i].LocalName, localName, StringComparison.Ordinal) &&
                    String.Equals(_items[i].NamespaceUri, namespaceUri, StringComparison.Ordinal))
                    return _items[i];
            }

            return null;
        }

        public IAttr SetNamedItem(IAttr item)
        {
            var name = item.Name;

            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Name.Equals(name, StringComparison.Ordinal))
                {
                    var attr = _items[i];
                    _items[i] = item;
                    return attr;
                }
            }

            _items.Add(item);
            return null;
        }

        public IAttr SetNamedItemWithNamespaceUri(IAttr item)
        {
            var localName = item.LocalName;
            var namespaceUri = item.NamespaceUri;

            for (int i = 0; i < _items.Count; i++)
            {
                if (String.Equals(_items[i].LocalName, localName, StringComparison.Ordinal) &&
                    String.Equals(_items[i].NamespaceUri, namespaceUri, StringComparison.Ordinal))
                {
                    var attr = _items[i];
                    _items[i] = item;
                    return attr;
                }
            }

            _items.Add(item);
            return null;
        }

        public IAttr RemoveNamedItem(String name)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Name.Equals(name, StringComparison.Ordinal))
                {
                    var attr = _items[i];
                    _items.RemoveAt(i);
                    return attr;
                }
            }

            throw new DomException(DomError.NotFound);
        }

        public IAttr RemoveNamedItem(String namespaceUri, String localName)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (String.Equals(_items[i].LocalName, localName, StringComparison.Ordinal) &&
                    String.Equals(_items[i].NamespaceUri, namespaceUri, StringComparison.Ordinal))
                {
                    var attr = _items[i];
                    _items.RemoveAt(i);
                    return attr;
                }
            }
            
            throw new DomException(DomError.NotFound);
        }

        public IEnumerator<IAttr> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
        
        #endregion
    }
}
