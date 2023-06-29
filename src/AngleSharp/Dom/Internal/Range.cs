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
    sealed class Range : IRange
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

        public Boolean IsCollapsed => _start.Node == _end.Node;

        public INode CommonAncestor
        {
            get
            {
                var container = Head;

                while (container != null && !container.Contains(Tail))
                {
                    container = container.Parent;
                }

                return container!;
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

            if (!_end.IsExplicit)
            {
                _start = bp;
                _end = bp;
            }
            else if (bp > _end || Root != refNode.GetRoot())
            {
                _start = bp;
            }
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

            if (!_start.IsExplicit)
            {
                _start = bp;
                _end = bp;
            }
            else if (bp < _start || Root != refNode.GetRoot())
            {
                _end = bp;
            }
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

            _start = new Boundary(parent, parent.ChildNodes.Index(refNode));

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

            _end = new Boundary(parent, parent.ChildNodes.Index(refNode));

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
                    var nodesToRemove = Nodes.Where(m => !Intersects(m.Parent!)).ToArray();

                    if (!originalStart.Node.IsInclusiveAncestorOf(originalEnd.Node))
                    {
                        var referenceNode = originalStart.Node;

                        while (referenceNode.Parent != null && referenceNode.Parent.IsInclusiveAncestorOf(originalEnd.Node))
                        {
                            referenceNode = referenceNode.Parent;
                        }

                        newBoundary = new Boundary(referenceNode.Parent!, referenceNode.Parent!.ChildNodes.Index(referenceNode) + 1);
                    }
                    else
                    {
                        newBoundary = originalStart;
                    }

                    if (originalStart.Node is ICharacterData)
                    {
                        var strt = originalStart.Offset;
                        var text = (ICharacterData)originalStart.Node;
                        var span = originalEnd.Offset - originalStart.Offset;
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
                    var commonAncestor = originalStart.Node;

                    while (!commonAncestor.IsInclusiveAncestorOf(originalEnd.Node))
                    {
                        commonAncestor = commonAncestor.Parent!;
                    }

                    var firstPartiallyContainedChild = !originalStart.Node.IsInclusiveAncestorOf(originalEnd.Node) ?
                        commonAncestor.GetNodes<INode>(predicate: IsPartiallyContained).FirstOrDefault() : null;
                    var lastPartiallyContainedchild = !originalEnd.Node.IsInclusiveAncestorOf(originalStart.Node) ?
                        commonAncestor.GetNodes<INode>(predicate: IsPartiallyContained).LastOrDefault() : null;
                    var containedChildren = commonAncestor.GetNodes<INode>(predicate: Intersects).ToList();

                    if (containedChildren.OfType<IDocumentType>().Any())
                    {
                        throw new DomException(DomError.HierarchyRequest);
                    }

                    if (!originalStart.Node.IsInclusiveAncestorOf(originalEnd.Node))
                    {
                        var referenceNode = originalStart.Node;

                        while (referenceNode.Parent != null && !referenceNode.IsInclusiveAncestorOf(originalEnd.Node))
                        {
                            referenceNode = referenceNode.Parent;
                        }

                        newBoundary = new Boundary(referenceNode, referenceNode.Parent!.ChildNodes.Index(referenceNode) + 1);
                    }

                    if (firstPartiallyContainedChild is ICharacterData)
                    {
                        var text = (ICharacterData)originalStart.Node;
                        var strt = originalStart.Offset;
                        var span = text.Length - originalStart.Offset;
                        var clone = (ICharacterData)text.Clone();
                        clone.Data = text.Substring(strt, span);
                        fragment.AppendChild(clone);
                        text.Replace(strt, span, String.Empty);
                    }
                    else if (firstPartiallyContainedChild != null)
                    {
                        var clone = firstPartiallyContainedChild.Clone();
                        fragment.AppendChild(clone);
                        var subrange = new Range(originalStart, new Boundary(firstPartiallyContainedChild, firstPartiallyContainedChild.ChildNodes.Length));
                        var subfragment = subrange.ExtractContent();
                        fragment.AppendChild(subfragment);
                    }

                    foreach (var child in containedChildren)
                    {
                        fragment.AppendChild(child);
                    }

                    if (lastPartiallyContainedchild is ICharacterData)
                    {
                        var text = (ICharacterData)originalEnd.Node;
                        var clone = (ICharacterData)text.Clone();
                        clone.Data = text.Substring(0, originalEnd.Offset);
                        fragment.AppendChild(clone);
                        text.Replace(0, originalEnd.Offset, String.Empty);
                    }
                    else if (lastPartiallyContainedchild != null)
                    {
                        var clone = lastPartiallyContainedchild.Clone();
                        fragment.AppendChild(clone);
                        var subrange = new Range(new Boundary(lastPartiallyContainedchild, 0), originalEnd);
                        var subfragment = subrange.ExtractContent();
                        fragment.AppendChild(subfragment);
                    }

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
                    var commonAncestor = originalStart.Node;

                    while (!commonAncestor.IsInclusiveAncestorOf(originalEnd.Node))
                    {
                        commonAncestor = commonAncestor.Parent!;
                    }

                    var firstPartiallyContainedChild = !originalStart.Node.IsInclusiveAncestorOf(originalEnd.Node) ?
                        commonAncestor.GetNodes<INode>(predicate: IsPartiallyContained).FirstOrDefault() : null;
                    var lastPartiallyContainedchild = !originalEnd.Node.IsInclusiveAncestorOf(originalStart.Node) ?
                        commonAncestor.GetNodes<INode>(predicate: IsPartiallyContained).LastOrDefault() : null;
                    var containedChildren = commonAncestor.GetNodes<INode>(predicate: Intersects).ToList();

                    if (containedChildren.OfType<IDocumentType>().Any())
                    {
                        throw new DomException(DomError.HierarchyRequest);
                    }

                    if (firstPartiallyContainedChild is ICharacterData)
                    {
                        var text = (ICharacterData)originalStart.Node;
                        var strt = originalStart.Offset;
                        var span = text.Length - originalStart.Offset;
                        var clone = (ICharacterData)text.Clone();
                        clone.Data = text.Substring(strt, span);
                        fragment.AppendChild(clone);
                    }
                    else if (firstPartiallyContainedChild != null)
                    {
                        var clone = firstPartiallyContainedChild.Clone();
                        fragment.AppendChild(clone);
                        var subrange = new Range(originalStart, new Boundary(firstPartiallyContainedChild, firstPartiallyContainedChild.ChildNodes.Length));
                        var subfragment = subrange.CopyContent();
                        fragment.AppendChild(subfragment);
                    }

                    foreach (var child in containedChildren)
                    {
                        fragment.AppendChild(child.Clone());
                    }

                    if (lastPartiallyContainedchild is ICharacterData)
                    {
                        var text = (ICharacterData)originalEnd.Node;
                        var clone = (ICharacterData)text.Clone();
                        clone.Data = text.Substring(0, originalEnd.Offset);
                        fragment.AppendChild(clone);
                    }
                    else if (lastPartiallyContainedchild != null)
                    {
                        var clone = lastPartiallyContainedchild.Clone();
                        fragment.AppendChild(clone);
                        var subrange = new Range(new Boundary(lastPartiallyContainedchild, 0), originalEnd);
                        var subfragment = subrange.CopyContent();
                        fragment.AppendChild(subfragment);
                    }
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

                return !IsStartAfter(node, offset) && !IsEndBefore(node, offset);
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
                    thisPoint = _start;
                    otherPoint = new Boundary(sourceRange.Tail, sourceRange.End);
                    break;

                case RangeType.EndToStart:
                    thisPoint = _end;
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
                    return IsEndAfter(parent, offset) && IsStartBefore(parent, offset + 1);
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

        private Boolean IsPartiallyContained(INode node)
        {
            var startAncestor = node.IsInclusiveAncestorOf(_start.Node);
            var endAncestor = node.IsInclusiveAncestorOf(_end.Node);

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
                return false;
            }

            public static Boolean operator <(Boundary a, Boundary b)
            {
                return false;
            }

            public Boolean Equals(Boundary other)
            {
                return Node == other.Node && Offset == other.Offset;
            }

            public RangePosition CompareTo(Boundary other)
            {
                if (this < other)
                {
                    return RangePosition.Before;
                }
                else if (this > other)
                {
                    return RangePosition.After;
                }
                else
                {
                    return RangePosition.Equal;
                }
            }

            public INode? ChildAtOffset => Node.ChildNodes.Length > Offset ? Node.ChildNodes[Offset] : null;
        }

        #endregion
    }
}
