using System;

namespace AngleSharp.DOM.Html
{
    interface IValidityState
    {  
        bool ValueMissing { get; }
        bool TypeMismatch { get; }
        bool PatternMismatch { get; }
        bool TooLong { get; }
        bool RangeUnderflow { get; }
        bool RangeOverflow { get; }
        bool StepMismatch { get; }
        bool BadInput { get; }
        bool CustomError { get; }
        bool Valid { get; }
    }
}
