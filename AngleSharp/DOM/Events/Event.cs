namespace AngleSharp.DOM.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an event argument.
    /// </summary>
    class Event : IEvent
    {
        #region Fields

        EventFlags _flags;
        EventPhase _phase;
        IEventTarget _current;
        IEventTarget _target;
        Boolean _bubbles;
        Boolean _cancelable;
        String _type;
        DateTime _time;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new event.
        /// </summary>
        public Event()
        {
            _flags = EventFlags.None;
            _phase = EventPhase.None;
            _time = DateTime.Now;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated flags.
        /// </summary>
        public EventFlags Flags
        {
            get { return _flags; }
        }

        /// <summary>
        /// Gets the type of event.
        /// </summary>
        public String Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets the original target of the event.
        /// </summary>
        public IEventTarget OriginalTarget
        {
            get { return _target; }
        }

        /// <summary>
        /// Gets the current target (if bubbled).
        /// </summary>
        public IEventTarget CurrentTarget
        {
            get { return _current; }
        }

        /// <summary>
        /// Gets the phase of the event.
        /// </summary>
        public EventPhase Phase
        {
            get { return _phase; }
        }

        /// <summary>
        /// Gets if the event is actually bubbling.
        /// </summary>
        public Boolean IsBubbling
        {
            get { return _bubbles; }
        }

        /// <summary>
        /// Gets if the event is cancelable.
        /// </summary>
        public Boolean IsCancelable
        {
            get { return _cancelable; }
        }

        /// <summary>
        /// Gets if the default behavior has been prevented.
        /// </summary>
        public Boolean IsDefaultPrevented
        {
            get { return _flags.HasFlag(EventFlags.Canceled); }
        }

        /// <summary>
        /// Gets if the event is trusted.
        /// </summary>
        public Boolean IsTrusted
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the originating timestamp.
        /// </summary>
        public DateTime Time
        {
            get { return _time; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prevents further propagation of the event.
        /// </summary>
        public void Stop()
        {
            _flags |= EventFlags.StopPropagation;
        }

        /// <summary>
        /// Stops the immediate propagation.
        /// </summary>
        public void StopImmediately()
        {
            _flags |= EventFlags.StopImmediatePropagation;
        }

        /// <summary>
        /// Prevents the default behavior.
        /// </summary>
        public void Cancel()
        {
            if (_cancelable)
                _flags |= EventFlags.Canceled;
        }

        /// <summary>
        /// Initializes the event.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        public void Init(String type, Boolean bubbles, Boolean cancelable)
        {
            _flags |= EventFlags.Initialized;

            if (_flags.HasFlag(EventFlags.Dispatch))
                return;

            _flags &= ~(EventFlags.StopPropagation | EventFlags.StopImmediatePropagation | EventFlags.Canceled);
            IsTrusted = false;
            _target = null;
            _type = type;
            _bubbles = bubbles;
            _cancelable = cancelable;
        }

        public Boolean Dispatch(Node target)
        {
            _flags |= EventFlags.Dispatch;
            _target = target;

            var eventPath = new List<Node>();
            var parent = target.Parent;

            while (parent != null)
            {
                eventPath.Add(parent);
                parent = parent.Parent;
            }

            _phase = EventPhase.Capturing;
            DispatchAt(eventPath.Reverse<Node>());
            _phase = EventPhase.AtTarget;

            if (!_flags.HasFlag(EventFlags.StopPropagation))
                CallListeners(target);

            if (_bubbles)
            {
                _phase = EventPhase.Bubbling;
                DispatchAt(eventPath);
            }

            _flags &= ~EventFlags.Dispatch;
            _phase = EventPhase.None;
            _current = null;
            return !_flags.HasFlag(EventFlags.Canceled);
        }

        void CallListeners(Node target)
        {
            _current = target;
            target.CallEventListener(this);
        }

        void DispatchAt(IEnumerable<Node> targets)
        {
            foreach (var target in targets)
            {
                CallListeners(target);

                if (_flags.HasFlag(EventFlags.StopPropagation))
                    break;
            }
        }

        #endregion
    }
}
