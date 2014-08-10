namespace AngleSharp.DOM.Events
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to Event instance creation mappings.
    /// </summary>
    static class EventFactory
    {
        static readonly Dictionary<String, Func<Event>> events = new Dictionary<String, Func<Event>>(StringComparer.OrdinalIgnoreCase);

        static EventFactory()
        {
            events.Add("event", () => new Event());
            events.Add("events", () => new Event());
            events.Add("htmlevents", () => new Event());
            events.Add("keyboardevent", () => new Event());//TODO KeyboardEvent
            events.Add("keyevents", () => new Event());//TODO KeyboardEvent
            events.Add("messageevent", () => new Event());//TODO MessageEvent
            events.Add("mouseevent", () => new Event());//TODO MouseEvent
            events.Add("mouseevents", () => new Event());//TODO MouseEvent
            events.Add("touchevent", () => new Event());//TODO TouchEvent
            events.Add("uievent", () => new Event());//TODO UIEvent
            events.Add("uievents", () => new Event());//TODO UIEvent
            events.Add("mutationevent", () => new Event());//TODO MutationEvent
            events.Add("mutationevents", () => new Event());//TODO MutationEvent
            events.Add("customevent", () => new CustomEvent());
        }

        /// <summary>
        /// Creates a new property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="style">The given style set.</param>
        /// <returns>The created property</returns>
        public static Event Create(String name)
        {
            Func<Event> eventCreator;

            if (events.TryGetValue(name, out eventCreator))
                return eventCreator();

            throw new DomException(ErrorCode.NotSupported);
        }
    }
}
