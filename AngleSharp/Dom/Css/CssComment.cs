namespace AngleSharp.Dom.Css
{
    using System;

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

        public override String ToCss(IStyleFormatter formatter)
        {
            return formatter.Comment(_data);
        }

        #endregion
    }
}
