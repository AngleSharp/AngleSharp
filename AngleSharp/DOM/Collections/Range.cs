namespace AngleSharp.DOM.Collections
{
    using System;

    /// <summary>
    /// A DOM range to gather DOM tree information.
    /// </summary>
    sealed class Range : IRange
    {
        #region Fields

        readonly IDocument _document;
        INode _startNode;
        INode _endNode;
        Int32 _startOffset;
        Int32 _endOffset;

        #endregion

        #region ctor

        public Range(IDocument document)
        {
            _document = document;
            _startOffset = 0;
            _endOffset = 0;
            _startNode = document;
            _endNode = document;
        }

        #endregion

        #region Properties

        public INode Head
        {
            get { return _startNode; }
        }

        public Int32 Start
        {
            get { return _startOffset; }
        }

        public INode Tail
        {
            get { return _endNode; }
        }

        public Int32 End
        {
            get { return _endOffset; }
        }

        public Boolean IsCollapsed
        {
            get { return _startNode == _endNode; }
        }

        public INode CommonAncestor
        {
            get 
            {
                var container = _startNode;

                while (container != null && !_endNode.Contains(container))
                    container = container.Parent;

                return container;
            }
        }

        #endregion

        #region Methods

        public void StartWith(INode refNode, Int32 offset)
        {
            throw new NotImplementedException();
        }

        public void EndWith(INode refNode, Int32 offset)
        {
            throw new NotImplementedException();
        }

        public void StartBefore(INode refNode)
        {
            throw new NotImplementedException();
        }

        public void EndBefore(INode refNode)
        {
            throw new NotImplementedException();
        }

        public void StartAfter(INode refNode)
        {
            throw new NotImplementedException();
        }

        public void EndAfter(INode refNode)
        {
            throw new NotImplementedException();
        }

        public void Collapse(Boolean toStart)
        {
            throw new NotImplementedException();
        }

        public void Select(INode refNode)
        {
            throw new NotImplementedException();
        }

        public void SelectContents(INode refNode)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public IDocumentFragment Flush()
        {
            throw new NotImplementedException();
        }

        public IDocumentFragment Copy()
        {
            throw new NotImplementedException();
        }

        public void Insert(INode node)
        {
            throw new NotImplementedException();
        }

        public void Surround(INode newParent)
        {
            throw new NotImplementedException();
        }

        public IRange Clone()
        {
            throw new NotImplementedException();
        }

        public void Detach()
        {
            //Does nothing.
        }

        public Boolean Contains(INode node, Int32 offset)
        {
            throw new NotImplementedException();
        }

        public RangePosition CompareBoundaryTo(RangeType how, IRange sourceRange)
        {
            throw new NotImplementedException();
        }

        public RangePosition CompareTo(INode node, Int32 offset)
        {
            throw new NotImplementedException();
        }

        public Boolean Intersects(INode node)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
