namespace AngleSharp.Core.Tests.Css
{
    using System;
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using NUnit.Framework;

    [TestFixture]
    public class CssSelectorParserTests
    {
        [Test]
        public void ParseSelector_WithSameText_ReusesCachedInstance()
        {
            var parser = new CssSelectorParser();

            var first = parser.ParseSelector("div > p");
            var second = parser.ParseSelector("div > p");

            Assert.NotNull(first);
            Assert.AreSame(first, second);
        }

        [Test]
        public void ParseSelector_WhenCacheCapacityExceeded_EvictsLeastRecentlyUsedSelector()
        {
            var parser = new CssSelectorParser();
            var first = parser.ParseSelector("div");

            Assert.NotNull(first);

            for (var i = 0; i < 300; i++)
            {
                var selector = parser.ParseSelector($"div:nth-child({i + 1})");
                Assert.NotNull(selector);
            }

            var second = parser.ParseSelector("div");

            Assert.NotNull(second);
            Assert.AreNotSame(first, second);
        }

        [TestCase(".foo\\:bar", "<p class='foo:bar'></p>")]
        [TestCase("#a\\31 b", "<p id='a1b'></p>")]
        [TestCase("#-x\\79", "<p id='-xy'></p>")]
        [TestCase("p\\.q", "<p.q></p.q>")]
        [TestCase(".-x\\79 z", "<p class='-xyz'></p>")]
        [TestCase("p:not(.a\\62 c)", "<p class='abc'></p><p></p>")]
        [TestCase("[da\\74 a-x]", "<p data-x></p>")]
        public void ParseSelector_WithEscapeAfterPlainPrefix_KeepsWholeName(String text, String html)
        {
            // Names are cut out of the selector text until an escape forces a copy, so the
            // part before the escape has to carry over into the copied name.
            var document = html.ToHtmlDocument();

            var selector = new CssSelectorParser().ParseSelector(text);

            Assert.NotNull(selector);
            Assert.AreEqual(1, document.QuerySelectorAll(text).Length);
        }

        [TestCase("div p", "div p")]
        [TestCase("div   >   p", "div>p")]
        [TestCase(" div > p", "div>p")]
        [TestCase("div >p ~ a", "div>p~a")]
        [TestCase("div\t+\np", "div+p")]
        [TestCase("div > + p", null)]
        [TestCase("div ~ > p", null)]
        [TestCase("div >> > p", "div>p")]
        [TestCase("div>>p", "div p")]
        public void ParseSelector_WithCombinatorsAndWhitespace_UsesSingleCombinator(String text, String expected)
        {
            // Whitespace around a combinator is insignificant, and so is ">>" (the descendant
            // combinator spelled out); two other combinators in a row are invalid
            var selector = new CssSelectorParser().ParseSelector(text);

            Assert.AreEqual(expected, selector?.Text);
        }

        [TestCase("p:NOT(.a)", ":not(.a)")]
        [TestCase("p:Is(.b)", ":is(.b)")]
        [TestCase("p:nth-CHILD(1)", ":nth-child(0n+1)")]
        public void ParseSelector_WithFunctionNameInOtherCase_IsStillKnown(String text, String part)
        {
            // Known function names come from a table matched exactly; any other spelling is
            // sliced from the text and still resolved case-insensitively.
            var document = "<p class='a'></p><p class='b'></p>".ToHtmlDocument();

            var selector = new CssSelectorParser().ParseSelector(text);

            Assert.NotNull(selector);
            Assert.AreEqual(1, document.QuerySelectorAll(text).Length);
            StringAssert.Contains(part, selector!.Text);
        }

        [TestCase(":not(.a, .b)")]
        [TestCase(":is(div, p).c")]
        [TestCase("li:has(:scope>a.c)")]
        [TestCase("body :has(.c):first-child")]
        [TestCase(":where(:is(nav, header) :not(.c)>a)")]
        [TestCase(":is(:not(.a), :where(.c>p))")]
        [TestCase(":host(.a)")]
        public void ParseSelector_WithFunctionalPseudoClass_SerializesArgument(String text)
        {
            // The text of a function is only built when first read, from its parsed argument
            var parser = new CssSelectorParser();

            var selector = parser.ParseSelector(text);

            Assert.NotNull(selector);
            Assert.AreEqual(text, selector!.Text);
            Assert.AreEqual(text, parser.ParseSelector(selector.Text)!.Text);
        }

        [Test]
        public void ParseSelector_WithHostPseudoClass_ReturnsSelector()
        {
            var parser = new CssSelectorParser();

            var selector = parser.ParseSelector(":host");

            Assert.NotNull(selector);
            Assert.AreEqual(":host", selector!.Text);
        }

