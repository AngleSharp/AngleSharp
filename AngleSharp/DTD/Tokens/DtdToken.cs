using System;
using System.Diagnostics;

namespace AngleSharp.DTD
{
    /// <summary>
    /// The abstract base class of any DTD token.
    /// </summary>
    [DebuggerStepThrough]
    abstract class DtdToken
    {
        #region Members

        static DtdEndOfFileToken eof;
        protected String _name;
        protected DtdTokenType _type;

        #endregion

        #region Factory

        /// <summary>
        /// Gets the end of file token.
        /// </summary>
        public static DtdEndOfFileToken EOF
        {
            get { return eof ?? (eof = new DtdEndOfFileToken()); }
        }

        #endregion

        #region Properties

        public DtdTokenType Type
        {
            get { return _type; }
        }

        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        #endregion
    }
}
