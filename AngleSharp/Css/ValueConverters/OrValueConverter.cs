namespace AngleSharp.Css.ValueConverters
{
    using AngleSharp.Dom.Css;
    using System;

    sealed class OrValueConverter<T> : IValueConverter<T>
    {
        readonly IValueConverter<T> _previous;
        readonly IValueConverter<T> _next;

        public OrValueConverter(IValueConverter<T> previous, IValueConverter<T> next)
        {
            _previous = previous;
            _next = next;
        }

        public Boolean TryConvert(ICssValue value, Action<T> setResult)
        {
            return _previous.TryConvert(value, setResult) || _next.TryConvert(value, setResult);
        }

        public Boolean Validate(ICssValue value)
        {
            return _previous.Validate(value) || _next.Validate(value);
        }

        public Int32 MinArgs
        {
            get { return Math.Min(_previous.MinArgs, _next.MinArgs); }
        }

        public Int32 MaxArgs
        {
            get { return Math.Max(_previous.MaxArgs, _next.MaxArgs); }
        }
    }
}
