using System;

namespace AngleSharp.DTD
{
    sealed class DtdParameterToken : DtdToken
    {
        #region ctor

        /// <summary>
        /// Creates a new parameter entity token.
        /// </summary>
        public DtdParameterToken()
        {
            _type = DtdTokenType.PEReference;
        }

        #endregion
    }
}
