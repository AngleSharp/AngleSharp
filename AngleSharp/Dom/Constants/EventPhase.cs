namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;

    /// <summary>
    /// Represents the different phases of an event.
    /// </summary>
    public enum EventPhase : ushort
    {
        /// <summary>
        /// Events not currently dispatched are in this phase.
        /// </summary>
        [DomName("NONE")]
        None = 0,
        /// <summary>
        /// When an event is dispatched to an object that
        /// participates in a tree it will be in this phase
        /// before it reaches its target attribute value.
        /// </summary>
        [DomName("CAPTURING_PHASE")]
        Capturing = 1,
        /// <summary>
        /// When an event is dispatched it will be in this
        /// phase on its target attribute value.
        /// </summary>
        [DomName("AT_TARGET")]
        AtTarget = 2,
        /// <summary>
        /// When an event is dispatched to an object that
        /// participates in a tree it will be in this phase
        /// after it reaches its target attribute value.
        /// </summary>
        [DomName("BUBBLING_PHASE")]
        Bubbling = 3
    }
}
