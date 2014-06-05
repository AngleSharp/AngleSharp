namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// Represents a propert event target.
    /// </summary>
    sealed class EventTarget : IEventTarget
    {
        //TODO

        /// <summary>
        /// Adds an event listener.
        /// </summary>
        /// <param name="type">The name of the event.</param>
        /// <param name="callback">The callback to add.</param>
        /// <param name="capture">Determines if the event captures.</param>
        public void AddEventListener(String type, EventListener callback = null, Boolean capture = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes an event listener.
        /// </summary>
        /// <param name="type">The name of the event.</param>
        /// <param name="callback">The callback to remove.</param>
        /// <param name="capture">If the event captured.</param>
        public void RemoveEventListener(String type, EventListener callback = null, Boolean capture = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dispatches an event.
        /// </summary>
        /// <param name="e">The arguments.</param>
        /// <returns>True if the event has been dispatched succesfully.</returns>
        public Boolean Dispatch(IEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
