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

        public INodeList Added
        {
            get;
            set;
        }

        public INodeList Removed
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

        public String PreviousValue
        {
            get;
            set;
        }
    }
}
