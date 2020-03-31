namespace AngleSharp.Html.Dom
{
    using System;

    /// <summary>
    /// A class for representing the current validity state.
    /// </summary>
    sealed class ValidityState : IValidityState
    {
        #region Fields

        private ValidationErrors _err;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new ValidityState instance.
        /// </summary>
        internal ValidityState()
        {
            _err = ValidationErrors.None;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if a required value is missing.
        /// </summary>
        public Boolean IsValueMissing
        {
            get => _err.HasFlag(ValidationErrors.ValueMissing);
            set => Set(IsValueMissing, value, ValidationErrors.ValueMissing);
        }

        /// <summary>
        /// Gets or sets if the given type is wrong.
        /// </summary>
        public Boolean IsTypeMismatch
        {
            get => _err.HasFlag(ValidationErrors.TypeMismatch);
            set => Set(IsTypeMismatch, value, ValidationErrors.TypeMismatch);
        }

        /// <summary>
        /// Gets or sets if the input does not match a given pattern.
        /// </summary>
        public Boolean IsPatternMismatch
        {
            get => _err.HasFlag(ValidationErrors.PatternMismatch);
            set => Set(IsPatternMismatch, value, ValidationErrors.PatternMismatch);
        }

        /// <summary>
        /// Gets or sets if the input is regarded as invalid.
        /// </summary>
        public Boolean IsBadInput
        {
            get => _err.HasFlag(ValidationErrors.BadInput);
            set => Set(IsBadInput, value, ValidationErrors.BadInput);
        }

        /// <summary>
        /// Gets or sets if the input is too long.
        /// </summary>
        public Boolean IsTooLong
        {
            get => _err.HasFlag(ValidationErrors.TooLong);
            set => Set(IsTooLong, value, ValidationErrors.TooLong);
        }

        /// <summary>
        /// Gets or sets if the input is too short.
        /// </summary>
        public Boolean IsTooShort
        {
            get => _err.HasFlag(ValidationErrors.TooShort);
            set => Set(IsTooShort, value, ValidationErrors.TooShort);
        }

        /// <summary>
        /// Gets or sets if the range is too small.
        /// </summary>
        public Boolean IsRangeUnderflow
        {
            get => _err.HasFlag(ValidationErrors.RangeUnderflow);
            set => Set(IsRangeUnderflow, value, ValidationErrors.RangeUnderflow);
        }

        /// <summary>
        /// Gets or sets if the range is too big.
        /// </summary>
        public Boolean IsRangeOverflow
        {
            get => _err.HasFlag(ValidationErrors.RangeOverflow);
            set => Set(IsRangeOverflow, value, ValidationErrors.RangeOverflow);
        }

        /// <summary>
        /// Gets or sets if the new value is invalid.
        /// </summary>
        public Boolean IsStepMismatch
        {
            get => _err.HasFlag(ValidationErrors.StepMismatch);
            set => Set(IsStepMismatch, value, ValidationErrors.StepMismatch);
        }

        /// <summary>
        /// Gets or sets if validation failed due to a custom error.
        /// </summary>
        public Boolean IsCustomError
        {
            get => _err.HasFlag(ValidationErrors.Custom);
            set => Set(IsCustomError, value, ValidationErrors.Custom);
        }

        /// <summary>
        /// Gets if the value is valid.
        /// </summary>
        public Boolean IsValid => _err == ValidationErrors.None;

        #endregion

        #region Methods

        public void Reset(ValidationErrors err = ValidationErrors.None)
        {
            _err = err;
        }

        private void Set(Boolean oldValue, Boolean newValue, ValidationErrors err)
        {
            if (newValue != oldValue)
            {
                _err ^= err;
            }
        }

        #endregion
    }
}
