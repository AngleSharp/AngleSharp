using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// A class for representing the current validity state.
    /// </summary>
    public class ValidityState : IValidityState
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
        public bool ValueMissing
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the given type is wrong.
        /// </summary>
        public bool TypeMismatch
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the input does not match a given pattern.
        /// </summary>
        public bool PatternMismatch
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the input is too long.
        /// </summary>
        public bool TooLong
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the range is too small.
        /// </summary>
        public bool RangeUnderflow
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the range is too big.
        /// </summary>
        public bool RangeOverflow
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the new value is invalid.
        /// </summary>
        public bool StepMismatch
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the input is bad.
        /// </summary>
        public bool BadInput
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if validation failed due to a custom error.
        /// </summary>
        public bool CustomError
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the value is valid.
        /// </summary>
        public bool Valid
        {
            get;
            internal set;
        }
    }
}
