using AngleSharp.DOM;
using System;

namespace AngleSharp.DTD
{
    sealed class DtdCommentToken : DtdToken
    {
        #region Members

        String _data;

        #endregion

        #region ctor

        public DtdCommentToken()
        {
            _type = DtdTokenType.Comment;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the supplied data.
        /// </summary>
        public String Data
        {
            get { return _data; }
            set { _data = value; }
        }

        #endregion

        #region Methods

        public Comment ToElement()
        {
            var comment = new Comment();
            comment.Data = _data;
            return comment;
        }

        #endregion
    }
}
