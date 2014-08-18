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
            //The real deal
            AddEventConstructor("event", () => new Event());
            AddEventConstructor("focusevent", () => new FocusEvent());
            AddEventConstructor("keyboardevent", () => new Event());//TODO KeyboardEvent
            AddEventConstructor("messageevent", () => new Event());//TODO MessageEvent
            AddEventConstructor("mouseevent", () => new Event());//TODO MouseEvent
            AddEventConstructor("touchevent", () => new Event());//TODO TouchEvent
            AddEventConstructor("uievent", () => new UiEvent());
            AddEventConstructor("customevent", () => new CustomEvent());

            //Alias
            AddEventAlias("events", "event");
            AddEventAlias("htmlevents", "event");
            AddEventAlias("uievents", "uievent");
            AddEventAlias("keyevents", "keyboardevent");
            AddEventAlias("mouseevents", "mouseevent");
        }

        static void AddEventConstructor(String name, Func<Event> constructor)
        {
            events.Add(name, constructor);
        }

        static void AddEventAlias(String aliasName, String aliasFor)
        {
            events.Add(aliasName, events[aliasFor]);
        }

        /// <summary>
        /// Creates a new event.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <returns>The created event.</returns>
        public static Event Create(String name)
        {
            Func<Event> eventCreator;

            if (events.TryGetValue(name, out eventCreator))
                return eventCreator();

            return null;
        }
    }
}
