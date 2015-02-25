namespace AngleSharp.Dom.Html
{
    using System;

    /// <summary>
    /// A class for representing the current validity state.
    /// </summary>
    sealed class ValidityState : IValidityState
    {
        #region Fields

        ErrorType _err;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new ValidityState instance.
        /// </summary>
        internal ValidityState()
        {
            _err = ErrorType.None;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if a required value is missing.
        /// </summary>
        public Boolean IsValueMissing
        {
            get { return _err.HasFlag(ErrorType.ValueMissing); }
            set { Set(IsValueMissing, value, ErrorType.ValueMissing); }
        }

        /// <summary>
        /// Gets or sets if the given type is wrong.
        /// </summary>
        public Boolean IsTypeMismatch
        {
            get { return _err.HasFlag(ErrorType.TypeMismatch); }
            set { Set(IsTypeMismatch, value, ErrorType.TypeMismatch); }
        }

        /// <summary>
        /// Gets or sets if the input does not match a given pattern.
        /// </summary>
        public Boolean IsPatternMismatch
        {
            get { return _err.HasFlag(ErrorType.PatternMismatch); }
            set { Set(IsPatternMismatch, value, ErrorType.PatternMismatch); }
        }

        /// <summary>
        /// Gets or sets if the input is regarded as invalid.
        /// </summary>
        public Boolean IsBadInput
        {
            get { return _err.HasFlag(ErrorType.BadInput); }
            set { Set(IsBadInput, value, ErrorType.BadInput); }
        }

        /// <summary>
        /// Gets or sets if the input is too long.
        /// </summary>
        public Boolean IsTooLong
        {
            get { return _err.HasFlag(ErrorType.TooLong); }
            set { Set(IsTooLong, value, ErrorType.TooLong); }
        }

        /// <summary>
        /// Gets or sets if the input is too short.
        /// </summary>
        public Boolean IsTooShort
        {
            get { return _err.HasFlag(ErrorType.TooShort); }
            set { Set(IsTooShort, value, ErrorType.TooShort); }
        }

        /// <summary>
        /// Gets or sets if the range is too small.
        /// </summary>
        public Boolean IsRangeUnderflow
        {
            get { return _err.HasFlag(ErrorType.RangeUnderflow); }
            set { Set(IsRangeUnderflow, value, ErrorType.RangeUnderflow); }
        }

        /// <summary>
        /// Gets or sets if the range is too big.
        /// </summary>
        public Boolean IsRangeOverflow
        {
            get { return _err.HasFlag(ErrorType.RangeOverflow); }
            set { Set(IsRangeOverflow, value, ErrorType.RangeOverflow); }
        }

        /// <summary>
        /// Gets or sets if the new value is invalid.
        /// </summary>
        public Boolean IsStepMismatch
        {
            get { return _err.HasFlag(ErrorType.StepMismatch); }
            set { Set(IsStepMismatch, value, ErrorType.StepMismatch); }
        }

        /// <summary>
        /// Gets or sets if validation failed due to a custom error.
        /// </summary>
        public Boolean IsCustomError
        {
            get { return _err.HasFlag(ErrorType.Custom); }
            set { Set(IsCustomError, value, ErrorType.Custom); }
        }

        /// <summary>
        /// Gets if the value is valid.
        /// </summary>
        public Boolean IsValid
        {
            get { return _err == ErrorType.None; }
        }

        #endregion

        #region Methods

        public void Reset()
        {
            _err = ErrorType.None;
        }

        void Set(Boolean oldValue, Boolean newValue, ErrorType err)
        {
            if (newValue != oldValue)
                _err ^= err;
        }

        #endregion

        #region Flags

        [Flags]
        enum ErrorType
        {
            None = 0,
            ValueMissing = 0x0001,
            TypeMismatch = 0x0002,
            PatternMismatch = 0x0004,
            TooLong = 0x0008,
            TooShort = 0x0010,
            RangeUnderflow = 0x0020,
            RangeOverflow = 0x0040,
            StepMismatch = 0x0080,
            BadInput = 0x0100,
            Custom = 0x0200
        }

        #endregion
    }
}
