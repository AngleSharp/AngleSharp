namespace AngleSharp.DOM
{
    using System;

    interface IRange
    {
        Node StartContainer { get; }
        Int32 StartOffset { get; }
        Node EndContainer { get; }
        Int32 EndOffset { get; }
        Boolean Collapsed { get; }
        Node CommonAncestorContainer { get; }

        void SetStart(Node refNode, Int32 offset);
        void SetEnd(Node refNode, Int32 offset);
        void SetStartBefore(Node refNode);
        void SetEndBefore(Node refNode);
        void SetStartAfter(Node refNode);
        void SetEndAfter(Node refNode);
        void Collapse(Boolean toStart);
        void SelectNode(Node refNode);
        void SelectNodeContents(Node refNode);

        void DeleteContents();
        DocumentFragment ExtractContents();
        DocumentFragment CloneContents();
        void InsertNode(Node node);
        void SurroundContents(Node newParent);
        Range CloneRange();
        void Detach();
        Boolean IsPointInRange(Node node, Int32 offset);
        RangePosition CompareBoundaryPoints(RangeType how, Range sourceRange);
        RangePosition ComparePoint(Node node, Int32 offset);
        Boolean IntersectsNode(Node node);
    }
}
