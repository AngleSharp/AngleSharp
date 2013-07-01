using System;
using System.Collections.Generic;

namespace AngleSharp
{
    class Pool<T> where T : new()
    {
        Stack<T> _items;

        public Pool()
        {
            _items = new Stack<T>();
        }

        public T Next
        {
            get
            {
                if (_items.Count == 0)
                    return new T();

                return _items.Pop();
            }
        }

        public void Return(T item)
        {
            _items.Push(item);
        }
    }
}
