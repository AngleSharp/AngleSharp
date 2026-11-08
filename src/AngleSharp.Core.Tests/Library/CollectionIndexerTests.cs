namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The collection interfaces became IReadOnlyList&lt;T&gt; on .NET 8 only, where the indexer
    /// was the IReadOnlyList&lt;T&gt; one. A library built against the netstandard2.0 build calls
    /// the interface's own indexer, so it failed with a MissingMethodException on .NET 8; that
    /// indexer has to exist on every target, without asking more of implementers.
    /// </summary>
    [TestFixture]
    public class CollectionIndexerTests
    {
        [TestCase(typeof(IHtmlCollection<IElement>), typeof(IElement))]
        [TestCase(typeof(IStringList), typeof(String))]
        [TestCase(typeof(ITokenList), typeof(String))]
        public void InterfaceDeclaresItsOwnIndexer(Type type, Type itemType)
        {
            var indexer = type.GetProperty("Item", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly, null, itemType, new[] { typeof(Int32) }, null);

            Assert.IsNotNull(indexer);
            Assert.AreEqual("item", indexer.GetCustomAttribute<DomNameAttribute>()?.OfficialName);
        }

        [Test]
        public void FormControlsAreIndexedThroughTheCollectionInterface()
        {
            var document = "<form><input name=a><button name=b></button></form>".ToHtmlDocument();
            IHtmlCollection<IHtmlElement> elements = ((IHtmlFormElement)document.QuerySelector("form")).Elements;

            Assert.AreEqual("input", elements[0].LocalName);
            Assert.AreEqual("button", elements[1].LocalName);
        }

#if NET8_0_OR_GREATER
        [Test]
        public void ImplementerWithOnlyTheReadOnlyListIndexerIsIndexedBothWays()
        {
            var document = "<p>a</p><p>b</p>".ToHtmlDocument();
            var collection = new ExplicitCollection(document.QuerySelectorAll("p"));
            IHtmlCollection<IElement> asCollection = collection;
            IReadOnlyList<IElement> asList = collection;

            Assert.AreEqual("b", asCollection[1].TextContent);
            Assert.AreEqual("b", asList[1].TextContent);
        }

        [Test]
        public void StringListWithOnlyTheReadOnlyListIndexerIsIndexedBothWays()
        {
            var list = new ExplicitStringList("a", "b");
            IStringList asStringList = list;
            IReadOnlyList<String> asList = list;

            Assert.AreEqual("b", asStringList[1]);
            Assert.AreEqual("b", asList[1]);
        }

        private sealed class ExplicitCollection : IHtmlCollection<IElement>
        {
            private readonly IElement[] _items;

            public ExplicitCollection(IEnumerable<IElement> items) => _items = items.ToArray();

            public Int32 Length => _items.Length;

            IElement IReadOnlyList<IElement>.this[Int32 index] => _items[index];

            public IElement this[String id] => null;

            public IEnumerator<IElement> GetEnumerator() => ((IEnumerable<IElement>)_items).GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private sealed class ExplicitStringList : IStringList
        {
            private readonly String[] _items;

            public ExplicitStringList(params String[] items) => _items = items;

            public Int32 Length => _items.Length;

            String IReadOnlyList<String>.this[Int32 index] => _items[index];

            public Boolean Contains(String entry) => Array.IndexOf(_items, entry) >= 0;

            public IEnumerator<String> GetEnumerator() => ((IEnumerable<String>)_items).GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
#endif
    }
}
