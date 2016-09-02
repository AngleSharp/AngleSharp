namespace AngleSharp.Dom
{
    using AngleSharp.Dom.Events;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Event target base of all DOM nodes.
    /// </summary>
    public abstract class EventTarget : IEventTarget
    {
        #region Fields

        private List<RegisteredEventListener> _listeners;

        #endregion

        #region Properties

        private List<RegisteredEventListener> Listeners
        {
            get { return _listeners ?? (_listeners = new List<RegisteredEventListener>()); }
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
            if (callback != null)
            {
                Listeners.Add(new RegisteredEventListener
                {
                    Type = type,
                    Callback = callback,
                    IsCaptured = capture
                });
            }
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
            if (callback != null)
            {
                _listeners?.Remove(new RegisteredEventListener
                {
                    Type = type,
                    Callback = callback,
                    IsCaptured = capture
                });
            }
        }

        /// <summary>
        /// Removes all listeners.
        /// </summary>
        public void RemoveEventListeners()
        {
            if (_listeners != null)
            {
                _listeners.Clear();
            }
        }

        /// <summary>
        /// Calls the listener registered for the given event.
        /// </summary>
        /// <param name="ev">The event that asks for the listeners.</param>
        public void InvokeEventListener(Event ev)
        {
            if (_listeners != null)
            {
                var type = ev.Type;
                var listeners = _listeners.ToArray();
                var target = ev.CurrentTarget;
                var phase = ev.Phase;

                foreach (var listener in listeners)
                {
                    if (_listeners.Contains(listener) && listener.Type.Is(type))
                    {
                        if ((ev.Flags & EventFlags.StopImmediatePropagation) == EventFlags.StopImmediatePropagation)
                        {
                            break;
                        }

                        if ((!listener.IsCaptured || phase != EventPhase.Bubbling) && (listener.IsCaptured || phase != EventPhase.Capturing))
                        {
                            listener.Callback(target, ev);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the given event type has any listeners registered.
        /// </summary>
        /// <param name="type">The name of the event.</param>
        /// <returns>
        /// True if listeners are registered, otherwise false.
        /// </returns>
        public Boolean HasEventListener(String type)
        {
            if (_listeners != null)
            {
                foreach (var listener in _listeners)
                {
                    if (listener.Type.Is(type))
                    {
                        return true;
                    }
                }
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
            if (ev == null || ((ev.Flags & EventFlags.Dispatch) == EventFlags.Dispatch) || ((ev.Flags & EventFlags.Initialized) != EventFlags.Initialized))
                throw new DomException(DomError.InvalidState);

            ev.IsTrusted = false;
            return ev.Dispatch(this);
        }

        #endregion

        #region Event Listener Structure

        private struct RegisteredEventListener
        {
            public String Type;
            public DomEventHandler Callback;
            public Boolean IsCaptured;
        }

        #endregion
    }
}
