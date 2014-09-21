namespace AngleSharp
{
    using System;

    sealed class CssIdentifier : ICssObject
    {
        readonly String _token;

        public CssIdentifier(String token)
        {
            _token = token;
        }

        public static explicit operator CssIdentifier(String str)
        {
            return new CssIdentifier(str);
        }

        public static implicit operator String(CssIdentifier str)
        {
            return str._token;
        }

        public String ToCss()
        {
            return _token;
        }
    }
}
