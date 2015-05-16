namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Parser.Css;

    sealed class ConstraintValueConverter<T> : IValueConverter<T>
    {
        readonly IValueConverter<T> _primary;
        readonly Predicate<T> _constraint;

        public ConstraintValueConverter(IValueConverter<T> primary, Predicate<T> constraint)
        {
            _primary = primary;
            _constraint = constraint;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<T> setResult)
        {
            var t = default(T);

            if (!_primary.TryConvert(value, m => t = m) || !_constraint(t))
                return false;

            setResult(t);
            return true;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var t = default(T);
            return _primary.TryConvert(value, m => t = m) && _constraint(t);
        }
    }
}
