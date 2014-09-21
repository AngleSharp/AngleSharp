namespace AngleSharp
{
    using System;

    sealed class CssString : ICssObject
    {
        readonly String _value;

        public CssString(String value)
        {
            _value = value;
        }

        public static explicit operator CssString(String str)
        {
            return new CssString(str);
        }

        public static implicit operator String(CssString str)
        {
            return str._value;
        }

        public String ToCss()
        {
            return String.Concat("'", _value, "'");
        }
    }
}
