namespace AngleSharp.DOM.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The container for the attributes.
    /// </summary>
    sealed class AttrContainer : IEnumerable<IAttr>
    {
        #region Fields

        readonly List<IAttr> _attributes;

        #endregion

        #region Events

        public event EventHandler<AttrChangedEventArgs> Changed;

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
            get { return _attributes.FirstOrDefault(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase)); }
        }

        #endregion

        #region Methods

        internal void Add(IAttr attribute)
        {
            _attributes.Add(attribute);
            RaiseChanged(attribute.Name, attribute.Value);
        }

        internal Boolean Has(String name)
        {
            for (int i = 0; i < _attributes.Count; i++)
            {
                if (_attributes[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        internal void RemoveAt(Int32 index)
        {
            var name = _attributes[index].Name;
            _attributes.RemoveAt(index);
            RaiseChanged(name, null);
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

        internal void RaiseChanged(String name, String value)
        {
            if (Changed != null)
                Changed(this, new AttrChangedEventArgs(name, value));
        }

        #endregion

        #region Event Arguments

        public sealed class AttrChangedEventArgs : EventArgs
        {
            public AttrChangedEventArgs(String name, String value)
            {
                Name = name;
                Value = value;
            }

            /// <summary>
            /// Gets the name of the changed attribute.
            /// </summary>
            public String Name
            {
                get;
                private set;
            }

            /// <summary>
            /// Gets the new value of the changed attribute.
            /// </summary>
            public String Value
            {
                get;
                private set;
            }
        }

        #endregion
    }
}
