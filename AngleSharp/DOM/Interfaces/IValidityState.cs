using System;

namespace AngleSharp.DOM.Html
{
    interface IValidityState
    {  
        Boolean ValueMissing { get; }
        Boolean TypeMismatch { get; }
        Boolean PatternMismatch { get; }
        Boolean TooLong { get; }
        Boolean RangeUnderflow { get; }
        Boolean RangeOverflow { get; }
        Boolean StepMismatch { get; }
        Boolean BadInput { get; }
        Boolean CustomError { get; }
        Boolean Valid { get; }
    }
}
