namespace AngleSharp.DOM.Collections
{
    using System;
using System.Collections.Generic;
using System.Linq;

    /// <summary>
    /// A DOM range to gather DOM tree information.
    /// </summary>
    sealed class Range : IRange
    {
        #region Fields

        Boundary _start;
        Boundary _end;

        #endregion

        #region ctor

        public Range(IDocument document)
        {
            _start = new Boundary { Offset = 0, Node = document };
            _end = new Boundary { Offset = 0, Node = document };
        }

        private Range(Boundary start, Boundary end)
        {
            _start = start;
            _end = end;
        }

        #endregion

        #region Properties

        public INode Root
        {
            get { return _start.Node.GetRoot(); }
        }

        public IEnumerable<INode> Nodes
        {
            get { return CommonAncestor.GetElements<INode>(predicate: Intersects); }
        }

        public INode Head
        {
            get { return _start.Node; }
        }

        public Int32 Start
        {
            get { return _start.Offset; }
        }

        public INode Tail
        {
            get { return _end.Node; }
        }

        public Int32 End
        {
            get { return _end.Offset; }
        }

        public Boolean IsCollapsed
        {
            get { return _start.Node == _end.Node; }
        }

        public INode CommonAncestor
        {
            get 
            {
                var container = Head;

                while (container != null && !Tail.Contains(container))
                    container = container.Parent;

                return container;
            }
        }

        #endregion

        #region Methods

        public void StartWith(INode refNode, Int32 offset)
        {
            if (refNode is IDocumentType)
                throw new DomException(ErrorCode.InvalidNodeType);
            else if (offset > refNode.ChildNodes.Length)
                throw new DomException(ErrorCode.IndexSizeError);

            var bp = new Boundary { Node = refNode, Offset = offset };

            if (bp > _end || Root != refNode.GetRoot())
                _start = bp;
        }

        public void EndWith(INode refNode, Int32 offset)
        {
            if (refNode is IDocumentType)
                throw new DomException(ErrorCode.InvalidNodeType);
            else if (offset > refNode.ChildNodes.Length)
                throw new DomException(ErrorCode.IndexSizeError);

            var bp = new Boundary { Node = refNode, Offset = offset };

            if (bp < _start || Root != refNode.GetRoot())
                _end = bp;
        }

        public void StartBefore(INode refNode)
        {
            var parent = refNode.Parent;

            if (parent == null)
                throw new DomException(ErrorCode.InvalidNodeType);

            _start = new Boundary { Node = parent, Offset = parent.ChildNodes.Index(refNode) };
        }

        public void EndBefore(INode refNode)
        {
            var parent = refNode.Parent;

            if (parent == null)
                throw new DomException(ErrorCode.InvalidNodeType);

            _end = new Boundary { Node = parent, Offset = parent.ChildNodes.Index(refNode) };
        }

        public void StartAfter(INode refNode)
        {
            var parent = refNode.Parent;

            if (parent == null)
                throw new DomException(ErrorCode.InvalidNodeType);

            _start = new Boundary { Node = parent, Offset = parent.ChildNodes.Index(refNode) + 1 };
        }

        public void EndAfter(INode refNode)
        {
            var parent = refNode.Parent;

            if (parent == null)
                throw new DomException(ErrorCode.InvalidNodeType);

            _end = new Boundary { Node = parent, Offset = parent.ChildNodes.Index(refNode) + 1 };
        }

        public void Collapse(Boolean toStart)
        {
            if (toStart)
                _end = _start;
            else
                _start = _end;
        }

        public void Select(INode refNode)
        {
            var parent = refNode.Parent;

            if (parent == null)
                throw new DomException(ErrorCode.InvalidNodeType);

            var index = parent.ChildNodes.Index(refNode);
            _start = new Boundary { Node = parent, Offset = index };
            _end = new Boundary { Node = parent, Offset = index + 1 };
        }

        public void SelectContents(INode refNode)
        {
            if (refNode is IDocumentType)
                throw new DomException(ErrorCode.InvalidNodeType);

            var length = refNode.ChildNodes.Length;
            _start = new Boundary { Node = refNode, Offset = 0 };
            _end = new Boundary { Node = refNode, Offset = length };
        }

