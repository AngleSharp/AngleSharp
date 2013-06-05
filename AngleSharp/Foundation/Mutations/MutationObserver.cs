using AngleSharp.DOM;
using System;
using System.Collections.Generic;

namespace AngleSharp
{
    class MutationObserver
    {
        List<MutationRecord> records;

        public MutationObserver()
        {
            records = new List<MutationRecord>();
        }

        public void Disconnect()
        {
            //TODO
        }

        public void Observe(Node target, MutationObserverInit options)
        {
            //TODO
        }

        public MutationRecord[] TakeRecords()
        {
            var r = records.ToArray();
            records.Clear();
            return r;
        }
    }
}
