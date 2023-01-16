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
        private IEventTarget? _current;
        private IEventTarget? _target;
        private Boolean _bubbles;
        private Boolean _cancelable;
        private Boolean _composed;
        private String? _type;
        private DateTime _time;
        private List<EventPathItem>? _currentPath = null;

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
            _composed = false;
        }

        /// <summary>
        /// Creates a new event and initializes it.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        public Event(String type)
            : this(type, false, false)
        {
        }

        /// <summary>
        /// Creates a new event and initializes it.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        public Event(String type, Boolean bubbles = false, Boolean cancelable = false)
            : this(type, bubbles, cancelable, false)
        {
        }

        /// <summary>
        /// Creates a new event and initializes it.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        /// <param name="composed">If the event is composable.</param>
        [DomConstructor]
        [DomInitDict(offset: 1, optional: true)]
        public Event(String type, Boolean bubbles = false, Boolean cancelable = false, Boolean composed = false)
            : this()
        {
            Init(type, bubbles, cancelable);
            _composed = composed;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated flags.
        /// </summary>
        internal EventFlags Flags => _flags;

        /// <summary>
        /// Gets the type of event.
        /// </summary>
        [DomName("type")]
        public String Type => _type!;

        /// <summary>
        /// Gets the original target of the event.
        /// </summary>
        [DomName("target")]
        public IEventTarget? OriginalTarget => _target;

        /// <summary>
        /// Gets the current target (if bubbled).
        /// </summary>
        [DomName("currentTarget")]
        public IEventTarget? CurrentTarget => _current;

        /// <summary>
        /// Gets the phase of the event.
        /// </summary>
        [DomName("eventPhase")]
        public EventPhase Phase => _phase;

        /// <summary>
        /// Gets if the event is propagating across the shadow DOM boundary into the standard DOM.
        /// </summary>
        [DomName("composed")]
        public Boolean IsComposed => _composed;

        /// <summary>
        /// Gets if the event is actually bubbling.
        /// </summary>
        [DomName("bubbles")]
        public Boolean IsBubbling => _bubbles;

        /// <summary>
        /// Gets if the event is cancelable.
        /// </summary>
        [DomName("cancelable")]
        public Boolean IsCancelable => _cancelable;

        /// <summary>
        /// Gets if the default behavior has been prevented.
        /// </summary>
        [DomName("defaultPrevented")]
        public Boolean IsDefaultPrevented => (_flags & EventFlags.Canceled) == EventFlags.Canceled;

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
        public DateTime Time => _time;

        #endregion

        #region Methods

        /// <summary>
        /// Returns the event's path which is an array of the objects on which listeners will be invoked.
        /// See https://dom.spec.whatwg.org/#dom-event-composedpath.
        /// </summary>
        [DomName("composedPath")]
        public IEnumerable<IEventTarget> GetComposedPath()
        {
            var composedPath = new List<IEventTarget>();

            if (_currentPath is not null && _currentPath.Count > 0)
            {
                composedPath.Add(_current!);

                var currentTargetIndex = 0;
                var currentTargetHiddenSubtreeLevel = 0;
                var pathSize = _currentPath.Count;

                for (var index = pathSize - 1; index >= 0; index--)
                {
                    var c = _currentPath[index];

                    if (c.IsRootOfClosedTree)
                    {
                        currentTargetHiddenSubtreeLevel++;
                    }

                    if (c.InvocationTarget == _current)
                    {
                        currentTargetIndex = index;
                        break;
                    }

                    if (c.IsSlotInClosedTree)
                    {
                        currentTargetHiddenSubtreeLevel--;
                    }
                }

                var currentHiddenLevel = currentTargetHiddenSubtreeLevel;
                var maxHiddenLevel = currentTargetHiddenSubtreeLevel;

                for (var index = currentTargetIndex - 1; index >= 0; index--)
                {
                    var c = _currentPath[index];

                    if (c.IsRootOfClosedTree)
                    {
                        currentHiddenLevel++;
                    }

                    if (currentHiddenLevel <= maxHiddenLevel)
                    {
                        composedPath.Insert(0, c.InvocationTarget);
                    }
                    
                    if (c.IsSlotInClosedTree)
                    {
                        currentHiddenLevel--;

                        if (currentHiddenLevel < maxHiddenLevel)
                        {
                            maxHiddenLevel = currentHiddenLevel;
                        }
                    }
                }
                
                currentHiddenLevel = currentTargetHiddenSubtreeLevel;
                maxHiddenLevel = currentTargetHiddenSubtreeLevel;

                for (var index = currentTargetIndex + 1; index < pathSize; index++)
                {
                    var c = _currentPath[index];
                    
                    if (c.IsRootOfClosedTree)
                    {
                        currentHiddenLevel++;
                    }

                    if (currentHiddenLevel <= maxHiddenLevel)
                    {
                        composedPath.Add(c.InvocationTarget);
                    }
                    
                    if (c.IsSlotInClosedTree)
                    {
                        currentHiddenLevel--;

                        if (currentHiddenLevel < maxHiddenLevel)
                        {
                            maxHiddenLevel = currentHiddenLevel;
                        }
                    }
                }
            }

            return composedPath;
        }

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
        /// https://dom.spec.whatwg.org/#dispatching-events
        /// </summary>
        /// <param name="target">The target of the event.</param>
        /// <returns>A boolean if the event has been cancelled.</returns>
        internal Boolean Dispatch(IEventTarget target)
        {
            _flags |= EventFlags.Dispatch;
            _target = target;

            var eventPath = new List<EventPathItem>();

            if (target is Node parent)
            {
                while ((parent = parent.Parent!) != null)
                {
                    var rootInClosedTree = false;
                    var slotInClosedTree = false;
                    var inShadowTree = false;

                    if (parent is IElement pe && pe.GetRoot() is IShadowRoot slot)
                    {
                        inShadowTree = true;

                        if (pe.AssignedSlot is not null && slot.Mode == ShadowRootMode.Closed)
                        {
                            slotInClosedTree = true;
                        }
                    }
                    
                    if (parent is IShadowRoot root && root.Mode == ShadowRootMode.Closed)
                    {
                        rootInClosedTree = true;
                    }

                    eventPath.Add(new EventPathItem
                    {
                        InvocationTarget = parent,
                        IsInvocationTargetInShadowTree = inShadowTree,
                        IsSlotInClosedTree = slotInClosedTree,
                        IsRootOfClosedTree = rootInClosedTree,
                        RelatedTarget = null,
                        ShadowAdjustedTarget = null,
                    });
                }
            }

            _currentPath = eventPath;
            _phase = EventPhase.Capturing;
            DispatchAt(eventPath.Reverse<EventPathItem>());
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
            _current = null!;
            return (_flags & EventFlags.Canceled) == EventFlags.Canceled;
        }

        private void CallListeners(IEventTarget target)
        {
            _current = target;
            target.InvokeEventListener(this);
        }

        private void DispatchAt(IEnumerable<EventPathItem> path)
        {
            foreach (var item in path)
            {
                CallListeners(item.InvocationTarget);

                if ((_flags & EventFlags.StopPropagation) == EventFlags.StopPropagation)
                {
                    break;
                }
            }
        }

        #endregion

        struct EventPathItem
        {
            public EventTarget InvocationTarget;
            public Boolean IsInvocationTargetInShadowTree;
            public Boolean IsRootOfClosedTree;
            public Boolean IsSlotInClosedTree;
            public EventTarget? ShadowAdjustedTarget;
            public EventTarget? RelatedTarget;
        }
    }
}
