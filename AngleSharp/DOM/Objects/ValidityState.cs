using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// A class for representing the current validity state.
    /// </summary>
    [DOM("ValidityState")]
    public sealed class ValidityState : IValidityState
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
        [DOM("valueMissing")]
        public Boolean ValueMissing
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the given type is wrong.
        /// </summary>
        [DOM("typeMismatch")]
        public Boolean TypeMismatch
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the input does not match a given pattern.
        /// </summary>
        [DOM("patternMismatch")]
        public Boolean PatternMismatch
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the input is too long.
        /// </summary>
        [DOM("tooLong")]
        public Boolean TooLong
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the range is too small.
        /// </summary>
        [DOM("rangeUnderflow")]
        public Boolean RangeUnderflow
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the range is too big.
        /// </summary>
        [DOM("rangeOverflow")]
        public Boolean RangeOverflow
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the new value is invalid.
        /// </summary>
        [DOM("stepMismatch")]
        public Boolean StepMismatch
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the input is bad.
        /// </summary>
        [DOM("badInput")]
        public Boolean BadInput
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if validation failed due to a custom error.
        /// </summary>
        [DOM("customError")]
        public Boolean CustomError
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the value is valid.
        /// </summary>
        [DOM("valid")]
        public Boolean Valid
        {
            get;
            internal set;
        }
    }
}
