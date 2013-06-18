using System;

namespace AngleSharp.DOM
{
    [DOM("Event")]
    public abstract class Event : IEvent
    {
        //TODO

        public String Type
        {
            get { throw new NotImplementedException(); }
        }

        public EventTarget Target
        {
            get { throw new NotImplementedException(); }
        }

        public EventTarget CurrentTarget
        {
            get { throw new NotImplementedException(); }
        }

        public EventPhase EventPhase
        {
            get { throw new NotImplementedException(); }
        }

        public void StopPropagation()
        {
            throw new NotImplementedException();
        }

        public void StopImmediatePropagation()
        {
            throw new NotImplementedException();
        }

        public Boolean Bubbles
        {
            get { throw new NotImplementedException(); }
        }

        public Boolean Cancelable
        {
            get { throw new NotImplementedException(); }
        }

        public void PreventDefault()
        {
            throw new NotImplementedException();
        }

        public Boolean DefaultPrevented
        {
            get { throw new NotImplementedException(); }
        }

        public Boolean IsTrusted
        {
            get { throw new NotImplementedException(); }
        }

        public DateTime TimeStamp
        {
            get { throw new NotImplementedException(); }
        }

        public void InitEvent(String type, Boolean bubbles, Boolean cancelable)
        {
            throw new NotImplementedException();
        }
    }
}
