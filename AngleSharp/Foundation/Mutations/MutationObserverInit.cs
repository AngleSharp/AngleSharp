namespace AngleSharp.DOM
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/API/MutationObserver#MutationObserverInit
    /// </summary>
    sealed class MutationObserverInit : IMutationObserverInit
    {
        public MutationObserverInit()
        {
            ObserveTargetChildNodes = false;
            ObserveTargetDescendents = false;
        }

        public Boolean ObserveTargetChildNodes
        {
            get;
            set;
        }

        public Boolean ObserveTargetAttributes
        {
            get;
            set;
        }

        public Boolean ObserveTargetData
        {
            get;
            set;
        }

        public Boolean ObserveTargetDescendents
        {
            get;
            set;
        }

        public Boolean StorePreviousAttributeValue
        {
            get;
            set;
        }

        public Boolean StorePreviousDataValue
        {
            get;
            set;
        }

        public IEnumerable<String> AttributeFilters
        {
            get;
            set;
        }
    }
}
