namespace AngleSharp.Dom.Css
{
    using System;
    using System.IO;

    /// <summary>
    /// Represents a comment in the CSS node tree.
    /// </summary>
    sealed class CssComment : CssNode, ICssComment
    {
        #region Fields

        readonly String _data;

        #endregion

        #region ctor

        public CssComment(String data)
        {
            _data = data;
        }

        #endregion

        #region Properties

        public String Data
        {
            get { return _data; }
        }

        #endregion

        #region String Representation

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(formatter.Comment(_data));
        }

        #endregion
    }
}