        public void Clear()
        {
            //TODO

            if (_start.Equals(_end))
                return;

            var newBoundary = new Boundary();
            var originalStart = _start;
            var originalEnd = _end;

            if (originalEnd.Node == originalStart.Node && originalStart.Node is ICharacterData)
            {
                //Replace data with node original start node, offset original start offset, 
                //count original end offset minus original start offset, and data the empty
                //string, and then terminate these steps. 
                return;
            }

            var nodesToRemove = Nodes.Where(m => !Intersects(m.Parent)).ToArray();

            if (!originalStart.Node.IsInclusiveAncestorOf(originalEnd.Node))
            {
                var referenceNode = originalStart.Node;

                while (referenceNode.Parent != null && referenceNode.Parent.IsInclusiveAncestorOf(originalEnd.Node))
                    referenceNode = referenceNode.Parent;

                newBoundary = new Boundary { Node = referenceNode.Parent, Offset = referenceNode.Parent.ChildNodes.Index(referenceNode) + 1 };
            }
            else
                newBoundary = originalStart;

            if (originalStart.Node is ICharacterData)
            {
                //Replace data with node original start node, offset original start offset, 
                //count original start node's length minus original start offset, data the empty
                //string. 
            }

            foreach (var node in nodesToRemove)
                node.Parent.RemoveChild(node);

            if (originalEnd.Node is ICharacterData)
            {
                //Replace data with node original end node, offset 0, count original end offset 
                //and data the empty string. 
            }

            _start = newBoundary;
            _end = newBoundary;
        }

        public IDocumentFragment Flush()
        {
            //TODO
            var fragment = new DocumentFragment { Owner = _start.Node.Owner as Document };

            if (_start.Equals(_end))
                return fragment;

            var newBoundary = _start;
            var originalStart = _start;
            var originalEnd = _end;

            if (originalStart.Node == originalEnd.Node && _start.Node is ICharacterData)
            {
                //1. Let clone be a clone of original start node. 
                //2. Set the data of clone to the result of substringing data with node original start node, offset original start offset, and count original end offset minus original start offset. 
                //3. Append clone to fragment. 
                //4. Replace data with node original start node, offset original start offset, count original end offset minus original start offset, and data the empty string. 
                return fragment;
            }

            var commonAncestor = originalStart.Node;

            while (!commonAncestor.IsInclusiveAncestorOf(originalEnd.Node))
                commonAncestor = commonAncestor.Parent;

            var firstPartiallyContainedChild = !originalStart.Node.IsInclusiveAncestorOf(originalEnd.Node) ? 
                commonAncestor.GetElements<INode>(predicate: IsPartiallyContained).FirstOrDefault() : null;
            var lastPartiallyContainedchild = !originalEnd.Node.IsInclusiveAncestorOf(originalStart.Node) ? 
                commonAncestor.GetElements<INode>(predicate: IsPartiallyContained).LastOrDefault() : null;
            var containedChildren = commonAncestor.GetElements<INode>(predicate: Intersects).ToList();

            if (containedChildren.OfType<IDocumentType>().Any())
                throw new DomException(ErrorCode.HierarchyRequest);

            if (!originalStart.Node.IsInclusiveAncestorOf(originalEnd.Node))
            {
                //1. Let reference node equal original start node. 
                //2. While reference node's parent is not null and is not an inclusive ancestor of original end node, set reference node to its parent. 
                //3. Set new node to the parent of reference node, and new offset to one plus reference node's index. 
                //  (Note:
                //  If reference node's parent is null, it would be the root of range, so would be an inclusive ancestor of original end node, and we could not reach this point.)
            }

            if (firstPartiallyContainedChild is ICharacterData)
            {
                //1. Let clone be a clone of original start node. 
                //2. Set the data of clone to the result of substringing data with node original start node, offset original start offset, and count original start node's length minus original start offset. 
                //3. Append clone to fragment. 
                //4. Replace data with node original start node, offset original start offset, count original start node's length minus original start offset, and data the empty string. 
            }
            else if (firstPartiallyContainedChild != null)
            {
                //1. Let clone be a clone of first partially contained child. 
                //2. Append clone to fragment. 
                //3. Let subrange be a new range whose start is (original start node, original start offset) and whose end is (first partially contained child, first partially contained child's length). 
                //4. Let subfragment be the result of extracting subrange. 
                //5. Append subfragment to fragment. 
            }

            foreach (var child in containedChildren)
                fragment.AppendChild(child);

            if (lastPartiallyContainedchild is ICharacterData)
            {
                //1. Let clone be a clone of original end node. 
                //2. Set the data of clone to the result of substringing data with node original end node, offset 0, and count original end offset. 
                //3. Append clone to fragment. 
                //4. Replace data with node original end node, offset 0, count original end offset, and data the empty string. 
            }
            else if (lastPartiallyContainedchild != null)
            {
                //1. Let clone be a clone of last partially contained child. 
                //2. Append clone to fragment. 
                //3. Let subrange be a new range whose start is (last partially contained child, 0) and whose end is (original end node, original end offset). 
                //4. Let subfragment be the result of extracting subrange. 
                //5. Append subfragment to fragment. 
            }

            _start = newBoundary;
            _end = newBoundary;
            return fragment;
        }

