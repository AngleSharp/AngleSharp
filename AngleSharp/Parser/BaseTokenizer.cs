namespace AngleSharp.Parser
{
    using AngleSharp.Extensions;
    using System;
    using System.Diagnostics;
    using System.Text;

    /// <summary>
    /// Common methods and variables of all tokenizers.
    /// </summary>
    [DebuggerStepThrough]
    abstract class BaseTokenizer : SourceManager
    {
        #region Fields

        protected readonly StringBuilder _stringBuffer;

        #endregion

        #region Events

        /// <summary>
        /// The event will be fired once an error has been detected.
        /// </summary>
        public event EventHandler<ParseErrorEventArgs> ErrorOccurred;

        #endregion

        #region ctor

        public BaseTokenizer(TextSource source)
            : base(source)
        {
            _stringBuffer = Pool.NewStringBuilder();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the current position.
        /// </summary>
        /// <returns>A new text position.</returns>
        public TextPosition GetCurrentPosition()
        {
            return new TextPosition(Line, Column, Position);
        }

        public override void Dispose()
        {
            base.Dispose();
            _stringBuffer.ToPool();
        }

        #endregion

        #region Event-Helpers

        /// <summary>
        /// Fires an error occurred event.
        /// </summary>
        /// <param name="code">The associated error code.</param>
        public void RaiseErrorOccurred(ErrorCode code)
        {
            if (ErrorOccurred != null)
            {
                var position = GetCurrentPosition();
                var errorArguments = new ParseErrorEventArgs(code.GetCode(), code.GetMessage(), position, position);
                ErrorOccurred(this, errorArguments);
            }
        }

        /// <summary>
        /// Fires an error occurred event.
        /// </summary>
        /// <param name="args">The arguments to pass on.</param>
        public void RaiseErrorOccurred(ParseErrorEventArgs args)
        {
            if (ErrorOccurred != null)
                ErrorOccurred(this, args);
        }

        #endregion
    }
}
