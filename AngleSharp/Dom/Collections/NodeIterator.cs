namespace AngleSharp.Dom.Collections
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The iterator through a collection of DOM nodes.
    /// </summary>
    sealed class NodeIterator : INodeIterator
    {
        #region Fields

        readonly INode _root;
        readonly FilterSettings _settings;
        readonly NodeFilter _filter;
        readonly IEnumerable<INode> _iterator;
        INode _reference;
        Boolean _beforeNode;

        #endregion

        #region ctor

        public NodeIterator(INode root, FilterSettings settings, NodeFilter filter)
        {
            _root = root;
            _settings = settings;
            _filter = filter ?? (m => FilterResult.Accept);
            _beforeNode = true;
            _iterator = _root.GetElements<INode>(settings);
            _reference = _iterator.First();
        }

        #endregion

        #region Properties

        public INode Root
        {
            get { return _root; }
        }

        public FilterSettings Settings
        {
            get { return _settings; }
        }

        public NodeFilter Filter
        {
            get { return _filter; }
        }

        public INode Reference
        {
            get { return _reference; }
        }

        public Boolean IsBeforeReference
        {
            get { return _beforeNode; }
        }

        #endregion

        #region Methods

        public INode Next()
        {
            var node = _reference;
            var beforeNode = _beforeNode;
            
            do
            {
                if (!beforeNode)
                    node = _iterator.SkipWhile(m => m != node).Skip(1).FirstOrDefault();

                if (node == null)
                    return null;

                beforeNode = false;
            }
            while (_filter(node) != FilterResult.Accept);

            _beforeNode = beforeNode;
            _reference = node;
            return node;
        }

        public INode Previous()
        {
            var node = _reference;
            var beforeNode = _beforeNode;

            do
            {
                if (beforeNode)
                    node = _iterator.TakeWhile(m => m != node).LastOrDefault();

                if (node == null)
                    return null;

                beforeNode = true;
            }
            while (_filter(node) != FilterResult.Accept);

            _beforeNode = beforeNode;
            _reference = node;
            return node;
        }

        #endregion
    }
}
