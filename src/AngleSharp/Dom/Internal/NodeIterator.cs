namespace AngleSharp.Dom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The iterator through a collection of DOM nodes.
    /// </summary>
    sealed class NodeIterator : INodeIterator
    {
        #region Fields

        private readonly INode _root;
        private readonly FilterSettings _settings;
        private readonly NodeFilter _filter;
        private readonly IEnumerable<INode> _iterator;
        private INode _reference;
        private Boolean _beforeNode;

        #endregion

        #region ctor

        public NodeIterator(INode root, FilterSettings settings, NodeFilter filter)
        {
            _root = root;
            _settings = settings;
            _filter = filter ?? (m => FilterResult.Accept);
            _beforeNode = true;
            _iterator = GetNodes(root);
            _reference = root;
        }

        #endregion

        #region Properties

        public INode Root => _root;

        public FilterSettings Settings => _settings;

        public NodeFilter Filter => _filter;

        public INode Reference => _reference;

        public Boolean IsBeforeReference => _beforeNode;

        #endregion

        #region Methods

        public INode Next()
        {
            var node = _reference;
            var beforeNode = _beforeNode;
            
            do
            {
                if (!beforeNode)
                {
                    node = _iterator.SkipWhile(m => !Object.ReferenceEquals(m, node)).Skip(1).FirstOrDefault();
                }

                if (node == null)
                {
                    return null;
                }

                beforeNode = false;
            }
            while (!_settings.Accepts(node) || _filter.Invoke(node) != FilterResult.Accept);

            _beforeNode = false;
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
                {
                    node = _iterator.TakeWhile(m => !Object.ReferenceEquals(m, node)).LastOrDefault();
                }

                if (node == null)
                {
                    return null;
                }

                beforeNode = true;
            }
            while (!_settings.Accepts(node) || _filter(node) != FilterResult.Accept);

            _beforeNode = true;
            _reference = node;
            return node;
        }

        #endregion

        #region Helpers

        private static IEnumerable<INode> GetNodes(INode root)
        {
            yield return root;

            var children = root.GetNodes<INode>();

            foreach (var child in children)
            {
                yield return child;
            }
        }

        #endregion
    }
}
