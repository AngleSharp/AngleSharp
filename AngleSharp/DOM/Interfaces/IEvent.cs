using System;

namespace AngleSharp.DOM
{
    interface IEvent
    {
        String Type { get; }
        EventTarget Target { get; }
        EventTarget CurrentTarget { get; }
        EventPhase EventPhase { get; }

        void StopPropagation();
        void StopImmediatePropagation();

        Boolean Bubbles { get; }
        Boolean Cancelable { get; }
        void PreventDefault();
        Boolean DefaultPrevented { get; }

        Boolean IsTrusted { get; }
        DateTime TimeStamp { get; }

        void InitEvent(String type, Boolean bubbles, Boolean cancelable);
    }
}
