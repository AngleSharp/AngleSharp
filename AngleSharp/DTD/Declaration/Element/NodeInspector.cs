using AngleSharp.DOM;
using AngleSharp.DOM.Collections;
using System;
using System.Collections.Generic;

namespace AngleSharp.DTD
{
    sealed class NodeInspector
    {
        List<Node> nodes;

        public NodeInspector(Element element)
        {
            nodes = new List<Node>();

            foreach (var child in element.ChildNodes)
            {
                if ((child is TextNode && !((TextNode)child).IsEmpty) || child is Element)
                    nodes.Add(child);
            }
        }
        public List<Node> Children
        {
            get { return nodes; }
        }

        public Node Current
        {
            get { return Children[Index]; }
        }

        public Int32 Length
        {
            get { return Children.Count; }
        }

        public Int32 Index
        {
            get;
            set;
        }

        public Boolean IsCompleted 
        {
            get { return Children.Count == Index; }
        }
    }
}
