namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// The ValidityState interface represents the validity states that an element
    /// can be in, with respect to constraint validation. Together, they help explain
    /// why an element's value fails to validate, if it's not valid.
    /// </summary>
    [DomName("ValidityState")]
    public interface IValidityState
    {
        /// <summary>
        /// Gets if the element has a required attribute, but no value.
        /// </summary>
        [DomName("valueMissing")]
        Boolean ValueMissing { get; }

        /// <summary>
        /// Gets if the value is not in the required syntax (when type is email or url).
        /// </summary>
        [DomName("typeMismatch")]
        Boolean TypeMismatch { get; }

        /// <summary>
        /// Gets if the value does not match the specified pattern.
        /// </summary>
        [DomName("patternMismatch")]
        Boolean PatternMismatch { get; }

        /// <summary>
        /// Gets if the value exceeds the specified maxlength.
        /// </summary>
        [DomName("tooLong")]
        Boolean TooLong { get; }

        /// <summary>
        /// Gets if the value is less than the minimum specified by the min attribute.
        /// </summary>
        [DomName("rangeUnderflow")]
        Boolean RangeUnderflow { get; }

        /// <summary>
        /// Gets if the value is greater than the maximum specified by the max attribute.
        /// </summary>
        [DomName("rangeOverflow")]
        Boolean RangeOverflow { get; }

        /// <summary>
        /// Gets if the value does not fit the rules determined by the step attribute
        /// (that is, it's not evenly divisible by the step value).
        /// </summary>
        [DomName("stepMismatch")]
        Boolean StepMismatch { get; }

        /// <summary>
        /// Gets the element's custom validity message.
        /// </summary>
        [DomName("customError")]
        Boolean CustomError { get; }

        /// <summary>
        /// Gets if the element meets all constraint validations, and is therefore
        /// considered to be valid.
        /// </summary>
        [DomName("valid")]
        Boolean Valid { get; }
    }
}
