namespace AngleSharp.DOM.Collections
{
    using AngleSharp.DOM.Events;
    using System;
    using System.Collections.Generic;

    sealed class TouchList : ITouchList
    {
        readonly List<ITouch> _touches;

        public TouchList(IEnumerable<ITouch> touches)
        {
            _touches = new List<ITouch>(touches);
        }

        public Int32 Length
        {
            get { return _touches.Count; }
        }

        public ITouch this[Int32 index]
        {
            get { return _touches[index]; }
        }
    }
}
