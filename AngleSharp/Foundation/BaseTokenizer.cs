using AngleSharp.Events;
using System;
using System.Diagnostics;
using System.Text;

namespace AngleSharp
{
    /// <summary>
    /// Common methods and variables of all tokenizers.
    /// </summary>
    [DebuggerStepThrough]
    abstract class BaseTokenizer
    {
        #region Members

        protected StringBuilder _stringBuffer;
        protected SourceManager _src;

        #endregion

        #region Events

        /// <summary>
        /// The event will be fired once an error has been detected.
        /// </summary>
        public event ParseErrorEventHandler ErrorOccurred;

        #endregion

        #region ctor

        public BaseTokenizer(SourceManager source)
        {
            _src = source;
            _stringBuffer = new StringBuilder();
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
                pck.Line = _src.Line;
                pck.Column = _src.Column;
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
