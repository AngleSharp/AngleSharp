namespace AngleSharp.DOM
{
    using System.Collections.Generic;

    sealed class MutationObserver : IMutationObserver
    {
        #region Fields

        List<IMutationRecord> records;

        #endregion

        #region ctor

        public MutationObserver()
        {
            records = new List<IMutationRecord>();
        }

        #endregion

        #region Methods

        public void Disconnect()
        {
            //TODO
        }

        public void Connect(INode target, IMutationObserverInit options)
        {
            //TODO
        }

        public IMutationRecord[] Flush()
        {
            var r = records.ToArray();
            records.Clear();
            return r;
        }

        #endregion
    }
}
