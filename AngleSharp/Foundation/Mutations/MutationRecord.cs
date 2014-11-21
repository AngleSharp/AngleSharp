namespace AngleSharp.DOM
{
    using System;

    sealed class MutationRecord : IMutationRecord
    {
        #region Fields

        static readonly String characterData = "characterData";
        static readonly String attributes = "attributes";
        static readonly String childList = "childList";

        #endregion

        #region ctor

        MutationRecord() { }

        #endregion

        #region Methods

        public static MutationRecord CharacterData(INode target, String previousValue = null)
        {
            return new MutationRecord
            {
                Type = characterData,
                Target = target,
                PreviousValue = previousValue
            };
        }

        public static MutationRecord ChildList(INode target, INodeList addedNodes = null, INodeList removedNodes = null, INode previousSibling = null, INode nextSibling = null)
        {
            return new MutationRecord
            {
                Type = childList,
                Target = target,
                Added = addedNodes,
                Removed = removedNodes,
                PreviousSibling = previousSibling,
                NextSibling = nextSibling
            };
        }

        public static MutationRecord Attributes(INode target, String attributeName = null, String attributeNamespace = null, String previousValue = null)
        {
            return new MutationRecord
            {
                Type = attributes,
                Target = target,
                AttributeName = attributeName,
                AttributeNamespace = attributeNamespace,
                PreviousValue = previousValue
            };
        }

        #endregion

        #region Properties

        public String Type
        {
            get;
            private set;
        }

        public INode Target
        {
            get;
            private set;
        }

        public INodeList Added
        {
            get;
            private set;
        }

        public INodeList Removed
        {
            get;
            private set;
        }

        public INode PreviousSibling
        {
            get;
            private set;
        }

        public INode NextSibling
        {
            get;
            private set;
        }

        public String AttributeName
        {
            get;
            private set;
        }

        public String AttributeNamespace
        {
            get;
            private set;
        }

        public String PreviousValue
        {
            get;
            private set;
        }

        #endregion
    }
}
