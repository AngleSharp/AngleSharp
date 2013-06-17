using System;
using AngleSharp.DOM;
using AngleSharp.DOM.Collections;

namespace AngleSharp
{
    class MutationRecord
    {
        public String Type
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

        public String AttributeName
        {
            get;
            set;
        }

        public String AttributeNamespace
        {
            get;
            set;
        }

        public String OldValue
        {
            get;
            set;
        }
    }
}
