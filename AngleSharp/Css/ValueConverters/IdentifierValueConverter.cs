namespace AngleSharp.Css.ValueConverters
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class IdentifierValueConverter<T> : IValueConverter<T>
    {
        readonly String _identifier;
        readonly T _result;

        public IdentifierValueConverter(String identifier, T result)
        {
            _identifier = identifier;
            _result = result;
        }

        public Boolean TryConvert(ICssValue value, Action<T> setResult)
        {
            if (!value.Is(_identifier))
                return false;

            setResult(_result);
            return true;
        }

        public Boolean Validate(ICssValue value)
        {
            return value.Is(_identifier);
        }

        public Int32 MinArgs
        {
            get { return 1; }
        }

        public Int32 MaxArgs
        {
            get { return 1; }
        }
    }
}
