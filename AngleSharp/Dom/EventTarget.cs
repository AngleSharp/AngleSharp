namespace AngleSharp.Dom
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using AngleSharp.Dom.Events;
    using AngleSharp.Html;

    /// <summary>
    /// Event target base of all DOM nodes.
    /// </summary>
    [DebuggerStepThrough]
    public abstract class EventTarget : IEventTarget
    {
        #region Fields

        readonly List<RegisteredEventListener> _listeners;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new event target in the DOM.
        /// </summary>
        public EventTarget()
        {
            _listeners = new List<RegisteredEventListener>();
        }

        #endregion

        #region Events

        /// <summary>
        /// Register an event handler of a specific event type on the Node.
        /// </summary>
        /// <param name="type">
        /// A string representing the event type to listen for.
        /// </param>
        /// <param name="callback">
        /// The listener parameter indicates the EventListener function to be
        /// added.
        /// </param>
        /// <param name="capture">
        /// True indicates that the user wishes to initiate capture. After
        /// initiating capture, all events of the specified type will be
        /// dispatched to the registered listener before being dispatched to
        /// any Node beneath it in the DOM tree. Events which are bubbling
        /// upward through the tree will not trigger a listener designated to
        /// use capture.
        /// </param>
        public void AddEventListener(String type, DomEventHandler callback = null, Boolean capture = false)
        {
            if (callback == null)
                return;

            _listeners.Add(new RegisteredEventListener
            {
                Type = type,
                Callback = callback,
                IsCaptured = capture
            });
        }

        /// <summary>
        /// Removes an event listener from the Node.
        /// </summary>
        /// <param name="type">
        /// A string representing the event type being removed.
        /// </param>
        /// <param name="callback">
        /// The listener parameter indicates the EventListener function to be
        /// removed.
        /// </param>
        /// <param name="capture">
        /// Specifies whether the EventListener being removed was registered as
        /// a capturing listener or not.
        /// </param>
        public void RemoveEventListener(String type, DomEventHandler callback = null, Boolean capture = false)
        {
            if (callback == null)
                return;

            _listeners.Remove(new RegisteredEventListener
            {
                Type = type,
                Callback = callback,
                IsCaptured = capture
            });
        }

        /// <summary>
        /// Calls the listener registered for the given event.
        /// </summary>
        /// <param name="ev">The event that asks for the listeners.</param>
        internal void CallEventListener(Event ev)
        {
            var type = ev.Type;
            var listeners = _listeners.ToArray();
            var target = ev.CurrentTarget;
            var phase = ev.Phase;

            foreach (var listener in listeners)
            {
                if (!_listeners.Contains(listener) || listener.Type != type)
                    continue;

                if (ev.Flags.HasFlag(EventFlags.StopImmediatePropagation))
                    break;

                if ((listener.IsCaptured && phase == EventPhase.Bubbling) || (!listener.IsCaptured && phase == EventPhase.Capturing))
                    continue;

                listener.Callback(target, ev);
            }
        }

        /// <summary>
        /// Checks if the given event type has any listeners registered.
        /// </summary>
        /// <param name="type">The name of the event.</param>
        /// <returns>
        /// True if listeners are registered, otherwise false.
        /// </returns>
        internal Boolean HasEventListener(String type)
        {
            foreach (var listener in _listeners)
            {
                if (listener.Type == type)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Dispatch an event to this Node.
        /// </summary>
        /// <param name="ev">The event to dispatch.</param>
        /// <returns>
        /// False if at least one of the event handlers, which handled this
        /// event called preventDefault(). Otherwise true.
        /// </returns>
        public Boolean Dispatch(Event ev)
        {
            if (ev == null || ev.Flags.HasFlag(EventFlags.Dispatch) || !ev.Flags.HasFlag(EventFlags.Initialized))
                throw new DomException(DomError.InvalidState);

            ev.IsTrusted = false;
            return ev.Dispatch(this);
        }

        #endregion

        #region Event Listener Structure

        struct RegisteredEventListener
        {
            public String Type;
            public DomEventHandler Callback;
            public Boolean IsCaptured;
        }

        #endregion
    }
}
