namespace AngleSharp.DOM
{
    using System.Collections.Generic;

    sealed class MutationObserver : IMutationObserver
    {
        #region Fields

        readonly Queue<IMutationRecord> records;

        #endregion

        #region ctor

        public MutationObserver()
        {
            records = new Queue<IMutationRecord>();
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

        public IEnumerable<IMutationRecord> Flush()
        {
            while (records.Count != 0)
                yield return records.Dequeue();
        }

        #endregion
    }
}
