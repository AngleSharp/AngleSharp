using AngleSharp.DOM;
using System;

namespace AngleSharp.DTD
{
    sealed class DtdPIToken : DtdToken
    {
        #region Members

        String _target;
        String _content;

        #endregion

        #region ctor

        public DtdPIToken()
        {
            _type = DtdTokenType.ProcessingInstruction;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the target data.
        /// </summary>
        public String Target
        {
            get { return _target; }
            set { _target = value; }
        }

        /// <summary>
        /// Gets or sets the content data.
        /// </summary>
        public String Content
        {
            get { return _content; }
            set { _content = value; }
        }

        #endregion

        #region Methods

        public ProcessingInstruction ToElement()
        {
            var pi = new ProcessingInstruction();
            pi.Data = _content;
            pi.Target = _target;
            return pi;
        }

        #endregion
    }
}
