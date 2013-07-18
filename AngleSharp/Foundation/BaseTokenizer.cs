using System;
using System.Text;

namespace AngleSharp
{
    /// <summary>
    /// Common methods and variables of all tokenizers.
    /// </summary>
    abstract class BaseTokenizer
    {
        #region Members

        protected StringBuilder stringBuffer;
        protected SourceManager src;

        #endregion

        #region Events

        /// <summary>
        /// The event will be fired once an error has been detected.
        /// </summary>
        public event EventHandler<ParseErrorEventArgs> ErrorOccurred;

        #endregion

        #region ctor

        public BaseTokenizer(SourceManager source)
        {
            src = source;
            stringBuffer = new StringBuilder();
        }

        #endregion

        #region Event-Helpers

        /// <summary>
        /// Fires an error occurred event.
        /// </summary>
        /// <param name="code">The associated error code.</param>
        protected void RaiseErrorOccurred(ErrorCode code)
        {
            if (ErrorOccurred != null)
            {
                var pck = new ParseErrorEventArgs((int)code, Errors.GetError(code));
                pck.Line = src.Line;
                pck.Column = src.Column;
                ErrorOccurred(this, pck);
            }
        }

        /// <summary>
        /// Fires an error occurred event (usually originated by another tokenizer).
        /// </summary>
        /// <param name="sender">The original sender.</param>
        /// <param name="eventArgs">The arguments of the event.</param>
        protected void RaiseErrorOccurred(Object sender, ParseErrorEventArgs eventArgs)
        {
            if (ErrorOccurred != null)
                ErrorOccurred(sender, eventArgs);
        }

        #endregion
    }
}
