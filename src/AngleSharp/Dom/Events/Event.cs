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

        private String? _type;
        private List<EventPathItem>? _currentPath = null;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new event.
        /// </summary>
        public Event()
        {
            Flags = EventFlags.None;
            Phase = EventPhase.None;
            Time = DateTime.Now;
            IsComposed = false;
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
            IsComposed = composed;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated flags.
        /// </summary>
        internal EventFlags Flags { get; private set; }

        /// <summary>
        /// Gets the type of event.
        /// </summary>
        [DomName("type")]
        public String Type => _type!;

        /// <summary>
        /// Gets the original target of the event.
        /// </summary>
        [DomName("target")]
        public IEventTarget? OriginalTarget { get; private set; }

        /// <summary>
        /// Gets the current target (if bubbled).
        /// </summary>
        [DomName("currentTarget")]
        public IEventTarget? CurrentTarget { get; private set; }

        /// <summary>
        /// Gets the phase of the event.
        /// </summary>
        [DomName("eventPhase")]
        public EventPhase Phase { get; private set; }

        /// <summary>
        /// Gets if the event is propagating across the shadow DOM boundary into the standard DOM.
        /// </summary>
        [DomName("composed")]
        public Boolean IsComposed { get; }

        /// <summary>
        /// Gets if the event is actually bubbling.
        /// </summary>
        [DomName("bubbles")]
        public Boolean IsBubbling { get; private set; }

        /// <summary>
        /// Gets if the event is cancelable.
        /// </summary>
        [DomName("cancelable")]
        public Boolean IsCancelable { get; private set; }

        /// <summary>
        /// Gets if the default behavior has been prevented.
        /// </summary>
        [DomName("defaultPrevented")]
        public Boolean IsDefaultPrevented => (Flags & EventFlags.Canceled) == EventFlags.Canceled;

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
        public DateTime Time { get; }

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
                composedPath.Add(CurrentTarget!);

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

                    if (c.InvocationTarget == CurrentTarget)
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
            Flags |= EventFlags.StopPropagation;
        }

        /// <summary>
        /// Stops the immediate propagation.
        /// </summary>
        [DomName("stopImmediatePropagation")]
        public void StopImmediately()
        {
            Flags |= EventFlags.StopImmediatePropagation;
        }

        /// <summary>
        /// Prevents the default behavior.
        /// </summary>
        [DomName("preventDefault")]
        public void Cancel()
        {
            if (IsCancelable)
            {
                Flags |= EventFlags.Canceled;
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
            Flags |= EventFlags.Initialized;

            if ((Flags & EventFlags.Dispatch) != EventFlags.Dispatch)
            {
                Flags &= ~(EventFlags.StopPropagation | EventFlags.StopImmediatePropagation | EventFlags.Canceled);
                IsTrusted = false;
                OriginalTarget = null;
                _type = type;
                IsBubbling = bubbles;
                IsCancelable = cancelable;
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
            Flags |= EventFlags.Dispatch;
            OriginalTarget = target;

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
            Phase = EventPhase.Capturing;
            DispatchAt(eventPath.Reverse<EventPathItem>());
            Phase = EventPhase.AtTarget;

            if ((Flags & EventFlags.StopPropagation) != EventFlags.StopPropagation)
            {
                CallListeners(target);
            }

            if (IsBubbling)
            {
                Phase = EventPhase.Bubbling;
                DispatchAt(eventPath);
            }

            Flags &= ~EventFlags.Dispatch;
            Phase = EventPhase.None;
            CurrentTarget = null!;
            return (Flags & EventFlags.Canceled) == EventFlags.Canceled;
        }

        private void CallListeners(IEventTarget target)
        {
            CurrentTarget = target;
            target.InvokeEventListener(this);
        }

        private void DispatchAt(IEnumerable<EventPathItem> path)
        {
            foreach (var item in path)
            {
                CallListeners(item.InvocationTarget);

                if ((Flags & EventFlags.StopPropagation) == EventFlags.StopPropagation)
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
