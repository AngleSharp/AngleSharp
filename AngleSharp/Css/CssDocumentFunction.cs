namespace AngleSharp.Css
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    abstract class CssDocumentFunction : IDocumentFunction
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

        public IEnumerable<ICssNode> Children
        {
            get { return Enumerable.Empty<ICssNode>(); }
        }

        public abstract Boolean Matches(Url url);

        public String ToCss()
        {
            return String.Concat(_name, "(", _data.CssString(), ")");
        }

        public String ToCss(IStyleFormatter formatter)
        {
            return ToCss();
        }
    }
}
