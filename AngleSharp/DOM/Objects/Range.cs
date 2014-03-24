namespace AngleSharp.DOM
{
    using System;

    [DOM("Range")]
    public sealed class Range : IRange
    {
        internal Range()
        {
        }

        //TODO

        public Node StartContainer
        {
            get { throw new NotImplementedException(); }
        }

        public Int32 StartOffset
        {
            get { throw new NotImplementedException(); }
        }

        public Node EndContainer
        {
            get { throw new NotImplementedException(); }
        }

        public Int32 EndOffset
        {
            get { throw new NotImplementedException(); }
        }

        public Boolean Collapsed
        {
            get { throw new NotImplementedException(); }
        }

        public Node CommonAncestorContainer
        {
            get { throw new NotImplementedException(); }
        }

        public void SetStart(Node refNode, Int32 offset)
        {
            throw new NotImplementedException();
        }

        public void SetEnd(Node refNode, Int32 offset)
        {
            throw new NotImplementedException();
        }

        public void SetStartBefore(Node refNode)
        {
            throw new NotImplementedException();
        }

        public void SetEndBefore(Node refNode)
        {
            throw new NotImplementedException();
        }

        public void SetStartAfter(Node refNode)
        {
            throw new NotImplementedException();
        }

        public void SetEndAfter(Node refNode)
        {
            throw new NotImplementedException();
        }

        public void Collapse(Boolean toStart)
        {
            throw new NotImplementedException();
        }

        public void SelectNode(Node refNode)
        {
            throw new NotImplementedException();
        }

        public void SelectNodeContents(Node refNode)
        {
            throw new NotImplementedException();
        }

        public void DeleteContents()
        {
            throw new NotImplementedException();
        }

        public DocumentFragment ExtractContents()
        {
            throw new NotImplementedException();
        }

        public DocumentFragment CloneContents()
        {
            throw new NotImplementedException();
        }

        public void InsertNode(Node node)
        {
            throw new NotImplementedException();
        }

        public void SurroundContents(Node newParent)
        {
            throw new NotImplementedException();
        }

        public Range CloneRange()
        {
            throw new NotImplementedException();
        }

        public void Detach()
        {
            throw new NotImplementedException();
        }

        public Boolean IsPointInRange(Node node, Int32 offset)
        {
            throw new NotImplementedException();
        }

        public RangePosition CompareBoundaryPoints(RangeType how, Range sourceRange)
        {
            throw new NotImplementedException();
        }

        public RangePosition ComparePoint(Node node, Int32 offset)
        {
            throw new NotImplementedException();
        }

        public Boolean IntersectsNode(Node node)
        {
            throw new NotImplementedException();
        }
    }
}
