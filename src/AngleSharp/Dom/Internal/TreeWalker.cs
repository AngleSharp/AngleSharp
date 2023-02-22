namespace AngleSharp.Dom
{
    /// <summary>
    /// The treewalker for walking through the DOM tree.
    /// </summary>
    sealed class TreeWalker : ITreeWalker
    {
        #region Fields

        #endregion

        #region ctor

        public TreeWalker(INode root, FilterSettings settings, NodeFilter? filter)
        {
            Root = root;
            Settings = settings;
            Filter = filter ?? (_ => FilterResult.Accept);
            Current = Root;
        }

        #endregion

        #region Properties

        public INode Root { get; }

        public FilterSettings Settings { get; }

        public NodeFilter Filter { get; }

        public INode Current { get; set; }

        #endregion

        #region Methods

        public INode? ToNext()
        {
            var node = Current;
            var result = FilterResult.Accept;

            while (node is not null)
            {
                while (result != FilterResult.Reject && node.HasChildNodes)
                {
                    node = node.FirstChild;
                    result = Check(node);

                    if (result == FilterResult.Accept)
                    {
                        Current = node;
                        return node;
                    }
                }

                while (node is not null && node != Root)
                {
                    var sibling = node!.NextSibling;

                    if (sibling != null)
                    {
                        node = sibling;
                        break;
                    }

                    node = node.Parent;
                }

                if (node is null || node == Root)
                {
                    break;
                }

                result = Check(node);

                if (result == FilterResult.Accept)
                {
                    Current = node;
                    return node;
                }
            }

            return null;
        }

        public INode? ToPrevious()
        {
            var node = Current;

            while (node is not null && node != Root)
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
                            Current = node;
                            return node;
                        }
                    }
                }

                if (node == Root)
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
                    Current = node;
                    return node;
                }
            }

            return null;
        }

        public INode? ToParent()
        {
            var node = Current;

            while (node is not null && node != Root)
            {
                node = node.Parent;

                if (node is not null && Check(node) == FilterResult.Accept)
                {
                    Current = node;
                    return node;
                }
            }

            return null;
        }

        public INode? ToFirst()
        {
            var node = Current?.FirstChild;

            while (node is not null)
            {
                var result = Check(node);

                if (result == FilterResult.Accept)
                {
                    Current = node;
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

                    if (parent is null || parent == Root || parent == Current)
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
            var node = Current?.LastChild;

            while (node is not null)
            {
                var result = Check(node);

                if (result == FilterResult.Accept)
                {
                    Current = node;
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

                    if (parent is null || parent == Root || parent == Current)
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
            var node = Current;

            if (node != Root)
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
                            Current = node;
                            return node;
                        }

                        sibling = node.LastChild;

                        if (result == FilterResult.Reject || sibling is null)
                        {
                            sibling = node.PreviousSibling;
                        }
                    }

                    node = node.Parent;

                    if (node is null || node == Root || Check(node) == FilterResult.Accept)
                    {
                        break;
                    }
                }
            }

            return null;
        }

        public INode? ToNextSibling()
        {
            var node = Current;

            if (node != Root)
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
                            Current = node;
                            return node;
                        }

                        sibling = node.FirstChild;

                        if (result == FilterResult.Reject || sibling is null)
                        {
                            sibling = node.NextSibling;
                        }
                    }

                    node = node.Parent;

                    if (node is null || node == Root || Check(node) == FilterResult.Accept)
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
            if (!Settings.Accepts(node))
            {
                return FilterResult.Skip;
            }

            return Filter(node);
        }

        #endregion
    }
}
