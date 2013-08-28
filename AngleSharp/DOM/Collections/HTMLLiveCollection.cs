using System;
using System.Collections.Generic;

namespace AngleSharp.DOM.Collections
{
    sealed class HTMLLiveCollection<T> : HTMLCollection
        where T : Element
    {
        Node _parent;
        Boolean _deep;

        public HTMLLiveCollection(Node parent, Boolean deep = true)
        {
            _deep = deep;
            _parent = parent;
        }

        public Boolean IsDeep
        {
            get { return _deep; }
            set { _deep = value; }
        }

        public IEnumerable<T> Elements
        {
            get 
            {
                var it = GetElements();

                while (it.MoveNext())
                    yield return it.Current;
            }
        }

        public T GetElementAt(Int32 index)
        {
            var it = GetElements();
            var i = 0;

            while (it.MoveNext())
            {
                if (i == index)
                    return it.Current;

                i++;
            }

            return null;
        }

        protected override Element GetItem(Int32 index)
        {
            return GetElementAt(index);
        }

        protected override Int32 GetLength()
        {
            var it = GetElements();
            var count = 0;

            while (it.MoveNext())
                count++;

            return count;
        }

        public override IEnumerator<Element> GetEnumerator()
        {
            return GetElements();
        }

        internal override Int32 IndexOf(Element element)
        {
            var it = GetElements();
            var i = 0;

            while (it.MoveNext())
            {
                if (it.Current == element)
                    return i;

                i++;
            }

            return -1;
        }

        IEnumerator<T> GetElements()
        {
            if (_deep)
                return GetElementsOf(_parent);
            else
                return GetOnlyElementsOf(_parent);
        }

        static IEnumerator<T> GetElementsOf(Node parent)
        {
            foreach (var child in parent.ChildNodes)
            {
                if (child is T)
                    yield return (T)child;

                if(child.ChildNodes.Length > 0)
                    GetElementsOf((Element)child);
            }
        }

        static IEnumerator<T> GetOnlyElementsOf(Node parent)
        {
            foreach (var child in parent.ChildNodes)
            {
                if (child is T)
                    yield return (T)child;
            }
        }
    }

    abstract class HTMLLiveCollection : HTMLCollection
    {
        Node _parent;
        Boolean _deep;

        public HTMLLiveCollection(Node parent, Boolean deep)
        {
            _deep = deep;
            _parent = parent;
        }

        public Node Parent
        {
            get { return _parent; }
        }

        public Boolean IsDeep
        {
            get { return _deep; }
            set { _deep = value; }
        }

        public IEnumerable<Element> Elements
        {
            get
            {
                var it = GetElements();

                while (it.MoveNext())
                    yield return it.Current;
            }
        }

        public Element GetElementAt(Int32 index)
        {
            var it = GetElements();
            var i = 0;

            while (it.MoveNext())
            {
                if (i == index)
                    return it.Current;

                i++;
            }

            return null;
        }

        protected override Element GetItem(Int32 index)
        {
            return GetElementAt(index);
        }

        protected override Int32 GetLength()
        {
            var it = GetElements();
            var count = 0;

            while (it.MoveNext())
                count++;

            return count;
        }

        public override IEnumerator<Element> GetEnumerator()
        {
            return GetElements();
        }

        internal override Int32 IndexOf(Element element)
        {
            var it = GetElements();
            var i = 0;

            while (it.MoveNext())
            {
                if (it.Current == element)
                    return i;

                i++;
            }

            return -1;
        }

        protected abstract IEnumerator<Element> GetElements();
    }

    sealed class HTMLLiveCollection<T1, T2, T3> : HTMLLiveCollection
        where T1 : Element
        where T2 : Element
        where T3 : Element
    {
        public HTMLLiveCollection(Node parent, Boolean deep = true)
            : base(parent, deep)
        {
        }

        protected override IEnumerator<Element> GetElements()
        {
            if (IsDeep)
                return GetElementsOf(Parent);
            else
                return GetOnlyElementsOf(Parent);
        }

        static IEnumerator<Element> GetElementsOf(Node parent)
        {
            foreach (var child in parent.ChildNodes)
            {
                if (child is T1 || child is T2 || child is T3)
                    yield return (Element)child;

                if (child.ChildNodes.Length > 0)
                    GetElementsOf((Element)child);
            }
        }

        static IEnumerator<Element> GetOnlyElementsOf(Node parent)
        {
            foreach (var child in parent.ChildNodes)
            {
                if (child is T1 || child is T2 || child is T3)
                    yield return (Element)child;
            }
        }
    }
}
