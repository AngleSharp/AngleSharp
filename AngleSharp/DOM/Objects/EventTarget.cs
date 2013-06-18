using System;

namespace AngleSharp.DOM
{
    [DOM("EventTarget")]
    public class EventTarget : IEventTarget
    {
        //TODO

        public void AddEventListener(string type, EventListener callback = null, bool capture = false)
        {
            throw new NotImplementedException();
        }

        public void RemoveEventListener(string type, EventListener callback = null, bool capture = false)
        {
            throw new NotImplementedException();
        }

        public bool DispatchEvent(Event e)
        {
            throw new NotImplementedException();
        }
    }
}
