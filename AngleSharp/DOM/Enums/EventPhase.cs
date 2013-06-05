using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents the different phases of an event.
    /// </summary>
    public enum EventPhase : ushort
    {
        /// <summary>
        /// Events not currently dispatched are in this phase.
        /// </summary>
        None = 0,
        /// <summary>
        /// When an event is dispatched to an object that
        /// participates in a tree it will be in this phase
        /// before it reaches its target attribute value.
        /// </summary>
        Capturing = 1,
        /// <summary>
        /// When an event is dispatched it will be in this
        /// phase on its target attribute value.
        /// </summary>
        AtTarget = 2,
        /// <summary>
        /// When an event is dispatched to an object that
        /// participates in a tree it will be in this phase
        /// after it reaches its target attribute value.
        /// </summary>
        Bubbling = 3
    }
}
