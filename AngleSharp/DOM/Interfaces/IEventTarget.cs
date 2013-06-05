using System;

namespace AngleSharp.DOM
{
    interface IEventTarget
    {
        void AddEventListener(string type, EventListener callback = null, bool capture = false);
        void RemoveEventListener(string type, EventListener callback = null, bool capture = false);
        bool DispatchEvent(Event e);
    }
}
