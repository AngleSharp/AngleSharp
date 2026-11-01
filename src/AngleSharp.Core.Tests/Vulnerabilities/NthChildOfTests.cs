namespace AngleSharp.Core.Tests.Vulnerabilities;

using System;
using System.Linq;
using AngleSharp.Css;
using AngleSharp.Css.Dom;
using AngleSharp.Dom;
using NUnit.Framework;

/// <summary>
/// :nth-child(An+B of S) evaluates S on the element's siblings, and nesting such clauses made
/// every level evaluate the one inside it on every sibling again: a query cost about
/// siblings^(depth + 1), a CPU denial of service well inside the selector nesting limit (three
/// levels over 1,000 siblings took minutes). Timing is left to NthChildBenchmark; these count
/// how often the innermost selector runs, through a pseudo-class registered for the test.
/// </summary>
public class NthChildOfTests
{
    private const Int32 Siblings = 10;

    [TestCase(":nth-child(n of {0})")]
    [TestCase(":nth-last-child(n of {0})")]
    public void NestingDoesNotMultiplyTheInnerEvaluations(String clause)
    {
        var shallow = CountEvaluations(clause, 2);
        var deep = CountEvaluations(clause, 10);

        // Each match counts the siblings once per level, whatever the depth: 2 (head, body)
        // and 1 (div) for the elements above, and 10 for each of the 10 paragraphs
        Assert.AreEqual(2 * 2 + 1 + Siblings * Siblings, shallow);
        Assert.AreEqual(shallow, deep);
    }

    [TestCase(":nth-child(n of :in)")]
    [TestCase(":nth-last-child(n of :in)")]
    public void ElementOutsideTheOfSelectorIsRejectedWithoutVisitingItsSiblings(String selector)
    {
        var count = 0;
        var html = "<div>" + String.Concat(Enumerable.Range(0, Siblings).Select(i => i == Siblings / 2 ? "<p id=out></p>" : "<p class=in></p>")) + "</div>";
        var document = Parse(html, "in", element =>
        {
            count++;
            return element.ClassList.Contains("in");
        });

        Assert.IsFalse(document.GetElementById("out").Matches(selector));
        Assert.AreEqual(1, count);
    }

    [TestCase(":nth-child(2 of :nth-child(odd of .a))", "p4")]
    [TestCase(":nth-child(odd of :nth-child(n of .a))", "p1 p4")]
    [TestCase(":nth-last-child(1 of :nth-last-child(even of .a))", "p4")]
    [TestCase(":nth-child(1 of :nth-last-child(odd of .a))", "p3")]
    [TestCase(":nth-last-child(2 of :nth-child(n+2 of :nth-child(n of .a)))", "p4")]
    public void NestedClausesMatchAsSpecified(String selector, String ids)
    {
        // Elements with class a: p1, p3, p4, p5
        var document = "<div><p id=p1 class=a><p id=p2><p id=p3 class=a><p id=p4 class=a><p id=p5 class=a></div>".ToHtmlDocument();
        var matched = document.QuerySelectorAll(selector).Select(m => m.Id);

        Assert.AreEqual(ids, String.Join(" ", matched));
    }

    [Test]
    public void InnerSelectorThrowingLeavesNothingBehind()
    {
        var fail = true;
        var document = Parse("<div><p id=p1 class=a><p id=p2><p id=p3 class=a></div>", "a", element =>
        {
            if (fail && element.Id == "p3")
            {
                throw new InvalidOperationException();
            }

            return element.ClassList.Contains("a");
        });
        var selector = ":nth-child(2 of :nth-child(n of :a))";

        // Throws after the counts for p1 and p2 are in, which must not outlive this match
        Assert.Throws<InvalidOperationException>(() => document.QuerySelectorAll(selector));
        fail = false;
        document.GetElementById("p2").ClassList.Add("a");

        Assert.AreEqual("p2", String.Join(" ", document.QuerySelectorAll(selector).Select(m => m.Id)));
    }

    private static Int32 CountEvaluations(String clause, Int32 depth)
    {
        var count = 0;
        var html = "<div>" + String.Concat(Enumerable.Repeat("<p></p>", Siblings)) + "</div>";
        var document = Parse(html, "counted", _ =>
        {
            count++;
            return true;
        });
        var selector = ":counted";

        for (var i = 0; i < depth; i++)
        {
            selector = String.Format(clause, selector);
        }

        Assert.AreEqual(Siblings + 3, document.QuerySelectorAll(selector).Length);
        return count;
    }

    private static IDocument Parse(String html, String name, Predicate<IElement> match)
    {
        var factory = new DefaultPseudoClassSelectorFactory();
        factory.Register(name, new PseudoClassSelector(match, name));
        return html.ToHtmlDocument(Configuration.Default.WithOnly<IPseudoClassSelectorFactory>(factory));
    }
}
