namespace AngleSharp.Css.ValueConverters
{
    using AngleSharp.Dom.Css;
    using System;

    sealed class ConstraintValueConverter<T> : IValueConverter<T>
    {
        readonly IValueConverter<T> _primary;
        readonly Predicate<T> _constraint;

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
            var t = default(T);
            return _primary.TryConvert(value, m => t = m) && _constraint(t);
        }

        public Int32 MinArgs
        {
            get { return _primary.MinArgs; }
        }

        public Int32 MaxArgs
        {
            get { return _primary.MaxArgs; }
        }
    }
}
