namespace AngleSharp.Dom
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents a recording of DOM changes.
    /// </summary>
    sealed class MutationRecord : IMutationRecord
    {
        #region Fields

        static readonly String CharacterDataType = "characterData";
        static readonly String AttributesType = "attributes";
        static readonly String ChildListType = "childList";

        #endregion

        #region ctor

        MutationRecord() { }

        #endregion

        #region Methods

        public static MutationRecord CharacterData(INode target, String previousValue = null)
        {
            return new MutationRecord
            {
                Type = CharacterDataType,
                Target = target,
                PreviousValue = previousValue
            };
        }

        public static MutationRecord ChildList(INode target, INodeList addedNodes = null, INodeList removedNodes = null, INode previousSibling = null, INode nextSibling = null)
        {
            return new MutationRecord
            {
                Type = ChildListType,
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
                Type = AttributesType,
                Target = target,
                AttributeName = attributeName,
                AttributeNamespace = attributeNamespace,
                PreviousValue = previousValue
            };
        }

        public MutationRecord Copy(Boolean clearPreviousValue)
        {
            return new MutationRecord
            {
                Type = Type,
                Target = Target,
                PreviousSibling = PreviousSibling,
                NextSibling = NextSibling,
                AttributeName = AttributeName,
                AttributeNamespace = AttributeNamespace,
                PreviousValue = clearPreviousValue ? null : PreviousValue,
                Added = Added,
                Removed = Removed
            };
        }

        #endregion

        #region Properties

        public Boolean IsAttribute
        {
            get { return Type.Is(AttributesType); }
        }

        public Boolean IsCharacterData
        {
            get { return Type.Is(CharacterDataType); }
        }

        public Boolean IsChildList
        {
            get { return Type.Is(ChildListType); }
        }

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
