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
            RemoveNamedItem(item);
            _items.Add(item);
        }

        public void RemoveNamedItem(IAttr item)
        {
            _items.RemoveAll(i => i.Name == item.Name);
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
