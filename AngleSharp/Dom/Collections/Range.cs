namespace AngleSharp.Dom.Collections
{
    using AngleSharp.Extensions;
    using AngleSharp.Linq;
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
            if (refNode == null)
                throw new ArgumentNullException("refNode");
            else if (refNode.NodeType == NodeType.DocumentType)
                throw new DomException(DomError.InvalidNodeType);
            else if (offset > refNode.ChildNodes.Length)
                throw new DomException(DomError.IndexSizeError);

            var bp = new Boundary { Node = refNode, Offset = offset };

            if (bp > _end || Root != refNode.GetRoot())
                _start = bp;
        }

        public void EndWith(INode refNode, Int32 offset)
        {
            if (refNode == null)
                throw new ArgumentNullException("refNode");
            else if (refNode.NodeType == NodeType.DocumentType)
                throw new DomException(DomError.InvalidNodeType);
            else if (offset > refNode.ChildNodes.Length)
                throw new DomException(DomError.IndexSizeError);

            var bp = new Boundary { Node = refNode, Offset = offset };

            if (bp < _start || Root != refNode.GetRoot())
                _end = bp;
        }

        public void StartBefore(INode refNode)
        {
            if (refNode == null)
                throw new ArgumentNullException("refNode");

            var parent = refNode.Parent;

            if (parent == null)
                throw new DomException(DomError.InvalidNodeType);

            _start = new Boundary { Node = parent, Offset = parent.ChildNodes.Index(refNode) };
        }

        public void EndBefore(INode refNode)
        {
            if (refNode == null)
                throw new ArgumentNullException("refNode");

            var parent = refNode.Parent;

            if (parent == null)
                throw new DomException(DomError.InvalidNodeType);

            _end = new Boundary { Node = parent, Offset = parent.ChildNodes.Index(refNode) };
        }

        public void StartAfter(INode refNode)
        {
            if (refNode == null)
                throw new ArgumentNullException("refNode");

            var parent = refNode.Parent;

            if (parent == null)
                throw new DomException(DomError.InvalidNodeType);

            _start = new Boundary { Node = parent, Offset = parent.ChildNodes.Index(refNode) + 1 };
        }

        public void EndAfter(INode refNode)
        {
            if (refNode == null)
                throw new ArgumentNullException("refNode");

            var parent = refNode.Parent;

            if (parent == null)
                throw new DomException(DomError.InvalidNodeType);

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
            if (refNode == null)
                throw new ArgumentNullException("refNode");

            var parent = refNode.Parent;

            if (parent == null)
                throw new DomException(DomError.InvalidNodeType);

            var index = parent.ChildNodes.Index(refNode);
            _start = new Boundary { Node = parent, Offset = index };
            _end = new Boundary { Node = parent, Offset = index + 1 };
        }

        public void SelectContent(INode refNode)
        {
            if (refNode == null)
                throw new ArgumentNullException("refNode");
            else if (refNode.NodeType == NodeType.DocumentType)
                throw new DomException(DomError.InvalidNodeType);

            var length = refNode.ChildNodes.Length;
            _start = new Boundary { Node = refNode, Offset = 0 };
            _end = new Boundary { Node = refNode, Offset = length };
        }

        public void ClearContent()
        {
            if (_start.Equals(_end))
                return;

            var newBoundary = new Boundary();
            var originalStart = _start;
            var originalEnd = _end;

            if (originalEnd.Node == originalStart.Node && originalStart.Node is ICharacterData)
            {
                var strt = originalStart.Offset;
                var text = (ICharacterData)originalStart.Node;
                var span = originalEnd.Offset - originalStart.Offset;
                text.Replace(strt, span, String.Empty);
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
                var strt = originalStart.Offset;
                var text = (ICharacterData)originalStart.Node;
                var span = originalEnd.Offset - originalStart.Offset;
                text.Replace(strt, span, String.Empty);
            }

            foreach (var node in nodesToRemove)
                node.Parent.RemoveChild(node);

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

        public IDocumentFragment ExtractContent()
        {
            var fragment = _start.Node.Owner.CreateDocumentFragment();

            if (_start.Equals(_end))
                return fragment;

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
                throw new DomException(DomError.HierarchyRequest);

            if (!originalStart.Node.IsInclusiveAncestorOf(originalEnd.Node))
            {
                var referenceNode = originalStart.Node;

                while (referenceNode.Parent != null && !referenceNode.IsInclusiveAncestorOf(originalEnd.Node))
                    referenceNode = referenceNode.Parent;

                newBoundary = new Boundary { Node = referenceNode, Offset = referenceNode.Parent.ChildNodes.Index(referenceNode) + 1 };
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
                var subrange = new Range(originalStart, new Boundary { Node = firstPartiallyContainedChild, Offset = firstPartiallyContainedChild.ChildNodes.Length });
                var subfragment = subrange.ExtractContent();
                fragment.AppendChild(subfragment); 
            }

            foreach (var child in containedChildren)
                fragment.AppendChild(child);

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
                var subrange = new Range(new Boundary { Node = lastPartiallyContainedchild, Offset = 0 }, originalEnd);
                var subfragment = subrange.ExtractContent();
                fragment.AppendChild(subfragment);
            }

            _start = newBoundary;
            _end = newBoundary;
            return fragment;
        }

        public IDocumentFragment CopyContent()
        {
            var fragment = _start.Node.Owner.CreateDocumentFragment();

            if (_start.Equals(_end))
                return fragment;

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
                throw new DomException(DomError.HierarchyRequest);

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
                var subrange = new Range(originalStart, new Boundary { Node = firstPartiallyContainedChild, Offset = firstPartiallyContainedChild.ChildNodes.Length });
                var subfragment = subrange.CopyContent();
                fragment.AppendChild(subfragment);
            }

            foreach (var child in containedChildren)
                fragment.AppendChild(child.Clone());

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
                var subrange = new Range(new Boundary { Node = lastPartiallyContainedchild, Offset = 0 }, originalEnd);
                var subfragment = subrange.CopyContent();
                fragment.AppendChild(subfragment);
            }

            return fragment;
        }

        public void Insert(INode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            var snode = _start.Node;
            var type = snode.NodeType;
            var istext = type == NodeType.Text;

            if (type == NodeType.ProcessingInstruction || type == NodeType.Comment || (istext && snode.Parent == null))
                throw new DomException(DomError.HierarchyRequest);

            var referenceNode = istext ? snode : _start.ChildAtOffset;
            var parent = referenceNode == null ? snode : referenceNode.Parent;
            parent.EnsurePreInsertionValidity(node, referenceNode);
            
            if (istext)
            {
                referenceNode = ((IText)snode).Split(_start.Offset);
                parent = referenceNode.Parent;
            }

            if (node == referenceNode)
            {
                referenceNode = referenceNode.NextSibling;
            }

            if (node.Parent != null)
            {
                node.Parent.RemoveChild(node);
            }

            var newOffset = referenceNode == null ? parent.ChildNodes.Length : parent.ChildNodes.Index(referenceNode);
            newOffset += node.NodeType == NodeType.DocumentFragment ? node.ChildNodes.Length : 1;
            parent.PreInsert(node, referenceNode);

            if (_start.Equals(_end))
            {
                _end = new Boundary { Node = parent, Offset = newOffset };
            }
        }

        public void Surround(INode newParent)
        {
            if (newParent == null)
                throw new ArgumentNullException("newParent");
            else if (Nodes.Any(m => m.NodeType != NodeType.Text && IsPartiallyContained(m)))
                throw new DomException(DomError.InvalidState);

            var type = newParent.NodeType;

            if (type == NodeType.Document || type == NodeType.DocumentType || type == NodeType.DocumentFragment)
                throw new DomException(DomError.InvalidNodeType);

            var fragment = ExtractContent();

            while (newParent.HasChildNodes)
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
            if (node == null)
                throw new ArgumentNullException("node");
            else if (node.GetRoot() != Root)
                return false;
            else if (node.NodeType == NodeType.DocumentType)
                throw new DomException(DomError.InvalidNodeType);
            else if (offset > node.ChildNodes.Length)
                throw new DomException(DomError.IndexSizeError);
            else if (_start > new Boundary { Node = node, Offset = offset } || _end < new Boundary { Node = node, Offset = offset })
                return false;

            return true;
        }

        public RangePosition CompareBoundaryTo(RangeType how, IRange sourceRange)
        {
            if (sourceRange == null)
                throw new ArgumentNullException("sourceRange");
            else if (Root != sourceRange.Head.GetRoot())
                throw new DomException(DomError.WrongDocument);

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
                    throw new DomException(DomError.NotSupported);
            }

            return thisPoint.CompareTo(otherPoint);
        }

        public RangePosition CompareTo(INode node, Int32 offset)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            if (Root != _start.Node.GetRoot())
                throw new DomException(DomError.WrongDocument);
            else if (node.NodeType == NodeType.DocumentType)
                throw new DomException(DomError.InvalidNodeType);
            else if (offset > node.ChildNodes.Length)
                throw new DomException(DomError.IndexSizeError);

            if (_start > new Boundary { Node = node, Offset = offset })
                return RangePosition.Before;
            else if (_end < new Boundary { Node = node, Offset = offset })
                return RangePosition.After;

            return RangePosition.Equal;
        }

        public Boolean Intersects(INode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");
            else if (Root != node.GetRoot())
                return false;

            var parent = node.Parent;

            if (parent == null)
                return true;

            var offset = parent.ChildNodes.Index(node);
            return (_end > new Boundary { Node = parent, Offset = offset } && _start < new Boundary { Node = parent, Offset = offset + 1 });
        }

        public override String ToString()
        {
            var s = Pool.NewStringBuilder();
            var offset = Start;
            var dest = End;
            var startText = Head as IText;
            var endText = Tail as IText;

            if (startText != null && Head == Tail)
                return startText.Substring(offset, dest - offset);
            else if (startText != null)
                s.Append(startText.Substring(offset, startText.Length - offset));

            var nodes = CommonAncestor.Descendents<IText>();

            foreach (var node in nodes)
            {
                if (_start < new Boundary { Node = node, Offset = 0 } && _end > new Boundary { Node = node, Offset = node.Length })
                    s.Append(node.Text);
            }

            if (endText != null)
                s.Append(endText.Substring(0, dest));

            return s.ToPool();
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
