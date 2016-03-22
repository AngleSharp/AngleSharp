namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
    using System;
    using System.IO;

    /// <summary>
    /// Represents a feature expression within a media query.
    /// </summary>
    abstract class DocumentFunction : CssNode, IDocumentFunction
    {
        #region Fields

        readonly String _name;
        readonly String _data;

        #endregion

        #region ctor

        internal DocumentFunction(String name, String data)
        {
            _name = name;
            _data = data;
        }

        #endregion

        #region Properties

        public String Name
        {
            get { return _name; }
        }

        public String Data
        {
            get { return _data; }
        }

        #endregion

        #region Methods

        public abstract Boolean Matches(Url url);

        #endregion

        #region String Representation

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(_name.CssFunction(_data.CssString()));
        }

        #endregion
    }
}
