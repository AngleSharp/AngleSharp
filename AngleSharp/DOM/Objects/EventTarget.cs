using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents a propert event target.
    /// </summary>
    [DOM("EventTarget")]
    public class EventTarget : IEventTarget
    {
        //TODO

        /// <summary>
        /// Adds an event listener.
        /// </summary>
        /// <param name="type">The name of the event.</param>
        /// <param name="callback">The callback to add.</param>
        /// <param name="capture">Determines if the event captures.</param>
        [DOM("addEventListener")]
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
        [DOM("removeEventListener")]
        public void RemoveEventListener(String type, EventListener callback = null, Boolean capture = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dispatches an event.
        /// </summary>
        /// <param name="e">The arguments.</param>
        /// <returns>True if the event has been dispatched succesfully.</returns>
        [DOM("dispatchEvent")]
        public Boolean DispatchEvent(Event e)
        {
            throw new NotImplementedException();
        }
    }
}
