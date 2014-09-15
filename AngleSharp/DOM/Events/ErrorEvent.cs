namespace AngleSharp.DOM.Events
{
    using System;

    /// <summary>
    /// Represents the error event arguments.
    /// </summary>
    sealed class ErrorEvent : Event, IEvent
    {
        public String Source
        {
            get;
            private set;
        }
        
        public Int32? Line
        {
            get;
            private set;
        }
        
        public Int32? Column
        {
            get;
            private set;
        }

        public DomException Error
        {
            get;
            private set;
        }
    }
}
