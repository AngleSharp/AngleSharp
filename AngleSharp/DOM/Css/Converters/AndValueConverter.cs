namespace AngleSharp.DOM.Css
{
    using System;

    sealed class AndValueConverter<T1, T2> : IValueConverter<Tuple<T1, T2>>
    {
        readonly IValueConverter<T1> _previous;
        readonly IValueConverter<T2> _next;

        public AndValueConverter(IValueConverter<T1> previous, IValueConverter<T2> next)
        {
            _previous = previous;
            _next = next;
        }

        public Boolean TryConvert(CSSValue value, Action<Tuple<T1, T2>> setResult)
        {
            var a = default(T1);
            var b = default(T2);

            if (_previous.TryConvert(value, x => a = x) && _next.TryConvert(value, x => b = x))
            {
                setResult(Tuple.Create(a, b));
                return true;
            }

            return false;
        }

        public Boolean Validate(CSSValue value)
        {
            return _previous.Validate(value) || _next.Validate(value);
        }
    }
}
