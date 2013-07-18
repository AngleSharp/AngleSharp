using System;

namespace AngleSharp.DTD
{
    sealed class DtdEndOfFileToken : DtdToken
    {
        /// <summary>
        /// Creates a new EOF token.
        /// </summary>
        public DtdEndOfFileToken()
        {
            _type = DtdTokenType.EOF;
        }
    }
}
