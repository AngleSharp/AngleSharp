namespace AngleSharp.DOM.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class AttrContainer : IEnumerable<IAttr>
    {
        #region Fields

        readonly List<IAttr> _attributes;

        #endregion

        #region ctor

        public AttrContainer()
        {
            _attributes = new List<IAttr>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of elements.
        /// </summary>
        public Int32 Length
        {
            get { return _attributes.Count; }
        }

        /// <summary>
        /// Gets the number of elements.
        /// </summary>
        public Int32 Count
        {
            get { return _attributes.Count; }
        }

        public IAttr this[Int32 index]
        {
            get { return _attributes[index]; }
        }

        public IAttr this[String name]
        {
            get { return _attributes.Where(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault(); }
        }

        #endregion

        #region Methods

        internal void Add(IAttr attribute)
        {
            _attributes.Add(attribute);
        }

        internal void RemoveAt(Int32 index)
        {
            _attributes.RemoveAt(index);
        }

        public IEnumerator<IAttr> GetEnumerator()
        {
            return _attributes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
