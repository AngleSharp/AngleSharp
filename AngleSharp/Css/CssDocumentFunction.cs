namespace AngleSharp.Css
{
    using System;

    abstract class CssDocumentFunction : IStyleFormattable
    {
        readonly String _name;
        readonly String _data;

        public CssDocumentFunction(String name, String data)
        {
            _name = name;
            _data = data;
        }

        public String Name
        {
            get { return _name; }
        }

        public String Data
        {
            get { return _data; }
        }

        public String ToCss()
        {
            return String.Concat(_name, "(", _data, ")");
        }

        public String ToCss(IStyleFormatter formatter)
        {
            return ToCss();
        }
    }
}
