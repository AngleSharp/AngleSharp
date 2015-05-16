namespace AngleSharp.Css
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Parser.Css;

    interface IValueConverter
    {
        Boolean Validate(IEnumerable<CssToken> value);
    }

    interface IValueConverter<T> : IValueConverter
    {
        Boolean TryConvert(IEnumerable<CssToken> value, Action<T> setResult);
    }
}
