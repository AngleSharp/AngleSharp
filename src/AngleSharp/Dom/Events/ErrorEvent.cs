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
        public String Message => Error.Message;

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
        public Exception Error
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes the error event.
        /// </summary>
        /// <param name="filename">The name of the file containing the error.</param>
        /// <param name="line">The line within the file.</param>
        /// <param name="column">The column within the line,.</param>
        /// <param name="error">The specific error that was thrown.</param>
        public void Init(String filename, Int32 line, Int32 column, Exception error)
        {
            FileName = filename;
            Line = line;
            Column = column;
            Error = error;
        }
    }
}
