namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// Represents an event argument.
    /// </summary>
    public abstract class Event : IEvent
    {
        #region Properties

        /// <summary>
        /// Gets the type of event.
        /// </summary>
        public String Type
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the original target of the event.
        /// </summary>
        public EventTarget OriginalTarget
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the current target (if bubbled).
        /// </summary>
        public EventTarget CurrentTarget
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the phase of the event.
        /// </summary>
        public EventPhase Phase
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets if the event is actually bubbling.
        /// </summary>
        public Boolean Bubbles
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets if the event is cancelable.
        /// </summary>
        public Boolean Cancelable
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets if the default behavior has been prevented.
        /// </summary>
        public Boolean DefaultPrevented
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets if the event is trusted.
        /// </summary>
        public Boolean IsTrusted
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the originating timestamp.
        /// </summary>
        public DateTime Time
        {
            get { throw new NotImplementedException(); }
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
        public void Init(String type, Boolean bubbles, Boolean cancelable)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
