using System;
using System.Collections.Generic;

namespace AngleSharp.DOM.Collections
{
    sealed class HTMLLiveCollectionWithAttr<T> : HTMLLiveCollection
        where T : Element
    {
        String _attribute;

        public HTMLLiveCollectionWithAttr(Node parent, String attribute)
            : base(parent, true)
        {
            _attribute = attribute;
        }

        protected override IEnumerator<Element> GetElements()
        {
            return GetElementsOf(Parent);
        }

        IEnumerator<Element> GetElementsOf(Node parent)
        {
            foreach (var child in parent.ChildNodes)
            {
                if (child is T && child.Attributes[_attribute] != null)
                    yield return (Element)child;

                if(child.ChildNodes.Length > 0)
                    GetElementsOf((Element)child);
            }
        }
    }

    sealed class HTMLLiveCollectionWithAttr<T1, T2> : HTMLLiveCollection
        where T1 : Element
        where T2 : Element
    {
        String _attribute;

        public HTMLLiveCollectionWithAttr(Node parent, String attribute)
            : base(parent, true)
        {
            _attribute = attribute;
        }

        protected override IEnumerator<Element> GetElements()
        {
            return GetElementsOf(Parent);
        }

        IEnumerator<Element> GetElementsOf(Node parent)
        {
            foreach (var child in parent.ChildNodes)
            {
                if ((child is T1 || child is T2) && child.Attributes[_attribute] != null)
                    yield return (Element)child;

                if (child.ChildNodes.Length > 0)
                    GetElementsOf((Element)child);
            }
        }
    }
}
