namespace AngleSharp.Html
{
    using System;

    /// <summary>
    /// Describes the various validation errors.
    /// </summary>
    [Flags]
    public enum ValidationErrors : ushort
    {
        /// <summary>
        /// No errors.
        /// </summary>
        None = 0,
        /// <summary>
        /// The required value is missing.
        /// </summary>
        ValueMissing = 0x0001,
        /// <summary>
        /// The inferred type is not correct.
        /// </summary>
        TypeMismatch = 0x0002,
        /// <summary>
        /// The given pattern is not matched.
        /// </summary>
        PatternMismatch = 0x0004,
        /// <summary>
        /// The value is too long.
        /// </summary>
        TooLong = 0x0008,
        /// <summary>
        /// The value is too short.
        /// </summary>
        TooShort = 0x0010,
        /// <summary>
        /// The given value is too small.
        /// </summary>
        RangeUnderflow = 0x0020,
        /// <summary>
        /// The given value is too large.
        /// </summary>
        RangeOverflow = 0x0040,
        /// <summary>
        /// The discrete step is not matched.
        /// </summary>
        StepMismatch = 0x0080,
        /// <summary>
        /// The input was classified as invalid.
        /// </summary>
        BadInput = 0x0100,
        /// <summary>
        /// A custom error appeared.
        /// </summary>
        Custom = 0x0200
    }
}
