namespace AngleSharp.Dom.Collections
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// The treewalker for walking through the DOM tree.
    /// </summary>
    sealed class TreeWalker : ITreeWalker
    {
        #region Fields

        readonly INode _root;
        readonly FilterSettings _settings;
        readonly NodeFilter _filter;
        INode _current;

        #endregion

        #region ctor

        public TreeWalker(INode root, FilterSettings settings, NodeFilter filter)
        {
            _root = root;
            _settings = settings;
            _filter = filter ?? (m => FilterResult.Accept);
            _current = _root;
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

        public INode Current
        {
            get { return _current; }
            set { _current = value; }
        }

        #endregion

        #region Methods

        public INode ToNext()
        {
            var node = _current;
            var result = FilterResult.Accept;

            while (node != null)
            {
                while (result != FilterResult.Reject && node.HasChildNodes)
                {
                    node = node.FirstChild;
                    result = Check(node);

                    if (result == FilterResult.Accept)
                    {
                        _current = node;
                        return node;
                    }
                }

                while (node != _root)
                {
                    var sibling = node.NextSibling;

                    if (sibling != null)
                    {
                        node = sibling;
                        break;
                    }

                    node = node.Parent;
                }

                if (node == _root)
                    break;

                result = Check(node);

                if (result == FilterResult.Accept)
                {
                    _current = node;
                    return node;
                }
            }

            return null;
        }

        public INode ToPrevious()
        {
            var node = _current;

            while (node != null && node != _root)
            {
                var sibling = node.PreviousSibling;

                while (sibling != null)
                {
                    node = sibling;
                    var result = Check(node);

                    while (result != FilterResult.Reject && node.HasChildNodes)
                    {
                        node = node.LastChild;
                        result = Check(node);

                        if (result == FilterResult.Accept)
                        {
                            _current = node;
                            return node;
                        }
                    }
                }

                if (node == _root)
                    break;

                var parent = node.Parent;

                if (parent == null)
                    break;

                if (Check(node) == FilterResult.Accept)
                {
                    _current = node;
                    return node;
                }
            }

            return null;
        }

        public INode ToParent()
        {
            var node = _current;

            while (node != null && node != _root)
            {
                node = node.Parent;

                if (node != null && Check(node) == FilterResult.Accept)
                {
                    _current = node;
                    return node;
                }
            }

            return null;
        }

        public INode ToFirst()
        {
            var node = _current != null ? _current.FirstChild : null;

            while (node != null)
            {
                var result = Check(node);

                if (result == FilterResult.Accept)
                {
                    _current = node;
                    return node;
                }
                else if (result == FilterResult.Skip)
                {
                    var child = node.FirstChild;

                    if (child != null)
                    {
                        node = child;
                        continue;
                    }
                }

                while (node != null)
                {
                    var sibling = node.NextSibling;

                    if (sibling != null)
                    {
                        node = sibling;
                        break;
                    }

                    var parent = node.Parent;

                    if (parent == null || parent == _root || parent == _current)
                        return null;

                    node = parent;
                }
            }

            return null;
        }

        public INode ToLast()
        {
            var node = _current != null ? _current.LastChild : null;

            while (node != null)
            {
                var result = Check(node);

                if (result == FilterResult.Accept)
                {
                    _current = node;
                    return node;
                }
                else if (result == FilterResult.Skip)
                {
                    var child = node.LastChild;

                    if (child != null)
                    {
                        node = child;
                        continue;
                    }
                }

                while (node != null)
                {
                    var sibling = node.PreviousSibling;

                    if (sibling != null)
                    {
                        node = sibling;
                        break;
                    }

                    var parent = node.Parent;

                    if (parent == null || parent == _root || parent == _current)
                        return null;

                    node = parent;
                }
            }

            return null;
        }

        public INode ToPreviousSibling()
        {
            var node = _current;

            if (node == _root)
                return null;

            while (node != null)
            {
                var sibling = node.PreviousSibling;

                while (sibling != null)
                {
                    node = sibling;
                    var result = Check(node);

                    if (result == FilterResult.Accept)
                    {
                        _current = node;
                        return node;
                    }

                    sibling = node.LastChild;

                    if (result == FilterResult.Reject || sibling == null)
                        sibling = node.PreviousSibling;
                }

                node = node.Parent;

                if (node == null || node == _root || Check(node) == FilterResult.Accept)
                    break;
            }

            return null;
        }

        public INode ToNextSibling()
        {
            var node = _current;

            if (node == _root)
                return null;

            while (node != null)
            {
                var sibling = node.NextSibling;

                while (sibling != null)
                {
                    node = sibling;
                    var result = Check(node);

                    if (result == FilterResult.Accept)
                    {
                        _current = node;
                        return node;
                    }

                    sibling = node.FirstChild;

                    if (result == FilterResult.Reject || sibling == null)
                        sibling = node.NextSibling;
                }

                node = node.Parent;

                if (node == null || node == _root || Check(node) == FilterResult.Accept)
                    break;
            }

            return null;
        }

        #endregion

        #region Helpers

        FilterResult Check(INode node)
        {
            if (!_settings.Accepts(node))
                return FilterResult.Skip;

            return _filter(node);
        }

        #endregion
    }
}
