namespace AngleSharp.Css
{
    using AngleSharp.Extensions;
    using System;

    abstract class CssDocumentFunction : CssNode
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

        public override String GetSource()
        {
            return String.Concat(_name, "(", _data.CssString(), ")");
        }
    }
}
