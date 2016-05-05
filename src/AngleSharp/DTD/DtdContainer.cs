using AngleSharp.DOM;
using AngleSharp.DOM.Xml;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AngleSharp.DTD
{
    /// <summary>
    /// The DTD container contains the whole DTD.
    /// </summary>
    sealed class DtdContainer : ICollection<Node>
    {
        #region Members

        Bin<Notation> _notations;
        Bin<Entity> _entities;
        Bin<Entity> _parameters;
        Bin<AttributeDeclaration> _attributes;
        Bin<ElementDeclaration> _elements;
        List<Node> _nodes;
        Boolean _invalid;
        DtdContainer _parent;
        List<DtdContainer> _children;
        String _url;

        #endregion

        #region ctor

        public DtdContainer()
        {
            _nodes = new List<Node>();
            _children = new List<DtdContainer>();
            _parameters = new Bin<Entity>(this, false, m => m.NodeName);
            _notations = new Bin<Notation>(this, false, m => m.NodeName);
            _entities = new Bin<Entity>(this, false, m => m.NodeName);
            _notations = new Bin<Notation>(this, false, m => m.NodeName);
            _attributes = new Bin<AttributeDeclaration>(this, false, m => m.Name);
            _elements = new Bin<ElementDeclaration>(this, true, m => m.Name);
        }

        public DtdContainer(DtdContainer parent)
            : this()
        {
            _parent = parent;
            _parent._children.Add(this);
        }

        #endregion

        #region Index

        public Node this[Int32 index]
        {
            get { return _nodes[index]; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the DTD is invalid.
        /// </summary>
        public Boolean IsInvalid
        {
            get { return _invalid; }
            private set
            {
                _invalid = value;

                if (_parent != null)
                    _parent.IsInvalid = value;
            }
        }

        /// <summary>
        /// Gets or sets the XML document parent if any.
        /// </summary>
        public XMLDocument Parent 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets the url of the container.
        /// </summary>
        public String Url
        {
            get { return _url ?? (Parent != null ? Parent.BaseURI : String.Empty); }
            set { _url = value; }
        }

        /// <summary>
        /// Gets the root container.
        /// </summary>
        public DtdContainer Root
        {
            get { return (_parent != null) ? _parent.Root : this; }
        }

        /// <summary>
        /// Gets the number of rules / nodes in this DTD (without the parent).
        /// </summary>
        public Int32 Count
        {
            get { return _nodes.Count; }
        }

        /// <summary>
        /// Gets the text of this DTD (without the parents text).
        /// </summary>
        public String Text
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the enumeration over all the contained (self and parent) notations.
        /// </summary>
        public IEnumerable<Notation> Notations
        {
            get { return _notations.Items(m => m._notations); }
        }

        /// <summary>
        /// Gets the enumeration over all the contained (self and parent) entities.
        /// </summary>
        public IEnumerable<Entity> Entities
        {
            get { return _entities.Items(m => m._entities); }
        }

        /// <summary>
        /// Gets the enumeration over all the contained (self and parent) parameters.
        /// </summary>
        public IEnumerable<Entity> Parameters
        {
            get { return _parameters.Items(m => m._parameters); }
        }

        /// <summary>
        /// Gets the enumeration over all the contained (self and parent) attributes.
        /// </summary>
        public IEnumerable<AttributeDeclaration> Attributes
        {
            get { return _attributes.Items(m => m._attributes); }
        }

        /// <summary>
        /// Gets the enumeration over all the contained (self and parent) elements.
        /// </summary>
        public IEnumerable<ElementDeclaration> Elements
        {
            get { return _elements.Items(m => m._elements); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the entity with the given name.
        /// </summary>
        /// <param name="name">The name of the entity.</param>
        /// <returns>The entity or null if no such entity exists.</returns>
        public Entity GetEntity(String name)
        {
            var entities = Root.Entities;

            foreach (var entity in entities)
            {
                if (entity.NodeName == name)
                    return entity;
            }

            return null;
        }

        /// <summary>
        /// Gets the parameter entity with the given name.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>The entity or null if no such entity exists.</returns>
        public Entity GetParameter(String name)
        {
            var parameters = Root.Parameters;

            foreach (var parameter in parameters)
            {
                if (parameter.NodeName == name)
                    return parameter;
            }

            return null;
        }

        #endregion

        #region Internal Methods

        internal void Reset()
        {
            _parameters.Reset();
            _attributes.Reset();
            _elements.Reset();
            _entities.Reset();
            _notations.Reset();
        }

        internal Boolean ContainsEntity(String name)
        {
            return _entities.Contains(name);
        }

        internal Boolean ContainsParameter(String name)
        {
            return _parameters.Contains(name);
        }

        internal Boolean ContainsAttribute(String name)
        {
            return _attributes.Contains(name);
        }

        internal Boolean ContainsElement(String name)
        {
            return _elements.Contains(name);
        }

        internal Boolean ContainsNotation(String name)
        {
            return _notations.Contains(name);
        }

        internal void AddNotation(Notation notation)
        {
            _notations.Add(notation);
        }

        internal void AddEntity(Entity entity)
        {
            _entities.Add(entity);
        }

        internal void AddParameter(Entity entity)
        {
            _parameters.Add(entity);
        }

        internal void AddAttribute(AttributeDeclaration attribute)
        {
            _attributes.Add(attribute);
        }

        internal void AddElement(ElementDeclaration element)
        {
            _elements.Add(element);
        }

        #endregion

        #region IEnumerable implementation

        public IEnumerator<Node> GetEnumerator()
        {
            foreach (var node in _nodes)
                yield return node;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region ICollection implementation

        public void Add(Node item)
        {
            if (item is Notation)
                AddNotation((Notation)item);
            else if (item is Entity)
                AddEntity((Entity)item);
            else if (item is ElementDeclaration)
                AddElement((ElementDeclaration)item);
            else if (item is AttributeDeclaration)
                AddAttribute((AttributeDeclaration)item);
        }

        public void Clear()
        {
            Reset();
        }

        public Boolean Contains(Node item)
        {
            return _nodes.Contains(item);
        }

        public void CopyTo(Node[] array, Int32 arrayIndex)
        {
            for (int i = arrayIndex, j = 0; i < array.Length && j < _nodes.Count; i++, j++)
                array[i] = _nodes[j];
        }

        public Boolean IsReadOnly
        {
            get { return false; }
        }

        public Boolean Remove(Node item)
        {
            return false;
        }

        #endregion

        #region Bin

        sealed class Bin<T> : IEnumerable<T>
            where T : Node
        {
            DtdContainer _container;
            List<T> _list;
            Boolean _unique;
            Func<T, String> _select;

            public Bin(DtdContainer container, Boolean unique, Func<T, String> select)
            {
                _unique = unique;
                _container = container;
                _list = new List<T>();
                _select = select;
            }

            /// <summary>
            /// Gets the enumeration over all the contained (self and parent) notations.
            /// </summary>
            public IEnumerable<T> Items(Func<DtdContainer, Bin<T>> source)
            {
                foreach (var item in _list)
                    yield return item;

                if(_container._children.Count != 0)
                {
                    foreach (var child in _container._children)
                    {
                        var bin = source(child);

                        foreach (var item in bin)
                            yield return item;
                    }
                }
            }

            /// <summary>
            /// Checks if the given name exists in the current bin.
            /// </summary>
            /// <param name="name">The name to check for.</param>
            /// <returns>True if such an item exists, otherwise false.</returns>
            public Boolean Contains(String name)
            {
                foreach (var item in _list)
                    if (_select(item) == name)
                        return true;

                return false;
            }

            /// <summary>
            /// Adds the item to the bin if the uniqueness contraint (if set) is fulfilled.
            /// </summary>
            /// <param name="item">The item to add.</param>
            public void Add(T item)
            {
                if (_unique && Contains(_select(item)))
                {
                    _container.IsInvalid = true;
                    return;
                }

                _list.Add(item);
                _container._nodes.Add(item);
            }

            /// <summary>
            /// Resets the current bin.
            /// </summary>
            public void Reset()
            {
                foreach (var entry in _list)
                    _container._nodes.Remove(entry);

                _list.Clear();
            }

            /// <summary>
            /// Gets the enumerator over the items of the current bin.
            /// </summary>
            /// <returns>The specific enumerator.</returns>
            public IEnumerator<T> GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            /// <summary>
            /// Gets the non-specific enumerator over all the items.
            /// </summary>
            /// <returns>The non-specific enumerator.</returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)_list).GetEnumerator();
            }
        }

        #endregion
    }
}
