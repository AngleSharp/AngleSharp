using System;

namespace AngleSharp.DOM
{
    interface IEvent
    {
        string Type { get; }
        EventTarget Target { get; }
        EventTarget CurrentTarget { get; }
        EventPhase EventPhase { get; }

        void StopPropagation();
        void StopImmediatePropagation();

        bool Bubbles { get; }
        bool Cancelable { get; }
        void PreventDefault();
        bool DefaultPrevented { get; }

        bool IsTrusted { get; }
        DateTime TimeStamp { get; }

        void InitEvent(string type, bool bubbles, bool cancelable);
    }
}
