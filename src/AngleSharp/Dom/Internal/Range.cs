namespace AngleSharp.Dom
{
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A DOM range to gather DOM tree information.
    /// </summary>
    sealed class Range : IRange, IPreRemove, IPreInsert
    {
        #region Fields

        private Boundary _start;
        private Boundary _end;

        #endregion

        #region ctor

        public Range(IDocument document)
        {
            _start = new Boundary(document, 0, false);
            _end = new Boundary(document, 0, false);
        }

        private Range(Boundary start, Boundary end)
        {
            _start = start;
            _end = end;
        }

        #endregion

        #region Properties

        public INode Root => _start.Node.GetRoot();

        public IEnumerable<INode> Nodes => CommonAncestor.GetNodes<INode>(predicate: Intersects);

        public INode Head => _start.Node;

        public Int32 Start => _start.Offset;

        public INode Tail => _end.Node;

        public Int32 End => _end.Offset;

        public Boolean IsCollapsed => _start.Node == _end.Node && _start.Offset == _end.Offset;

        public INode CommonAncestor
        {
            get
            {
                var container = Head;

                while (container != null && !container.IsInclusiveAncestorOf(Tail))
                {
                    container = container.Parent;
                }

                return container!;
            }
        }

        #endregion

        #region Internal

        void IPreInsert.PreInsert(Node parent, Node node, Int32 index)
        {
            var count = node.NodeType == NodeType.DocumentFragment ? node.ChildNodes.Length : 1;

            if (Head == parent && Start > index)
            {
                StartWith(parent, Start + count);
            }

            if (Tail == parent && End > index)
            {
                EndWith(parent, End + count);
            }
        }

        void IPreRemove.PreRemove(Node parent, Node node, Int32 index)
        {
            if (Head.IsInclusiveDescendantOf(node))
            {
                StartWith(parent, index);
            }
            if (Tail.IsInclusiveDescendantOf(node))
            {
                EndWith(parent, index);
            }
            if (Head == parent && Start > index)
            {
                StartWith(parent, Start - 1);
            }
            if (Tail == parent && End > index)
            {
                EndWith(parent, End - 1);
            }
        }

        #endregion

        #region Methods

        public void StartWith(INode refNode, Int32 offset)
        {
            if (refNode is null)
            {
                throw new ArgumentNullException(nameof(refNode));
            }

            if (refNode.NodeType == NodeType.DocumentType)
            {
                throw new DomException(DomError.InvalidNodeType);
            }

            if (offset < 0)
            {
                throw new DomException(DomError.IndexSizeError);
            }

            var length = GetNodeLength(refNode);

            if (offset > length)
            {
                throw new DomException(DomError.IndexSizeError);
            }

            var bp = new Boundary(refNode, offset);

            if (!_end.IsExplicit || Root != refNode.GetRoot() || bp > _end)
            {
                _end = bp;
            }
            _start = bp;
        }

        public void EndWith(INode refNode, Int32 offset)
        {
            if (refNode is null)
            {
                throw new ArgumentNullException(nameof(refNode));
            }

            if (refNode.NodeType == NodeType.DocumentType)
            {
                throw new DomException(DomError.InvalidNodeType);
            }

            if (offset < 0)
            {
                throw new DomException(DomError.IndexSizeError);
            }

            var length = GetNodeLength(refNode);

            if (offset >length)
            {
                throw new DomException(DomError.IndexSizeError);
            }

            var bp = new Boundary(refNode, offset);

            if (!_start.IsExplicit || Root != refNode.GetRoot() || bp < _start)
            {
                _start = bp;
            }
            _end = bp;
        }

        public void StartBefore(INode refNode)
        {
            if (refNode is null)
            {
                throw new ArgumentNullException(nameof(refNode));
            }

            var parent = refNode.Parent;

            if (parent is null)
            {
                throw new DomException(DomError.InvalidNodeType);
            }

            _start = new Boundary(parent, parent.ChildNodes.Index(refNode));

            if (!_end.IsExplicit)
            {
                _end = _start;
            }
        }

        public void EndBefore(INode refNode)
        {
            if (refNode is null)
            {
                throw new ArgumentNullException(nameof(refNode));
            }

            var parent = refNode.Parent;

            if (parent is null)
            {
                throw new DomException(DomError.InvalidNodeType);
            }

            _end = new Boundary(parent, parent.ChildNodes.Index(refNode));

            if (!_start.IsExplicit)
            {
                _start = _end;
            }
        }

        public void StartAfter(INode refNode)
        {
            if (refNode is null)
            {
                throw new ArgumentNullException(nameof(refNode));
            }

            var parent = refNode.Parent;

            if (parent is null)
            {
                throw new DomException(DomError.InvalidNodeType);
            }

            _start = new Boundary(parent, parent.ChildNodes.Index(refNode) + 1);

            if (!_end.IsExplicit)
            {
                _end = _start;
            }
        }

        public void EndAfter(INode refNode)
        {
            if (refNode is null)
            {
                throw new ArgumentNullException(nameof(refNode));
            }

            var parent = refNode.Parent;

            if (parent is null)
            {
                throw new DomException(DomError.InvalidNodeType);
            }

            _end = new Boundary(parent, parent.ChildNodes.Index(refNode) + 1);

            if (!_start.IsExplicit)
            {
                _start = _end;
            }
        }

        public void Collapse(Boolean toStart)
        {
            if (toStart)
            {
                _end = _start;
            }
            else
            {
                _start = _end;
            }
        }

        public void Select(INode refNode)
        {
            if (refNode is null)
            {
                throw new ArgumentNullException(nameof(refNode));
            }

            var parent = refNode.Parent;

            if (parent is null)
            {
                throw new DomException(DomError.InvalidNodeType);
            }

            var index = parent.ChildNodes.Index(refNode);
            _start = new Boundary(parent, index);
            _end = new Boundary(parent, index + 1);
        }

        public void SelectContent(INode refNode)
        {
            if (refNode is null)
            {
                throw new ArgumentNullException(nameof(refNode));
            }

            if (refNode.NodeType == NodeType.DocumentType)
            {
                throw new DomException(DomError.InvalidNodeType);
            }

            var length = refNode.ChildNodes.Length;
            _start = new Boundary(refNode, 0);
            _end = new Boundary(refNode, length);
        }

        public void ClearContent()
        {
            if (!_start.Equals(_end))
            {
                var newBoundary = new Boundary();
                var originalStart = _start;
                var originalEnd = _end;

                if (originalEnd.Node == originalStart.Node && originalStart.Node is ICharacterData)
                {
                    var strt = originalStart.Offset;
                    var text = (ICharacterData)originalStart.Node;
                    var span = originalEnd.Offset - originalStart.Offset;
                    text.Replace(strt, span, String.Empty);
                }
                else
                {
                    var nodesToRemove = CommonAncestor.GetNodes<INode>(predicate: (node)=>Contains(node,0)).Where(m => !Contains(m.Parent!, 0)).ToArray();

                    if (!originalStart.Node.IsInclusiveAncestorOf(originalEnd.Node))
                    {
                        var referenceNode = originalStart.Node;

                        while (referenceNode.Parent != null && !referenceNode.Parent.IsInclusiveAncestorOf(originalEnd.Node))
                        {
                            referenceNode = referenceNode.Parent;
                        }

                        newBoundary = new Boundary(referenceNode.Parent!, referenceNode.Index() + 1);
                    }
                    else
                    {
                        newBoundary = originalStart;
                    }

                    if (originalStart.Node is ICharacterData)
                    {
                        var strt = originalStart.Offset;
                        var text = (ICharacterData)originalStart.Node;
                        var span = text.Data.Length - originalStart.Offset;
                        text.Replace(strt, span, String.Empty);
                    }

                    foreach (var node in nodesToRemove)
                    {
                        node.Parent!.RemoveChild(node);
                    }

                    if (originalEnd.Node is ICharacterData)
                    {
                        var strt = 0;
                        var text = (ICharacterData)originalEnd.Node;
                        var span = originalEnd.Offset;
                        text.Replace(strt, span, String.Empty);
                    }

                    _start = newBoundary;
                    _end = newBoundary;
                }
            }
        }

        public IDocumentFragment ExtractContent()
        {
            var fragment = _start.Node.Owner!.CreateDocumentFragment();

            if (!_start.Equals(_end))
            {
                var newBoundary = _start;
                var originalStart = _start;
                var originalEnd = _end;

                if (originalStart.Node == originalEnd.Node && _start.Node is ICharacterData)
                {
                    var text = (ICharacterData)originalStart.Node;
                    var strt = originalStart.Offset;
                    var span = originalEnd.Offset - originalStart.Offset;
                    var clone = (ICharacterData)text.Clone();
                    clone.Data = text.Substring(strt, span);
                    fragment.AppendChild(clone);
                    text.Replace(strt, span, String.Empty);
                }
                else
                {
                    if (!originalStart.Node.IsInclusiveAncestorOf(originalEnd.Node))
                    {
                        var referenceNode = originalStart.Node;

                        while (referenceNode.Parent != null && !referenceNode.Parent.IsInclusiveAncestorOf(originalEnd.Node))
                        {
                            referenceNode = referenceNode.Parent;
                        }

                        newBoundary = new Boundary(referenceNode.Parent!, referenceNode.Index() + 1);
                    }

                    CopyContent(fragment, originalStart, originalEnd, extract: true);
                    _start = newBoundary;
                    _end = newBoundary;
                }
            }

            return fragment;
        }

        public IDocumentFragment CopyContent()
        {
            var fragment = _start.Node.Owner!.CreateDocumentFragment();

            if (!_start.Equals(_end))
            {
                var originalStart = _start;
                var originalEnd = _end;

                if (originalStart.Node == originalEnd.Node && _start.Node is ICharacterData)
                {
                    var text = (ICharacterData)originalStart.Node;
                    var strt = originalStart.Offset;
                    var span = originalEnd.Offset - originalStart.Offset;
                    var clone = (ICharacterData)text.Clone();
                    clone.Data = text.Substring(strt, span);
                    fragment.AppendChild(clone);
                }
                else
                {
                    CopyContent(fragment, originalStart, originalEnd, extract: false);
                }
            }

            return fragment;
        }

        public void Insert(INode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var snode = _start.Node;
            var type = snode.NodeType;
            var istext = type == NodeType.Text;

            if (type == NodeType.ProcessingInstruction || type == NodeType.Comment || (istext && snode.Parent is null))
            {
                throw new DomException(DomError.HierarchyRequest);
            }

            var referenceNode = istext ? snode : _start.ChildAtOffset;
            var parent = referenceNode is null ? snode : referenceNode.Parent;
            parent!.EnsurePreInsertionValidity(node, referenceNode);

            if (istext)
            {
                referenceNode = ((IText)snode).Split(_start.Offset);
                parent = referenceNode.Parent;
            }

            if (node == referenceNode)
            {
                referenceNode = referenceNode.NextSibling;
            }

            node.Parent?.RemoveChild(node);
            var newOffset = referenceNode is null ? parent!.ChildNodes.Length : parent!.ChildNodes.Index(referenceNode);
            newOffset += node.NodeType == NodeType.DocumentFragment ? node.ChildNodes.Length : 1;
            parent.PreInsert(node, referenceNode);

            if (_start.Equals(_end))
            {
                _end = new Boundary(parent, newOffset);
            }
        }

        public void Surround(INode newParent)
        {
            if (newParent is null)
            {
                throw new ArgumentNullException(nameof(newParent));
            }

            if (Nodes.Any(m => m.NodeType != NodeType.Text && IsPartiallyContained(m)))
            {
                throw new DomException(DomError.InvalidState);
            }

            var type = newParent.NodeType;

            if (type == NodeType.Document || type == NodeType.DocumentType || type == NodeType.DocumentFragment)
            {
                throw new DomException(DomError.InvalidNodeType);
            }

            var fragment = ExtractContent();

            while (newParent.HasChildNodes)
            {
                newParent.RemoveChild(newParent.FirstChild);
            }

            Insert(newParent);
            newParent.PreInsert(fragment, null);
            Select(newParent);
        }

        public IRange Clone()
        {
            return new Range(_start, _end);
        }

        public void Detach()
        {
            //Does nothing.
        }

        public Boolean Contains(INode node, Int32 offset)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (node.GetRoot() == Root)
            {
                if (node.NodeType == NodeType.DocumentType)
                {
                    throw new DomException(DomError.InvalidNodeType);
                }

                if (offset > node.ChildNodes.Length)
                {
                    throw new DomException(DomError.IndexSizeError);
                }
                var length = node is IDocumentType || node is IAttr ? 0 : (node is ICharacterData charData ? charData.Data.Length : node.ChildNodes.Length);
                return new Boundary(node, offset) > _start && new Boundary(node, length) < _end;
            }

            return false;
        }

        public RangePosition CompareBoundaryTo(RangeType how, IRange sourceRange)
        {
            if (sourceRange is null)
            {
                throw new ArgumentNullException(nameof(sourceRange));
            }

            if (Root != sourceRange.Head.GetRoot())
            {
                throw new DomException(DomError.WrongDocument);
            }

            var thisPoint = default(Boundary);
            var otherPoint = default(Boundary);

            switch (how)
            {
                case RangeType.StartToStart:
                    thisPoint = _start;
                    otherPoint = new Boundary(sourceRange.Head, sourceRange.Start);
                    break;

                case RangeType.StartToEnd:
                    thisPoint = _end;
                    otherPoint = new Boundary(sourceRange.Head, sourceRange.Start);
                    break;

                case RangeType.EndToEnd:
                    thisPoint = _end;
                    otherPoint = new Boundary(sourceRange.Tail, sourceRange.End);
                    break;

                case RangeType.EndToStart:
                    thisPoint = _start;
                    otherPoint = new Boundary(sourceRange.Tail, sourceRange.End);
                    break;

                default:
                    throw new DomException(DomError.NotSupported);
            }

            return thisPoint.CompareTo(otherPoint);
        }

        public RangePosition CompareTo(INode node, Int32 offset)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (Root != _start.Node.GetRoot())
            {
                throw new DomException(DomError.WrongDocument);
            }

            if (node.NodeType == NodeType.DocumentType)
            {
                throw new DomException(DomError.InvalidNodeType);
            }

            if (offset > node.ChildNodes.Length)
            {
                throw new DomException(DomError.IndexSizeError);
            }

            if (IsStartAfter(node, offset))
            {
                return RangePosition.Before;
            }
            else if (IsEndBefore(node, offset))
            {
                return RangePosition.After;
            }

            return RangePosition.Equal;
        }

        public Boolean Intersects(INode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (Root == node.GetRoot())
            {
                var parent = node.Parent;

                if (parent != null)
                {
                    var offset = parent.ChildNodes.Index(node);
                    return new Boundary(parent, offset) < _end && new Boundary(parent, offset + 1) > _start;
                }

                return true;
            }

            return false;
        }

        public override String ToString()
        {
            var sb = default(StringBuilder);
            var offset = Start;
            var dest = End;

            if (Head is IText startText)
            {
                if (Head == Tail)
                {
                    return startText.Substring(offset, dest - offset);
                }
                else
                {
                    sb ??= StringBuilderPool.Obtain();

                    sb.Append(startText.Substring(offset, startText.Length - offset));
                }
            }

            sb ??= StringBuilderPool.Obtain();

            var nodes = CommonAncestor.Descendants<IText>();

            foreach (var node in nodes)
            {
                if (IsStartBefore(node, 0) && IsEndAfter(node, node.Length))
                {
                    sb.Append(node.Text);
                }
            }

            if (Tail is IText endText)
            {
                sb.Append(endText.Substring(0, dest));
            }

            return sb.ToPool();
        }

        #endregion

        #region Helpers

        // The spec extracts and clones a range recursively, once for every level between a
        // boundary and the common ancestor, so a deeply nested boundary would overflow the stack.
        // Below the common ancestor only one partially contained child exists per level: the
        // subrange towards the start ends at its own container, the one towards the end starts
        // there. The levels therefore form two chains, which are walked in loops instead.
        private static void CopyContent(INode fragment, Boundary start, Boundary end, Boolean extract)
        {
            var contained = GetContainedChildren(start, end, out var first, out var last);

            if (first is not null)
            {
                CopyStartChain(fragment, start, first, extract);
            }

            CopyChildren(fragment, contained, extract);

            if (last is not null)
            {
                CopyEndChain(fragment, last, end, extract);
            }
        }

        private static void CopyStartChain(INode container, Boundary start, INode child, Boolean extract)
        {
            // The recursion moves the contained children of a level only once the levels below
            // it are done, so they are collected on the way down and moved on the way up to keep
            // the document's mutations in the order the spec makes them.
            var levels = new List<KeyValuePair<INode, List<INode>>>();

            while (child is not ICharacterData)
            {
                var clone = child.Clone(false);
                container.AppendChild(clone);
                var contained = GetContainedChildren(start, new Boundary(child, child.ChildNodes.Length), out var next, out _);
                levels.Add(new KeyValuePair<INode, List<INode>>(clone, contained));
                container = clone;

                if (next is null)
                {
                    break;
                }

                child = next;
            }

            if (child is ICharacterData)
            {
                var text = (ICharacterData)start.Node;
                var strt = start.Offset;
                var span = text.Length - start.Offset;
                var clone = (ICharacterData)text.Clone();
                clone.Data = text.Substring(strt, span);
                container.AppendChild(clone);

                if (extract)
                {
                    text.Replace(strt, span, String.Empty);
                }
            }

            for (var i = levels.Count - 1; i >= 0; i--)
            {
                CopyChildren(levels[i].Key, levels[i].Value, extract);
            }
        }

        private static void CopyEndChain(INode container, INode child, Boundary end, Boolean extract)
        {
            while (child is not ICharacterData)
            {
                var clone = child.Clone(false);
                container.AppendChild(clone);
                var contained = GetContainedChildren(new Boundary(child, 0), end, out _, out var next);
                CopyChildren(clone, contained, extract);
                container = clone;

                if (next is null)
                {
                    return;
                }

                child = next;
            }

            var text = (ICharacterData)end.Node;
            var textClone = (ICharacterData)text.Clone();
            textClone.Data = text.Substring(0, end.Offset);
            container.AppendChild(textClone);

            if (extract)
            {
                text.Replace(0, end.Offset, String.Empty);
            }
        }

        private static void CopyChildren(INode container, List<INode> children, Boolean extract)
        {
            foreach (var child in children)
            {
                container.AppendChild(extract ? child : child.Clone());
            }
        }

        private static List<INode> GetContainedChildren(Boundary start, Boundary end, out INode? first, out INode? last)
        {
            var commonAncestor = start.Node;

            while (!commonAncestor.IsInclusiveAncestorOf(end.Node))
            {
                commonAncestor = commonAncestor.Parent!;
            }

            var children = commonAncestor.ChildNodes;
            var contained = new List<INode>();
            first = null;
            last = null;

            for (var i = 0; i < children.Length; i++)
            {
                var child = children[i];

                if (IsPartiallyContained(child, start, end))
                {
                    first ??= child;
                    last = child;
                }
                else if (new Boundary(child, 0) > start && new Boundary(child, GetNodeLength(child)) < end)
                {
                    if (child.NodeType == NodeType.DocumentType)
                    {
                        throw new DomException(DomError.HierarchyRequest);
                    }

                    contained.Add(child);
                }
            }

            if (start.Node.IsInclusiveAncestorOf(end.Node))
            {
                first = null;
            }

            if (end.Node.IsInclusiveAncestorOf(start.Node))
            {
                last = null;
            }

            return contained;
        }

        private static Int32 GetNodeLength(INode node)
        {
            if (node is IDocumentType || node is IAttr)
            {
                return 0;
            }

            if (node is ICharacterData text)
            {
                return text.Data.Length;
            }

            return node.ChildNodes.Length;
        }

        private Boolean IsStartBefore(INode node, Int32 offset)
        {
            return _start < new Boundary(node, offset);
        }

        private Boolean IsStartAfter(INode node, Int32 offset)
        {
            return _start > new Boundary(node, offset);
        }

        private Boolean IsEndBefore(INode node, Int32 offset)
        {
            return _end < new Boundary(node, offset);
        }

        private Boolean IsEndAfter(INode node, Int32 offset)
        {
            return _end > new Boundary(node, offset);
        }

        private Boolean IsPartiallyContained(INode node) => IsPartiallyContained(node, _start, _end);

        private static Boolean IsPartiallyContained(INode node, Boundary start, Boundary end)
        {
            var startAncestor = node.IsInclusiveAncestorOf(start.Node);
            var endAncestor = node.IsInclusiveAncestorOf(end.Node);

            return (startAncestor && !endAncestor) || (!startAncestor && endAncestor);
        }

        #endregion

        #region Boundary

        private readonly struct Boundary : IEquatable<Boundary>
        {
            public Boundary(INode node, Int32 offset, Boolean given = true)
            {
                Node = node;
                Offset = offset;
                IsExplicit = given;
            }

            public readonly INode Node;
            public readonly Int32 Offset;
            public readonly Boolean IsExplicit;

            public static Boolean operator >(Boundary a, Boundary b)
            {
                return a.CompareTo(b) == RangePosition.After;
            }
            public static Boolean operator <(Boundary a, Boundary b)
            {
                return a.CompareTo(b) == RangePosition.Before;
            }
            public static Boolean operator ==(Boundary a, Boundary b)
            {
                return a.CompareTo(b) == RangePosition.Equal;
            }
            public static Boolean operator !=(Boundary a, Boundary b)
            {
                return a.CompareTo(b) != RangePosition.Equal;
            }
            public static Boolean operator <=(Boundary a, Boundary b)
            {
                switch(a.CompareTo(b))
                {
                    case RangePosition.Before:
                        return true;
                    case RangePosition.Equal:
                        return true;
                }
                return false;
            }
            public static Boolean operator >=(Boundary a, Boundary b)
            {
                switch (a.CompareTo(b))
                {
                    case RangePosition.After:
                        return true;
                    case RangePosition.Equal:
                        return true;
                }
                return false;
            }
            public Boolean Equals(Boundary other)
            {
                return CompareTo(other) == RangePosition.Equal;
            }

            public RangePosition CompareTo(Boundary other)
            {
                if (Node.GetRoot() != other.Node.GetRoot())
                {
                    throw new DomException(DomError.InvalidAccess);
                }
                if (Node == other.Node)
                {
                    if (Offset < other.Offset)
                    {
                        return RangePosition.Before;
                    }
                    else if (Offset == other.Offset)
                    {
                        return RangePosition.Equal;
                    }
                    else if (Offset > other.Offset)
                    {
                        return RangePosition.After;
                    }
                }
                if (Node.IsFollowing(other.Node))
                {
                    var position = other.CompareTo(this);
                    if (position == RangePosition.Before)
                    {
                        return RangePosition.After;
                    }
                    else if (position == RangePosition.After)
                    {
                        return RangePosition.Before;
                    }
                }
                if (Node.IsAncestorOf(other.Node))
                {
                    var child = other.Node;
                    while (child != null && child.Parent != Node)
                    {
                        child = child.Parent;
                    }
                    if (child != null && child.Index() < Offset)
                    {
                        return RangePosition.After;
                    }
                }

                return RangePosition.Before;
            }

            public INode? ChildAtOffset => Node.ChildNodes.Length > Offset ? Node.ChildNodes[Offset] : null;

            public override Boolean Equals(Object? obj)
            {
                return obj is Boundary && Equals((Boundary)obj);
            }

            public override Int32 GetHashCode()
            {
                unchecked
                {
                    var hash = 17;
                    hash = hash * 23 + Node.GetHashCode();
                    hash = hash * 23 + Offset.GetHashCode();
                    hash = hash * 23 + IsExplicit.GetHashCode();
                    return hash;
                }
            }
        }

        #endregion
    }
}
