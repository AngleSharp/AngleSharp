namespace AngleSharp.Dom
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The iterator through a collection of DOM nodes.
    /// </summary>
    sealed class NodeIterator : INodeIterator, IPreRemove
    {
        #region Fields

        private readonly INode _root;
        private readonly FilterSettings _settings;
        private readonly NodeFilter _filter;
        private readonly IEnumerable<INode> _iterator;
        private INode _reference;
        private Boolean _beforeNode;
        private INode? _candidate;
        private Boolean _candidateBefore;

        #endregion

        #region ctor

        public NodeIterator(INode root, FilterSettings settings, NodeFilter? filter)
        {
            _root = root;
            _settings = settings;
            _filter = filter ?? (_ => FilterResult.Accept);
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

        #region Internal

        void IPreRemove.PreRemove(Node parent, Node node, Int32 index)
        {
            // Algorithm according to:
            // https://dom.spec.whatwg.org/#nodeiterator-pre-removing-steps
            // which also applies to the node a filter is looking at (e.g. one removing it), as
            // the iterator continues from there once it has been accepted (Acid3 test 2)
            Adjust(parent, node, ref _reference, ref _beforeNode);

            if (_candidate is not null)
            {
                Adjust(parent, node, ref _candidate, ref _candidateBefore);
            }
        }

        private void Adjust(Node parent, Node node, ref INode reference, ref Boolean beforeNode)
        {
            if (Root != node && node.IsInclusiveAncestorOf(reference))
            {
                if (beforeNode)
                {
                    var next = Root.GetDescendants()
                        .SkipWhile(m => !Object.ReferenceEquals(m, node))
                        .FirstOrDefault(m => !m.IsInclusiveDescendantOf(node));

                    if (next is not null)
                    {
                        reference = next;
                        return;
                    }

                    beforeNode = false;
                }

                if (node.PreviousSibling is null)
                {
                    reference = parent;
                }
                else
                {
                    reference = node.PreviousSibling.GetDescendantsAndSelf().Last();
                }
            }
        }

        #endregion

        #region Methods

        public INode? Next() => Traverse(forward: true);

        public INode? Previous() => Traverse(forward: false);

        private INode? Traverse(Boolean forward)
        {
            _candidate = _reference;
            _candidateBefore = _beforeNode;

            try
            {
                while (true)
                {
                    if (forward && !_candidateBefore)
                    {
                        var current = _candidate;
                        var next = _iterator.SkipWhile(m => !Object.ReferenceEquals(m, current)).Skip(1).FirstOrDefault();

                        if (next is null)
                        {
                            return null;
                        }

                        _candidate = next;
                    }
                    else if (!forward && _candidateBefore)
                    {
                        var current = _candidate;
                        var previous = _iterator.TakeWhile(m => !Object.ReferenceEquals(m, current)).LastOrDefault();

                        if (previous is null)
                        {
                            return null;
                        }

                        _candidate = previous;
                    }

                    _candidateBefore = !forward;
                    var node = _candidate;

                    if (_settings.Accepts(node) && _filter.Invoke(node) == FilterResult.Accept)
                    {
                        // the filter may have removed the node, which moved the candidate
                        _reference = _candidate;
                        _beforeNode = _candidateBefore;
                        return node;
                    }
                }
            }
            finally
            {
                _candidate = null;
            }
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
