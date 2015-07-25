using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Interfaces;

namespace AngleSharp.Dom.Collections
{
    /// <summary>
    /// NamedNodeNap is a key/value pair of nodes that can be accessed by numeric or string index
    /// </summary>
    sealed class NamedNodeMap : INamedNodeMap
    {
        #region Fields
        
        private readonly List<IAttr> _items = new List<IAttr>();

        #endregion

        #region Index

        public IAttr this[string name]
        {
            get { return _items.FirstOrDefault(i => i.Name == name); }
        }

        public IAttr this[int index]
        {
            get { return _items.ElementAtOrDefault(index); }
        }

        #endregion

        public int Length
        {
            get { return _items.Count; }
        }

        #region Methods

        public IAttr GetNamedItem(string name)
        {
            return this[name];
        }

        public void SetNamedItem(IAttr item)
        {
            RemoveNamedItem(item.Name);
            _items.Add(item);
        }

        public void RemoveNamedItem(string name)
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

        public IAttr GetNamedItemNS(string namespaceUri, string localName)
        {
            return _items.FirstOrDefault(attribute => attribute.NamespaceUri == namespaceUri && attribute.LocalName == localName);
        }

        public void SetNamedItemNS(IAttr item)
        {
            RemoveNamedItemNS(item.NamespaceUri, item.LocalName);
            _items.Add(item);
        }

        public void RemoveNamedItemNS(string namespaceUri, string localName)
        {
            _items.RemoveAll(i => i.NamespaceUri == namespaceUri && i.LocalName == localName);
        }
        
        #endregion
    }
}
