using System;

namespace AngleSharp
{
    /// <summary>
    /// The base class for any event argument package send from the parser.
    /// </summary>
    public abstract class ParserEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the line within the document.
        /// </summary>
        public int Line
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the column within the document.
        /// </summary>
        public int Column
        {
            get;
            set;
        }
    }
}
