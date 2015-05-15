namespace AngleSharp.Css
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Parser.Css;

    interface IValueConverter
    {
        Boolean Validate(IEnumerable<CssToken> value);

        Int32 MinArgs { get; }

        Int32 MaxArgs { get; }
    }

    interface IValueConverter<T> : IValueConverter
    {
        Boolean TryConvert(IEnumerable<CssToken> value, Action<T> setResult);
    }
}
