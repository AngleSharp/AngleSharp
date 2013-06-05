using AngleSharp.DOM;
using System;
using System.Collections.Generic;

namespace AngleSharp
{
    class MutationObserverInit
    {
        public bool ChildList
        {
            get;
            set;
        }

        public bool Attributes
        {
            get;
            set;
        }

        public bool CharacterData
        {
            get;
            set;
        }

        public bool Subtree
        {
            get;
            set;
        }

        public bool AttributeOldValue
        {
            get;
            set;
        }

        public bool CharacterDataOldValue
        {
            get;
            set;
        }
    }
}
