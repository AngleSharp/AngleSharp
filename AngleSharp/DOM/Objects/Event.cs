using System;

namespace AngleSharp.DOM
{
    public abstract class Event : IEvent
    {
        //TODO

        public string Type
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

        public bool Bubbles
        {
            get { throw new NotImplementedException(); }
        }

        public bool Cancelable
        {
            get { throw new NotImplementedException(); }
        }

        public void PreventDefault()
        {
            throw new NotImplementedException();
        }

        public bool DefaultPrevented
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsTrusted
        {
            get { throw new NotImplementedException(); }
        }

        public DateTime TimeStamp
        {
            get { throw new NotImplementedException(); }
        }

        public void InitEvent(string type, bool bubbles, bool cancelable)
        {
            throw new NotImplementedException();
        }
    }
}