        public IDocumentFragment Copy()
        {
            //TODO
            /*
1. Let fragment be a new DocumentFragment node whose node document is range's start node's node document. 
2. If range's start equals its end, return fragment. 
3. Let original start node, original start offset, original end node, and original end offset be range's start node, start offset, end node, and end offset, respectively. 
4. If original start node equals original end node, and they are a Text, ProcessingInstruction, or Comment node: 
  1. Let clone be a clone of original start node. 
  2. Set the data of clone to the result of substringing data with node original start node, offset original start offset, and count original end offset minus original start offset. 
  3. Append clone to fragment. 
  4. Return fragment. 
5. Let common ancestor be original start node. 
6. While common ancestor is not an inclusive ancestor of original end node, set common ancestor to its own parent. 
7. Let first partially contained child be null. 
8. If original start node is not an inclusive ancestor of original end node, set first partially contained child to the first child of common ancestor that is partially contained in range. 
9. Let last partially contained child be null. 
10. If original end node is not an inclusive ancestor of original start node, set last partially contained child to the last child of common ancestor that is partially contained in range. 
  (Note:
  These variable assignments do actually always make sense. For instance, if original start node is not an inclusive ancestor of original end node, original start node is itself partially contained in range, and so are all its ancestors up until a child of common ancestor. common ancestor cannot be original start node, because it has to be an inclusive ancestor of original end node. The other case is similar. Also, notice that the two children will never be equal if both are defined.)
11. Let contained children be a list of all children of common ancestor that are contained in range, in tree order. 
12. If any member of contained children is a doctype, throw a "HierarchyRequestError" exception. 
  (Note:
  We do not have to worry about the first or last partially contained node, because a doctype can never be partially contained. It cannot be a boundary point of a range, and it cannot be the ancestor of anything.)
13. If first partially contained child is a Text, ProcessingInstruction, or Comment node: 
  (Note:
  In this case, first partially contained child is original start node.)
  1. Let clone be a clone of original start node. 
  2. Set the data of clone to the result of substringing data with node original start node, offset original start offset, and count original start node's length minus original start offset. 
  3. Append clone to fragment. 
14. Otherwise, if first partially contained child is not null: 
  1. Let clone be a clone of first partially contained child. 
  2. Append clone to fragment. 
  3. Let subrange be a new range whose start is (original start node, original start offset) and whose end is (first partially contained child, first partially contained child's length). 
  4. Let subfragment be the result of cloning subrange. 
  5. Append subfragment to fragment. 
15. For each contained child in contained children: 
  1. Let clone be a clone of contained child. 
  2. Append clone to fragment. 
16. If last partially contained child is a Text, ProcessingInstruction, or Comment node: 
  (Note:
  In this case, last partially contained child is original end node.)
  1. Let clone be a clone of original end node. 
  2. Set the data of clone to the result of substringing data with node original end node, offset 0, and count original end offset. 
  3. Append clone to fragment. 
17. Otherwise, if last partially contained child is not null: 
  1. Let clone be a clone of last partially contained child. 
  2. Append clone to fragment. 
  3. Let subrange be a new range whose start is (last partially contained child, 0) and whose end is (original end node, original end offset). 
  4. Let subfragment be the result of cloning subrange. 
  5. Append subfragment to fragment. 
18. Return fragment.
             */
            throw new NotImplementedException();
        }

