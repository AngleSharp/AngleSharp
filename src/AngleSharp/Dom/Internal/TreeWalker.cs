namespace AngleSharp.Dom
{
    /// <summary>
    /// The treewalker for walking through the DOM tree.
    /// </summary>
    sealed class TreeWalker : ITreeWalker
    {
        #region Fields

        private readonly INode _root;
        private readonly FilterSettings _settings;
        private readonly NodeFilter _filter;
        private INode _current;

        #endregion

        #region ctor

        public TreeWalker(INode root, FilterSettings settings, NodeFilter? filter)
        {
            _root = root;
            _settings = settings;
            _filter = filter ?? (_ => FilterResult.Accept);
            _current = _root;
        }

        #endregion

        #region Properties

        public INode Root => _root;

        public FilterSettings Settings => _settings;

        public NodeFilter Filter => _filter;

        public INode Current
        {
            get => _current;
            set => _current = value;
        }

        #endregion

        #region Methods

        public INode? ToNext()
        {
            var node = _current;
            var result = FilterResult.Accept;

            while (node is not null)
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

                while (node is not null && node != _root)
                {
                    var sibling = node!.NextSibling;

                    if (sibling != null)
                    {
                        node = sibling;
                        break;
                    }

                    node = node.Parent;
                }

                if (node is null || node == _root)
                {
                    break;
                }

                result = Check(node);

                if (result == FilterResult.Accept)
                {
                    _current = node;
                    return node;
                }
            }

            return null;
        }

        public INode? ToPrevious()
        {
            var node = _current;

            while (node is not null && node != _root)
            {
                var sibling = node.PreviousSibling;

                while (sibling is not null)
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
                {
                    break;
                }

                var parent = node.Parent;

                if (parent is null)
                {
                    break;
                }

                if (Check(node) == FilterResult.Accept)
                {
                    _current = node;
                    return node;
                }
            }

            return null;
        }

        public INode? ToParent()
        {
            var node = _current;

            while (node is not null && node != _root)
            {
                node = node.Parent;

                if (node is not null && Check(node) == FilterResult.Accept)
                {
                    _current = node;
                    return node;
                }
            }

            return null;
        }

        public INode? ToFirst()
        {
            var node = _current?.FirstChild;

            while (node is not null)
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

                    if (child is not null)
                    {
                        node = child;
                        continue;
                    }
                }

                while (node is not null)
                {
                    var sibling = node.NextSibling;

                    if (sibling is not null)
                    {
                        node = sibling;
                        break;
                    }

                    var parent = node.Parent;

                    if (parent is null || parent == _root || parent == _current)
                    {
                        node = null;
                        break;
                    }

                    node = parent;
                }
            }

            return null;
        }

        public INode? ToLast()
        {
            var node = _current?.LastChild;

            while (node is not null)
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

                    if (child is not null)
                    {
                        node = child;
                        continue;
                    }
                }

                while (node is not null)
                {
                    var sibling = node.PreviousSibling;

                    if (sibling is not null)
                    {
                        node = sibling;
                        break;
                    }

                    var parent = node.Parent;

                    if (parent is null || parent == _root || parent == _current)
                    {
                        node = null;
                        break;
                    }

                    node = parent;
                }
            }

            return null;
        }

        public INode? ToPreviousSibling()
        {
            var node = _current;

            if (node != _root)
            {
                while (node is not null)
                {
                    var sibling = node.PreviousSibling;

                    while (sibling is not null)
                    {
                        node = sibling;
                        var result = Check(node);

                        if (result == FilterResult.Accept)
                        {
                            _current = node;
                            return node;
                        }

                        sibling = node.LastChild;

                        if (result == FilterResult.Reject || sibling is null)
                        {
                            sibling = node.PreviousSibling;
                        }
                    }

                    node = node.Parent;

                    if (node is null || node == _root || Check(node) == FilterResult.Accept)
                    {
                        break;
                    }
                }
            }

            return null;
        }

        public INode? ToNextSibling()
        {
            var node = _current;

            if (node != _root)
            {
                while (node is not null)
                {
                    var sibling = node.NextSibling;

                    while (sibling is not null)
                    {
                        node = sibling;
                        var result = Check(node);

                        if (result == FilterResult.Accept)
                        {
                            _current = node;
                            return node;
                        }

                        sibling = node.FirstChild;

                        if (result == FilterResult.Reject || sibling is null)
                        {
                            sibling = node.NextSibling;
                        }
                    }

                    node = node.Parent;

                    if (node is null || node == _root || Check(node) == FilterResult.Accept)
                    {
                        break;
                    }
                }
            }

            return null;
        }

        #endregion

        #region Helpers

        private FilterResult Check(INode node)
        {
            if (!_settings.Accepts(node))
            {
                return FilterResult.Skip;
            }

            return _filter(node);
        }

        #endregion
    }
}
