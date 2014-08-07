namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// Represents an event argument.
    /// </summary>
    sealed class Event : IEvent
    {
        /// <summary>
        /// Gets a dummy placeholder event.
        /// </summary>
        public static readonly Event Empty = new Event();

        #region Fields

        EventFlags _flags;
        EventPhase _phase;
        IEventTarget _current;
        IEventTarget _target;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new event.
        /// </summary>
        public Event()
        {
            _flags = EventFlags.None;
            _phase = EventPhase.None;
            _current = null;
            _target = null;
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
            get;
            set;
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
            get;
            set;
        }

        /// <summary>
        /// Gets if the event is cancelable.
        /// </summary>
        public Boolean IsCancelable
        {
            get;
            set;
        }

        /// <summary>
        /// Gets if the default behavior has been prevented.
        /// </summary>
        public Boolean IsDefaultPrevented
        {
            get;
            set;
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
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prevents further propagation of the event.
        /// </summary>
        public void Stop()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stops the immediate propagation.
        /// </summary>
        public void StopImmediately()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Prevents the default behavior.
        /// </summary>
        public void Cancel()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initializes the event.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        public void Init(String type, Boolean bubbles, Boolean cancelable)
        {
            throw new NotImplementedException();
        }

        public Boolean Dispatch(IEventTarget target = null)
        {
            _flags |= EventFlags.Dispatch;
            _target = target;
            
            //TODO

            _flags ^= EventFlags.Dispatch;
            _phase = EventPhase.None;
            _current = null;
            return !_flags.HasFlag(EventFlags.Canceled);
        }

        #endregion
    }
}
