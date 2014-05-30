namespace AngleSharp.DOM
{
    using System;

    interface IEventTarget
    {
        void AddEventListener(String type, EventListener callback = null, Boolean capture = false);
        void RemoveEventListener(String type, EventListener callback = null, Boolean capture = false);
        Boolean DispatchEvent(Event e);
    }
}
