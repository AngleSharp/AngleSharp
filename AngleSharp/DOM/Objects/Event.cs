using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents an event argument.
    /// </summary>
    [DOM("Event")]
    public abstract class Event : IEvent
    {
        #region Properties

        //TODO

        /// <summary>
        /// Gets the type of event.
        /// </summary>
        [DOM("type")]
        public String Type
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the original target of the event.
        /// </summary>
        [DOM("target")]
        public EventTarget Target
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the current target (if bubbled).
        /// </summary>
        [DOM("currentTarget")]
        public EventTarget CurrentTarget
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the phase of the event.
        /// </summary>
        [DOM("eventPhase")]
        public EventPhase EventPhase
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets if the event is actually bubbling.
        /// </summary>
        [DOM("bubbles")]
        public Boolean Bubbles
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets if the event is cancelable.
        /// </summary>
        [DOM("cancelable")]
        public Boolean Cancelable
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets if the default behavior has been prevented.
        /// </summary>
        [DOM("defaultPrevented")]
        public Boolean DefaultPrevented
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets if the event is trusted.
        /// </summary>
        [DOM("isTrusted")]
        public Boolean IsTrusted
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the originating timestamp.
        /// </summary>
        [DOM("timeStamp")]
        public DateTime TimeStamp
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prevents further propagation of the event.
        /// </summary>
        [DOM("stopPropagation")]
        public void StopPropagation()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stops the immediate propagation.
        /// </summary>
        [DOM("stopImmediatePropagation")]
        public void StopImmediatePropagation()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Prevents the default behavior.
        /// </summary>
        [DOM("preventDefault")]
        public void PreventDefault()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initializes the event.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        [DOM("initEvent")]
        public void InitEvent(String type, Boolean bubbles, Boolean cancelable)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
