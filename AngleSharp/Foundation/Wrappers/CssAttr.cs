namespace AngleSharp
{
    using System;

    sealed class CssAttr : ICssObject
    {
        readonly String _name;

        public CssAttr(String name)
        {
            _name = name;
        }

        public static explicit operator CssAttr(String str)
        {
            return new CssAttr(str);
        }

        public static implicit operator String(CssAttr str)
        {
            return str._name;
        }

        public String ToCss()
        {
            return FunctionNames.Build(FunctionNames.Attr, _name);
        }
    }
}
