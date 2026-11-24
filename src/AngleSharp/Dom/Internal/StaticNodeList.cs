namespace AngleSharp.Dom
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The NodeList a script gets for what querySelectorAll returns ([DomReturnType]): the method
    /// keeps its typed IHtmlCollection&lt;IElement&gt; result for .NET callers, and this wraps it
    /// for a DOM registration, which cannot hand out an object as another type than its own.
    /// </summary>
    /// <remarks>
    /// The result of querySelectorAll is static and a new one per call, so wrapping it changes
    /// neither the identity script sees nor what it contains.
    /// </remarks>
    sealed class StaticNodeList : INodeList
    {
        private readonly IHtmlCollection<IElement> _elements;

        public StaticNodeList(IHtmlCollection<IElement> elements)
        {
            _elements = elements;
        }

        public INode this[Int32 index] => _elements[index];

        public Int32 Length => _elements.Length;

        public IEnumerator<INode> GetEnumerator()
        {
            foreach (var element in _elements)
            {
                yield return element;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void ToHtml(TextWriter writer, IMarkupFormatter formatter)
        {
            foreach (var element in _elements)
            {
                element.ToHtml(writer, formatter);
            }
        }
    }
}
