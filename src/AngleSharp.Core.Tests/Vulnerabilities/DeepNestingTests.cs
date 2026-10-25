namespace AngleSharp.Core.Tests.Vulnerabilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using NUnit.Framework;

/// <summary>
/// HTML puts no limit on nesting, so a page with thousands of unclosed tags parses into a tree
/// thousands of levels deep. Members that recurse once per level overflow the stack on such a
/// tree, and a StackOverflowException cannot be caught: one hostile page ends the process.
/// Each test runs on a thread with a small, fixed stack, so a member that still recursed would
/// overflow on every platform rather than only where the default stack happens to be small.
/// </summary>
public class DeepNestingTests
{
    // Far deeper than any recursive walk survives on the small stack below, yet quick to parse:
    // nested spans, unlike block elements, do not make the tree builder scan its open elements.
    private const Int32 Depth = 5000;
    // For the members that also take time in proportion to the depth for every level (Range
    // boundary comparisons, base URL lookups), still well past where recursion overflowed
    private const Int32 SlowerDepth = 2000;
    private const Int32 StackSize = 256 * 1024;

    [Test]
    public void TextContentOfDeeplyNestedElements()
    {
        var document = ParseNested();

        var text = RunWithSmallStack(() => document.Body.TextContent);

        Assert.AreEqual(Depth + 1, text.Length);
        Assert.IsTrue(text.EndsWith("xy"));
    }

    [Test]
    public void ContainsSelectorOnDeeplyNestedElements()
    {
        var document = ParseNested();

        var found = RunWithSmallStack(() => document.QuerySelector("body:contains('xy')"));

        Assert.AreSame(document.Body, found);
    }

    [Test]
    public void GetElementsByTagNameOnDeeplyNestedElements()
    {
        var document = ParseNested();

        var spans = RunWithSmallStack(() => document.GetElementsByTagName("span").Length);
        var namespaced = RunWithSmallStack(() => document.GetElementsByTagName(NamespaceNames.HtmlUri, "span").Length);

        Assert.AreEqual(Depth, spans);
        Assert.AreEqual(Depth, namespaced);
    }

    [Test]
    public void GetElementsByClassNameOnDeeplyNestedElements()
    {
        var document = ParseNested(innermost: "<i class=\"deep\" id=\"deep\" name=\"deep\">y</i>");

        var byClass = RunWithSmallStack(() => document.GetElementsByClassName("deep").Single());
        var byId = RunWithSmallStack(() => document.GetElementById("deep"));
        var byName = RunWithSmallStack(() => document.GetElementsByName("deep").Single());

        Assert.AreEqual("I", byClass.NodeName);
        Assert.AreSame(byClass, byId);
        Assert.AreSame(byClass, byName);
    }

    [Test]
    public void DeepCloneOfDeeplyNestedElements()
    {
        // Lifted, so cloning itself has to survive a depth the parser would otherwise cap
        var document = ParseNested(maximumDepth: Int32.MaxValue);

        var clone = RunWithSmallStack(() => document.Body.Clone(true));

        Assert.AreEqual(document.Body.InnerHtml, ((IElement)clone).InnerHtml);
        Assert.IsTrue(RunWithSmallStack(() => document.Body.Equals(clone)));
    }

    [Test]
    public void DeepCloneOfDeeplyNestedTemplates()
    {
        // Built through the DOM, where no parser limit applies: each template sits in the
        // contents of the one before it rather than among its children
        var document = new HtmlParser().ParseDocument(String.Empty);
        var outermost = (IHtmlTemplateElement)document.CreateElement("template");
        var current = outermost;

        for (var i = 1; i < Depth; i++)
        {
            var next = (IHtmlTemplateElement)document.CreateElement("template");
            current.Content.AppendChild(document.CreateTextNode("x"));
            current.Content.AppendChild(next);
            current = next;
        }

        current.Content.AppendChild(document.CreateTextNode("y"));

        var clone = (IHtmlTemplateElement)RunWithSmallStack(() => outermost.Clone(true));

        var count = 1;
        var text = String.Empty;

        for (var template = clone; template is not null; template = template.Content.LastChild as IHtmlTemplateElement)
        {
            Assert.AreNotSame(outermost, template);
            text = template.Content.LastChild.TextContent;
            count++;
        }

        Assert.AreEqual(Depth + 1, count);
        Assert.AreEqual("y", text);
    }

    [Test]
    public void ImportOfDeeplyNestedElements()
    {
        // Lifted, so importing itself has to survive a depth the parser would otherwise cap
        var document = ParseNested(maximumDepth: Int32.MaxValue);
        var target = new HtmlParser().ParseDocument(String.Empty);

        var imported = RunWithSmallStack(() => target.Import(document.Body));

        Assert.AreEqual(document.Body.InnerHtml, ((IElement)imported).InnerHtml);
        Assert.AreSame(target, imported.Owner);
    }

