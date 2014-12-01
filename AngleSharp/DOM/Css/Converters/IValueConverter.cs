namespace AngleSharp.DOM.Css
{
    using System;

    interface IValueConverter
    {
        Boolean Validate(ICssValue value);
    }

    interface IValueConverter<T> : IValueConverter
    {
        Boolean TryConvert(ICssValue value, Action<T> setResult);
    }
}
