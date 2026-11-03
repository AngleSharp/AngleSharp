namespace AngleSharp.Core.Tests.Vulnerabilities;

using System;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using AngleSharp.Css.Dom;
using AngleSharp.Css.Parser;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using NUnit.Framework;

/// <summary>
/// Selector functions such as :is() or :not() take selectors as arguments, so a selector can nest
/// them without bound. Parsing and matching both recurse once per level, and a few thousand
/// levels overflowed the stack, which cannot be caught: one hostile selector or style sheet ended
/// the process. A selector nesting deeper than the limit is now invalid like any other, so
/// QuerySelector throws the usual SyntaxError and a style sheet drops the rule. Each test runs on
/// a thread with a small, fixed stack, so the depths below overflowed on every platform.
/// </summary>
public class SelectorNestingTests
{
    // Several times past where any of these overflowed on the stack below, in any JIT tier
    private const Int32 Depth = 10000;
    private const Int32 StackSize = 256 * 1024;
    private const Int32 Limit = CssSelectorConstructor.MaxNestingDepth;

    [TestCase(":is(")]
    [TestCase(":where(")]
    [TestCase(":matches(")]
    [TestCase(":not(")]
    [TestCase(":has(")]
    [TestCase(":nth-child(n of ")]
    [TestCase(":nth-last-child(n of ")]
    [TestCase(":host-context(")]
    public void DeeplyNestedFunctionIsInvalid(String function)
    {
        AssertSyntaxError(Nest(function, Depth));
    }

    [Test]
    public void DeeplyNestedMixOfFunctionsIsInvalid()
    {
        var functions = new[] { ":is(", ":where(", ":not(", ":matches(", ":has(", ":nth-child(n of " };
        var selector = String.Concat(Enumerable.Range(0, Depth).Select(i => functions[i % functions.Length])) + "p" + new String(')', Depth);

        AssertSyntaxError(selector);
    }

    [TestCase(":is(")]
    [TestCase(":where(")]
    [TestCase(":matches(")]
    [TestCase(":has(")]
    [TestCase(":nth-child(n of ")]
    public void NestingUpToTheLimitIsValid(String function)
    {
        Assert.IsNotNull(Parse(Nest(function, Limit)));
        Assert.IsNull(Parse(Nest(function, Limit + 1)));
    }

    [Test]
    public void NotWithNestingUpToTheLimitIsValid()
    {
        // A :not() directly inside another one is invalid on its own, so it nests :is() instead
        Assert.IsNotNull(Parse(":not(" + Nest(":is(", Limit - 1) + ")"));
        Assert.IsNull(Parse(":not(" + Nest(":is(", Limit) + ")"));
    }

    [Test]
    public void MixOfFunctionsUpToTheLimitMatches()
    {
        var document = new HtmlParser().ParseDocument("<div><p>x</p></div>");

        var found = RunWithSmallStack(() => document.QuerySelector(Mix(Limit)));

        Assert.AreEqual("P", found.NodeName);
        AssertSyntaxError(Mix(Limit + 1));

        static String Mix(Int32 depth)
        {
            var functions = new[] { ":is(", ":where(", ":matches(" };
            return String.Concat(Enumerable.Range(0, depth).Select(i => functions[i % functions.Length])) + "p" + new String(')', depth);
        }
    }

    [Test]
    public void FunctionWithoutSelectorArgumentCountsTowardsTheLimit()
    {
        // As in Firefox, whose limit this is, every function is a level of nesting
        Assert.IsNotNull(Parse(Nest(":is(", Limit - 1, ":lang(en)")));
        Assert.IsNull(Parse(Nest(":is(", Limit, ":lang(en)")));
    }

    private static String Nest(String function, Int32 depth, String innermost = "p") =>
        String.Concat(Enumerable.Repeat(function, depth)) + innermost + new String(')', depth);

    private static ISelector Parse(String selector) =>
        RunWithSmallStack(() => new CssSelectorParser().ParseSelector(selector));

    private static void AssertSyntaxError(String selector)
    {
        var document = new HtmlParser().ParseDocument("<div><p>x</p></div>");

        var error = Assert.Throws<DomException>(() => RunWithSmallStack(() => document.QuerySelector(selector)));

        Assert.AreEqual((Int32)DomError.Syntax, error.Code);
        Assert.IsNull(Parse(selector));
    }

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
