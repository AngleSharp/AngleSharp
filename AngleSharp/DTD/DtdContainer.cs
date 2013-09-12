using AngleSharp.DOM;
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

        List<Notation> _notations;
        List<ProcessingInstruction> _pis;
        List<Comment> _comments;
        List<Entity> _entities;
        List<AttributeDeclaration> _attributes;
        List<ElementDeclaration> _elements;
        List<Node> _nodes;
        Boolean _invalid;

        #endregion

        #region ctor

        public DtdContainer()
        {
            _nodes = new List<Node>();
            _notations = new List<Notation>();
            _pis = new List<ProcessingInstruction>();
            _comments = new List<Comment>();
            _entities = new List<Entity>();
            _notations = new List<Notation>();
            _attributes = new List<AttributeDeclaration>();
            _elements = new List<ElementDeclaration>();
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
        }

        /// <summary>
        /// Gets the nu
        /// </summary>
        public Int32 Count
        {
            get { return _nodes.Count; }
        }

        public String Text
        {
            get;
            set;
        }

        public IEnumerable<Notation> Notations
        {
            get
            {
                foreach (var notation in _notations)
                    yield return notation;
            }
        }

        public IEnumerable<ProcessingInstruction> ProcessingInstructions
        {
            get
            {
                foreach (var pi in _pis)
                    yield return pi;
            }
        }

        public IEnumerable<Comment> Comments
        {
            get
            {
                foreach (var comment in _comments)
                    yield return comment;
            }
        }

        public IEnumerable<Entity> Entities
        {
            get
            {
                foreach (var entity in _entities)
                    yield return entity;
            }
        }

        public IEnumerable<AttributeDeclaration> Attributes
        {
            get
            {
                foreach (var attribute in _attributes)
                    yield return attribute;
            }
        }

        public IEnumerable<ElementDeclaration> Elements
        {
            get
            {
                foreach (var element in _elements)
                    yield return element;
            }
        }

        #endregion

        #region Public Methods

        public Entity GetEntity(String name)
        {
            for (int i = 0; i < _entities.Count; i++)
            {
                if (_entities[i].NodeName == name)
                    return _entities[i];
            }

            return null;
        }

        #endregion

        #region Internal Methods

        internal void FillWith(DtdContainer external)
        {
            foreach (var entity in external.Entities)
            {
                if (!ContainsEntity(entity.NotationName))
                    AddEntity(entity);
            }

            foreach (var notation in external.Notations)
            {
                if (!ContainsNotation(notation.PublicId))
                    AddNotation(notation);
            }

            foreach (var attribute in external.Attributes)
            {
                if (!ContainsAttribute(attribute.Name))
                    AddAttribute(attribute);
            }

            foreach (var element in external.Elements)
            {
                if (!ContainsElement(element.Name))
                    AddElement(element);
            }
        }

        internal void Reset()
        {
            _attributes.Clear();
            _comments.Clear();
            _elements.Clear();
            _entities.Clear();
            _nodes.Clear();
            _notations.Clear();
            _pis.Clear();
        }

        internal Boolean ContainsNotation(String name)
        {
            foreach (var notation in _notations)
                if (notation.PublicId == name)
                    return true;

            return false;
        }

        internal void AddNotation(Notation notation)
        {
            _nodes.Add(notation);
            _notations.Add(notation);
        }

        internal void AddComment(Comment comment)
        {
            _nodes.Add(comment);
            _comments.Add(comment);
        }

        internal Boolean ContainsEntity(String name)
        {
            foreach (var entity in _entities)
                if (entity.NotationName == name)
                    return true;

            return false;
        }

        internal void AddEntity(Entity entity)
        {
            _nodes.Add(entity);
            _entities.Add(entity);
        }

        internal void AddProcessingInstruction(ProcessingInstruction pi)
        {
            _nodes.Add(pi);
            _pis.Add(pi);
        }

        internal Boolean ContainsAttribute(String name)
        {
            foreach (var attr in _attributes)
                if (attr.Name == name)
                    return true;

            return false;
        }

        internal void AddAttribute(AttributeDeclaration attribute)
        {
            _nodes.Add(attribute);
            _attributes.Add(attribute);
        }

        internal Boolean ContainsElement(String name)
        {
            foreach (var el in _elements)
                if (el.Name == name)
                    return true;

            return false;
        }

        internal void AddElement(ElementDeclaration element)
        {
            if (ContainsElement(element.Name))
                _invalid = true;

            _nodes.Add(element);
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
            else if (item is Comment)
                AddComment((Comment)item);
            else if (item is Entity)
                AddEntity((Entity)item);
            else if (item is ProcessingInstruction)
                AddProcessingInstruction((ProcessingInstruction)item);
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
    }
}
