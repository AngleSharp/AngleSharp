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
        public Boolean ValueMissing
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the given type is wrong.
        /// </summary>
        public Boolean TypeMismatch
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the input does not match a given pattern.
        /// </summary>
        public Boolean PatternMismatch
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the input is too long.
        /// </summary>
        public Boolean TooLong
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the range is too small.
        /// </summary>
        public Boolean RangeUnderflow
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the range is too big.
        /// </summary>
        public Boolean RangeOverflow
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the new value is invalid.
        /// </summary>
        public Boolean StepMismatch
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if validation failed due to a custom error.
        /// </summary>
        public Boolean CustomError
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the value is valid.
        /// </summary>
        public Boolean Valid
        {
            get;
            internal set;
        }
    }
}