    [Test]
    public void NormalizeOfDeeplyNestedElements()
    {
        var document = ParseNested();
        var innermost = document.QuerySelector("i");
        innermost.AppendChild(document.CreateTextNode("z"));

        RunWithSmallStack(() => document.Body.Normalize());

        Assert.AreEqual(1, innermost.ChildNodes.Length);
        Assert.AreEqual("yz", innermost.TextContent);
    }

    [Test]
    public void IsEqualNodeOfDeeplyNestedElements()
    {
        // Lifted, so the comparison itself has to survive a depth the parser would otherwise cap
        var first = ParseNested(SlowerDepth, maximumDepth: Int32.MaxValue);
        var second = ParseNested(SlowerDepth, maximumDepth: Int32.MaxValue);
        var different = ParseNested(SlowerDepth, innermost: "<i>z</i>", maximumDepth: Int32.MaxValue);

        Assert.IsTrue(RunWithSmallStack(() => first.Body.Equals((INode)second.Body)));
        Assert.IsFalse(RunWithSmallStack(() => first.Body.Equals((INode)different.Body)));
    }

    [Test]
    public void RangeCopyContentAcrossDeeplyNestedElements()
    {
        // Lifted, so the walk itself has to survive a depth the parser would otherwise cap
        var document = ParseNested(SlowerDepth, maximumDepth: Int32.MaxValue);
        var range = RangeFromBodyToInnermostText(document);

        var fragment = RunWithSmallStack(() => range.CopyContent());

        Assert.AreEqual(SlowerDepth + 1, fragment.TextContent.Length);
        Assert.AreEqual(SlowerDepth + 1, document.Body.TextContent.Length, "Copying must leave the document unchanged");
    }

    [Test]
    public void RangeExtractContentAcrossDeeplyNestedElements()
    {
        // Lifted, so the walk itself has to survive a depth the parser would otherwise cap
        var document = ParseNested(SlowerDepth, maximumDepth: Int32.MaxValue);
        var range = RangeFromBodyToInnermostText(document);

        var fragment = RunWithSmallStack(() => range.ExtractContent());

        Assert.AreEqual(SlowerDepth + 1, fragment.TextContent.Length);
        Assert.AreEqual(0, document.Body.TextContent.Length);
        Assert.IsTrue(range.IsCollapsed);
    }

    [TestCase(false)]
    [TestCase(true)]
    public void RangeContentBetweenTwoDeeplyNestedElements(Boolean extract)
    {
        // Both boundaries sit deep in different branches, so the range descends towards the start
        // and towards the end alike
        var nested = String.Concat(Enumerable.Repeat("<span>x", SlowerDepth / 2));
        var source = "<!DOCTYPE html><body><div>" + nested + "<i>y</i></div><div>" + nested + "<b>z</b>";
        var parser = new HtmlParser(new HtmlParserOptions { MaximumDepth = Int32.MaxValue });
        var document = parser.ParseDocument(source);
        var inner = document.QuerySelector("i").FirstChild;
        var outer = document.QuerySelector("b").FirstChild;
        var range = document.CreateRange();
        range.StartWith(inner, 0);
        range.EndWith(outer, 1);

        var text = document.Body.TextContent;

        var fragment = RunWithSmallStack(() => extract ? range.ExtractContent() : range.CopyContent());

        Assert.AreEqual("y" + new String('x', SlowerDepth / 2) + "z", fragment.TextContent);
        Assert.AreEqual(SlowerDepth / 2 + 2, GetDepth(fragment.QuerySelector("b")));
        Assert.AreEqual(extract ? new String('x', SlowerDepth / 2) : text, document.Body.TextContent);
    }

    [Test]
    public void ParserLimitsNestingToDefaultMaximumDepth()
    {
        var document = ParseNested();

        Assert.AreEqual(HtmlParserOptions.DefaultMaximumDepth, GetMaximumDepth(document));
        Assert.AreEqual(Depth, document.QuerySelectorAll("span").Length);
        Assert.AreEqual(Depth + 1, document.Body.TextContent.Length);
    }

    [Test]
    public void ParserAttachesElementsBeyondMaximumDepthAsSiblings()
    {
        var parser = new HtmlParser(new HtmlParserOptions { MaximumDepth = 4 });

        var document = parser.ParseDocument("<body><div><p><b>a<i>b<u>c</u>d</i>e</b>f</p>g</div>");

        // body is at depth 2, so p at depth 4 takes no element children. Each text run still
        // goes to the element it was opened in, which is why "f" lands in p before b, i and u.
        Assert.AreEqual("<div><p>f</p><b>ae</b><i>bd</i><u>c</u>g</div>", document.Body.InnerHtml);
    }

