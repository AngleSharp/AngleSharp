using System;
using AngleSharp.DOM;
using AngleSharp.DOM.Collections;

namespace AngleSharp
{
    class MutationRecord
    {
        public string Type
        {
            get;
            set;
        }

        public Node Target
        {
            get;
            set;
        }

        public NodeList AddedNodes
        {
            get;
            set;
        }

        public NodeList RemovedNodes
        {
            get;
            set;
        }

        public Node PreviousSibling
        {
            get;
            set;
        }

        public Node NextSibling
        {
            get;
            set;
        }

        public string AttributeName
        {
            get;
            set;
        }

        public string AttributeNamespace
        {
            get;
            set;
        }

        public string OldValue
        {
            get;
            set;
        }
    }
}
