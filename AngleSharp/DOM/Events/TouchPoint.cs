namespace AngleSharp.DOM.Events
{
    using System;

    /// <summary>
    /// Implements the interface to access the data of a touch point.
    /// </summary>
    sealed class TouchPoint : ITouch
    {
        public Int32 Id
        {
            get;
            set;
        }

        public IEventTarget Target
        {
            get;
            set;
        }

        public Int32 ScreenX
        {
            get;
            set;
        }

        public Int32 ScreenY
        {
            get;
            set;
        }

        public Int32 ClientX
        {
            get;
            set;
        }

        public Int32 ClientY
        {
            get;
            set;
        }

        public Int32 PageX
        {
            get;
            set;
        }

        public Int32 PageY
        {
            get;
            set;
        }
    }
}