    [Test]
    public void ParserNestsWithoutLimitWhenMaximumDepthIsLifted()
    {
        var source = "<!DOCTYPE html><body>" + String.Concat(Enumerable.Repeat("<span>x", SlowerDepth)) + "<i>y</i>";
        var parser = new HtmlParser(new HtmlParserOptions { MaximumDepth = Int32.MaxValue });

        var document = parser.ParseDocument(source);

        // html, body, the spans and the innermost i
        Assert.AreEqual(SlowerDepth + 3, GetDepth(document.QuerySelector("i")));
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void ParserUsesDefaultMaximumDepthForUnsetValues(Int32 value)
    {
        var options = new HtmlParserOptions { MaximumDepth = value };

        Assert.AreEqual(HtmlParserOptions.DefaultMaximumDepth, options.MaximumDepth);
        Assert.AreEqual(HtmlParserOptions.DefaultMaximumDepth, default(HtmlParserOptions).MaximumDepth);
    }

    // Insertion modes that re-attach nodes rather than appending them to the current node:
    // the adoption agency, foster parenting, foreign content, tables and templates
    [TestCase("<b><div>x")]
    [TestCase("<div><b>x")]
    [TestCase("<b><p>x</b>y")]
    [TestCase("<a><div>x<a>y")]
    [TestCase("<b><i><p>x</b>y</i>z")]
    [TestCase("<table><tr><td>x")]
    [TestCase("<table><b>x")]
    [TestCase("<table><div>x")]
    [TestCase("<svg><g>x")]
    [TestCase("<math><mi>x")]
    [TestCase("<svg><foreignObject><div>x")]
    [TestCase("<template><div>x")]
    [TestCase("<ul><li>x<ul>")]
    [TestCase("<select><option>x")]
    public void ParserKeepsHostileNestingWithinMaximumDepth(String pattern)
    {
        const Int32 maximumDepth = 20;
        var source = "<!DOCTYPE html><body>" + String.Concat(Enumerable.Repeat(pattern, 200));
        var parser = new HtmlParser(new HtmlParserOptions { MaximumDepth = maximumDepth });

        var document = parser.ParseDocument(source);

        Assert.LessOrEqual(GetMaximumDepth(document), maximumDepth);
    }

    // The depth of the deepest element, the root element being at depth 1. Template contents
    // count as nested in their template: they are separate trees in the DOM, but not in markup.
    private static Int32 GetMaximumDepth(IDocument document)
    {
        var maximum = 0;
        var pending = new Queue<(IParentNode Root, Int32 Offset)>();
        pending.Enqueue((document, 0));

        while (pending.Count > 0)
        {
            var (root, offset) = pending.Dequeue();

            foreach (var element in root.QuerySelectorAll("*"))
            {
                var depth = offset + GetDepth(element);
                maximum = Math.Max(maximum, depth);

                if (element is IHtmlTemplateElement template)
                {
                    pending.Enqueue((template.Content, depth));
                }
            }
        }

        return maximum;
    }

    private static Int32 GetDepth(IElement element)
    {
        var depth = 0;

        for (var current = element; current is not null; current = current.ParentElement)
        {
            depth++;
        }

        return depth;
    }

    // <span>x<span>x ... <i>y</i>, with depth spans
    private static IHtmlDocument ParseNested(Int32 depth = Depth, String innermost = "<i>y</i>", Int32 maximumDepth = 0)
    {
        var source = "<!DOCTYPE html><body>" + String.Concat(Enumerable.Repeat("<span>x", depth)) + innermost;
        return new HtmlParser(new HtmlParserOptions { MaximumDepth = maximumDepth }).ParseDocument(source);
    }

    private static IRange RangeFromBodyToInnermostText(IDocument document)
    {
        var range = document.CreateRange();
        range.StartWith(document.Body, 0);
        range.EndWith(document.QuerySelector("i").FirstChild, 1);
        return range;
    }

    private static void RunWithSmallStack(Action action) => RunWithSmallStack(() =>
    {
        action();
        return 0;
    });

    private static T RunWithSmallStack<T>(Func<T> function)
    {
        var result = default(T);
        var error = default(ExceptionDispatchInfo);
        var thread = new Thread(() =>
        {
            try
            {
                result = function();
            }
            catch (Exception e)
            {
                error = ExceptionDispatchInfo.Capture(e);
            }
        }, StackSize);

        thread.Start();
        thread.Join();
        error?.Throw();
        return result!;
    }
}
