namespace AngleSharp.Css
{
    using System;

    interface ICondition
    {
        Boolean Check();

        String Text { get; }
    }
}
