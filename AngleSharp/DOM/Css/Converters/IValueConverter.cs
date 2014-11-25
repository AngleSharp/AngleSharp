namespace AngleSharp.DOM.Css
{
    using System;

    interface IValueConverter
    {
        Boolean Validate(CSSValue value);
    }

    interface IValueConverter<T> : IValueConverter
    {
        Boolean TryConvert(CSSValue value, Action<T> setResult);
    }
}