        public void Insert(INode node)
        {
            if (_start.Node is IProcessingInstruction || _start.Node is IComment || (_start.Node is IText) && _start.Node.Parent == null)
                throw new DomException(ErrorCode.HierarchyRequest);

            var referenceNode = _start.Node is IText ? _start.Node : _start.ChildAtOffset;
            var parent = referenceNode == null ? _start.Node : referenceNode.Parent;
            parent.EnsurePreInsertionValidity(node, referenceNode);
            
            if (_start.Node is IText)
            {
                referenceNode = ((IText)_start.Node).Split(_start.Offset);
                parent = referenceNode.Parent;
            }

            if (node == referenceNode)
                referenceNode = referenceNode.NextSibling;

            if (node.Parent != null)
                node.Parent.RemoveChild(node);

            var newOffset = referenceNode == null ? parent.ChildNodes.Length : parent.ChildNodes.Index(referenceNode);
            newOffset += node is IDocumentFragment ? node.ChildNodes.Length : 1;
            parent.PreInsert(node, referenceNode);

            if (_start.Equals(_end))
                _end = new Boundary { Node = parent, Offset = newOffset };
        }

        public void Surround(INode newParent)
        {
            if (Nodes.Any(m => m is IText == false && IsPartiallyContained(m)))
                throw new DomException(ErrorCode.InvalidState);

            if (newParent is IDocument || newParent is IDocumentType || newParent is IDocumentFragment)
                throw new DomException(ErrorCode.InvalidNodeType);

            var fragment = Flush();

            while (newParent.HasChilds)
                newParent.RemoveChild(newParent.FirstChild);

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
            if (node.GetRoot() != Root)
                return false;
            else if (node is IDocumentType)
                throw new DomException(ErrorCode.InvalidNodeType);
            else if (offset > node.ChildNodes.Length)
                throw new DomException(ErrorCode.IndexSizeError);
            else if (_start > new Boundary { Node = node, Offset = offset } || _end < new Boundary { Node = node, Offset = offset })
                return false;

            return true;
        }

        public RangePosition CompareBoundaryTo(RangeType how, IRange sourceRange)
        {
            if (Root != sourceRange.Head.GetRoot())
                throw new DomException(ErrorCode.WrongDocument);

            Boundary thisPoint;
            Boundary otherPoint;

            switch (how)
            {
                case RangeType.StartToStart:
                    thisPoint = _start;
                    otherPoint = new Boundary { Node = sourceRange.Head, Offset = sourceRange.Start };
                    break;
                case RangeType.StartToEnd:
                    thisPoint = _end;
                    otherPoint = new Boundary { Node = sourceRange.Head, Offset = sourceRange.Start };
                    break;
                case RangeType.EndToEnd:
                    thisPoint = _start;
                    otherPoint = new Boundary { Node = sourceRange.Tail, Offset = sourceRange.End };
                    break;
                case RangeType.EndToStart:
                    thisPoint = _end;
                    otherPoint = new Boundary { Node = sourceRange.Tail, Offset = sourceRange.End };
                    break;
                default:
                    throw new DomException(ErrorCode.NotSupported);
            }

            return thisPoint.CompareTo(otherPoint);
        }

        public RangePosition CompareTo(INode node, Int32 offset)
        {
            if (Root != _start.Node.GetRoot())
                throw new DomException(ErrorCode.WrongDocument);
            else if (node is IDocumentType)
                throw new DomException(ErrorCode.InvalidNodeType);
            else if (offset > node.ChildNodes.Length)
                throw new DomException(ErrorCode.IndexSizeError);

            if (_start > new Boundary { Node = node, Offset = offset })
                return RangePosition.Before;
            else if (_end < new Boundary { Node = node, Offset = offset })
                return RangePosition.After;

            return RangePosition.Equal;
        }

        public Boolean Intersects(INode node)
        {
            if (Root != node.GetRoot())
                return false;

            var parent = node.Parent;

            if (parent == null)
                return true;

            var offset = parent.ChildNodes.Index(node);
            return (_end > new Boundary { Node = parent, Offset = offset } && _start < new Boundary { Node = parent, Offset = offset + 1 });
        }

        #endregion

        #region Helpers

        Boolean IsPartiallyContained(INode node)
        {
            var startAncestor = node.IsInclusiveAncestorOf(_start.Node);
            var endAncestor = node.IsInclusiveAncestorOf(_end.Node);

            return (startAncestor && !endAncestor) || (!startAncestor && endAncestor);
        }

        #endregion

        #region Boundary

        struct Boundary : IEquatable<Boundary>
        {
            public INode Node;
            public Int32 Offset;

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
                    return RangePosition.Before;
                else if (this > other)
                    return RangePosition.After;

                return RangePosition.Equal;
            }

            public INode ChildAtOffset
            {
                get { return Node.ChildNodes.Length > Offset ? Node.ChildNodes[Offset] : null; }
            }
        }

        #endregion
    }
}
