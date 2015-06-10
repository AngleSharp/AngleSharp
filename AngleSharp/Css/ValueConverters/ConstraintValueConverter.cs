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

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            //TODO Use Convert instead of Validate
            var tmp = default(T);
            return _primary.Validate(value) && _constraint(tmp);
        }
    }
}
