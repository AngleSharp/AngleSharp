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

        private readonly IEnumerable<INode> _iterator;

        #endregion

        #region ctor

        public NodeIterator(INode root, FilterSettings settings, NodeFilter? filter)
        {
            Root = root;
            Settings = settings;
            Filter = filter ?? (_ => FilterResult.Accept);
            IsBeforeReference = true;
            _iterator = GetNodes(root);
            Reference = root;
        }

        #endregion

        #region Properties

        public INode Root { get; }

        public FilterSettings Settings { get; }

        public NodeFilter Filter { get; }

        public INode Reference { get; private set; }

        public Boolean IsBeforeReference { get; private set; }

        #endregion

        #region Methods

        public INode? Next()
        {
            var node = Reference;
            var beforeNode = IsBeforeReference;
            
            do
            {
                if (!beforeNode)
                {
                    node = _iterator.SkipWhile(m => !Object.ReferenceEquals(m, node)).Skip(1).FirstOrDefault();
                }

                if (node is null)
                {
                    return null;
                }

                beforeNode = false;
            }
            while (!Settings.Accepts(node) || Filter.Invoke(node) != FilterResult.Accept);

            IsBeforeReference = false;
            Reference = node;
            return node;
        }

        public INode? Previous()
        {
            var node = Reference;
            var beforeNode = IsBeforeReference;

            do
            {
                if (beforeNode)
                {
                    node = _iterator.TakeWhile(m => !Object.ReferenceEquals(m, node)).LastOrDefault();
                }

                if (node is null)
                {
                    return null;
                }

                beforeNode = true;
            }
            while (!Settings.Accepts(node) || Filter(node) != FilterResult.Accept);

            IsBeforeReference = true;
            Reference = node;
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
