using AngleSharp.DOM;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AngleSharp.DTD
{
    /// <summary>
    /// The DTD container contains the whole DTD.
    /// </summary>
    sealed class DtdContainer : IEnumerable<Node>
    {
        #region Members

        List<Notation> _notations;
        List<ProcessingInstruction> _pis;
        List<Comment> _comments;
        List<Entity> _entities;
        List<AttributeDeclaration> _attributes;
        List<ElementDeclaration> _elements;
        List<Node> _nodes;

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

        #region Methods

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

        internal void AddAttribute(AttributeDeclaration attribute)
        {
            _nodes.Add(attribute);
            _attributes.Add(attribute);
        }

        internal void AddElement(ElementDeclaration element)
        {
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
    }
}