        [Test]
        public void ParseSelector_WithHostContextFunction_ReturnsSelector()
        {
            var parser = new CssSelectorParser();

            var selector = parser.ParseSelector(":host-context(.card)");

            Assert.NotNull(selector);
            Assert.AreEqual(":host-context(.card)", selector!.Text);
        }

        [Test]
        public void ParseSelector_WithCaseInsensitiveAttributeValue_KeepsModifierInText()
        {
            var parser = new CssSelectorParser();

            var selector = parser.ParseSelector("[href$=\"B\" i]");

            Assert.NotNull(selector);
            Assert.AreEqual("[href$=\"B\" i]", selector!.Text);
        }

        [TestCase("=")]
        [TestCase("~=")]
        [TestCase("|=")]
        [TestCase("^=")]
        [TestCase("$=")]
        [TestCase("*=")]
        [TestCase("!=")]
        public void ParseSelector_WithCaseInsensitiveAttributeValue_SerializesToAnEquivalentSelector(String op)
        {
            // Text is what every consumer stores, compares and re-parses, so a modifier dropped
            // here silently turns a case-insensitive selector into a case-sensitive one.
            var text = $"[href{op}\"B\" i]";
            var parser = new CssSelectorParser();

            var selector = parser.ParseSelector(text);

            Assert.NotNull(selector);
            Assert.AreEqual(text, selector!.Text);
            Assert.AreEqual(text, parser.ParseSelector(selector.Text)!.Text);
        }

        [TestCase("type", "text")]
        [TestCase("align", "center")]
        public void ParseSelector_WithHtmlCaseInsensitiveAttribute_DoesNotAddAModifier(String name, String value)
        {
            // CSSOM appends the modifier only if the flag is present. These attributes compare
            // case-insensitively because HTML says so, not because a flag was written.
            var text = $"[{name}=\"{value}\"]";
            var parser = new CssSelectorParser();

            var selector = parser.ParseSelector(text);

            Assert.NotNull(selector);
            Assert.AreEqual(text, selector!.Text);
        }

        [TestCase("=")]
        [TestCase("~=")]
        [TestCase("|=")]
        [TestCase("^=")]
        [TestCase("$=")]
        [TestCase("*=")]
        [TestCase("!=")]
        public void ParseSelector_WithCaseSensitiveAttributeValue_KeepsModifierInText(String op)
        {
            var text = $"[align{op}\"center\" s]";

            var selector = new CssSelectorParser().ParseSelector(text);

            Assert.NotNull(selector);
            Assert.AreEqual(text, selector!.Text);
        }

        [Test]
        public void ParseSelector_WithCaseSensitiveAttributeValue_SerializedFormSelectsTheSame()
        {
            // align compares case-insensitively by default, so what the omitted "s" costs is
            // visible in what the serialized text selects.
            var document = "<div align=\"CENTER\"></div>".ToHtmlDocument();
            var text = "[align=\"center\" s]";

            var selector = new CssSelectorParser().ParseSelector(text);

            Assert.NotNull(selector);
            Assert.AreEqual(0, document.QuerySelectorAll(text).Length);
            Assert.AreEqual(0, document.QuerySelectorAll(selector!.Text).Length);
        }

        [Test]
        public void ParseSelector_WithCaseInsensitiveAttributeValue_SerializedFormSelectsTheSame()
        {
            var document = "<a href=\"X\"></a>".ToHtmlDocument();
            var text = "[href=\"x\" i]";

            var selector = new CssSelectorParser().ParseSelector(text);

            Assert.NotNull(selector);
            Assert.AreEqual(text, selector!.Text);
            Assert.AreEqual(1, document.QuerySelectorAll(text).Length);
            Assert.AreEqual(1, document.QuerySelectorAll(selector.Text).Length);
        }

        [Test]
        public void ParseSelector_WithOfClause_KeepsTheFilterInText()
        {
            var parser = new CssSelectorParser();

            var selector = parser.ParseSelector("li:nth-child(2 of .item)");

            Assert.NotNull(selector);
            Assert.AreEqual("li:nth-child(0n+2 of .item)", selector!.Text);
        }

        [TestCase("nth-child")]
        [TestCase("nth-last-child")]
        public void ParseSelector_WithOfClause_SerializesToAnEquivalentSelector(String name)
        {
            // The of-clause decides which siblings are counted at all, so losing it in Text
            // widens the selector and drops the inner selector out of the specificity with it.
            var text = $"li:{name}(2n+1 of .item)";

            var selector = new CssSelectorParser().ParseSelector(text);

            Assert.NotNull(selector);

            // A parser of its own: the one above caches by selector text and would answer a
            // re-parse of an unchanged serialization out of that cache.
            var reparsed = new CssSelectorParser().ParseSelector(selector!.Text);

            Assert.NotNull(reparsed);
            Assert.AreEqual(selector.Text, reparsed!.Text);
            Assert.AreEqual(selector.Specificity, reparsed.Specificity);
        }
    }
}
