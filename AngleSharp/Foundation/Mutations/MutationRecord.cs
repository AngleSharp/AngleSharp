namespace AngleSharp.DOM
{
    using System;

    sealed class MutationRecord : IMutationRecord
    {
        public String Type
        {
            get;
            set;
        }

        public INode Target
        {
            get;
            set;
        }

        public INodeList AddedNodes
        {
            get;
            set;
        }

        public INodeList RemovedNodes
        {
            get;
            set;
        }

        public INode PreviousSibling
        {
            get;
            set;
        }

        public INode NextSibling
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
