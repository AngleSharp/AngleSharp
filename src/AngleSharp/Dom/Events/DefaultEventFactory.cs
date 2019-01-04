namespace AngleSharp.Dom.Events
{
    using AngleSharp.Html.Dom.Events;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to Event instance creation mappings.
    /// </summary>
    public class DefaultEventFactory : IEventFactory
    {
        /// <summary>
        /// Represents a creator delegate for creating a new event.
        /// </summary>
        /// <returns>The created event.</returns>
        public delegate Event Creator();

        private readonly Dictionary<String, Creator> _creators = new Dictionary<String, Creator>(StringComparer.OrdinalIgnoreCase)
        {
            { "event", () => new Event() },
            { "uievent", () => new UiEvent() },
            { "focusevent", () => new FocusEvent() },
            { "keyboardevent", () => new KeyboardEvent() },
            { "mouseevent", () => new MouseEvent() },
            { "wheelevent", () => new WheelEvent() },
            { "customevent", () => new CustomEvent() }
        };

        /// <summary>
        /// Creates a new event factory.
        /// </summary>
        public DefaultEventFactory()
        {
            //Aliases
            AddEventAlias("events", "event");
            AddEventAlias("htmlevents", "event");
            AddEventAlias("uievents", "uievent");
            AddEventAlias("keyevents", "keyboardevent");
            AddEventAlias("mouseevents", "mouseevent");
        }

        /// <summary>
        /// Registers a new creator for the specified event name.
        /// Throws an exception if another creator for the given
        /// event name is already added.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <param name="creator">The creator to invoke.</param>
        public void Register(String name, Creator creator)
        {
            _creators.Add(name, creator);
        }

        /// <summary>
        /// Unregisters an existing creator for the given event name.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <returns>The registered creator, if any.</returns>
        public Creator Unregister(String name)
        {
            if (_creators.TryGetValue(name, out var creator))
            {
                _creators.Remove(name);
            }

            return creator;
        }

        /// <summary>
        /// Creates the default event for the given name. By default
        /// this returns null.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <returns>The created event.</returns>
        protected virtual Event CreateDefault(String name)
        {
            return default(Event);
        }

        /// <summary>
        /// Creates a new event.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <returns>The created event.</returns>
        public Event Create(String name)
        {
            if (name != null && _creators.TryGetValue(name, out var creator))
            {
                return creator.Invoke();
            }

            return CreateDefault(name);
        }

        private void AddEventAlias(String aliasName, String aliasFor)
        {
            _creators.Add(aliasName, _creators[aliasFor]);
        }
    }
}
