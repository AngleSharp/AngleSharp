namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;
    using AngleSharp.Html;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an event argument.
    /// </summary>
    [DomName("Event")]
    public class Event : EventArgs
    {
        #region Fields

        private EventFlags _flags;
        private EventPhase _phase;
        private IEventTarget _current;
        private IEventTarget _target;
        private Boolean _bubbles;
        private Boolean _cancelable;
        private String _type;
        private DateTime _time;

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

        /// <summary>
        /// Creates a new event and initializes it.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        [DomConstructor]
        [DomInitDict(offset: 1, optional: true)]
        public Event(String type, Boolean bubbles = false, Boolean cancelable = false)
            : this()
        {
            Init(type, bubbles, cancelable);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated flags.
        /// </summary>
        internal EventFlags Flags
        {
            get { return _flags; }
        }

        /// <summary>
        /// Gets the type of event.
        /// </summary>
        [DomName("type")]
        public String Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets the original target of the event.
        /// </summary>
        [DomName("target")]
        public IEventTarget OriginalTarget
        {
            get { return _target; }
        }

        /// <summary>
        /// Gets the current target (if bubbled).
        /// </summary>
        [DomName("currentTarget")]
        public IEventTarget CurrentTarget
        {
            get { return _current; }
        }

        /// <summary>
        /// Gets the phase of the event.
        /// </summary>
        [DomName("eventPhase")]
        public EventPhase Phase
        {
            get { return _phase; }
        }

        /// <summary>
        /// Gets if the event is actually bubbling.
        /// </summary>
        [DomName("bubbles")]
        public Boolean IsBubbling
        {
            get { return _bubbles; }
        }

        /// <summary>
        /// Gets if the event is cancelable.
        /// </summary>
        [DomName("cancelable")]
        public Boolean IsCancelable
        {
            get { return _cancelable; }
        }

        /// <summary>
        /// Gets if the default behavior has been prevented.
        /// </summary>
        [DomName("defaultPrevented")]
        public Boolean IsDefaultPrevented
        {
            get { return (_flags & EventFlags.Canceled) == EventFlags.Canceled; }
        }

        /// <summary>
        /// Gets if the event is trusted.
        /// </summary>
        [DomName("isTrusted")]
        public Boolean IsTrusted
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the originating timestamp.
        /// </summary>
        [DomName("timeStamp")]
        public DateTime Time
        {
            get { return _time; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prevents further propagation of the event.
        /// </summary>
        [DomName("stopPropagation")]
        public void Stop()
        {
            _flags |= EventFlags.StopPropagation;
        }

        /// <summary>
        /// Stops the immediate propagation.
        /// </summary>
        [DomName("stopImmediatePropagation")]
        public void StopImmediately()
        {
            _flags |= EventFlags.StopImmediatePropagation;
        }

        /// <summary>
        /// Prevents the default behavior.
        /// </summary>
        [DomName("preventDefault")]
        public void Cancel()
        {
            if (_cancelable)
            {
                _flags |= EventFlags.Canceled;
            }
        }

        /// <summary>
        /// Initializes the event.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        [DomName("initEvent")]
        public void Init(String type, Boolean bubbles, Boolean cancelable)
        {
            _flags |= EventFlags.Initialized;

            if ((_flags & EventFlags.Dispatch) != EventFlags.Dispatch)
            {
                _flags &= ~(EventFlags.StopPropagation | EventFlags.StopImmediatePropagation | EventFlags.Canceled);
                IsTrusted = false;
                _target = null;
                _type = type;
                _bubbles = bubbles;
                _cancelable = cancelable;
            }
        }

        /// <summary>
        /// Dispatch the event as described in the specification.
        /// http://www.w3.org/TR/DOM-Level-3-Events/
        /// </summary>
        /// <param name="target">The target of the event.</param>
        /// <returns>A boolean if the event has been cancelled.</returns>
        internal Boolean Dispatch(IEventTarget target)
        {
            _flags |= EventFlags.Dispatch;
            _target = target;

            var eventPath = new List<IEventTarget>();
            var parent = target as Node;

            if (parent != null)
            {
                while ((parent = parent.Parent) != null)
                {
                    eventPath.Add(parent);
                }
            }

            _phase = EventPhase.Capturing;
            DispatchAt(eventPath.Reverse<IEventTarget>());
            _phase = EventPhase.AtTarget;

            if ((_flags & EventFlags.StopPropagation) != EventFlags.StopPropagation)
            {
                CallListeners(target);
            }

            if (_bubbles)
            {
                _phase = EventPhase.Bubbling;
                DispatchAt(eventPath);
            }

            _flags &= ~EventFlags.Dispatch;
            _phase = EventPhase.None;
            _current = null;
            return (_flags & EventFlags.Canceled) == EventFlags.Canceled;
        }

        void CallListeners(IEventTarget target)
        {
            _current = target;
            target.InvokeEventListener(this);
        }

        void DispatchAt(IEnumerable<IEventTarget> targets)
        {
            foreach (var target in targets)
            {
                CallListeners(target);

                if ((_flags & EventFlags.StopPropagation) == EventFlags.StopPropagation)
                {
                    break;
                }
            }
        }

        #endregion
    }
}
