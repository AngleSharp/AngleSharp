namespace AngleSharp.DOM
{
    using System;
    
    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/API/MutationObserver#MutationObserverInit
    /// </summary>
    sealed class MutationObserverInit : IMutationObserverInit
    {
        public Boolean ChildList
        {
            get;
            set;
        }

        public Boolean Attributes
        {
            get;
            set;
        }

        public Boolean CharacterData
        {
            get;
            set;
        }

        public Boolean Subtree
        {
            get;
            set;
        }

        public Boolean AttributeOldValue
        {
            get;
            set;
        }

        public Boolean CharacterDataOldValue
        {
            get;
            set;
        }

        public Boolean AttributeFilter
        {
            get;
            set;
        }
    }
}
