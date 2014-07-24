namespace AngleSharp
{
    using System;

    /// <summary>
    /// The collection of (known / used) event names.
    /// </summary>
    static class EventNames
    {
        /// <summary>
        /// The invalid event.
        /// </summary>
        public static readonly String Invalid = "invalid";

        /// <summary>
        /// The load event.
        /// </summary>
        public static readonly String Load = "load";

        /// <summary>
        /// The DOMContentLoaded event.
        /// </summary>
        public static readonly String DomContentLoaded = "DOMContentLoaded";

        /// <summary>
        /// The error event.
        /// </summary>
        public static readonly String Error = "error";

        /// <summary>
        /// The beforescriptexecute event.
        /// </summary>
        public static readonly String BeforeScriptExecute = "beforescriptexecute";

        /// <summary>
        /// The afterscriptexecute event.
        /// </summary>
        public static readonly String AfterScriptExecute = "afterscriptexecute";
    }
}
