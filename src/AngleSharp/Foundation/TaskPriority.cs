namespace AngleSharp
{
    /// <summary>
    /// Describes the various task priorities.
    /// </summary>
    public enum TaskPriority : byte
    {
        /// <summary>
        /// The lowest possible priority.
        /// </summary>
        None,
        /// <summary>
        /// The normal priority.
        /// </summary>
        Normal,
        /// <summary>
        /// Microtasks are preferred.
        /// </summary>
        Microtask,
        /// <summary>
        /// Critical tasks are always executed asap.
        /// </summary>
        Critical
    }
}
