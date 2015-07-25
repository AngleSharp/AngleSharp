namespace AngleSharp.Dom.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// NamedNodeNap is a key/value pair of nodes that can be accessed by numeric or string index
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
            get { return _items.FirstOrDefault(i => i.Name == name); }
        }

        public IAttr this[Int32 index]
        {
            get { return _items.ElementAtOrDefault(index); }
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
            return this[name];
        }

        public void SetNamedItem(IAttr item)
        {
            RemoveNamedItem(item.Name);
            _items.Add(item);
        }

        public void RemoveNamedItem(String name)
        {
            _items.RemoveAll(i => i.Name == name);
        }

        public IEnumerator<IAttr> GetEnumerator()
        {
            return _items.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public IAttr GetNamedItem(String namespaceUri, String localName)
        {
            return _items.FirstOrDefault(attribute => attribute.NamespaceUri == namespaceUri && attribute.LocalName == localName);
        }

        public void SetNamedItemWithNamespaceUri(IAttr item)
        {
            RemoveNamedItem(item.NamespaceUri, item.LocalName);
            _items.Add(item);
        }

        public void RemoveNamedItem(String namespaceUri, String localName)
        {
            _items.RemoveAll(i => i.NamespaceUri == namespaceUri && i.LocalName == localName);
        }
        
        #endregion
    }
}
