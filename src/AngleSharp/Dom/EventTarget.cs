namespace AngleSharp.Dom
{
    using AngleSharp.Dom.Events;
    using AngleSharp.Html;
    using AngleSharp.Text;
    using System;
    using System.Threading;

    /// <summary>
    /// Event target base of all DOM nodes.
    /// </summary>
    public abstract class EventTarget : IEventTarget
    {
        #region Fields

        // Copy-on-write and swapped atomically: AwaitEventAsync removes its listener from whichever
        // thread cancels its token, which can race a dispatch running on the DOM thread.
        private RegisteredEventListener[]? _listeners;

        /// <summary>
        /// Raised after all native listeners have been reset.
        /// Script bindings should discard cached handlers synchronously and defer
        /// script execution until this notification returns.
        /// </summary>
        public event EventHandler? OnReset;

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
        public void AddEventListener(String type, DomEventHandler? callback = null, Boolean capture = false)
        {
            if (callback != null)
            {
                var listener = new RegisteredEventListener(type, callback, capture);
                var current = Volatile.Read(ref _listeners);

                while (true)
                {
                    var length = current?.Length ?? 0;
                    var updated = new RegisteredEventListener[length + 1];

                    if (current != null)
                    {
                        Array.Copy(current, updated, length);
                    }

                    updated[length] = listener;
                    var previous = Interlocked.CompareExchange(ref _listeners, updated, current);

                    if (ReferenceEquals(previous, current))
                    {
                        break;
                    }

                    current = previous;
                }
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
        public void RemoveEventListener(String type, DomEventHandler? callback = null, Boolean capture = false)
        {
            if (callback != null)
            {
                var listener = new RegisteredEventListener(type, callback, capture);
                var current = Volatile.Read(ref _listeners);

                while (current != null)
                {
                    var index = IndexOf(current, listener);

                    if (index < 0)
                    {
                        break;
                    }

                    RegisteredEventListener[]? updated = null;

                    if (current.Length > 1)
                    {
                        updated = new RegisteredEventListener[current.Length - 1];
                        Array.Copy(current, 0, updated, 0, index);
                        Array.Copy(current, index + 1, updated, index, current.Length - index - 1);
                    }

                    var previous = Interlocked.CompareExchange(ref _listeners, updated, current);

                    if (ReferenceEquals(previous, current))
                    {
                        break;
                    }

                    current = previous;
                }
            }
        }

        /// <summary>
        /// Removes all listeners.
        /// </summary>
        public void RemoveEventListeners()
        {
            Volatile.Write(ref _listeners, null);
            OnReset?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Calls the listener registered for the given event.
        /// </summary>
        /// <param name="ev">The event that asks for the listeners.</param>
        public void InvokeEventListener(Event ev)
        {
            // The array is never mutated, so it is this dispatch's snapshot; a listener removed
            // meanwhile is skipped by looking it up in the current array.
            if (Volatile.Read(ref _listeners) is { } listeners)
            {
                var type = ev.Type;
                var target = ev.CurrentTarget;
                var phase = ev.Phase;

                foreach (var listener in listeners)
                {
                    if (listener.Type.Is(type) && IndexOf(Volatile.Read(ref _listeners), listener) >= 0)
                    {
                        if ((ev.Flags & EventFlags.StopImmediatePropagation) == EventFlags.StopImmediatePropagation)
                        {
                            break;
                        }

                        if ((!listener.IsCaptured || phase != EventPhase.Bubbling) && (listener.IsCaptured || phase != EventPhase.Capturing))
                        {
                            listener.Callback(target!, ev);
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
            if (Volatile.Read(ref _listeners) is { } listeners)
            {
                foreach (var listener in listeners)
                {
                    if (listener.Type.Is(type))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        internal Boolean HasEventListeners => Volatile.Read(ref _listeners) != null;

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
            if (ev is null || ((ev.Flags & EventFlags.Dispatch) == EventFlags.Dispatch) || ((ev.Flags & EventFlags.Initialized) != EventFlags.Initialized))
            {
                throw new DomException(DomError.InvalidState);
            }

            ev.IsTrusted = false;
            // The internal dispatch reports cancellation (as the Fire* helpers do), while
            // dispatchEvent reports the opposite: whether the default action may proceed.
            return !ev.Dispatch(this);
        }

        #endregion

        #region Helpers

        private static Int32 IndexOf(RegisteredEventListener[]? listeners, RegisteredEventListener listener)
        {
            if (listeners != null)
            {
                for (var i = 0; i < listeners.Length; i++)
                {
                    if (listeners[i].Equals(listener))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        #endregion

        #region Event Listener Structure

        private readonly struct RegisteredEventListener : IEquatable<RegisteredEventListener>
        {
            public RegisteredEventListener(String type, DomEventHandler callback, Boolean isCaptured)
            {
                Type = type;
                Callback = callback;
                IsCaptured = isCaptured;
            }

            public readonly String Type;
            public readonly DomEventHandler Callback;
            public readonly Boolean IsCaptured;

            public Boolean Equals(RegisteredEventListener other) =>
                IsCaptured == other.IsCaptured && Type.Is(other.Type) && Callback.Equals(other.Callback);

            public override Boolean Equals(Object? obj) => obj is RegisteredEventListener other && Equals(other);

            public override Int32 GetHashCode() => Type.GetHashCode() ^ Callback.GetHashCode();
        }

        #endregion
    }
}
