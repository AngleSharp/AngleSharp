namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// A class for representing the current validity state.
    /// </summary>
    sealed class ValidityState : IValidityState
    {
        /// <summary>
        /// Creates a new ValidityState instance.
        /// </summary>
        internal ValidityState()
        {
        }

        /// <summary>
        /// Gets if a required value is missing.
        /// </summary>
        public Boolean IsValueMissing
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the given type is wrong.
        /// </summary>
        public Boolean IsTypeMismatch
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the input does not match a given pattern.
        /// </summary>
        public Boolean IsPatternMismatch
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the input is too long.
        /// </summary>
        public Boolean IsTooLong
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the range is too small.
        /// </summary>
        public Boolean IsRangeUnderflow
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the range is too big.
        /// </summary>
        public Boolean IsRangeOverflow
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the new value is invalid.
        /// </summary>
        public Boolean IsStepMismatch
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if validation failed due to a custom error.
        /// </summary>
        public Boolean IsCustomError
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the value is valid.
        /// </summary>
        public Boolean IsValid
        {
            get;
            internal set;
        }
    }
}
