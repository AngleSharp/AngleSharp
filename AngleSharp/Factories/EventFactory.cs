namespace AngleSharp.Factories
{
    using AngleSharp.Dom.Events;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to Event instance creation mappings.
    /// </summary>
    sealed class EventFactory
    {
        readonly Dictionary<String, Func<Event>> creators = new Dictionary<String, Func<Event>>(StringComparer.OrdinalIgnoreCase)
        {
            { "event", () => new Event() },
            { "uievent", () => new UiEvent() },
            { "focusevent", () => new FocusEvent() },
            { "keyboardevent", () => new KeyboardEvent() },
            { "mouseevent", () => new MouseEvent() },
            { "wheelevent", () => new WheelEvent() },
            { "customevent", () => new CustomEvent() }
        };

        public EventFactory()
        {
            //Alias
            AddEventAlias("events", "event");
            AddEventAlias("htmlevents", "event");
            AddEventAlias("uievents", "uievent");
            AddEventAlias("keyevents", "keyboardevent");
            AddEventAlias("mouseevents", "mouseevent");
        }

        void AddEventAlias(String aliasName, String aliasFor)
        {
            creators.Add(aliasName, creators[aliasFor]);
        }

        /// <summary>
        /// Creates a new event.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <returns>The created event.</returns>
        public Event Create(String name)
        {
            Func<Event> creator;

            if (creators.TryGetValue(name, out creator))
                return creator();

            return null;
        }
    }
}
