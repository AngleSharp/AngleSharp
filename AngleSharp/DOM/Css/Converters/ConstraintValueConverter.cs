namespace AngleSharp.DOM.Css
{
    using System;

    sealed class ConstraintValueConverter<T> : IValueConverter<T>
    {
        readonly Predicate<T> _constraint;
        readonly IValueConverter<T> _primary;

        public ConstraintValueConverter(IValueConverter<T> primary, Predicate<T> constraint)
        {
            _primary = primary;
            _constraint = constraint;
        }

        public Boolean TryConvert(ICssValue value, Action<T> setResult)
        {
            var t = default(T);

            if (!_primary.TryConvert(value, m => t = m) || !_constraint(t))
                return false;

            setResult(t);
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            return _primary.Validate(value);
        }
    }
}
