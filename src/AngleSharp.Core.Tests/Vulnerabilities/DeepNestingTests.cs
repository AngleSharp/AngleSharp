namespace AngleSharp.Core.Tests.Vulnerabilities;

using System;
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
        var document = ParseNested();

        var clone = RunWithSmallStack(() => document.Body.Clone(true));

        Assert.AreEqual(document.Body.InnerHtml, ((IElement)clone).InnerHtml);
    }

    [Test]
    public void ImportOfDeeplyNestedElements()
    {
        var document = ParseNested();
        var target = new HtmlParser().ParseDocument(String.Empty);

        var imported = RunWithSmallStack(() => target.Import(document.Body));

        Assert.AreEqual(document.Body.InnerHtml, ((IElement)imported).InnerHtml);
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
        var first = ParseNested(SlowerDepth);
        var second = ParseNested(SlowerDepth);
        var different = ParseNested(SlowerDepth, innermost: "<b>y</b>");

        Assert.IsTrue(RunWithSmallStack(() => first.Body.Equals((INode)second.Body)));
        Assert.IsFalse(RunWithSmallStack(() => first.Body.Equals((INode)different.Body)));
    }

    [Test]
    public void RangeCopyContentAcrossDeeplyNestedElements()
    {
        var document = ParseNested(SlowerDepth);
        var range = RangeFromBodyToInnermostText(document);

        var fragment = RunWithSmallStack(() => range.CopyContent());

        Assert.AreEqual(SlowerDepth + 1, fragment.TextContent.Length);
        Assert.AreEqual(SlowerDepth + 1, document.Body.TextContent.Length, "Copying must leave the document unchanged");
    }

    [Test]
    public void RangeExtractContentAcrossDeeplyNestedElements()
    {
        var document = ParseNested(SlowerDepth);
        var range = RangeFromBodyToInnermostText(document);

        var fragment = RunWithSmallStack(() => range.ExtractContent());

        Assert.AreEqual(SlowerDepth + 1, fragment.TextContent.Length);
        Assert.AreEqual(0, document.Body.TextContent.Length);
        Assert.IsTrue(range.IsCollapsed);
    }

    // <span>x<span>x ... <i>y</i>, with depth spans
    private static IHtmlDocument ParseNested(Int32 depth = Depth, String innermost = "<i>y</i>")
    {
        var source = "<!DOCTYPE html><body>" + String.Concat(Enumerable.Repeat("<span>x", depth)) + innermost;
        return new HtmlParser().ParseDocument(source);
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
