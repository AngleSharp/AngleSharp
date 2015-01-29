namespace AngleSharp.Css
{
    using AngleSharp.Dom.Css;
    using System;

    interface IValueConverter
    {
        Boolean Validate(ICssValue value);

        Int32 MinArgs { get; }

        Int32 MaxArgs { get; }
    }

    interface IValueConverter<T> : IValueConverter
    {
        Boolean TryConvert(ICssValue value, Action<T> setResult);
    }
}
