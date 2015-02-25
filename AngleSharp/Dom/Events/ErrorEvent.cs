namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the error event arguments.
    /// </summary>
    [DomName("ErrorEvent")]
    public class ErrorEvent : Event
    {
        /// <summary>
        /// Gets the message describing the error.
        /// </summary>
        [DomName("message")]
        public String Message
        {
            get { return Error.Message; }
        }

        /// <summary>
        /// Gets the filename where the error occurred.
        /// </summary>
        [DomName("filename")]
        public String FileName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the line number of the error.
        /// </summary>
        [DomName("lineno")]
        public Int32 Line
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the column number of the error.
        /// </summary>
        [DomName("colno")]
        public Int32 Column
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the exception describing the error.
        /// </summary>
        [DomName("error")]
        public DomException Error
        {
            get;
            private set;
        }
    }
}
