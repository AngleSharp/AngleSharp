using System;

namespace AngleSharp.DOM
{
    interface IRange
    {
        Node StartContainer { get; }
        int StartOffset { get; }
        Node EndContainer { get; }
        int EndOffset { get; }
        bool Collapsed { get; }
        Node CommonAncestorContainer { get; }

        void SetStart(Node refNode, int offset);
        void SetEnd(Node refNode, int offset);
        void SetStartBefore(Node refNode);
        void SetEndBefore(Node refNode);
        void SetStartAfter(Node refNode);
        void SetEndAfter(Node refNode);
        void Collapse(bool toStart);
        void SelectNode(Node refNode);
        void SelectNodeContents(Node refNode);

        void DeleteContents();
        DocumentFragment ExtractContents();
        DocumentFragment CloneContents();
        void InsertNode(Node node);
        void SurroundContents(Node newParent);
        Range CloneRange();
        void Detach();
        bool IsPointInRange(Node node, int offset);
        RangePosition CompareBoundaryPoints(RangeType how, Range sourceRange);
        RangePosition ComparePoint(Node node, int offset);
        bool IntersectsNode(Node node);
    }
}
