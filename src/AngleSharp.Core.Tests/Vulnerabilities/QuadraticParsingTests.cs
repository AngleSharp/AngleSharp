namespace AngleSharp.Core.Tests.Vulnerabilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using NUnit.Framework;

/// <summary>
/// Shapes that took the parser quadratic time, a CPU denial of service for anything parsing
/// user-supplied markup: elements and text foster-parented out of a table, a text run split by
/// ignored end tags, and start tags piling attributes onto html or body. Timing is left to the
/// benchmarks; these pin the trees, which the shortcuts behind the fixes must not change.
/// </summary>
public class QuadraticParsingTests
{
    [Test]
    public void ManyElementsFosteredOutOfATable()
    {
        var document = ("<table>" + String.Concat(Enumerable.Repeat("<br>", 5000))).ToHtmlDocument();
        var children = document.Body.ChildNodes;

        Assert.AreEqual(5001, children.Length);
        Assert.IsTrue(children.Take(5000).All(m => m.GetTagName() == "br"));
        Assert.AreEqual("table", children[5000].GetTagName());
    }

    [Test]
    public void ManyTextRunsFosteredOutOfATable()
    {
        var document = ("<table>" + String.Concat(Enumerable.Range(0, 3000).Select(i => $"{i}<br>"))).ToHtmlDocument();
        var children = document.Body.ChildNodes;

        Assert.AreEqual(6001, children.Length);

        for (var i = 0; i < 3000; i++)
        {
            Assert.AreEqual(i.ToString(), ((IText)children[2 * i]).Data);
            Assert.AreEqual("br", children[2 * i + 1].GetTagName());
        }

        Assert.AreEqual("table", children[6000].GetTagName());
    }

    [Test]
    public void FosteredTextMergesIntoTheTextBeforeTheTable()
    {
        var document = ("<table>" + String.Concat(Enumerable.Repeat("ab</q>", 3000))).ToHtmlDocument();
        var children = document.Body.ChildNodes;

        Assert.AreEqual(2, children.Length);
        Assert.AreEqual(String.Concat(Enumerable.Repeat("ab", 3000)), ((IText)children[0]).Data);
        Assert.AreEqual("table", children[1].GetTagName());
    }

    [Test]
    public void TextSplitByIgnoredEndTagsIsOneNode()
    {
        var document = String.Concat(Enumerable.Range(0, 5000).Select(i => (Char)('a' + i % 26) + "</q>")).ToHtmlDocument();
        var expected = String.Concat(Enumerable.Range(0, 5000).Select(i => (Char)('a' + i % 26)));
        var text = (IText)document.Body.ChildNodes.Single();

        // Length first: it must not depend on the content having been read.
        Assert.AreEqual(5000, text.Length);
        Assert.AreEqual(expected, text.Data);
        Assert.AreEqual(expected.Substring(1200, 300), text.Substring(1200, 300));
        Assert.AreEqual(expected, document.Body.TextContent);
    }

    [Test]
    public void LongParsedTextKeepsTheDomOperationsWorking()
    {
        var document = String.Concat(Enumerable.Repeat("xy</q>", 2000)).ToHtmlDocument();
        var text = (IText)document.Body.FirstChild;
        var records = new List<IMutationRecord>();
        var observer = new MutationObserver((delivered, _) => records.AddRange(delivered));
        observer.Connect(text, characterData: true, characterDataOldValue: true);

        text.Append("!");

        Assert.AreEqual(4001, text.Length);
        Assert.IsTrue(text.Data.EndsWith("xy!"));
        Assert.AreEqual(1, records.Count);
        Assert.AreEqual(4000, records[0].PreviousValue.Length);

        text.Replace(0, 3998, "");
        Assert.AreEqual("xy!", text.Data);
    }

    [Test]
    public void ConcurrentReadsOfLongParsedTextAgree()
    {
        for (var round = 0; round < 20; round++)
        {
            var document = String.Concat(Enumerable.Repeat("xy</q>", 2000)).ToHtmlDocument();
            var text = (IText)document.Body.FirstChild;
            var values = new String[8];

            Parallel.For(0, values.Length, i => values[i] = text.Data);

            Assert.IsTrue(values.All(m => Object.ReferenceEquals(m, values[0])));
            Assert.AreEqual(4000, values[0].Length);
        }
    }

    [TestCase("html")]
    [TestCase("body")]
    public void AttributesMergedOntoTheRootKeepTheFirstValue(String tag)
    {
        var source = new StringBuilder();

        for (var t = 0; t < 50; t++)
        {
            source.Append('<').Append(tag);

            // Half the names are new on each tag, half repeat the previous tag's with a new value.
            for (var i = 0; i < 40; i++)
            {
                source.Append($" a{t + i / 20}x{i % 20}={t}");
            }

            source.Append('>');
        }

        var document = source.ToString().ToHtmlDocument();
        var element = tag == "html" ? document.DocumentElement : document.Body;

        Assert.AreEqual(51 * 20, element.Attributes.Length);

        for (var t = 0; t <= 50; t++)
        {
            for (var i = 0; i < 20; i++)
            {
                Assert.AreEqual(Math.Max(0, t - 1).ToString(), element.GetAttribute($"a{t}x{i}"));
            }
        }
    }

    [Test]
    public void ManyAttributesKeepNameLookupsCorrectThroughMutations()
    {
        var document = "<body>".ToHtmlDocument();
        var element = document.CreateElement("div");

        for (var i = 0; i < 40; i++)
        {
            element.SetAttribute($"a{i}", i.ToString());
        }

        element.SetAttribute("a5", "five");
        element.RemoveAttribute("a6");
        element.SetAttribute("a6", "six");

        Assert.AreEqual(40, element.Attributes.Length);
        Assert.AreEqual("five", element.GetAttribute("a5"));
        Assert.AreEqual("six", element.GetAttribute("a6"));
        Assert.AreSame(element.Attributes[39], element.Attributes["a6"]);
        Assert.IsNull(element.GetAttribute("a40"));

        // Two attributes sharing a qualified name, which only the namespaced setter (matching
        // namespace and local name) can produce: lookups by name find the first one, and once
        // it has been renamed, the second.
        var attributes = element.Attributes;
        attributes.SetNamedItemWithNamespaceUri(new Attr("p", "dup", "1", "http://example.com/one"));
        attributes.SetNamedItemWithNamespaceUri(new Attr("p", "dup", "2", "http://example.com/two"));
        Assert.AreEqual("1", element.GetAttribute("p:dup"));

        attributes.SetNamedItemWithNamespaceUri(new Attr("p", "dup", "two", "http://example.com/two"));
        Assert.AreEqual("1", element.GetAttribute("p:dup"));

        attributes.SetNamedItemWithNamespaceUri(new Attr("q", "dup", "one", "http://example.com/one"));
        Assert.AreEqual("two", element.GetAttribute("p:dup"));
        Assert.AreEqual("one", element.GetAttribute("q:dup"));

        element.RemoveAttribute("http://example.com/two", "dup");
        Assert.IsNull(element.GetAttribute("p:dup"));
        Assert.AreEqual("one", element.GetAttribute("q:dup"));

        element.RemoveAttribute("q:dup");
        Assert.IsFalse(element.HasAttribute("q:dup"));

        for (var i = 0; i < 40; i++)
        {
            element.RemoveAttribute($"a{i}");
        }

        Assert.AreEqual(0, element.Attributes.Length);
    }
}
