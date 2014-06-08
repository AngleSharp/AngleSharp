namespace AngleSharp.DOM.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The container for the attributes.
    /// </summary>
    public sealed class AttrContainer : IEnumerable<IAttr>
    {
        #region Fields

        readonly List<IAttr> _attributes;

        #endregion

        #region ctor

        internal AttrContainer()
        {
            _attributes = new List<IAttr>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of elements.
        /// </summary>
        public Int32 Count
        {
            get { return _attributes.Count; }
        }

        /// <summary>
        /// Gets the attribute with the specified index.
        /// </summary>
        /// <param name="index">The index of the attribute to get.</param>
        /// <returns>The attribute at the index.</returns>
        public IAttr this[Int32 index]
        {
            get { return _attributes[index]; }
        }

        /// <summary>
        /// Gets the attribute with the specified name.
        /// </summary>
        /// <param name="name">The name of the attribute to get.</param>
        /// <returns>The attribute with the given name.</returns>
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

        /// <summary>
        /// Gets the iterator over the attributes.
        /// </summary>
        /// <returns>The IEnumerator.</returns>
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
