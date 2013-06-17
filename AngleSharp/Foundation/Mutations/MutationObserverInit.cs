using AngleSharp.DOM;
using System;
using System.Collections.Generic;

namespace AngleSharp
{
    class MutationObserverInit
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
    }
}
