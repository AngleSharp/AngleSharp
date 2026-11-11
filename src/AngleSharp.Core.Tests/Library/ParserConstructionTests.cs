namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Common;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// What the parser relies on while it builds a tree: the attribute map of an element, the
    /// strings a document shares between its attributes and text nodes, and the token buffer
    /// that goes back to its pool once the tree stands.
    /// </summary>
    [TestFixture]
    public class ParserConstructionTests
    {
        private const String Namespace1 = "urn:first";
        private const String Namespace2 = "urn:second";

        [Test]
        public void ManyParsedAttributesAreFoundByName()
        {
            var names = Enumerable.Range(0, 40).Select(i => "data-n" + i).ToArray();
            var html = "<p " + String.Join(" ", names.Select((name, i) => $"{name}=v{i}")) + ">";
            var paragraph = html.ToHtmlDocument().QuerySelector("p");

            Assert.AreEqual(40, paragraph.Attributes.Length);

            for (var i = 0; i < names.Length; i++)
            {
                Assert.AreEqual("v" + i, paragraph.GetAttribute(names[i]));
                Assert.AreSame(paragraph.Attributes[i], paragraph.Attributes[names[i]]);
            }

            Assert.IsNull(paragraph.GetAttribute("data-n40"));
            Assert.IsFalse(paragraph.HasAttribute("data-n"));
        }

        [Test]
        public void AttributesAreFoundByNameThroughRemovalsAndAdditions()
        {
            // Twenty attributes are looked up through an index, which then has to follow the
            // map down below the size it is built for and up again.
            var element = "<p>".ToHtmlDocument().QuerySelector("p");
            var expected = new Dictionary<String, String>();

            for (var i = 0; i < 20; i++)
            {
                element.SetAttribute("a" + i, "v" + i);
                expected["a" + i] = "v" + i;
            }

            AssertAttributes(expected, element);

            for (var i = 0; i < 20; i += 2)
            {
                element.RemoveAttribute("a" + i);
                expected.Remove("a" + i);
            }

            AssertAttributes(expected, element);

            for (var i = 20; i < 23; i++)
            {
                element.SetAttribute("a" + i, "v" + i);
                expected["a" + i] = "v" + i;
            }

            element.SetAttribute("a1", "changed");
            expected["a1"] = "changed";
            AssertAttributes(expected, element);

            for (var i = 30; i < 50; i++)
            {
                element.SetAttribute("a" + i, "v" + i);
                expected["a" + i] = "v" + i;
            }

            AssertAttributes(expected, element);

            foreach (var name in expected.Keys.ToArray())
            {
                element.RemoveAttribute(name);
                expected.Remove(name);
                Assert.IsNull(element.GetAttribute(name));
            }

            Assert.AreEqual(0, element.Attributes.Length);
        }

        [Test]
        public void OneNameInTwoNamespacesIsFoundAmongManyAttributes()
        {
            var element = "<p>".ToHtmlDocument().QuerySelector("p");

            for (var i = 0; i < 20; i++)
            {
                element.SetAttribute("a" + i, "v" + i);
            }

            var first = element.Owner.CreateAttribute(Namespace1, "p:x");
            var second = element.Owner.CreateAttribute(Namespace2, "p:x");
            first.Value = "first";
            second.Value = "second";
            element.Attributes.SetNamedItemWithNamespaceUri(first);
            element.Attributes.SetNamedItemWithNamespaceUri(second);

            Assert.AreEqual(22, element.Attributes.Length);
            Assert.AreEqual("first", element.GetAttribute("p:x"));
            Assert.AreEqual("first", element.GetAttribute(Namespace1, "x"));
            Assert.AreEqual("second", element.GetAttribute(Namespace2, "x"));

            element.RemoveAttribute(Namespace1, "x");

            Assert.AreEqual("second", element.GetAttribute("p:x"));
            Assert.AreEqual("v19", element.GetAttribute("a19"));

            element.RemoveAttribute("p:x");

            Assert.IsNull(element.GetAttribute("p:x"));
            Assert.IsNull(element.GetAttribute(Namespace2, "x"));
            Assert.AreEqual(20, element.Attributes.Length);
        }

        [Test]
        public void AttributesCanChangeWhileTheyAreEnumerated()
        {
            // The map is live, and so is going through it: no snapshot, no exception.
            var element = "<p a=1 b=2 c=3>".ToHtmlDocument().QuerySelector("p");
            var seen = new List<String>();

            foreach (var attribute in element.Attributes)
            {
                seen.Add(attribute.Name);
                element.RemoveAttribute("c");
            }

            CollectionAssert.AreEqual(new[] { "a", "b" }, seen);
        }

        [Test]
        public void AttributeKnowsItsElementForAsLongAsItBelongsToIt()
        {
            var element = "<p a=1 b=2>".ToHtmlDocument().QuerySelector("p");
            var parsed = element.Attributes["a"];
            element.SetAttribute("c", "3");
            var added = element.Attributes["c"];

            Assert.AreSame(element, parsed.OwnerElement);
            Assert.AreSame(element, added.OwnerElement);

            element.RemoveAttribute("a");
            element.RemoveAttribute("c");

            Assert.IsNull(parsed.OwnerElement);
            Assert.IsNull(added.OwnerElement);
        }

        [Test]
        public void ClonedElementHasAttributesOfItsOwn()
        {
            var element = "<p a=1 b=2 c=3>".ToHtmlDocument().QuerySelector("p");
            var clone = (IElement)element.Clone();

            clone.SetAttribute("a", "changed");
            clone.RemoveAttribute("b");
            clone.SetAttribute("d", "4");

            Assert.AreEqual("<p a=\"1\" b=\"2\" c=\"3\"></p>", element.ToHtml());
            Assert.AreEqual("<p a=\"changed\" c=\"3\" d=\"4\"></p>", clone.ToHtml());
            Assert.AreSame(clone, clone.Attributes["c"].OwnerElement);
        }

        [Test]
        public void EqualAttributeValuesOfADocumentAreOneString()
        {
            var sb = new StringBuilder();

            for (var i = 0; i < 100; i++)
            {
                sb.Append("<p class='note' data-kind=\"k").Append(i % 3).Append("\">text</p>\n");
            }

            var paragraphs = sb.ToString().ToHtmlDocument().QuerySelectorAll("p");
            var first = paragraphs[90];
            var second = paragraphs[93];

            Assert.AreEqual("note", first.GetAttribute("class"));
            Assert.AreEqual("k0", first.GetAttribute("data-kind"));
            Assert.AreSame(first.GetAttribute("class"), second.GetAttribute("class"));
            Assert.AreSame(first.GetAttribute("data-kind"), second.GetAttribute("data-kind"));
            Assert.AreSame(first.Attributes[1].Name, second.Attributes[1].Name);
            Assert.AreSame(first.NextSibling.TextContent, second.NextSibling.TextContent);
        }

        [Test]
        public void EveryDistinctValueOfADocumentIsKept()
        {
            // Far more distinct strings than the cache has places for, alike in length and in
            // most of their characters, each of them twice.
            var sb = new StringBuilder();

            for (var i = 0; i < 20000; i++)
            {
                sb.Append("<i id=item-").Append(i % 10000).Append("-x title='").Append((i % 10000) * 7).Append("'>t").Append(i % 10000).Append("</i>");
            }

            var items = sb.ToString().ToHtmlDocument().QuerySelectorAll("i");

            Assert.AreEqual(20000, items.Length);

            for (var i = 0; i < items.Length; i++)
            {
                var n = i % 10000;
                Assert.AreEqual($"item-{n}-x", items[i].Id);
                Assert.AreEqual((n * 7).ToString(), items[i].GetAttribute("title"));
                Assert.AreEqual("t" + n, items[i].TextContent);
            }
        }

        [Test]
        public void StringCacheReturnsTheCharactersItWasAskedFor()
        {
            var cache = new StringCache();
            var random = new Random(4711);
            var known = new Dictionary<String, String>();

            for (var i = 0; i < 50000; i++)
            {
                var length = random.Next(1, StringCache.MaximumLength + 1);
                var chars = new Char[length];

                for (var j = 0; j < length; j++)
                {
                    // Few distinct characters, so that many strings differ in one place only.
                    chars[j] = (Char)('a' + random.Next(i % 2 == 0 ? 2 : 26));
                }

                var expected = new String(chars);
                var actual = cache.Get(chars);

                Assert.AreEqual(expected, actual);
                known[expected] = actual;
            }

            Assert.AreSame(cache.Get("again".AsSpan()), cache.Get("again".AsSpan()));
        }

        [Test]
        public void ParsedDocumentKeepsItsContentWhenTheNextOneIsParsed()
        {
            // The token buffer of a finished parse is handed to the next one, which must not
            // find anything of the first tree still pointing into it.
            var first = Markup('a');
            var second = Markup('b');
            var document = first.ToHtmlDocument();
            var before = document.ToHtml();

            Assert.AreEqual(before, first.ToHtmlDocument().ToHtml());

            for (var i = 0; i < 4; i++)
            {
                var other = second.ToHtmlDocument();
                Assert.AreNotEqual(before, other.ToHtml());
            }

            Assert.AreEqual(before, document.ToHtml());
            Assert.AreEqual(new String('a', 300), document.QuerySelector("custom-a").GetAttribute("data-long-a"));
        }

        [Test]
        public void ParsedDocumentKeepsItsContentWithAnAttributeFilter()
        {
            var first = Markup('a');
            var second = Markup('b');
            var options = new HtmlParserOptions
            {
                ShouldEmitAttribute = (ref _, name) => !name.Span.StartsWith("data-short".AsSpan()),
            };
            var parser = new HtmlParser(options);
            var document = parser.ParseDocument(first.AsMemory());
            var before = document.ToHtml();

            parser.ParseDocument(second.AsMemory());
            parser.ParseDocument(second);

            Assert.AreEqual(before, document.ToHtml());
            Assert.IsNull(document.QuerySelector("custom-a").GetAttribute("data-short-a"));
            Assert.AreEqual(new String('a', 300), document.QuerySelector("custom-a").GetAttribute("data-long-a"));
        }

        [Test]
        public void ParsedFragmentKeepsItsContentWhenTheNextOneIsParsed()
        {
            var document = "<div id=a></div><div id=b></div>".ToHtmlDocument();
            var first = document.GetElementById("a");
            var second = document.GetElementById("b");

            first.InnerHtml = Markup('a');
            var before = first.InnerHtml;
            second.InnerHtml = Markup('b');

            Assert.AreEqual(before, first.InnerHtml);
            Assert.AreNotEqual(before, second.InnerHtml);
        }

        [Test]
        public void ImageWithoutResourceLoaderIsParsedAndLoadsNothing()
        {
            var html = "<picture><source srcset='a.webp 1x, b.webp 2x' type=image/webp><img src=cat.jpg srcset='cat-1.jpg 100w, cat-2.jpg 200w' sizes=50vw alt=cat></picture>";
            var image = html.ToHtmlDocument().QuerySelector<IHtmlImageElement>("img");

            Assert.AreEqual("cat-1.jpg 100w, cat-2.jpg 200w", image.SourceSet);
            Assert.AreEqual("50vw", image.Sizes);
            Assert.AreEqual("cat", image.AlternativeText);
            Assert.IsNull(image.CurrentDownload);
            Assert.IsFalse(image.IsCompleted);
            Assert.AreEqual(String.Empty, image.ActualSource);
            Assert.AreEqual(0, image.OriginalWidth);

            image.Source = "dog.jpg";
            image.SourceSet = null;

            Assert.AreEqual("dog.jpg", image.GetAttribute("src"));
            Assert.IsNull(image.CurrentDownload);
        }

        private static void AssertAttributes(Dictionary<String, String> expected, IElement element)
        {
            Assert.AreEqual(expected.Count, element.Attributes.Length);

            foreach (var pair in expected)
            {
                Assert.IsTrue(element.HasAttribute(pair.Key), pair.Key);
                Assert.AreEqual(pair.Value, element.GetAttribute(pair.Key), pair.Key);
                Assert.AreEqual(pair.Value, element.Attributes[pair.Key].Value, pair.Key);
            }

            CollectionAssert.AreEquivalent(expected.Keys, element.Attributes.Select(m => m.Name));
        }

        // Names and values that are neither well-known nor short enough to be shared, so the
        // tree gets them from the token buffer and nowhere else.
        private static String Markup(Char c)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < 50; i++)
            {
                sb.Append("<custom-").Append(c).Append(" data-long-").Append(c).Append("='").Append(c, 300).Append("' data-short-").Append(c).Append('=').Append(c).Append(i).Append('>');
                sb.Append(c, 200).Append("</custom-").Append(c).Append("><!--").Append(c, 150).Append("-->");
            }

            return sb.ToString();
        }
    }
}
