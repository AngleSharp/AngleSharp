namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// A class for representing the current validity state.
    /// </summary>
    [DomName("ValidityState")]
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
        [DomName("valueMissing")]
        public Boolean ValueMissing
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the given type is wrong.
        /// </summary>
        [DomName("typeMismatch")]
        public Boolean TypeMismatch
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the input does not match a given pattern.
        /// </summary>
        [DomName("patternMismatch")]
        public Boolean PatternMismatch
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the input is too long.
        /// </summary>
        [DomName("tooLong")]
        public Boolean TooLong
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the range is too small.
        /// </summary>
        [DomName("rangeUnderflow")]
        public Boolean RangeUnderflow
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the range is too big.
        /// </summary>
        [DomName("rangeOverflow")]
        public Boolean RangeOverflow
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the new value is invalid.
        /// </summary>
        [DomName("stepMismatch")]
        public Boolean StepMismatch
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the input is bad.
        /// </summary>
        [DomName("badInput")]
        public Boolean BadInput
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if validation failed due to a custom error.
        /// </summary>
        [DomName("customError")]
        public Boolean CustomError
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the value is valid.
        /// </summary>
        [DomName("valid")]
        public Boolean Valid
        {
            get;
            internal set;
        }
    }
}
