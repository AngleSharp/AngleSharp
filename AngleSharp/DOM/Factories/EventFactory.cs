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
            AddEventConstructor("uievent", () => new UiEvent());
            AddEventConstructor("focusevent", () => new FocusEvent());
            AddEventConstructor("keyboardevent", () => new KeyboardEvent());
            AddEventConstructor("mouseevent", () => new MouseEvent());
            AddEventConstructor("wheelevent", () => new WheelEvent());
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
